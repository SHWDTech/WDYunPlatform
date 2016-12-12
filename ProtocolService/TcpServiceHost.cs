using System;
using System.Net;
using System.Net.Sockets;

namespace SHWDTech.Platform.ProtocolService
{
    public class TcpServiceHost : ServiceHost
    {
        /// <summary>
        /// 接受客户端连接事件
        /// </summary>
        public event ActiveClientEventHandler ClientAccepted;

        /// <summary>
        /// 客户端接收缓存大小
        /// </summary>
        public int ClientReceiveBufferSize = 4096;

        public TcpServiceHost()
        {
            HostSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public TcpServiceHost(string ipAddress, int port) : this()
        {
            HostSocket.Bind(new IPEndPoint(IPAddress.Parse(ipAddress), port));
        }

        public TcpServiceHost(EndPoint endPoint) : this()
        {
            HostSocket.Bind(endPoint);
        }

        public override void Start()
        {
            HostSocket.LingerState = new LingerOption(false, 1);
            HostSocket.Listen(2048);
            HostSocket.BeginAccept(AcceptClient, HostSocket);
            StartDateTime = DateTime.Now;
            IsRunning = true;
        }

        public override void Stop()
        {
            if (!IsRunning) return;

            try
            {
                if (HostSocket.Connected)
                {
                    HostSocket.Shutdown(SocketShutdown.Both);
                }

                IsRunning = false;
                StartDateTime = DateTime.MinValue;
            }
            catch (SocketException)
            {
                return;
            }

            ServiceManager.Unregister(this);
        }

        public override void RemoveClient(IActiveClient client)
        {
            
        }

        private void AcceptClient(IAsyncResult result)
        {
            var server = (Socket) result.AsyncState;

            try
            {
                var client = new SocketActiveClient(server.EndAccept(result), this);
                ActiveClients.Add(client);
                OnClientAccedped(client);
            }
            catch (ObjectDisposedException)
            {
                Restart();
                return;
            }
            catch (SocketException)
            {
                
            }

            server.BeginAccept(AcceptClient, server);
        }

        /// <summary>
        /// 触发客户端连接接受事件
        /// </summary>
        /// <param name="client"></param>
        private void OnClientAccedped(IActiveClient client)
        {
            ClientAccepted?.Invoke(new ActiveClientEventArgs(client));
        }
    }
}
