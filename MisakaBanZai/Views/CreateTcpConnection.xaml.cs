using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MisakaBanZai.Enums;
using MisakaBanZai.Services;

namespace MisakaBanZai.Views
{
    /// <summary>
    /// Interaction logic for AddTcpServer.xaml
    /// </summary>
    public partial class CreateTcpConnection
    {
        /// <summary>
        /// 连接类型
        /// </summary>
        private readonly string _type;

        private CreateTcpConnection()
        {
            InitializeComponent();
            IpAddressBox.Focus();
        }

        /// <summary>
        /// 键盘监听事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EnterHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }

            if (!(sender is TextBox) || e.Key != Key.Enter) return;

            var textBox = (TextBox)sender;

            if (textBox.Name == "IpAddressBox")
            {
                PortBox.Focus();
            }
            else
            {
                ConfirmCreate(sender, e);
            }
            e.Handled = true;
        }

        /// <summary>
        /// 创建PCT连接
        /// </summary>
        /// <param name="type"></param>
        public CreateTcpConnection(string type) : this()
        {
            switch (type)
            {
                case ConnectionItemType.TcpServer:
                    Title = Title + "服务器";
                    break;
                case ConnectionItemType.TcpClient:
                    Title += "客户端";
                    break;
                default:
                    throw new ArgumentException("未知的TCP连接类型。");
            }

            _type = type;
        }

        /// <summary>
        /// 确认连接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConfirmCreate(object sender, EventArgs e)
        {
            IMisakaConnection connection;
            try
            {
                connection = ConnectionManager.NewMisakaConnection(_type, GetIpAddress(), GetPort());
            }
            catch (Exception)
            {
                MessageBox.Show("无效的IP地址或端口号。", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (connection == null)
            {
                MessageBox.Show("创建连接失败！", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
                IpAddressBox.Focus();
                return;
            }

            ConnectionManager.AddConnection(connection);
            DialogResult = true;
            Close();
        }

        /// <summary>
        /// 获取IP地址
        /// </summary>
        /// <returns></returns>
        private IPAddress GetIpAddress() => IPAddress.Parse(IpAddressBox.Text);

        /// <summary>
        /// 获取端口号
        /// </summary>
        /// <returns></returns>
        private int GetPort() => int.Parse(PortBox.Text);

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Close(object sender, EventArgs e)
        {
            base.Close();
        }
    }
}
