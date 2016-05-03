using System.Windows;

namespace WdAdminTools.Views
{
    /// <summary>
    /// Login.xaml 的交互逻辑
    /// </summary>
    public partial class Login
    {
        public Login()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 执行登录操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DoLogin(object sender, RoutedEventArgs e)
        {
            
        }

        /// <summary>
        /// 关闭窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Close(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
