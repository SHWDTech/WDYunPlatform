using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using MisakaBanZai.Common;
using MisakaBanZai.Enums;
using MisakaBanZai.Models;
using SHWDTech.Platform.Utility;

namespace MisakaBanZai.Services
{
    public class MisakaTcpClient : IMisakaConnection
    {
        /// <summary>
        /// TCP客户端对象
        /// </summary>
        private readonly TcpClient _tcpClient;

        public event ClientReceivedDataEventHandler ClientReceivedDataEvent;

        /// <summary>
        /// 客户端断开事件
        /// </summary>
        public event ClientDisconnectEventHandler ClientDisconnectEvent;

        public string ConnectionName { get; set; }

        public string ConnectionType { get; set; }

        public object ConnObject => _tcpClient.Client;

        /// <summary>
        /// 指示客户端是否已经连接到服务器
        /// </summary>
        //public bool Connected;

        /// <summary>
        /// 数据接收缓存
        /// </summary>
        public IList<ArraySegment<byte>> ReceiveBuffer { get; }
            = new List<ArraySegment<byte>>() { new ArraySegment<byte>(new byte[Appconfig.TcpBufferSize]) };

        public IMisakaConnectionManagerWindow ParentWindow { get; set; }

        public byte[] OutPutSocketBytes()
        {
            byte[] receivedData;

            lock (ProcessBuffer)
            {
                receivedData = ProcessBuffer.ToArray();
                for (var i = 0; i < receivedData.Length; i++)
                {
                    ProcessBuffer.RemoveAt(0);
                }
            }
            return receivedData;
        }

        /// <summary>
        /// 数据处理缓存
        /// </summary>
        public IList<byte> ProcessBuffer { get; } = new List<byte>();

        public MisakaTcpClient(IPAddress ipAddress, int port)
        {
            _tcpClient = new TcpClient(new IPEndPoint(ipAddress, port));
            ConnectionName = $"{ipAddress}:{port}";
        }

        public MisakaTcpClient(TcpClient client)
        {
            _tcpClient = client;
            var ipEndPoint = ((IPEndPoint)client.Client.RemoteEndPoint).ToString().Split(':');
            ConnectionName = $"{IPAddress.Parse(ipEndPoint[0])}:{ipEndPoint[1]}";
        }

        /// <summary>
        /// 开始接受数据
        /// </summary>
        public void ClientBeginReceive()
        {
            _tcpClient.Client.BeginReceive(ReceiveBuffer, SocketFlags.None, Received, _tcpClient.Client);
        }

        /// <summary>
        /// 连接服务器
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <param name="port"></param>
        public void Connect(IPAddress ipAddress, int port)
        {
            _tcpClient.Connect(ipAddress, port);
            _tcpClient.Client.BeginReceive(ReceiveBuffer, SocketFlags.None, Received, _tcpClient.Client);
            ParentWindow.DispatcherAddReportData(ReportMessageType.Error, "连接服务器成功！");
        }

        public int Send(byte[] bytes)
        {
            if (!_tcpClient.Connected) return 0;
            try
            {
                _tcpClient.Client.Send(bytes);
            }
            catch (ObjectDisposedException)
            {
                OnClientDisconnect();
            }
            return bytes.Length;
        }

        public void Close()
        {
            try
            {
                ClientReceivedDataEvent = null;
                _tcpClient.Client.Shutdown(SocketShutdown.Both);
                _tcpClient.Client.Disconnect(false);
            }
            catch (Exception ex)
            {
                LogService.Instance.Error("关闭套接字错误。", ex);
            }
        }

        /// <summary>
        /// TCP客户端异步接收数据
        /// </summary>
        /// <param name="result"></param>
        private void Received(IAsyncResult result)
        {
            var client = (Socket)result.AsyncState;

            lock (ReceiveBuffer)
            {
                try
                {
                    var readCount = client.EndReceive(result);

                    var array = ReceiveBuffer.Last().Array;
                    for (var i = 0; i < readCount; i++)
                    {
                        ProcessBuffer.Add(array[i]);
                    }

                    //TODO 客户端发送数据到服务器，服务器回复了一个空包，是否正常。搞清楚才能判断。
                    if (readCount <= 0)
                    {
                        OnClientDisconnect();
                        client.Close();
                    }

                    OnReceivedData();
                }
                catch (Exception ex)
                {
                    ParentWindow.DispatcherAddReportData(ReportMessageType.Warning, "接收客户端数据错误！");
                    LogService.Instance.Error("接收客户端数据错误！", ex);
                    return;
                }
            }

            try
            {
                client.BeginReceive(ReceiveBuffer, SocketFlags.None, Received, client);
            }
            catch(Exception)
            {
                client.Close();
            }
        }

        /// <summary>
        /// 数据接收时出发
        /// </summary>
        private void OnReceivedData()
        {
            ClientReceivedDataEvent?.Invoke(this);
        }

        /// <summary>
        /// 断开连接时触发
        /// </summary>
        private void OnClientDisconnect()
        {
            ClientDisconnectEvent?.Invoke(this);
        }
    }
}
