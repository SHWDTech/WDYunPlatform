using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using SHWDTech.Platform.ProtocolCoding;
using SHWDTech.Platform.ProtocolCoding.MessageQueueModel;
using WdTech_Protocol_AdminTools.Services;
using System.Timers;
using SHWDTech.Platform.Utility;

namespace WdTech_Protocol_AdminTools.TcpCore
{
    /// <summary>
    /// 活动客户端管理器
    /// </summary>
    public class ActiveClientManager
    {
        /// <summary>
        /// 未认证的客户端连接
        /// </summary>
        // ReSharper disable once CollectionNeverQueried.Local
        private readonly List<TcpClientManager> _clientSockets
            = new List<TcpClientManager>();

        /// <summary>
        /// 连接检查定时器
        /// </summary>
        private readonly Timer _connectionCheckTimer;

        /// <summary>
        /// 连接超时断开周期
        /// </summary>
        private static TimeSpan _disconnectInterval;

        /// <summary>
        /// 是否已经初始化
        /// </summary>
        private static bool _inited;

        /// <summary>
        /// 连接检查时间间隔
        /// </summary>
        private static double _checkInterval;

        /// <summary>
        /// 有效设备连接
        /// </summary>
        public int AliveConnection => _clientSockets.Count;

        public ActiveClientManager()
        {
            if (!_inited) throw new InvalidOperationException("必须先初始化才能创建管理器！");
            _connectionCheckTimer = new Timer();
            _connectionCheckTimer.Elapsed += ConnectionCheck;
            _connectionCheckTimer.Enabled = true;
            _connectionCheckTimer.Interval = _checkInterval;
        }

        public static void Init(double interval, TimeSpan timeSpan)
        {
            _checkInterval = interval;
            _disconnectInterval = timeSpan;
            _inited = true;
        }

        /// <summary>
        /// 启动管理器
        /// </summary>
        public void Start()
        {
            _connectionCheckTimer.Start();
        }

        /// <summary>
        /// 停止管理器
        /// </summary>
        public void Stop()
        {
            lock (_clientSockets)
            {
                for (var i = 0; i < _clientSockets.Count; i++)
                {
                    _clientSockets[i].Dispose();
                    _clientSockets.Remove(_clientSockets[i]);
                }
            }
            _connectionCheckTimer.Stop();
        }

        /// <summary>
        /// 添加一个客户端
        /// </summary>
        /// <param name="client"></param>
        public void AddClient(Socket client)
        {
            var tcpClientManager = new TcpClientManager(client) { ReceiverName = $"UnIdentified - {client.RemoteEndPoint}" };
            tcpClientManager.ClientDisconnectEvent += ClientDisconnected;
            tcpClientManager.ClientAuthenticationEvent += ClientAuthenticationed;
            client.BeginReceive(tcpClientManager.ReceiveBuffer, SocketFlags.None, tcpClientManager.Received, client);

            lock (_clientSockets)
            {
                _clientSockets.Add(tcpClientManager);
            }
        }

        /// <summary>
        /// 客户端断开连接事件
        /// </summary>
        /// <param name="tcpClient">客户端连接</param>
        private void ClientDisconnected(TcpClientManager tcpClient)
        {
            lock (_clientSockets)
            {
                try
                {
                    _clientSockets.Remove(tcpClient);
                }
                catch (Exception ex)
                {
                    LogService.Instance.Fatal($"尝试删除设备失败，目标设备：{tcpClient.DeviceGuid}", ex);
                    return;
                }
            }
            if (tcpClient.IsConnected)
            {
                AdminReportService.Instance.Info($"客户端连接断开，客户端信息：{tcpClient.ReceiverName}");
            }
        }

        private void ClientAuthenticationed(TcpClientManager tcpClient)
        {
            lock (_clientSockets)
            {
                var unUsedCLients =
                    _clientSockets.Where(obj => obj.ClientDevice != null && obj.DeviceGuid == tcpClient.DeviceGuid && obj.ClientGuid != tcpClient.ClientGuid)
                        .Select(item => item.DeviceGuid)
                        .ToList();

                foreach (var client in unUsedCLients)
                {
                    var unUsedCLient = _clientSockets.First(obj => obj.DeviceGuid == client);
                    unUsedCLient.Dispose();
                    try
                    {
                        _clientSockets.Remove(unUsedCLient);
                    }
                    catch (Exception ex)
                    {
                        LogService.Instance.Fatal($"尝试删除连接失效设备失败，目标设备：{unUsedCLient.DeviceGuid}", ex);
                    }
                }

            }

            AdminReportService.Instance.Info($"客户端授权通过，客户端设备NODEID：{tcpClient.ClientDevice.DeviceNodeId}");
        }

        /// <summary>
        /// 发送指令
        /// </summary>
        /// <param name="message">指令消息</param>
        public void SendCommand(CommandMessage message)
        {
            TcpClientManager device;
            lock (_clientSockets)
            {
                device = _clientSockets.FirstOrDefault(client => client.DeviceGuid == message.DeviceGuid);
            }

            if (device == null) return;

            var command = ProtocolInfoManager.GetCommand(message.CommandGuid);

            if (command == null) return;

            device.Send(command, message.Params);
        }

        private void ConnectionCheck(object sender, ElapsedEventArgs e)
        {
            var checkTime = DateTime.Now;
            var currentIndex = 0;
            while (currentIndex < _clientSockets.Count)
            {
                var client = _clientSockets[currentIndex];
                if (checkTime - client.LastAliveDateTime >= _disconnectInterval)
                {
                    client.Close();
                }
                else
                {
                    currentIndex++;
                }
            }
        }
    }
}
