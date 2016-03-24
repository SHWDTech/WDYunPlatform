using System;
using System.ComponentModel;
using MisakaBanZai.Common;
using MisakaBanZai.Enums;
using MisakaBanZai.Services;

namespace MisakaBanZai.Views
{
    /// <summary>
    /// Interaction logic for MessageManager.xaml
    /// </summary>
    public partial class MessageManager
    {
        public MessageManager()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 添加消息信息调用方法
        /// </summary>
        /// <param name="message"></param>
        public void DispatcherAppendMessage(ReportMessage message)
        {
            Dispatcher.Invoke(() => AppendMessage(message));
        }

        /// <summary>
        /// 添加消息信息
        /// </summary>
        /// <param name="message"></param>
        private void AppendMessage(ReportMessage message)
        {
            TxtMessageContainer.AppendText($"[{DateTime.Now.ToString(Appconfig.FullDateFormat)}]", OutPutDataColor.DateTimeColor);
            TxtMessageContainer.AppendText("=>\r", OutPutDataColor.DefualtColor);
            TxtMessageContainer.AppendText($"{message.Message}", message.MessageColor);
            TxtMessageContainer.AppendText("\r\n");
            TxtMessageContainer.ScrollToEnd();
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
        /// 窗口关闭事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnClosing(object sender, CancelEventArgs e)
        {
            Hide();
            e.Cancel = true;
        }
    }
}
