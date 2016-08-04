﻿using System;
using System.Windows;
using System.Windows.Threading;
using Platform.Process;
using Platform.Process.Process;
using SHWD.Platform.Repository;
using SHWDTech.Platform.Utility;

namespace DeviceUnitTestTools
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

            DbRepository.ConnectionName = "Lampblack_Platform";

            var serverUser = GeneralProcess.GetUserByLoginName("CommnicationServer");

            if (serverUser == null)
            {
                MessageBox.Show("通信管理员账号信息错误，请检查配置！");
                return;
            }

            ProcessInvoke.SetupGlobalRepositoryContext(serverUser, serverUser.Domain);

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
        }

        protected virtual void AppUnhandleExceptionHandler(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            LogService.Instance.Fatal("程序出现未处理异常。", e.Exception);
        }
    }
}