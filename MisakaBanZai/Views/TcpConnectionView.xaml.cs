using System.Net;
using System.Net.Sockets;
using System.Windows;
using MisakaBanZai.Enums;
using MisakaBanZai.Services;

namespace MisakaBanZai.Views
{
    /// <summary>
    /// Interaction logic for TcpConnectionView.xaml
    /// </summary>
    public partial class TcpConnectionView
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

        private TcpConnectionView()
        {
            InitializeComponent();
        }

        public TcpConnectionView(IMisakaConnection connection) : this()
        {
            _misakaConnection = connection;
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
            var endPoint = ((IPEndPoint) server.LocalEndpoint).ToString().Split(':');
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
            var endPoint = ((IPEndPoint) client.LocalEndPoint).ToString().Split(':');
            LocalConnInfo.Content = $"{IPAddress.Parse(endPoint[0])}：{endPoint[1]}";
            ClientLayer.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// 刷新已连接客户端列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RefreshClients(object sender, RoutedEventArgs e)
        {
            CmbConnectedClient.Items.Clear();
            foreach (var clientName in ((MisakaTcpServer)_misakaConnection).GetClientNameList())
            {
                CmbConnectedClient.Items.Add(clientName);
            }
        }
    }
}
