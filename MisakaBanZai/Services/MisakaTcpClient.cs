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
        private readonly Socket _tcpClient;

        public event ClientReceivedDataEventHandler ClientReceivedDataEvent;

        /// <summary>
        /// 客户端断开事件
        /// </summary>
        public event ClientDisconnectEventHandler ClientDisconnectEvent;

        public string ConnectionName => $"{IpAddress}:{Port}";

        public string ConnectionType { get; set; }

        public object ConnObject => _tcpClient;

        public string IpAddress { get; }

        public int Port { get; }

        public bool IsConnected { get;  private set; }

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
            _tcpClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _tcpClient.Bind(new IPEndPoint(ipAddress, port));
            IpAddress = $"{ipAddress}";
            Port = port;
        }

        public MisakaTcpClient(Socket client)
        {
            _tcpClient = client;
            var ipEndPoint = ((IPEndPoint)client.RemoteEndPoint).ToString().Split(':');
            IpAddress = $"{ipEndPoint[0]}";
            Port = int.Parse(ipEndPoint[1]);
        }

        /// <summary>
        /// 开始接受数据
        /// </summary>
        public void ClientBeginReceive()
        {
            _tcpClient.BeginReceive(ReceiveBuffer, SocketFlags.None, Received, _tcpClient);
        }

        /// <summary>
        /// 连接服务器
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <param name="port"></param>
        public bool Connect(IPAddress ipAddress, int port)
        {
            try
            {
                _tcpClient.Connect(ipAddress, port);
                _tcpClient.BeginReceive(ReceiveBuffer, SocketFlags.None, Received, _tcpClient);
                IsConnected = true;
            }
            catch (Exception ex)
            {
                LogService.Instance.Error("连接服务器失败！", ex);
                return false;
            }

            return true;
        }

        public int Send(byte[] bytes)
        {
            if (!_tcpClient.Connected) return 0;
            try
            {
                _tcpClient.Send(bytes);
            }
            catch (ObjectDisposedException)
            {
                OnClientDisconnect();
                IsConnected = false;
            }
            return bytes.Length;
        }

        public bool Close()
        {
            try
            {
                ClientReceivedDataEvent = null;
                _tcpClient.Shutdown(SocketShutdown.Both);
                _tcpClient.Disconnect(false);
                IsConnected = false;
            }
            catch (ObjectDisposedException)
            {
                OnClientDisconnect();
                IsConnected = false;
            }
            catch (Exception ex)
            {
                LogService.Instance.Error("关闭套接字错误。", ex);
                return false;
            }

            return true;
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

                    if (readCount <= 0)
                    {
                        OnClientDisconnect();
                        client.Close(0);
                        return;
                    }

                    OnReceivedData();
                }
                catch (SocketException)
                {
                    OnClientDisconnect();
                    client.Close(0);
                }
                catch (ObjectDisposedException)
                {
                    OnClientDisconnect();
                    client.Close(0);
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
                client.Close(0);
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
