﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using SHWDTech.Platform.Model.IModel;
using SHWDTech.Platform.Model.Model;
using SHWDTech.Platform.ProtocolCoding.Authentication;
using SHWDTech.Platform.ProtocolCoding.Coding;
using SHWDTech.Platform.ProtocolCoding.Enums;
using SHWDTech.Platform.Utility;
using WdTech_Protocol_AdminTools.Models;
using WdTech_Protocol_AdminTools.Services;

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
        private readonly List<byte> _processBuffer = new List<byte>();

        /// <summary>
        /// 协议编解码器
        /// </summary>
        private ProtocolEncoding _protocolEncoding;

        /// <summary>
        /// 客户端数据接收事件
        /// </summary>
        public event ClientReceivedDataEventHandler ClientReceivedDataEvent;

        /// <summary>
        /// 客户端断开事件
        /// </summary>
        public event ClientDisconnectEventHandler ClientDisconnectEvent;

        /// <summary>
        /// 指示通信对象是否已经连接上
        /// </summary>
        public bool IsConnected { get; private set; }

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
                int readCount;
                try
                {
                    readCount = client.EndReceive(result);

                    var array = ReceiveBuffer.Last().Array;
                    lock (_processBuffer)
                    {
                        for (var i = 0; i < readCount; i++)
                        {
                            _processBuffer.Add(array[i]);
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (ex.Message == "远程主机强迫关闭了一个现有的连接。")
                    {
                        client.Close();
                        IsConnected = false;
                    }
                    else
                    {
                        ReportService.Instance.Warning("接收客户端数据错误！", ex);
                    }

                    OnClientDisconnect();
                    return;
                }

                if (readCount <= 0)
                {
                    OnClientDisconnect();
                    client.Close(0);
                    IsConnected = false;
                    return;
                }
            }

            OnReceivedData();

            client.BeginReceive(ReceiveBuffer, SocketFlags.None, Received, client);
        }

        private void OnReceivedData()
        {
            switch (_authStatus)
            {
                case AuthenticationStatus.NotAuthed:
                    Authentication();
                    break;
                case AuthenticationStatus.Authed:
                    Decode();
                    break;
            }

            ClientReceivedDataEvent?.Invoke(this);
        }

        /// <summary>
        /// 断开连接时触发
        /// </summary>
        private void OnClientDisconnect()
        {
            ClientDisconnectEvent?.Invoke(this);
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
            if (_processBuffer.Count <= 0) return;

            lock (_processBuffer)
            {
                var result = AuthenticationService.DeviceAuthcation(_processBuffer.ToArray());

                AsyncCleanBuffer(result.Package);

                if (result.ResultType == AuthResultType.Faild)
                {
                    return;
                }

                ClientDevice = result.AuthDevice;
                _protocolEncoding = new ProtocolEncoding(ClientDevice);
                _authStatus = AuthenticationStatus.Authed;
                Send(result.ReplyBytes);
            }
        }

        private void Decode()
        {
            lock (_processBuffer)
            {
                var result = _protocolEncoding.Decode(_processBuffer.ToArray());

                AsyncCleanBuffer(result);
            }
        }

        /// <summary>
        /// 同步清除处理BUFF
        /// </summary>
        /// <param name="package"></param>
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
