using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using SHWDTech.Platform.Model.IModel;
using SHWDTech.Platform.Model.Model;
using SHWDTech.Platform.ProtocolCoding.Coding;
using SHWDTech.Platform.ProtocolCoding.Enums;
using SHWDTech.Platform.Utility;
using WdTech_Protocol_AdminTools.Models;

namespace WdTech_Protocol_AdminTools.TcpCore
{
    /// <summary>
    /// TCP客户端数据接收器
    /// </summary>
    public class TcpClientManager
    {
        /// <summary>
        /// 接收器关联的套接字
        /// </summary>
        private readonly Socket _clientSocket;

        /// <summary>
        /// 协议解析缓存
        /// </summary>
        private readonly IList<byte> _processBuffer = new List<byte>();

        /// <summary>
        /// 协议编解码器
        /// </summary>
        private ProtocolEncoding _protocolEncoding;

        /// <summary>
        /// 设备认证状态
        /// </summary>
        private AuthenticationStatus _authStatus = AuthenticationStatus.NotAuthed;

        /// <summary>
        /// 数据接收缓存
        /// </summary>
        public IList<ArraySegment<byte>> ReceiveBuffer { get; } = new List<ArraySegment<byte>>();

        /// <summary>
        /// 套接字设备ID
        /// </summary>
        public Guid DeviceGuid => ClientDevice.Id;

        /// <summary>
        /// 套接字对应设备
        /// </summary>
        public IDevice ClientDevice { get; private set; }

        /// <summary>
        /// 接收器名称
        /// </summary>
        public string ReceiverName { get; set; }

        /// <summary>
        /// 初始化新的TCP客户端接收器实例
        /// </summary>
        /// <param name="clientSocket"></param>
        public TcpClientManager(Socket clientSocket)
        {
            _clientSocket = clientSocket;
            ReceiveBuffer.Add(new ArraySegment<byte>(new byte[AppConfig.TcpBufferSize]));
        }

        /// <summary>
        /// TCP客户端异步接收数据
        /// </summary>
        /// <param name="result"></param>
        public void Received(IAsyncResult result)
        {
            var client = (Socket)result.AsyncState;

            lock (ReceiveBuffer)
            {
                var readCount = client.EndReceive(result);

                var array = ReceiveBuffer.Last().Array;
                for (var i = 0; i < readCount; i++)
                {
                    _processBuffer.Add(array[i]);
                }
            }

            client.BeginReceive(ReceiveBuffer, SocketFlags.None, Received, client);
        }

        /// <summary>
        /// 关闭TCP客户端连接
        /// </summary>
        public void Close()
        {
            _clientSocket.Close();
        }

        /// <summary>
        /// 身份验证
        /// </summary>
        public bool Authentication()
        {
            if (_processBuffer.Count <= 0) return false;

            switch (_authStatus)
            {
                case AuthenticationStatus.NotAuthed:
                    lock (_processBuffer)
                    {
                        var device = ProtocolEncoding.Authentication(_processBuffer.ToArray());
                        if (device == null) return false;
                        _authStatus = AuthenticationStatus.Authed;
                        ClientDevice = device;
                        _protocolEncoding = new ProtocolEncoding(device.Id);
                    }
                    break;
                case AuthenticationStatus.Authed:
                    Send(ProtocolEncoding.ReplyAuthentication(ClientDevice));
                    _authStatus = AuthenticationStatus.AuthReplyed;
                    break;
                case AuthenticationStatus.AuthReplyed:
                    lock (_processBuffer)
                    {
                        if (ProtocolEncoding.ConfirmAuthentication(_processBuffer.ToArray()))
                        {
                            _authStatus = AuthenticationStatus.AuthConfirmed;
                        }
                    }
                    break;
            }

            return _authStatus == AuthenticationStatus.AuthConfirmed;
        }

        public ProtocolPackage Decode()
        {
            ProtocolPackage package;
            lock (_processBuffer)
            {
                package = _protocolEncoding.Decode(_processBuffer.ToArray());

                if (package == null)
                {
                    _processBuffer.RemoveAt(0);
                    return null;
                }

                for (var i = 0; i < package.PackageLenth; i++)
                {
                    _processBuffer.RemoveAt(0);
                }
            }

            return package;
        } 

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="protocolBytes"></param>
        private void Send(byte[] protocolBytes)
        {
            try
            {
                _clientSocket.Send(protocolBytes);
            }
            catch (Exception ex)
            {
                LogService.Instance.Error($"套接字数据发送错误，相关套接字信息：{ReceiverName}", ex);
                if (ex is ObjectDisposedException)
                {
                    Close();
                }
            }
        }

        /// <summary>
        /// 发送协议包数据
        /// </summary>
        /// <param name="command"></param>
        public void Send(ProtocolCommand command) => Send(_protocolEncoding.Encode(command));
    }
}
