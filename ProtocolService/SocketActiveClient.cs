using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using SHWDTech.Platform.ProtocolService.ProtocolEncoding;

namespace SHWDTech.Platform.ProtocolService
{
    public class SocketActiveClient : IActiveClient
    {
        /// <summary>
        /// 客户端套接字
        /// </summary>
        private readonly Socket _clientSocket;

        /// <summary>
        /// 协议解析缓存
        /// </summary>
        private readonly List<byte> _processBuffer = new List<byte>();

        /// <summary>
        /// 是否正在解析协议
        /// </summary>
        private bool _isProcessing;

        /// <summary>
        /// 客户端数据接收事件
        /// </summary>
        public event ActiveClientEventHandler ClientReceivedData;

        /// <summary>
        /// 客户端断开事件
        /// </summary>
        public event ActiveClientEventHandler ClientDisconnect;

        /// <summary>
        /// 客户端授权事件
        /// </summary>
        public event ActiveClientEventHandler ClientAuthenticated;

        /// <summary>
        /// 客户端解码失败事件
        /// </summary>
        public event ActiveClientEventHandler ClientDecodeFalied;

        /// <summary>
        /// 客户端身份码
        /// </summary>
        public string ClientIdentity => ClientSource.ClientIdentity;

        /// <summary>
        /// 客户端GUID
        /// </summary>
        public Guid ClientGuid { get; private set; }

        /// <summary>
        /// 指示客户端是否处于连接状态
        /// </summary>
        public bool IsConnected { get; private set; }

        /// <summary>
        /// 客户端授权状态
        /// </summary>
        public AuthenticationStatus AuthStatus { get; private set; }

        /// <summary>
        /// 数据接收缓存
        /// </summary>
        private IList<ArraySegment<byte>> ReceiveBuffer { get; } = new List<ArraySegment<byte>>();

        /// <summary>
        /// 客户端数据源
        /// </summary>
        public IClientSource ClientSource { get; set; }

        /// <summary>
        /// 此客户端所属协议编解码器
        /// </summary>
        private IProtocolEncoder ProtocolEncoder => ClientSource.ProtocolEncoder;

        /// <summary>
        /// 所属服务宿主
        /// </summary>
        private readonly TcpServiceHost _tcpServiceHost;

        /// <summary>
        /// 客户端最后一次活跃时间
        /// </summary>
        public DateTime LastAliveDateTime { get; private set; }

        public SocketActiveClient(Socket clientSocket, TcpServiceHost host)
        {
            ClientGuid = Guid.NewGuid();
            _tcpServiceHost = host;
            _clientSocket = clientSocket;
            ReceiveBuffer.Add(new ArraySegment<byte>(new byte[host.ClientReceiveBufferSize]));
            IsConnected = true;
            _clientSocket.BeginReceive(ReceiveBuffer, SocketFlags.None, Received, _clientSocket);
        }

        private void Received(IAsyncResult result)
        {
            var client = (Socket)result.AsyncState;

            lock (ReceiveBuffer)
            {
                try
                {
                    var readCount = client.EndReceive(result);

                    var array = ReceiveBuffer.Last().Array;
                    lock (_processBuffer)
                    {
                        for (var i = 0; i < readCount; i++)
                        {
                            _processBuffer.Add(array[i]);
                        }
                    }

                    LastAliveDateTime = DateTime.Now;

                    client.BeginReceive(ReceiveBuffer, SocketFlags.None, Received, client);
                }
                catch (Exception)
                {
                    InvalidConnection();
                }
            }

            OnDataReceived();
        }

        private void Process()
        {
            lock (_processBuffer)
            {
                _isProcessing = true;

                while (_processBuffer.Count > 0)
                {
                    try
                    {
                        switch (AuthStatus)
                        {
                            case AuthenticationStatus.NotAuthed:
                                Authentication();
                                break;
                            case AuthenticationStatus.Authed:
                                Decode();
                                break;
                            default:
                                return;
                        }
                    }
                    catch (Exception ex)
                    {
                        OnClientDecodeFailed(ex, string.Empty);
                        _isProcessing = false;
                        return;
                    }
                }

                _isProcessing = false;
            }
        }

        private void Authentication()
        {
            var result = EncodingManager.Authentication(_processBuffer.ToArray());

            AsyncCleanBuffer(result.Package);

            if (result.ResultType != AuthenticationStatus.Authed)
            {
                AuthStatus = result.ResultType;
                return;
            }

            ClientSource = result.AuthedClientSource;
            AuthStatus = AuthenticationStatus.Authed;

            OnClientAuthentication();
        }

        /// <summary>
        /// 解码缓存字节为协议包
        /// </summary>
        private void Decode()
        {
            var result = ProtocolEncoder.Decode(_processBuffer.ToArray());

            AsyncCleanBuffer(result);

            if (result.Finalized)
            {
                ProtocolEncoder.Delive(result, this);
            }
        }

        /// <summary>
        /// 同步清除处理BUFF
        /// </summary>
        /// <param name="package">当前处理中的协议包</param>
        private void AsyncCleanBuffer(IProtocolPackage package)
        {

            switch (package.Status)
            {
                case PackageStatus.NoEnoughBuffer:
                    return;
                case PackageStatus.InvalidHead:
                    _processBuffer.RemoveAt(0);
                    return;
                case PackageStatus.InvalidPackage:
                    _processBuffer.RemoveRange(0, package.PackageLenth);
                    return;
                case PackageStatus.Finalized:
                    _processBuffer.RemoveRange(0, package.PackageLenth);
                    return;
            }

        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="protocolBytes">协议包字节流</param>
        public void Send(byte[] protocolBytes)
        {
            try
            {
                _clientSocket.Send(protocolBytes);
                LastAliveDateTime = DateTime.Now;
            }
            catch (Exception)
            {
                InvalidConnection();
            }
        }

        private void InvalidConnection()
        {
            _clientSocket.Dispose();
            _tcpServiceHost.RemoveClient(this);
            OnClientDisconneted();
        }

        private void OnDataReceived()
        {
            if (!_isProcessing) Process();
            ClientReceivedData?.Invoke(new ActiveClientEventArgs(this));
        }

        private void OnClientDisconneted()
        {
            ClientDisconnect?.Invoke(new ActiveClientEventArgs(this));
        }

        private void OnClientAuthentication()
        {
            ClientAuthenticated?.Invoke(new ActiveClientEventArgs(this));
        }

        private void OnClientDecodeFailed(Exception ex, string message)
        {
            ClientDecodeFalied?.Invoke(new ActiveClientEventArgs(this, ex, message));
        }
    }
}
