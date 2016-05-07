using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MisakaBanZai.Enums;
using MisakaBanZai.Models;
using MisakaBanZai.Services;
using SHWDTech.Platform.Utility;

namespace MisakaBanZai.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        /// <summary>
        /// 连接对象窗口
        /// </summary>
        private readonly Dictionary<Label, IMisakaConnectionManagerWindow> _connectionWindows = new Dictionary<Label, IMisakaConnectionManagerWindow>();

        public MainWindow()
        {
            InitializeComponent();
            ConnectionManager.ConnectionAddEvent += ConnectionAdded;
            var desktopWorkingArea = SystemParameters.WorkArea;
            Top = (desktopWorkingArea.Bottom - Height) / 2;

            LocalAddr.Text = Globals.GetLocalIpAddressString();
        }

        /// <summary>
        /// 创建新连接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateNewConnection(object sender, RoutedEventArgs e)
        {
            if (ConnTypeTreeView.SelectedValue is Label) return;

            var selectType = ConnTypeTreeView.SelectedValue as TreeViewItem;
            if (selectType == null)
            {
                MessageBox.Show("请先选择一个连接类型！", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            ConnectionManager.NewMisakaConnection($"{selectType.Tag}");

            e.Handled = true;
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
            var label = new Label { Content = connection.ConnectionName, Tag = "ConnectionItem" };

            treeViewItem.Items.Add(label);
            if (!treeViewItem.IsExpanded)
            {
                treeViewItem.ExpandSubtree();
            }
            label.MouseDoubleClick += ViewConnectionWindow;
            var view = new TcpConnectionView(connection);
            _connectionWindows.Add(label, connection.ParentWindow);
            view.ConnectionModefied += OnConnectionModefied;
            view.ShowAtPosition(Left + Width, Top);
        }

        /// <summary>
        /// 显示连接窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ViewConnectionWindow(object sender, MouseButtonEventArgs e)
        {
            if (!(sender is Label)) return;

            var label = (Label)sender;
            var connWindow = _connectionWindows[label];

            connWindow.PopUp();

            e.Handled = true;
        }

        /// <summary>
        /// 移除连接对象
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoveTreeViewItem(object sender, EventArgs e)
        {
            var treeViewItem = ConnTypeTreeView.SelectedItem;
            if (treeViewItem == null)
            {
                MessageBox.Show("请先选择一个连接！", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (!(treeViewItem is Label)) return;

            var label = (Label)treeViewItem;

            if (label.Tag.ToString() != "ConnectionItem" || !_connectionWindows.ContainsKey(label)) return;

            var window = _connectionWindows[label];

            _connectionWindows.Remove(label);

            Dispatcher.Invoke(() => window.DoClose());

            ConnectionManager.ConnectionRemove(label.Content.ToString());

            ((TreeViewItem)label.Parent).Items.Remove(treeViewItem);
        }

        /// <summary>
        /// 连接改变时触发
        /// </summary>
        /// <param name="conn"></param>
        private void OnConnectionModefied(IMisakaConnection conn)
        {
            if (!_connectionWindows.ContainsValue(conn.ParentWindow)) return;
            var connection = _connectionWindows.FirstOrDefault(obj => obj.Value == conn.ParentWindow);
            connection.Key.Content = Dispatcher.Invoke(() => connection.Value.GetConnectionName());
        }

        /// <summary>
        /// 关闭事件
        /// </summary>
        private void OnClosing(object sender, EventArgs e)
        {
            foreach (var misakaConnectionManagerWindow in _connectionWindows)
            {
                Dispatcher.Invoke(() => misakaConnectionManagerWindow.Value.DoClose());
            }

            Process.GetCurrentProcess().Kill();
        }
    }
}
