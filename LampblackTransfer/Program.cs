using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using SHWDTech.Platform.Utility;

namespace LampblackTransfer
{
    internal static class Program
    {
        private static readonly List<DeviceInfo> DeviceInfos = new List<DeviceInfo>();

        private static readonly Dictionary<DeviceInfo, int> DevicePort = new Dictionary<DeviceInfo, int>();

        private static readonly Dictionary<DeviceInfo, TcpClient> Clients = new Dictionary<DeviceInfo, TcpClient>();

        private static string _connectionString;

        private static IPAddress _serverIpAddress;

        private static int _serverPort;

        private static IPAddress _clientIpAddress;

        private static int _clientPort;

        private static readonly Dictionary<string, DeviceTime> DeviceTimes = new Dictionary<string, DeviceTime>();

        private static void Main()
        {
            InitProgramConfig();
            Console.ReadKey();
            StartTransfer();
            Task.Factory.StartNew(DeviceTryReConnect);
            while (true)
            {
                SendData();
                Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss}本次任务结束。");
                RefreashDeviceInfos();
                Thread.Sleep(60000);
            }
            // ReSharper disable once FunctionNeverReturns
        }

        private static void InitProgramConfig()
        {
            _connectionString = ConfigurationManager.AppSettings["dbcon"];
            _serverIpAddress = IPAddress.Parse(ConfigurationManager.AppSettings["serverIp"]);
            _serverPort = int.Parse(ConfigurationManager.AppSettings["serverPort"]);
            _clientIpAddress = IPAddress.Parse(ConfigurationManager.AppSettings["clientIp"]);
            _clientPort = int.Parse(ConfigurationManager.AppSettings["clientPort"]);
            RefreashDeviceInfos();
        }

        private static void RefreashDeviceInfos()
        {
            const string tableName = "DeviceInfo";
            using (var conn = new SQLiteConnection(_connectionString))
            {
                var cmd = new SQLiteCommand($"SELECT * FROM {tableName}", conn);
                var adapter = new SQLiteDataAdapter(cmd);
                var table = new DataTable();
                adapter.Fill(table);
                var freashInfo = table.ToListOf<DeviceInfo>();
                foreach (var source in freashInfo.Where(d => DeviceInfos.All(i => i.Id != d.Id)))
                {
                    DeviceInfos.Add(source);
                }
            }
            AddDevicePort();
            RefreashDeviceTime();
        }

        private static void AddDevicePort()
        {
            foreach (var deviceInfo in DeviceInfos)
            {
                if (DevicePort.ContainsKey(deviceInfo)) continue;
                _clientPort++;
                DevicePort.Add(deviceInfo, _clientPort);
            }
        }

        private static void RefreashDeviceTime()
        {
            DeviceTimes.Clear();
            var rd = new Random();
            foreach (var deviceInfo in DeviceInfos)
            {
                DeviceTimes.Add(deviceInfo.NodeId,new DeviceTime()
                {
                    StartTime = rd.Next(800, 1000),
                    EndTime = rd.Next(2100, 2300)
                } );
            }
        }

        private static void StartTransfer()
        {
            foreach (var deviceInfo in DeviceInfos)
            {
                Connect(deviceInfo);
            }
        }

        private static void Connect(DeviceInfo device)
        {
            try
            {
                var port = DevicePort[device];
                var ipEndPoint = new IPEndPoint(_clientIpAddress, port);
                var client = new TcpClient(ipEndPoint);
                client.Connect(_serverIpAddress, _serverPort);
                client.Client.Send(AutoProtocol.GetHeartBytes(device.NodeId));
                Clients.Add(device, client);
            }
            catch (Exception ex)
            {
                LogService.Instance.Error("连接服务器失败。", ex);
            }
        }

        private static void DeviceTryReConnect()
        {
            while (true)
            {
                var disconnectDevices = DeviceInfos.Where(obj => !Clients.ContainsKey(obj)).ToList();
                foreach (var disconnectDevice in disconnectDevices)
                {
                    Connect(disconnectDevice);
                }
                Thread.Sleep(60000);
            }
            // ReSharper disable once FunctionNeverReturns
        }

        private static void SendData()
        {
            foreach (var dev in DeviceInfos)
            {
                if (!Clients.ContainsKey(dev)) continue;
                var tcpClient = Clients[dev];
                try
                {
                    var nowTime = int.Parse(DateTime.Now.ToString("HHmm"));
                    var time = DeviceTimes[dev.NodeId];
                    if (nowTime < time.StartTime || nowTime > time.EndTime)
                        continue;
                    tcpClient.Client.Send(
                        AutoProtocol.GetAutoReportBytes(new AutoReportConfig
                        {
                            CleanerNumber = GetRate(dev.Rate),
                            CleanerSwitch = dev.Opened,
                            FanSwitch = dev.Opened,
                            NodeId = dev.NodeId
                        }));

                    Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss}：发送数据成功，设备NODEID：{dev.NodeId}。");
                }
                catch (Exception ex)
                {
                    LogService.Instance.Error($"发送数据失败，设备NODEID：{dev.NodeId}。", ex);
                    tcpClient.Client.Close();
                    tcpClient.Client.Dispose();
                    Clients.Remove(dev);
                }
            }
        }

        private static int GetRate(long rate)
        {
            switch (rate)
            {
                case 0:
                    return new Random().Next(5,50);
                case 1:
                    return new Random().Next(51, 200);
                case 2:
                    return new Random().Next(201, 500);
                default:
                    return new Random().Next(501, 1000);
            }
        }
    }
}
