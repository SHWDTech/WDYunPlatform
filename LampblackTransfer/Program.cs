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

        static void Main()
        {
            InitProgramConfig();
            StartTransfer();
            while (true)
            {
                SendData();
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
        }

        static void StartTransfer()
        {
            foreach (var deviceInfo in _deviceInfos)
            {
                Connect(deviceInfo);
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
            Thread.Sleep(100);
        }

        static void SendData()
        {
            foreach (var tcpClient in Clients)
            {
                tcpClient.Value.Client.Send(
                    AutoProtocol.GetAutoReportBytes(new AutoReportConfig
                    {
                        CleanerNumber = GetRate(tcpClient.Key.Rate),
                        CleanerSwitch = tcpClient.Key.Opened,
                        FanSwitch = tcpClient.Key.Opened,
                        NodeId = tcpClient.Key.NodeId
                    }));
            }
        }

        static int GetRate(long rate)
        {
            switch (rate)
            {
                case 0:
                    return new Random().Next(0,5000);
                case 1:
                    return new Random().Next(5001, 20000);
                case 2:
                    return new Random().Next(20001, 50000);
                default:
                    return new Random().Next(50001, 100000);
            }
        }
    }
}
