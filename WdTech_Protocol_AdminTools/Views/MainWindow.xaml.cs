using System;
using System.Windows.Threading;
using WdTech_Protocol_AdminTools.Models;
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
            ServerStartTDateTime.Text = CommunicationServices.IsStart
                                        ?"-"
                                        : $"{CommunicationServices.StartDateTime.ToString("yyyy-MM-dd HH:mm:ss")}";
            ServerNowDateTime.Text = $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}";
            ServerRunningDateTime.Text = $"{(CommunicationServices.StartDateTime - DateTime.Now).ToString("h'h 'm'm 's's'")}";
        }
    }
}