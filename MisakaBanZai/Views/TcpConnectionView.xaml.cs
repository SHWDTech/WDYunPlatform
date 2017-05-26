using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
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
using System.Threading.Tasks;
using System.Timers;
using SHWDTech.Platform.Utility.ExtensionMethod;
using Timer = System.Timers.Timer;

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
        /// 郑爷爷的自动回复
        /// </summary>
        private bool AutoReply => ChkAutoReply.IsChecked == true;

        /// <summary>
        /// 是否添加空行
        /// </summary>
        private bool AddBlankToReceive => ChkAddBlankToReceive.IsChecked == true;

        /// <summary>
        /// 郑爷爷的定时发送
        /// </summary>
        private bool IsRegularSend => ChkRegularSend.IsChecked == true;

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

        private Timer RegularSendTimer { get; set; }

        private TcpConnectionView()
        {
            InitializeComponent();

            foreach (var ipAddress in Globals.GetLocalIpV4AddressStringList())
            {
                TxtLocalAddr.Items.Add(ipAddress);
            }

            RegularSendTimer = new Timer
            {
                Interval = 120000,
                Enabled = true
            };
            RegularSendTimer.Elapsed += RegularSend;
        }

        public TcpConnectionView(IMisakaConnection connection) : this()
        {
            _misakaConnection = connection;
            _misakaConnection.ParentWindow = this;
            ReportService.ReportDataAdded += AppendReport;
            InitControl(connection);
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

            if (textBox.Name == @"TxtRemoteConnPort")
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
            var currentIndex = CmbConnectedClient.SelectedIndex;
            CmbConnectedClient.Items.Clear();
            CmbConnectedClient.Items.Add(Appconfig.SelectAllConnection);

            if (_misakaConnection == null) return;
            foreach (var clientName in ((MisakaTcpServer)_misakaConnection).GetClientNameList())
            {
                CmbConnectedClient.Items.Add(clientName);
            }

            if (currentIndex <= CmbConnectedClient.Items.Count)
            {
                CmbConnectedClient.SelectedIndex = currentIndex;
            }
            else if (CmbConnectedClient.SelectedIndex > 0)
            {
                CmbConnectedClient.SelectedIndex = 0;
            }
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
                Task.Run(() => StartServer());
            }
            else
            {
                Task.Run(() => StopServer());
            }
        }

        /// <summary>
        /// 开启侦听服务
        /// </summary>
        private void StartServer()
        {
            var addressPort = new AddressPort();
            try
            {
                Dispatcher.Invoke(() =>
                {
                    BtnStartListening.IsEnabled = false;
                    BtnStartListening.Content = "正在开始侦听";
                    addressPort = GetLocalAddressPort();
                });
            }
            catch (Exception)
            {
                Dispatcher.Invoke(() =>
                {
                    BtnStartListening.IsEnabled = true;
                    MessageBox.Show("错误的IP地址或端口号！");
                });
                return;
            }

            var result = _misakaConnection.Connect(addressPort.Address, addressPort.Port);

            if (result)
            {
                Dispatcher.Invoke(() =>
                {
                    BtnStartListening.Content = "停止侦听";
                    OnConnectionModefied(_misakaConnection);
                    AddReportData(ReportMessageType.Info, "服务器侦听启动");
                });
            }
            else
            {
                Dispatcher.Invoke(() =>
                {
                    BtnStartListening.Content = "开始侦听";
                    AddReportData(ReportMessageType.Info, "服务器侦听失败");
                });
            }

            Dispatcher.Invoke(() =>
            {
                ChangeServerControlStatus(false);
                BtnStartListening.IsEnabled = true;
            });
        }

        /// <summary>
        /// 关闭侦听服务
        /// </summary>
        private void StopServer()
        {
            Dispatcher.Invoke(() =>
            {
                BtnStartListening.Content = "正在结束侦听";
                BtnStartListening.IsEnabled = false;
            });

            if (_misakaConnection.Close())
            {
                Dispatcher.Invoke(() =>
                {
                    _misakaConnection = null;
                });
            }
            else
            {
                Dispatcher.Invoke(() =>
                {
                    AddReportData(ReportMessageType.Error, "关闭服务器失败");
                });
                return;
            }

            Dispatcher.Invoke(() =>
            {
                ChangeServerControlStatus(true);
                BtnStartListening.IsEnabled = true;
                BtnStartListening.Content = "开始侦听";
            });
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
                TxtReceiveViewer.AppendText($"[{conn.TargetConnectionName}]");
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

            if (Globals.ByteArrayToString(socketBytes, false) == "System Reset\r\n" && AutoReply)
            {
                SendTimeCheck();
            }

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

        /// <summary>
        /// 发送数据
        /// </summary>
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
                Task.Run(() => ClientConnect());
            }
            else
            {
                Task.Run(() => ClientDisconnect());
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
            var addressPort = new AddressPort();
            try
            {
                Dispatcher.Invoke(() =>
                {
                    addressPort = Dispatcher.Invoke(GetTargetAddressPort);
                    BtnConnect.IsEnabled = false;
                    BtnConnect.Content = "正在连接服务器";
                });
            }
            catch (Exception)
            {
                Dispatcher.Invoke(() => MessageBox.Show("错误的IP地址或端口号！"));
                return;
            }

            var result = _misakaConnection.Connect(addressPort.Address, addressPort.Port);

            if (result)
            {
                Dispatcher.Invoke(() =>
                {
                    _misakaConnection.TargetConnectionName = $"{addressPort.Address}:{addressPort.Port}";
                    BtnConnect.Content = "断开连接";
                    OnConnectionModefied(_misakaConnection);
                    ReportService.Info("连接服务器成功！");
                });
            }
            else
            {
                Dispatcher.Invoke(() =>
                {
                    ReportService.Error("尝试连接失败！");
                    BtnConnect.IsEnabled = true;
                    BtnConnect.Content = "连接服务器";
                });
                return;
            }

            Dispatcher.Invoke(() =>
            {
                ChangeClientControlStatus(false);
                BtnConnect.IsEnabled = true;
            });
        }

        /// <summary>
        /// 关闭远程连接
        /// </summary>
        private void ClientDisconnect()
        {
            if (_misakaConnection.Close())
            {
                Dispatcher.Invoke(() =>
                {
                    BtnConnect.Content = "连接服务器";
                    _misakaConnection = null;
                    ReportService.Info("断开服务器连接。");
                });
            }
            else
            {
                Dispatcher.Invoke(() => ReportService.Error("断开连接失败！"));
                return;
            }

            Dispatcher.Invoke(() => ChangeClientControlStatus(true));
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

        /// <summary>
        /// 客户端连接断开
        /// </summary>
        /// <param name="conn"></param>
        private void ClientDisconnected(IMisakaConnection conn)
        {
            _misakaConnection = null;
            DispatcherAddReportData(ReportMessageType.Warning, "客户端连接已经断开！");
            Dispatcher.Invoke(() =>
            {
                ChangeClientControlStatus(true);
                BtnConnect.Content = "连接服务器";
            });

            if (_autoSendThread != null && _autoSendThread.IsAlive)
            {
                _autoSendThread.Abort();
            }
        }

        /// <summary>
        /// 获取本地连接目标地址及端口号
        /// </summary>
        /// <returns></returns>
        private AddressPort GetLocalAddressPort()
        {
            var addressPort = new AddressPort
            {
                Address = GetLocalIpAddress(),
                Port = GetLocalPort()
            };

            return addressPort;
        }

        /// <summary>
        /// 获取目标连接目标地址及端口号
        /// </summary>
        /// <returns></returns>
        private AddressPort GetTargetAddressPort()
        {
            var addressPort = new AddressPort
            {
                Address = GetTargetIpAddress(),
                Port = GetTargetPort()
            };

            return addressPort;
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

        public void PopUp()
        {
            if (!IsVisible)
            {
                Show();
            }

            if (WindowState == WindowState.Minimized)
            {
                WindowState = WindowState.Normal;
            }

            Activate();
            Topmost = true;  // important
            Topmost = false; // important
            Focus();         // important
        }

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
                if (checkBox.Name == @"ChkFullDateMode" && ChkFullDateMode.IsChecked == true)
                {
                    ChkShowDate.IsChecked = true;
                }
                if (checkBox.Name == @"ChkShowDate" && ChkShowDate.IsChecked == false)
                {
                    ChkFullDateMode.IsChecked = false;
                }
                if (checkBox.Name == @"ChkRegularSend" && ChkRegularSend.IsChecked == true)
                {
                    RegularSendTimer.Start();
                }
                else if (checkBox.Name == @"ChkRegularSend" && ChkRegularSend.IsChecked == false)
                {
                    RegularSendTimer.Stop();
                }
            }
        }

        private void RegularSend(object sender, ElapsedEventArgs e)
        {
            var regularSend = Dispatcher.Invoke(() => IsRegularSend);
            if (regularSend && DateTime.Now.Minute >= 6 && DateTime.Now.Minute <= 8)
            {
                SendTimeCheck();
            }
        }

        /// <summary>
        /// 自动发送
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AutoSend(object sender, EventArgs e)
        {
            if (!CheckTextInput())
            {
                ChkAutoSend.IsChecked = false;
                return;
            }
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
        /// 接收框文档变更
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ReceiveTextBoxTextChanged(object sender, EventArgs e)
        {
            if (TxtReceiveViewer.Text.Length > 524288)
            {
                try
                {
                    using (var file = File.Open($"{Directory.GetCurrentDirectory()}\\HistoryFile\\{DateTime.Now:yyyy-MM-dd_HH_mm_ss}.txt", FileMode.OpenOrCreate))
                    {
                        var logs = Encoding.GetEncoding("GB2312").GetBytes(TxtReceiveViewer.Text);
                        file.Write(logs, 0, logs.Length);
                    }
                }
                catch (Exception ex)
                {
                    LogService.Instance.Error("Save Log Failed", ex);
                }
                TxtReceiveViewer.Clear();
            }
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
                if (conn == null || conn.Send(sendBytes) == -1)
                {
                    Dispatcher.Invoke(RestoreAutoSendControls);
                    continue;
                }

                Thread.Sleep(sendInterver);
            }
            // ReSharper disable once FunctionNeverReturns
        }

        /// <summary>
        /// 还原自动发送相关控件状态
        /// </summary>
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
        private bool CheckTextInput()
        {
            int result;
            if (!int.TryParse(AutoSendInterval.Text, out result) || result == 0)
            {
                DispatcherAddReportData(ReportMessageType.Error, "自动发送间隔只能输入大于零的数字！");
                AutoSendInterval.Text = "10000";
                return false;
            }

            return true;
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

            if (button.Name == @"ClearSend") TxtDataSend.Clear();

            if (button.Name == @"ClearReceive") TxtReceiveViewer.Clear();
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

        private void SendTimeCheck()
        {
            var sendContnt = new List<byte> { 0xAC, 0xF1, 0x04, 0x01, 0xC0, 0x00, 0x00, 0x08 };
            var year = DateTime.Now.Year - 2000;
            sendContnt.Add((byte)year);
            sendContnt.Add((byte)DateTime.Now.Month);
            sendContnt.Add((byte)DateTime.Now.Day);
            sendContnt.Add((byte)DateTime.Now.Hour);
            sendContnt.Add((byte)DateTime.Now.Minute);
            sendContnt.Add((byte)DateTime.Now.Second);
            sendContnt.Add((byte)DateTime.Now.DayOfWeek);
            sendContnt.Add(0x00);
            var crc = Globals.GetUsmbcrc16(sendContnt.ToArray(), (ushort)sendContnt.Count);
            var crcBytes = Globals.Uint16ToBytes(crc, false);
            sendContnt.AddRange(crcBytes);
            sendContnt.Add(0xB1);

            _misakaConnection.Send(sendContnt.ToArray());
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
            _messageManager.Hide();
            e.Cancel = true;
        }

        public void DoClose()
        {
            _hideInstead = false;
            _misakaConnection?.Close();
            _messageManager.Close();
            Close();
        }
    }

    /// <summary>
    /// IP地址及端口
    /// </summary>
    public struct AddressPort
    {
        public IPAddress Address { get; set; }

        public int Port { get; set; }
    }
}
