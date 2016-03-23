using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using MisakaBanZai.Common;
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

        public string ConnectionName { get; set; }

        public string ConnectionType { get; set; }

        public object ConnObject => _tcpClient.Client;

        /// <summary>
        /// 指示客户端是否已经连接到服务器
        /// </summary>
        public bool Connected => _tcpClient.Connected;

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
            var ipEndPoint = ((IPEndPoint)client.Client.LocalEndPoint).ToString().Split(':');
            ConnectionName = $"{IPAddress.Parse(ipEndPoint[0])}:{ipEndPoint[1]}";
        }

        /// <summary>
        /// 连接服务器
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <param name="port"></param>
        public void Connect(IPAddress ipAddress, int port)
        {
            try
            {
                _tcpClient.Connect(ipAddress, port);
                _tcpClient.Client.BeginReceive(ReceiveBuffer, SocketFlags.None, Received, _tcpClient.Client);
                ParentWindow.DispatcherAddReportData("连接服务器成功！");
            }
            catch (Exception ex)
            {
                ParentWindow.DispatcherAddReportData("连接服务器失败！");
                LogService.Instance.Error("连接服务器失败！", ex);
            }
            
        }

        /// <summary>
        /// 关闭连接
        /// </summary>
        public void Disconnect()
        {
            _tcpClient.Client.Disconnect(true);
        }

        public void Send(byte[] bytes)
        {
            _tcpClient.Client.Send(bytes);
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

                    OnReceivedData();
                }
                catch (Exception ex)
                {
                    ParentWindow.DispatcherAddReportData("接收客户端数据错误！");
                    LogService.Instance.Error("接收客户端数据错误！", ex);
                    return;
                }

            }

            client.BeginReceive(ReceiveBuffer, SocketFlags.None, Received, client);
        }

        /// <summary>
        /// 数据接收时出发
        /// </summary>
        private void OnReceivedData()
        {
            ClientReceivedDataEvent?.Invoke(this);
        }
    }
}
