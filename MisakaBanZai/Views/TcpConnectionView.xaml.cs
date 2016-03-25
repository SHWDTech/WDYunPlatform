using System;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
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
        private IMisakaConnection _misakaConnection;

        /// <summary>
        /// 消息管理器
        /// </summary>
        private readonly MessageManager _messageManager = new MessageManager();

        /// <summary>
        /// 是否HEX发送
        /// </summary>
        private bool HexSend => ChkHexSend.IsChecked == true;

        /// <summary>
        /// 是否HEX接收
        /// </summary>
        private bool HexReceive => ChkHexReceive.IsChecked == true;

        /// <summary>
        /// 显示消息来源
        /// </summary>
        private bool ShowSource => ChkShowSource.IsChecked == true;

        /// <summary>
        /// 显示时间戳
        /// </summary>
        private bool ShowDate => ChkShowDate.IsChecked == true;

        /// <summary>
        /// 不是关闭而是隐藏窗口
        /// </summary>
        private bool _hideInstead = true;

        /// <summary>
        /// 报告委托
        /// </summary>
        /// <returns></returns>
        private delegate ReportMessage ReportDispatcherDelegate();

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
        /// 总接收字节数
        /// </summary>
        private int _totalReceive;

        /// <summary>
        /// 最后一次接收字节数
        /// </summary>
        private int _lastReceive;

        /// <summary>
        /// 总发送字节数
        /// </summary>
        private int _totalSend;

        /// <summary>
        /// 最后一次发送字节数
        /// </summary>
        private int _lastSend;

        /// <summary>
        /// 计时器
        /// </summary>
        private readonly DispatcherTimer _dispatcherTimer = new DispatcherTimer();

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
            _misakaConnection.ParentWindow = this;
            ReportService.ReportDataAdded += AppendReport;
            InitControl(connection);
        }

        /// <summary>
        /// 在指定位置显示
        /// </summary>
        /// <param name="left"></param>
        /// <param name="top"></param>
        public void ShowAtPosition(double left, double top)
        {
            Left = left;
            Top = top;
            Show();
        }

        /// <summary>
        /// 键盘监听事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EnterHandler(object sender, KeyEventArgs e)
        {
            if (!(sender is TextBox) || e.Key != Key.Enter) return;

            var textBox = (TextBox)sender;

            if (textBox.Name == "TxtRemoteConnPort")
            {
                SwitchConnectStatus(sender, e);
            }
            e.Handled = true;
        }

        /// <summary>
        /// 初始化控件
        /// </summary>
        /// <param name="connection"></param>
        private void InitControl(IMisakaConnection connection)
        {
            CmbConnectedClient.Items.Add(Appconfig.SelectAllConnection);
            CmbConnectedClient.SelectedIndex = 0;

            _dispatcherTimer.Interval = new TimeSpan(5);
            _dispatcherTimer.Tick += UpdateStatusBar;
            _dispatcherTimer.Start();

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
        /// <param name="isFirst"></param>
        private void InitServer(IMisakaConnection connection, bool isFirst = true)
        {
            connection.ParentWindow = this;
            connection.ClientReceivedDataEvent += DispatcherOutPutSocketData;
            var misakaServer = connection as MisakaTcpServer;
            if (misakaServer != null) misakaServer.ClientAccept += RefreshClients;

            if (!isFirst) return;
            TxtLocalAddr.Text = connection.IpAddress;
            TxtLocalPort.Text = $"{connection.Port}";
            ConnType.Content = "Tcp服务器";
            ServerLayer.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// 初始化客户端模式
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="isFirst"></param>
        private void InitClient(IMisakaConnection connection, bool isFirst = true)
        {
            connection.ParentWindow = this;
            connection.ClientReceivedDataEvent += DispatcherOutPutSocketData;

            if (!isFirst) return;
            TxtLocalAddr.Text = connection.IpAddress;
            TxtLocalPort.Text = $"{connection.Port}";
            ConnType.Content = "Tcp客户端";
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
            CmbConnectedClient.Items.Add(Appconfig.SelectAllConnection);
            foreach (var clientName in ((MisakaTcpServer)_misakaConnection).GetClientNameList())
            {
                CmbConnectedClient.Items.Add(clientName);
            }

            CmbConnectedClient.SelectedIndex = CmbConnectedClient.Items.Count - 1;
        }

        /// <summary>
        /// 开启/关闭侦听服务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SwitchServerStatus(object sender, RoutedEventArgs e)
        {
            CheckServerConnection();

            if (!_misakaConnection.IsConnected)
            {
                StartServer();
            }
            else
            {
                StopServer();
            }
        }

        /// <summary>
        /// 开启侦听服务
        /// </summary>
        private void StartServer()
        {
            if (_misakaConnection.Connect(GetLocalIpAddress(), GetLocalPort()))
            {
                BtnStartListening.Content = "停止侦听";
                DispatcherAddReportData(ReportMessageType.Info, "服务器侦听启动");
            }
            else
            {
                DispatcherAddReportData(ReportMessageType.Info, "服务器侦听失败");
                return;
            }

            ChangeServerControlStatus();
        }

        /// <summary>
        /// 关闭侦听服务
        /// </summary>
        private void StopServer()
        {
            if (_misakaConnection.Close())
            {
                _misakaConnection = null;
                BtnStartListening.Content = "开始侦听";
                DispatcherAddReportData(ReportMessageType.Info, "服务器侦听结束");
            }
            else
            {
                DispatcherAddReportData(ReportMessageType.Error, "关闭服务器失败");
                return;
            }

            ChangeServerControlStatus();
        }

        /// <summary>
        /// 更改服务器套接字控件状态
        /// </summary>
        private void ChangeServerControlStatus()
        {
            TxtLocalAddr.IsEnabled = !TxtLocalAddr.IsEnabled;
            TxtLocalPort.IsEnabled = !TxtLocalPort.IsEnabled;
        }

        /// <summary>
        /// 检查服务器连接对象
        /// </summary>
        private void CheckServerConnection()
        {
            if (_misakaConnection != null) return;
            _misakaConnection = new MisakaTcpServer(GetLocalIpAddress(), GetLocalPort())
            {
                ConnectionType = ConnectionItemType.TcpServer
            };
            InitServer(_misakaConnection, false);
        }

        /// <summary>
        /// 输出报告
        /// </summary>
        /// <param name="e"></param>
        private void AppendReport(EventArgs e)
        {

            var reportData = (ReportMessage)Dispatcher.Invoke(DispatcherPriority.Normal, new ReportDispatcherDelegate(ReportService.PopupReport));

            while (reportData != null)
            {
                _messageManager.DispatcherAppendMessage(reportData);

                LabelMsg.Text = reportData.Message;
                var convertFromString = ColorConverter.ConvertFromString(reportData.MessageColor);
                if (convertFromString != null)
                    MsgItem.Foreground = new SolidColorBrush((Color)convertFromString);
                reportData = ReportService.PopupReport();
            }

            TxtReceiveViewer.ScrollToEnd();
        }

        /// <summary>
        /// 添加报告数据调度方法
        /// </summary>
        /// <param name="type"></param>
        /// <param name="str"></param>
        public void DispatcherAddReportData(ReportMessageType type, string str)
        {
            Dispatcher.Invoke(() => AddReportData(type, str));
        }

        /// <summary>
        /// 添加报告数据
        /// </summary>
        /// <param name="type"></param>
        /// <param name="str"></param>
        private void AddReportData(ReportMessageType type, string str)
        {
            ReportService.AddReportMessage(type, str);
        }

        /// <summary>
        /// 设置当前客户端
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetCurrentClient(object sender, RoutedEventArgs e)
        {
            if (!(_misakaConnection is MisakaTcpServer) || CmbConnectedClient.Items.Count <= 0) return;

            var conn = (MisakaTcpServer)_misakaConnection;
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
        private void OutPutSocketData(IMisakaConnection conn)
        {
            var socketBytes = conn.OutPutSocketBytes();

            if (ShowDate || ShowSource)
            {
                TxtReceiveViewer.AppendText("数据：", OutPutDataColor.DefualtColor);
            }
            if (ShowDate)
            {
                TxtReceiveViewer.AppendText($"[{DateTime.Now.ToString(DateDisplayFormat)}]", OutPutDataColor.DateTimeColor);
            }
            if (ShowSource)
            {
                TxtReceiveViewer.AppendText($"[{conn.ConnectionName}]", OutPutDataColor.ConnectionColor);
            }
            if (ShowDate || ShowSource)
            {
                TxtReceiveViewer.AppendText("=>", OutPutDataColor.DefualtColor);
                TxtReceiveViewer.AppendText("\r");
            }
            TxtReceiveViewer.AppendText($"{Globals.ByteArrayToString(socketBytes, HexReceive)}", OutPutDataColor.ReceiveDataColor);
            TxtReceiveViewer.AppendText("\r\n");

            _totalReceive += _lastReceive = socketBytes.Length;

            TxtReceiveViewer.ScrollToEnd();
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Send(object sender, RoutedEventArgs e)
        {
            byte[] sendBytes;
            try
            {
                sendBytes = Globals.StringToByteArray(TxtDataSend.Text, HexSend);
            }
            catch (Exception ex)
            {
                DispatcherAddReportData(ReportMessageType.Error, "非法字符！");
                LogService.Instance.Error("字符串转换失败！", ex);
                MessageBox.Show("文本中含有非法字符！", "错误！", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var count = _misakaConnection.Send(sendBytes);

            if (count <= 0) return;
            _totalSend += sendBytes.Length;
            _lastSend = sendBytes.Length;
        }

        /// <summary>
        /// 尝试连接服务器
        /// </summary>
        private void SwitchConnectStatus(object sender, RoutedEventArgs e)
        {
            CheckClientConnection();

            if (!_misakaConnection.IsConnected)
            {
                ClientConnect();
            }
            else
            {
                ClientDisconnect();
            }
        }

        /// <summary>
        /// 检查客户端连接对象
        /// </summary>
        private void CheckClientConnection()
        {
            if (_misakaConnection != null) return;
            _misakaConnection = new MisakaTcpClient(GetLocalIpAddress(), GetLocalPort()) { ConnectionType = ConnectionItemType.TcpClient };
            InitClient(_misakaConnection, false);
        }

        /// <summary>
        /// 客户端发起连接
        /// </summary>
        private void ClientConnect()
        {
            var misakaClient = (MisakaTcpClient)_misakaConnection;

            if (misakaClient.Connect(GetTargetIpAddress(), GetTargetPort()))
            {
                BtnConnect.Content = "断开连接";
                ReportService.Info("连接服务器成功！");
            }
            else
            {
                ReportService.Error("尝试连接失败！");
                return;
            }

            ChangeClientControlStatus();
        }

        /// <summary>
        /// 关闭远程连接
        /// </summary>
        private void ClientDisconnect()
        {
            if (_misakaConnection.Close())
            {
                BtnConnect.Content = "连接服务器";
                _misakaConnection = null;
                ReportService.Info("断开服务器连接。");
            }
            else
            {
                ReportService.Error("断开连接失败！");
                return;
            }

            ChangeClientControlStatus();
        }

        /// <summary>
        /// 更改客户端连接控件状态
        /// </summary>
        private void ChangeClientControlStatus()
        {
            TxtLocalAddr.IsEnabled = !TxtLocalAddr.IsEnabled;
            TxtLocalPort.IsEnabled = !TxtLocalPort.IsEnabled;
            TxtRemoteConnAddr.IsEnabled = !TxtRemoteConnAddr.IsEnabled;
            TxtRemoteConnPort.IsEnabled = !TxtRemoteConnPort.IsEnabled;
        }

        /// <summary>
        /// 解析本地IP地址
        /// </summary>
        /// <returns></returns>
        private IPAddress GetLocalIpAddress() => IPAddress.Parse(TxtLocalAddr.Text);

        /// <summary>
        /// 解析本地端口号
        /// </summary>
        /// <returns></returns>
        private int GetLocalPort() => int.Parse(TxtLocalPort.Text);

        /// <summary>
        /// 解析目标服务IP地址
        /// </summary>
        /// <returns></returns>
        private IPAddress GetTargetIpAddress() => IPAddress.Parse(TxtRemoteConnAddr.Text);

        /// <summary>
        /// 解析目标服务器端口号
        /// </summary>
        /// <returns></returns>
        private int GetTargetPort() => int.Parse(TxtRemoteConnPort.Text);

        /// <summary>
        /// 点击标签更改勾选框状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LaberForCheckBoxOnMouseLeave(object sender, RoutedEventArgs e)
        {
            if (!(sender is Label)) return;

            var label = (Label)sender;

            foreach (var checkBox in from result in ConfigGrid.Children.OfType<Grid>()
                                     select result.Children.OfType<CheckBox>()
                                     into chks
                                     from checkBox in chks
                                     where checkBox.Name == label.Tag.ToString()
                                     select checkBox)
            {
                checkBox.IsChecked = !checkBox.IsChecked;
                if (checkBox.Name == "ChkFullDateMode")
                {
                    ChkShowDate.IsChecked = ChkFullDateMode.IsChecked;
                }
            }
        }

        /// <summary>
        /// 清空文本框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearTextBox(object sender, RoutedEventArgs e)
        {
            if (!(sender is Button)) return;

            var button = (Button)sender;

            if (button.Name == "ClearSend") TxtDataSend.Clear();

            if (button.Name == "ClearReceive") TxtReceiveViewer.Document.Blocks.Clear();
        }

        /// <summary>
        /// 更新状态栏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateStatusBar(object sender, EventArgs e)
        {
            LabelTotalReceive.Text = $"{_totalReceive}";

            LabelLastReceive.Text = $"{_lastReceive}";

            LabelTotalSend.Text = $"{_totalSend}";

            LabelLastSend.Text = $"{_lastSend}";
        }

        /// <summary>
        /// 打开消息管理器
        /// </summary>
        private void OpenMessageManager(object sender, EventArgs e)
        {
            _messageManager.ShowAtPosition(Left + Width, Top);
        }

        /// <summary>
        /// 清空计数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearCount(object sender, EventArgs e)
        {
            _totalReceive = _lastReceive = _totalSend = _lastSend = 0;
            UpdateStatusBar(sender, e);
        }

        /// <summary>
        /// 关闭事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnClosing(object sender, CancelEventArgs e)
        {
            if (!_hideInstead) return;
            Hide();
            e.Cancel = true;
        }

        public void DoClose()
        {
            _hideInstead = false;
            _misakaConnection?.Close();
            Close();
        }
    }
}
