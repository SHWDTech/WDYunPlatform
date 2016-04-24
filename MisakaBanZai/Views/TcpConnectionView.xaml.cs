using System;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Threading;
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
using SHWDTech.Platform.Utility.Enum;

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

        public event ConnectionModefiedEventHandler ConnectionModefied;

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
        /// 是否添加空行
        /// </summary>
        private bool AddBlankToReceive => ChkAddBlankToReceive.IsChecked == true;

        /// <summary>
        /// 自动发送线程
        /// </summary>
        private Thread _autoSendThread;

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
        /// 时间显示格式
        /// </summary>
        private string DateDisplayFormat => ChkFullDateMode.IsChecked == true
                                                ? Appconfig.FullDateFormat
                                                : Appconfig.ShortDateFormat;

        public ReportService ReportService { get; set; } = new ReportService();

        private TcpConnectionView()
        {
            InitializeComponent();

            foreach (var ipAddress in Globals.GetLocalIpV4AddressStringList())
            {
                TxtLocalAddr.Items.Add(ipAddress);
            }
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

            InitConnection(connection);

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
        /// 初始化连接对象
        /// </summary>
        /// <param name="connection"></param>
        private void InitConnection(IMisakaConnection connection)
        {
            connection.ParentWindow = this;
            connection.ClientReceivedDataEvent += DispatcherOutPutSocketData;
            connection.DataSendEvent += DispatcherUpdateSendData;

            var misakaServer = connection as MisakaTcpServer;
            if (misakaServer != null)
            {
                misakaServer.ClientAccept += RefreshClients;
                connection.ClientDisconnectEvent += ServerDisconnected;
            }
            else
            {
                connection.ClientDisconnectEvent += ClientDisconnected;
            }
        }

        /// <summary>
        /// 初始化服务器模式
        /// </summary>
        /// <param name="connection"></param>
        private void InitServer(IMisakaConnection connection)
        {
            TxtLocalAddr.SelectedItem = connection.IpAddress;
            TxtLocalPort.Text = $"{connection.Port}";
            Title = "Tcp服务器";
            ConnType.Content = "Tcp服务器";
            ServerLayer.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// 初始化客户端模式
        /// </summary>
        /// <param name="connection"></param>
        private void InitClient(IMisakaConnection connection)
        {
            TxtLocalAddr.SelectedItem = connection.IpAddress;
            TxtLocalPort.Text = $"{connection.Port}";
            Title = "Tcp客户端";
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
        /// 服务器所属客户端连接断开刷新列表
        /// </summary>
        public void OnServerClientDisconnect()
        {
            RefreshClients(this, new EventArgs());
        }

        /// <summary>
        /// 刷新已连接客户端列表
        /// </summary>
        private void RefreshClients()
        {
            CmbConnectedClient.Items.Clear();
            CmbConnectedClient.Items.Add(Appconfig.SelectAllConnection);

            if (_misakaConnection != null)
            {
                foreach (var clientName in ((MisakaTcpServer)_misakaConnection).GetClientNameList())
                {
                    CmbConnectedClient.Items.Add(clientName);
                }
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
                OnConnectionModefied(_misakaConnection);
                DispatcherAddReportData(ReportMessageType.Info, "服务器侦听启动");
            }
            else
            {
                DispatcherAddReportData(ReportMessageType.Info, "服务器侦听失败");
                return;
            }

            ChangeServerControlStatus(false);
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
            }
            else
            {
                DispatcherAddReportData(ReportMessageType.Error, "关闭服务器失败");
                return;
            }

            ChangeServerControlStatus(true);
        }

        /// <summary>
        /// 更改服务器套接字控件状态
        /// </summary>
        private void ChangeServerControlStatus(bool status)
        {
            TxtLocalAddr.IsEnabled = status;
            TxtLocalPort.IsEnabled = status;
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
            InitConnection(_misakaConnection);
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
                TxtReceiveViewer.AppendText("数据：");
            }
            if (ShowDate)
            {
                TxtReceiveViewer.AppendText($"[{DateTime.Now.ToString(DateDisplayFormat)}]");
            }
            if (ShowSource)
            {
                TxtReceiveViewer.AppendText($"[{conn.ConnectionName}]");
            }
            if (ShowDate || ShowSource)
            {
                TxtReceiveViewer.AppendText("=>");
                TxtReceiveViewer.AppendText("\r");
            }
            TxtReceiveViewer.AppendText($"{Globals.ByteArrayToString(socketBytes, HexReceive)}");
            TxtReceiveViewer.AppendText("\r\n");

            if (AddBlankToReceive)
            {
                TxtReceiveViewer.AppendText("\r\n");
            }

            _totalReceive += _lastReceive = socketBytes.Length;

            TxtReceiveViewer.ScrollToEnd();

            UpdateStatusBar();
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Send(object sender, RoutedEventArgs e)
        {
            Send();
        }

        private void Send()
        {
            var sendBytes = GetSendBytes();

            if (sendBytes == null)
            {
                DispatcherAddReportData(ReportMessageType.Error, "文本中含有非法字符！");
                MessageBox.Show("文本中含有非法字符！", "错误！", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (sendBytes.Length == 0)
            {
                MessageBox.Show("没有可发送的文本！", "消息！", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (_misakaConnection.Send(sendBytes) == 0)
            {
                ReportService.Error("发送失败！");
            }
        }

        /// <summary>
        /// 跨线程调用更新方法
        /// </summary>
        /// <param name="count"></param>
        private void DispatcherUpdateSendData(int count) => Dispatcher.Invoke(() => UpdateSendData(count));

        /// <summary>
        /// 更新发送数据
        /// </summary>
        /// <param name="count"></param>
        private void UpdateSendData(int count)
        {
            _totalSend += count;
            _lastSend = count;

            UpdateStatusBar();
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
            InitConnection(_misakaConnection);
        }

        /// <summary>
        /// 客户端发起连接
        /// </summary>
        private void ClientConnect()
        {
            if (_misakaConnection.Connect(GetTargetIpAddress(), GetTargetPort()))
            {
                BtnConnect.Content = "断开连接";
                ReportService.Info("连接服务器成功！");
                OnConnectionModefied(_misakaConnection);
            }
            else
            {
                ReportService.Error("尝试连接失败！");
                return;
            }

            ChangeClientControlStatus(false);
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

            ChangeClientControlStatus(true);
        }

        /// <summary>
        /// 更改客户端连接控件状态
        /// </summary>
        private void ChangeClientControlStatus(bool status)
        {
            TxtLocalAddr.IsEnabled = status;
            TxtLocalPort.IsEnabled = status;
            TxtRemoteConnAddr.IsEnabled = status;
            TxtRemoteConnPort.IsEnabled = status;
            ChkAutoSend.IsChecked = false;
        }

        /// <summary>
        /// 服务器连接断开
        /// </summary>
        /// <param name="conn"></param>
        private void ServerDisconnected(IMisakaConnection conn)
        {
            DispatcherAddReportData(ReportMessageType.Warning, "服务器连接已经断开！");
            Dispatcher.Invoke(() => ChangeServerControlStatus(true));

            if (_autoSendThread != null && _autoSendThread.IsAlive)
            {
                _autoSendThread.Abort();
            }
        }

        private void ClientDisconnected(IMisakaConnection conn)
        {
            _misakaConnection = null;
            DispatcherAddReportData(ReportMessageType.Warning, "客户端连接已经断开！");
            Dispatcher.Invoke(() => ChangeClientControlStatus(true));
            Dispatcher.Invoke(() => BtnConnect.Content = "连接服务器");

            if (_autoSendThread != null && _autoSendThread.IsAlive)
            {
                _autoSendThread.Abort();
            }
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
        /// 当连接信息更改时触发
        /// </summary>
        /// <param name="conn"></param>
        private void OnConnectionModefied(IMisakaConnection conn)
        {
            ConnectionModefied?.Invoke(conn);
        }

        public string GetConnectionName() => _misakaConnection.ConnectionName;

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
                if (checkBox.Name == "ChkFullDateMode" && ChkFullDateMode.IsChecked == true)
                {
                    ChkShowDate.IsChecked = true;
                }
            }
        }

        /// <summary>
        /// 自动发送
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AutoSend(object sender, EventArgs e)
        {
            var interval = int.Parse(AutoSendInterval.Text);
            _autoSendThread = new Thread(() => DoAutoSend(interval));
            _autoSendThread.Start();
            TxtDataSend.IsEnabled = false;
            AutoSendInterval.IsEnabled = false;
        }

        /// <summary>
        /// 停止自动发送
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StopAutoSend(object sender, EventArgs e)
        {
            _autoSendThread.Abort();
            TxtDataSend.IsEnabled = true;
            AutoSendInterval.IsEnabled = true;
        }

        /// <summary>
        /// Receives the text box text changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ReceiveTextBoxTextChanged(object sender, EventArgs e)
        {
            if (TxtReceiveViewer.Text.Length > 10000)
                TxtReceiveViewer.Clear();
        }

        /// <summary>
        /// 执行自动发送
        /// </summary>
        /// <param name="sendInterver"></param>
        private void DoAutoSend(int sendInterver)
        {
            var sendBytes = Dispatcher.Invoke(GetSendBytes);
            if (sendBytes == null)
            {
                DispatcherAddReportData(ReportMessageType.Error, "文本中含有非法字符！");
                MessageBox.Show("文本中含有非法字符！", "错误！", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var conn = Dispatcher.Invoke(GetCurrentConnection);

            if (sendBytes.Length == 0)
            {
                MessageBox.Show("没有可发送的文本！", "消息！", MessageBoxButton.OK, MessageBoxImage.Information);
                Dispatcher.Invoke(RestoreAutoSendControls);
                return;
            }

            while (true)
            {
                if (conn == null)
                {
                    Dispatcher.Invoke(RestoreAutoSendControls);
                    continue;
                }

                conn.Send(sendBytes);

                Thread.Sleep(sendInterver);
            }
            // ReSharper disable once FunctionNeverReturns
        }

        private void RestoreAutoSendControls()
        {
            TxtDataSend.IsEnabled = true;
            AutoSendInterval.IsEnabled = true;
            ChkAutoSend.IsChecked = false;
        }

        /// <summary>
        /// 获取发送数据
        /// </summary>
        /// <returns></returns>
        private byte[] GetSendBytes() => Globals.StringToByteArray(TxtDataSend.Text, HexSend);

        /// <summary>
        /// 获取连接对象
        /// </summary>
        /// <returns></returns>
        private IMisakaConnection GetCurrentConnection() => _misakaConnection;

        /// <summary>
        /// 检查自动发送间隔输入内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckTextInput(object sender, EventArgs e)
        {
            if (!(sender is TextBox)) return;

            var textBox = (TextBox)sender;

            int result;
            if (!int.TryParse(textBox.Text, out result) || result == 0)
            {
                textBox.Text = "10000";
                MessageBox.Show("自动发送间隔只能输入大于零的数字！", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
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

            if (button.Name == "ClearReceive") TxtReceiveViewer.Clear();
        }

        /// <summary>
        /// 更新状态栏
        /// </summary>
        private void UpdateStatusBar()
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
            UpdateStatusBar();
        }

        /// <summary>
        /// 打开常用指令窗口
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OpenCommonUsed(object sender, EventArgs e)
        {
            var window = new CommonlyUsed();
            window.Show();
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
