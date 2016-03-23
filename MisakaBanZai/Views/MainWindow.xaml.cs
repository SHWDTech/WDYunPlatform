using System;
using System.Windows;
using System.Windows.Controls;
using MisakaBanZai.Enums;
using MisakaBanZai.Models;
using MisakaBanZai.Services;

namespace MisakaBanZai.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            ConnectionManager.ConnectionAddEvent += ConnectionAdded;
        }

        /// <summary>
        /// 创建新连接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateNewConnection(object sender, EventArgs e)
        {
            var selectType = ConnTypeTreeView.SelectedValue as TreeViewItem;
            if (selectType == null)
            {
                MessageBox.Show("请先选择一个连接类型！", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            var createWindow = new CreateTcpConnection(selectType.Tag.ToString());
            createWindow.ShowDialog();
        }

        /// <summary>
        /// 连接添加事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConnectionAdded(object sender, MisakaConnectionEventArgs e)
        {
            AddNewConnection(e.Connection);
        }

        /// <summary>
        /// 添加新连接
        /// </summary>
        /// <param name="connection"></param>
        private void AddNewConnection(IMisakaConnection connection)
        {
            TreeViewItem item = null;
            switch (connection.ConnectionType)
            {
                case ConnectionItemType.TcpServer:
                    item = TcpServerTreeItem;
                    break;
                case ConnectionItemType.TcpClient:
                    item = TcpClientTreeItem;
                    break;
            }

            if (item == null) return;

            AddTreeViewItem(item, connection);
        }

        /// <summary>
        /// 添加属性图项目
        /// </summary>
        /// <param name="treeViewItem"></param>
        /// <param name="connection"></param>
        private void AddTreeViewItem(TreeViewItem treeViewItem, IMisakaConnection connection)
        {
            var label = new Label {Content = connection.ConnectionName};

            treeViewItem.Items.Add(label);
            treeViewItem.ExpandSubtree();
            WindowState = WindowState.Minimized;
            var view = new TcpConnectionView(connection);
            view.Show();
        }
    }
}
