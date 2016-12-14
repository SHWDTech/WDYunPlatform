using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace LampblackTransfer
{
    class Program
    {
        private static List<DeviceInfo> _deviceInfos = new List<DeviceInfo>();

        private static readonly Dictionary<DeviceInfo, TcpClient> Clients = new Dictionary<DeviceInfo, TcpClient>();

        private static string _connectionString;

        private static IPAddress _serverIpAddress;

        private static int _serverPort;

        private static IPAddress _clientIpAddress;

        private static int _clientPort;

        private static readonly Dictionary<string, DeviceTime> DeviceTimes = new Dictionary<string, DeviceTime>();

        static void Main()
        {
            InitProgramConfig();
            Console.ReadKey();
            StartTransfer();
            while (true)
            {
                SendData();
                Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss}本次任务结束。");
                var nowTime = int.Parse(DateTime.Now.ToString("HHmm"));
                if (nowTime > 855 && nowTime < 900)
                {
                    RefreashDeviceInfos();
                }
                Thread.Sleep(60000);
            }
            // ReSharper disable once FunctionNeverReturns
        }

        static void InitProgramConfig()
        {
            _connectionString = ConfigurationManager.AppSettings["dbcon"];
            _serverIpAddress = IPAddress.Parse(ConfigurationManager.AppSettings["serverIp"]);
            _serverPort = int.Parse(ConfigurationManager.AppSettings["serverPort"]);
            _clientIpAddress = IPAddress.Parse(ConfigurationManager.AppSettings["clientIp"]);
            _clientPort = int.Parse(ConfigurationManager.AppSettings["clientPort"]);
            RefreashDeviceInfos();
        }

        static void RefreashDeviceInfos()
        {
            var tableName = "DeviceInfo";
            using (var conn = new SQLiteConnection(_connectionString))
            {
                var cmd = new SQLiteCommand($"SELECT * FROM {tableName}", conn);
                var adapter = new SQLiteDataAdapter(cmd);
                var table = new DataTable();
                adapter.Fill(table);
                _deviceInfos = table.ToListOf<DeviceInfo>();
            }

            RefreashDeviceTime();
        }

        static void RefreashDeviceTime()
        {
            DeviceTimes.Clear();
            var rd = new Random();
            foreach (var deviceInfo in _deviceInfos)
            {
                DeviceTimes.Add(deviceInfo.NodeId,new DeviceTime()
                {
                    StartTime = rd.Next(900, 1000),
                    EndTime = rd.Next(2200, 2300)
                } );
            }
        }

        static void StartTransfer()
        {
            foreach (var deviceInfo in _deviceInfos)
            {
                Connect(deviceInfo);
                Thread.Sleep(100);
            }
        }

        static void Connect(DeviceInfo device)
        {

            var ipEndPoint = new IPEndPoint(_clientIpAddress, _clientPort);
            var client = new TcpClient(ipEndPoint);
            client.Connect(_serverIpAddress, _serverPort);
            client.Client.Send(AutoProtocol.GetHeartBytes(device.NodeId));
            _clientPort++;
            Clients.Add(device, client);
        }

        static void SendData()
        {
            foreach (var tcpClient in Clients)
            {
                var nowTime = int.Parse(DateTime.Now.ToString("HHmm"));
                var time = DeviceTimes[tcpClient.Key.NodeId];
                if(nowTime < time.StartTime || nowTime > time.EndTime)
                    continue;
                tcpClient.Value.Client.Send(
                    AutoProtocol.GetAutoReportBytes(new AutoReportConfig
                    {
                        CleanerNumber = GetRate(tcpClient.Key.Rate),
                        CleanerSwitch = tcpClient.Key.Opened,
                        FanSwitch = tcpClient.Key.Opened,
                        NodeId = tcpClient.Key.NodeId
                    }));

                Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss}：发送数据成功，设备NODEID：{tcpClient.Key.NodeId}。");
                Thread.Sleep(300);
            }
        }

        static int GetRate(long rate)
        {
            switch (rate)
            {
                case 0:
                    return new Random().Next(0,50);
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
