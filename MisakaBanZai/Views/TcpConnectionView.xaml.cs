using System;
using System.Net;
using System.Net.Sockets;
using System.Windows;
using System.Windows.Threading;
using MisakaBanZai.Common;
using MisakaBanZai.Enums;
using MisakaBanZai.Models;
using MisakaBanZai.Services;
using SHWDTech.Platform.Utility;

namespace MisakaBanZai.Views
{
    /// <summary>
    /// Interaction logic for TcpConnectionView.xaml
    /// </summary>
    public partial class TcpConnectionView : IMisakaConnectionManagerWindow
    {
        /// <summary>
        /// 通信连接对象
        /// </summary>
        private readonly IMisakaConnection _misakaConnection;

        /// <summary>
        /// 是否HEX发送
        /// </summary>
        private bool? HexSend => ChkHexSend.IsChecked;

        /// <summary>
        /// 是否HEX接收
        /// </summary>
        private bool? HexReceive => ChkHexReceive.IsChecked;

        /// <summary>
        /// 报告委托
        /// </summary>
        /// <returns></returns>
        private delegate string ReportDispatcherDelegate();

        /// <summary>
        /// 添加报告数据委托
        /// </summary>
        /// <param name="str"></param>
        private delegate void AddReportDataDispatcherDelegate(string str);

        /// <summary>
        /// 无返回值无参通用委托
        /// </summary>
        private delegate void CommonMethodDispatcherDelegate();

        /// <summary>
        /// 数据接收委托
        /// </summary>
        /// <param name="conn"></param>
        private delegate void ReceivedDataDispatcherDelegate(IMisakaConnection conn);

        /// <summary>
        /// 时间显示格式
        /// </summary>
        private string DateDisplayFormat => ChkFullDateMode.IsChecked == true
                                                ? Appconfig.FullDateFormat
                                                : Appconfig.ShortDateFormat;

        public ReportService ReportService { get; set; } = new ReportService();

        private TcpConnectionView()
        {
            InitializeComponent();
        }

        public TcpConnectionView(IMisakaConnection connection) : this()
        {
            _misakaConnection = connection;
            ReportService.ReportDataAdded += AppendReport;
            InitControl(connection);
        }

        /// <summary>
        /// 初始化控件
        /// </summary>
        /// <param name="connection"></param>
        private void InitControl(IMisakaConnection connection)
        {
            ConnType.Content = _misakaConnection.ConnectionType;

            switch (connection.ConnectionType)
            {
                case ConnectionItemType.TcpServer:
                    InitServer(connection);
                    break;
                case ConnectionItemType.TcpClient:
                    InitClient(connection);
                    break;
            }
        }

        /// <summary>
        /// 初始化服务器模式
        /// </summary>
        /// <param name="connection"></param>
        private void InitServer(IMisakaConnection connection)
        {
            var server = (TcpListener)connection.ConnObject;
            var endPoint = ((IPEndPoint)server.LocalEndpoint).ToString().Split(':');
            connection.ParentWindow = this;
            connection.ClientReceivedDataEvent += DispatcherOutPutSocketData;
            var misakaServer = connection as MisakaTcpServer;
            if (misakaServer != null) misakaServer.ClientAccept += RefreshClients;
            LocalConnInfo.Content = $"{IPAddress.Parse(endPoint[0])}：{endPoint[1]}";
            ServerLayer.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// 初始化客户端模式
        /// </summary>
        /// <param name="connection"></param>
        private void InitClient(IMisakaConnection connection)
        {
            var client = (Socket)connection.ConnObject;
            var endPoint = ((IPEndPoint)client.LocalEndPoint).ToString().Split(':');
            connection.ParentWindow = this;
            connection.ClientReceivedDataEvent += DispatcherOutPutSocketData;
            LocalConnInfo.Content = $"{IPAddress.Parse(endPoint[0])}：{endPoint[1]}";
            ClientLayer.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// 刷新已连接客户端列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RefreshClients(object sender, EventArgs e)
        {
            Dispatcher.Invoke(DispatcherPriority.Normal, new CommonMethodDispatcherDelegate(RefreshClients));
        }

        /// <summary>
        /// 刷新已连接客户端列表
        /// </summary>
        private void RefreshClients()
        {
            CmbConnectedClient.Items.Clear();
            foreach (var clientName in ((MisakaTcpServer)_misakaConnection).GetClientNameList())
            {
                CmbConnectedClient.Items.Add(clientName);
                CmbConnectedClient.SelectedIndex = CmbConnectedClient.Items.Count - 1;
            }
        }

        /// <summary>
        /// 开启/关闭侦听服务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SwitchServerStatus(object sender, RoutedEventArgs e)
        {
            var conn = (MisakaTcpServer)_misakaConnection;
            if (!conn.IsStarted)
            {
                conn.Start();
                BtnStartListening.Content = "停止侦听";
                DispatcherAddReportData("服务器侦听启动");
            }
            else
            {
                conn.Stop();
                BtnStartListening.Content = "开始侦听";
                DispatcherAddReportData("服务器侦听结束");
            }
        }

        /// <summary>
        /// 输出报告
        /// </summary>
        /// <param name="e"></param>
        private void AppendReport(EventArgs e)
        {
            
            var reportData = Dispatcher.Invoke(DispatcherPriority.Normal, new ReportDispatcherDelegate(ReportService.PopupReport)).ToString();
            while (reportData != null)
            {
                TxtReceiveViewer.AppendText($"Re[{DateTime.Now.ToString(DateDisplayFormat)}] =>{reportData}\r\n");
                reportData = ReportService.PopupReport();
            }
        }

        /// <summary>
        /// 添加报告数据调度方法
        /// </summary>
        /// <param name="str"></param>
        public void DispatcherAddReportData(string str)
        {
            Dispatcher.Invoke(DispatcherPriority.Normal,
                new AddReportDataDispatcherDelegate(AddReportData), str);
        }

        /// <summary>
        /// 添加报告数据
        /// </summary>
        /// <param name="str"></param>
        private void AddReportData(string str)
        {
            ReportService.AddReportData(str);
        }

        /// <summary>
        /// 设置当前客户端
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetCurrentClient(object sender, RoutedEventArgs e)
        {
            if (!(_misakaConnection is MisakaTcpServer)) return;

            var conn = (MisakaTcpServer) _misakaConnection;
            conn.SetCurrentClient(CmbConnectedClient.SelectedItem.ToString());
        }

        /// <summary>
        /// 输出套接字接收到的字节
        /// </summary>
        private void DispatcherOutPutSocketData(IMisakaConnection conn)
        {
            Dispatcher.Invoke(DispatcherPriority.Normal, new ReceivedDataDispatcherDelegate(OutPutSocketData), conn);
        }

        /// <summary>
        /// 输出套接字接收到的字节
        /// </summary>
        /// <param name="conn"></param>
        private void OutPutSocketData (IMisakaConnection conn)
        {
            var socketBytes = conn.OutPutSocketBytes();

            TxtReceiveViewer.AppendText($"Data[{DateTime.Now.ToString(DateDisplayFormat)}]:{Globals.ByteArrayToString(socketBytes, HexReceive == true)}\r\n");
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Send(object sender, RoutedEventArgs e)
        {
            _misakaConnection.Send(Globals.StringToByteArray(TxtDataSend.Text, HexSend == true));
        }

        /// <summary>
        /// 尝试连接服务器
        /// </summary>
        private void Connect(object sender, RoutedEventArgs e)
        {
            if (!(_misakaConnection is MisakaTcpClient)) return;

            var conn = (MisakaTcpClient) _misakaConnection;

            if (conn.Connected)
            {
                conn.Disconnect();
                BtnConnect.Content = "连接服务器";
            }
            else
            {
                IPAddress ipAddress;
                int port;
                try
                {
                    ipAddress = IPAddress.Parse(TxtRemoteConnAddr.Text);

                }
                catch (Exception)
                {
                    ReportService.AddReportData("非法的IP地址！");
                    return;
                }

                try
                {
                    port = int.Parse(TxtRemoteConnPort.Text);
                }
                catch (Exception)
                {
                    ReportService.AddReportData("非法的端口号！");
                    return;
                }

                var misakaClient = (MisakaTcpClient)_misakaConnection;
                misakaClient.Connect(ipAddress, port);
                BtnConnect.Content = "断开连接";
            }
        }
    }
}
