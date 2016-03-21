using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
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
    public class TcpClientReceiver
    {
        /// <summary>
        /// 所属的客户端管理器
        /// </summary>
        private readonly ActiveClientManager _manager;

        /// <summary>
        /// 接收器关联的套接字
        /// </summary>
        private readonly Socket _clientSocket;

        /// <summary>
        /// 协议解析缓存
        /// </summary>
        private readonly IList<byte> _processBuffer = new List<byte>();

        /// <summary>
        /// 标识是否正在运行业务数据收发
        /// </summary>
        private bool _running;

        /// <summary>
        /// 协议编解码器
        /// </summary>
        private ProtocolEncoding _protocolEncoding;

        /// <summary>
        /// 数据接收缓存
        /// </summary>
        public IList<ArraySegment<byte>> ReceiveBuffer { get; set; } = new List<ArraySegment<byte>>();

        /// <summary>
        /// 是否已认定身份
        /// </summary>
        public bool Identified { get; set; }

        /// <summary>
        /// 接收器名称
        /// </summary>
        public string ReceiverName { get; set; }

        /// <summary>
        /// 初始化新的TCP客户端接收器实例
        /// </summary>
        /// <param name="manager"></param>
        /// <param name="clientSocket"></param>
        public TcpClientReceiver(ActiveClientManager manager, Socket clientSocket)
        {
            _manager = manager;
            _clientSocket = clientSocket;
            ReceiveBuffer.Add(new ArraySegment<byte>(new byte[AppConfig.TcpBufferSize]));
            var authenticationThread = new Thread(Authentication) { IsBackground = true };
            authenticationThread.Start();
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
        private void Authentication()
        {
            var authStatus = AuthenticationStatus.NotAuthed;

            while (!Identified)
            {
                if (_processBuffer.Count <= 0) continue;

                if (authStatus == AuthenticationStatus.NotAuthed)
                {
                    var device = ProtocolEncoding.Authentication(_processBuffer.ToArray());
                    if (device == null) continue;
                    authStatus = AuthenticationStatus.Authed;
                    _protocolEncoding = new ProtocolEncoding(device.Id);
                }

                if (authStatus == AuthenticationStatus.Authed)
                {
                    //TODO 回复授权信息
                    authStatus = AuthenticationStatus.AuthReplyed;
                }

                if (authStatus == AuthenticationStatus.AuthReplyed)
                {
                    //TODO 接收授权确认
                    authStatus = AuthenticationStatus.AuthConfirmed;
                }

                if (authStatus == AuthenticationStatus.AuthConfirmed)
                {
                    Identified = true;
                    _manager.Authentication(this, new Device());
                    _running = true;
                    var business = new Thread(Start) { IsBackground = true };
                    business.Start();
                }

                Thread.Sleep(10);
            }
        }

        /// <summary>
        /// 开始业务数据收发
        /// </summary>
        private void Start()
        {
            while (_running)
            {
                if (_processBuffer.Count <= 0) continue;
                lock (_processBuffer)
                {
                    var package = _protocolEncoding.Decode(_processBuffer.ToArray());
                    if (!package.Finalized)
                    {
                        _processBuffer.RemoveAt(0);
                        continue;
                    }
                    else
                    {
                        
                    }
                }
            }
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="protocolBytes"></param>
        public void Send(byte[] protocolBytes)
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
    }
}
