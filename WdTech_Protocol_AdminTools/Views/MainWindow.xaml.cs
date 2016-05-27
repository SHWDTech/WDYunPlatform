using SHWDTech.Platform.Utility;
using System;
using System.ComponentModel;
using System.Net;
using System.Windows;
using System.Windows.Threading;
using WdTech_Protocol_AdminTools.Common;
using WdTech_Protocol_AdminTools.Enums;
using WdTech_Protocol_AdminTools.Services;
using WdTech_Protocol_AdminTools.TcpCore;

namespace WdTech_Protocol_AdminTools.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        /// <summary>
        /// 状态栏状态更新计时器
        /// </summary>
        private readonly DispatcherTimer _statusBarTimer = new DispatcherTimer();

        /// <summary>
        /// 服务器IP地址
        /// </summary>
        private IPAddress _serverAddress;

        /// <summary>
        /// 服务器端口
        /// </summary>
        private ushort _serverPort;

        public MainWindow()
        {
            InitializeComponent();

            InitControl();
        }

        /// <summary>
        /// 初始化窗口控件
        /// </summary>
        private void InitControl()
        {
            _statusBarTimer.Interval = new TimeSpan(0, 0, 0, 0, 500);
            _statusBarTimer.Tick += UpdateStatusBar;
            _statusBarTimer.Start();

            TxtServerIpAddress.Text = $"{AppConfig.ServerIpAddress}";
            TxtServerPort.Text = $"{AppConfig.ServerPort}";

            AliveConnection.Text = $"{CommunicationServices.AliveConnection}";


            AdminReportService.Instance.ReportDataAdded += AppendReport;
        }

        /// <summary>
        /// 发送报告文本到界面
        /// </summary>
        /// <param name="e"></param>
        private void AppendReport(EventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var message = AdminReportService.Instance.PopupReport();

                if (message == null) return;

                TxtReport.AppendText($"[{DateTime.Now.ToString(AppConfig.FullDateFormat)}]");
                TxtReport.AppendText(" => ");
                TxtReport.AppendText(message.Message);
                TxtReport.AppendText("\r\n");
                TxtReport.ScrollToEnd();

                if (TxtReport.Text.Length > 3000)
                {
                    TxtReport.Clear();
                }
            });
        }

        /// <summary>
        /// 更新状态栏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateStatusBar(object sender, EventArgs e)
        {
            if (CommunicationServices.IsStart)
            {
                ServerIpAddress.Text = $"{CommunicationServices.ServerIpEndPoint}";
            }
            ServerStartTDateTime.Text = !CommunicationServices.IsStart
                                        ? "-"
                                        : $"{CommunicationServices.StartDateTime.ToString(AppConfig.StartDateFormat)}";
            ServerRunningDateTime.Text = !CommunicationServices.IsStart
                                        ? "-"
                                        : $"{(CommunicationServices.StartDateTime - DateTime.Now).ToString("h'h 'm'm 's's'")}";

            AliveConnection.Text = $"{CommunicationServices.AliveConnection}";
        }

        /// <summary>
        /// 切换日期显示格式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SwitchDateDisplayMode(object sender, EventArgs e)
        {
            AppConfig.StartDateFormat = AppConfig.StartDateFormat == DateTimeViewFormat.DateTimeWithoutYear
                ? DateTimeViewFormat.DateTimeWithYear
                : DateTimeViewFormat.DateTimeWithoutYear;

            UpdateStatusBar(sender, e);
        }

        /// <summary>
        /// 窗口关闭处理程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnClosing(object sender, CancelEventArgs e)
        {
            try
            {
                _statusBarTimer.Stop();
                CommunicationServices.Close();
            }
            catch (Exception ex)
            {
                e.Cancel = true;
                LogService.Instance.Error("通信服务停止失败", ex);
                MessageBox.Show("程序关闭时发生严重错误，请检查错误信息。", "警告", MessageBoxButton.OK);
            }
        }

        /// <summary>
        /// 开始服务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartService(object sender, EventArgs e)
        {
            if (!ParseServerAddress()) return;

            if (CommunicationServices.Start(new IPEndPoint(_serverAddress, _serverPort)))
            {
                SetServerInfoInputStatus(false);
            }
        }

        /// <summary>
        /// 体制服务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StopService(object sender, EventArgs e)
        {
            if (CommunicationServices.Stop())
            {
                SetServerInfoInputStatus(true);
            }
        }

        /// <summary>
        /// 设置服务器信息控件状态
        /// </summary>
        /// <param name="status"></param>
        private void SetServerInfoInputStatus(bool status)
             => TxtServerIpAddress.IsEnabled = TxtServerPort.IsEnabled = status;

        /// <summary>
        /// 获取服务器IP地址和端口号
        /// </summary>
        /// <returns></returns>
        private bool ParseServerAddress()
        {
            if (!IPAddress.TryParse(TxtServerIpAddress.Text, out _serverAddress))
            {
                MessageBox.Show("无效的IP地址！", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (ushort.TryParse(TxtServerPort.Text, out _serverPort)) return true;

            MessageBox.Show("无效的端口号！", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return false;
        }

        /// <summary>
        /// 清空消息记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearTextReport(object sender, RoutedEventArgs e)
        {
            TxtReport.Clear();
        }
    }
}