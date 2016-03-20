using SHWDTech.Platform.Utility;
using System;
using System.Windows;
using System.Windows.Threading;
using WdTech_Protocol_AdminTools.Enums;
using WdTech_Protocol_AdminTools.Models;
using WdTech_Protocol_AdminTools.TcpCore;
using WdTech_Protocol_AdminTools.WorkApp;

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

        public MainWindow()
        {
            InitializeComponent();
            _statusBarTimer.Interval = new TimeSpan(0, 0, 0, 0, 500);
            _statusBarTimer.Tick += UpdateStatusBar;
            _statusBarTimer.Start();
        }

        /// <summary>
        /// 更新状态栏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateStatusBar(object sender, EventArgs e)
        {
            ServerIpAddress.Text = $"{AppConfig.ServerIpAddress}";
            ServerPort.Text = $"{AppConfig.ServerPort}";
            ServerStartTDateTime.Text = !CommunicationServices.IsStart
                                        ? "-"
                                        : $"{CommunicationServices.StartDateTime.ToString(UiElementViewFormat.StartDateFormat)}";
            ServerRunningDateTime.Text = $"{(CommunicationServices.StartDateTime - DateTime.Now).ToString("h'h 'm'm 's's'")}";
        }

        /// <summary>
        /// 切换日期显示格式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SwitchDateDisplayMode(object sender, EventArgs e)
        {
            UiElementViewFormat.StartDateFormat = UiElementViewFormat.StartDateFormat == DateTimeViewFormat.DateTimeWithoutYear
                ? DateTimeViewFormat.DateTimeWithYear
                : DateTimeViewFormat.DateTimeWithoutYear;

            UpdateStatusBar(sender, e);
        }

        /// <summary>
        /// 窗口关闭处理程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                CommunicationServices.Stop();
            }
            catch (Exception ex)
            {
                e.Cancel = true;
                LogService.Instance.Error("通信服务停止失败", ex);
                MessageBox.Show("程序关闭时发生严重错误，请检查错误信息。", "警告", MessageBoxButton.OK);
            }
        }
    }
}