using System;
using System.Net;
using System.Windows;
using System.Windows.Threading;
using SHWDTech.Platform.Utility;
using WdTech_Protocol_AdminTools.Models;
using WdTech_Protocol_AdminTools.TcpCore;

namespace WdTech_Protocol_AdminTools
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionHandler;
            Current.DispatcherUnhandledException += AppUnhandleExceptionHandler;
            CommunicationServices.Start(new IPEndPoint(AppConfig.ServerIpAddress, AppConfig.ServerPort));

            base.OnStartup(e);
        }

        /// <summary>
        /// 未处理异常捕获器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void UnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs e)
        {
            LogService.Instance.Fatal("未处理异常。", (Exception)e.ExceptionObject);
            MessageBox.Show("系统运行出现严重错误！");

            Current.Shutdown();
        }

        protected virtual void AppUnhandleExceptionHandler(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            LogService.Instance.Fatal("未处理异常。", e.Exception);

            Current.Shutdown();
        }
    }
}
