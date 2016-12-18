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

        public event ActiveClientEventHandler ClientReceivedData;

        public event ActiveClientEventHandler ClientDisconnect;

        public event ActiveClientEventHandler ClientAuthenticated;

        public event ActiveClientEventHandler ClientAuthenticateFailed;

        public event ActiveClientEventHandler ClientDecodeFalied;

        public string ClientAddress { get; }

        public string ClientIdentity => ClientSource == null ? "未认证连接" : ClientSource.ClientIdentity;

        public Guid ClientGuid { get; }

        public bool IsConnected { get; private set; }

        public AuthenticationStatus AuthStatus { get; private set; }

        /// <summary>
        /// 数据接收缓存
        /// </summary>
        private IList<ArraySegment<byte>> ReceiveBuffer { get; } = new List<ArraySegment<byte>>();

        public IClientSource ClientSource { get; set; }

        /// <summary>
        /// 此客户端所属协议编解码器
        /// </summary>
        private IProtocolEncoder ProtocolEncoder => ClientSource.ProtocolEncoder;

        /// <summary>
        /// 所属服务宿主
        /// </summary>
        private readonly TcpServiceHost _tcpServiceHost;

        public DateTime LastAliveDateTime { get; private set; }

        public SocketActiveClient(Socket clientSocket, TcpServiceHost host)
        {
            ClientGuid = Guid.NewGuid();
            _tcpServiceHost = host;
            _clientSocket = clientSocket;
            ClientAddress = _clientSocket.RemoteEndPoint.ToString();
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
                    if (readCount == 0)
                    {
                        client.Disconnect(true);
                        InvalidConnection();
                        return;
                    }

                    if (AuthStatus == AuthenticationStatus.AuthFailed ||
                        AuthStatus == AuthenticationStatus.ClientNotRegistered)
                    {
                        return;
                    }
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
                    return;
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
                                _processBuffer.Clear();
                                _isProcessing = false;
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
                OnClientAuthenticaFailed();
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
            var package = ProtocolEncoder.Decode(_processBuffer.ToArray());
            AsyncCleanBuffer(package);
            if (!package.Finalized) return;

            package.ClientSource = ClientSource;
            EncodingManager.RunBuinessHandler(package);
        }

        /// <summary>
        /// 同步清除处理BUFF
        /// </summary>
        /// <param name="package">当前处理中的协议包</param>
        private void AsyncCleanBuffer(IProtocolPackage package)
        {
            if (package == null) return;
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

        /// <summary>
        /// 关闭连接
        /// </summary>
        public void Close()
            => InvalidConnection(false);

        /// <summary>
        /// 处理失效的连接
        /// </summary>
        private void InvalidConnection(bool remove = true)
        {
            IsConnected = false;
            _clientSocket.Close();
            _clientSocket.Dispose();
            if (remove)
            {
                _tcpServiceHost.RemoveClient(this);
            }
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

        private void OnClientAuthenticaFailed()
        {
            ClientAuthenticateFailed?.Invoke(new ActiveClientEventArgs(this));
        }

        private void OnClientDecodeFailed(Exception ex, string message)
        {
            ClientDecodeFalied?.Invoke(new ActiveClientEventArgs(this, ex, message));
        }
    }
}
