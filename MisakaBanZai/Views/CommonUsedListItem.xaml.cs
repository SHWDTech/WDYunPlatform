using System.Windows;
using System.Windows.Controls;

namespace MisakaBanZai.Views
{
    /// <summary>
    /// CommonUsedListItem.xaml 的交互逻辑
    /// </summary>
    public partial class CommonUsedListItem
    {
        /// <summary>
        /// 是否Hex发送
        /// </summary>
        public bool IsHexSend => ChkHexSend.IsChecked == true;

        /// <summary>
        /// 是否自动发送
        /// </summary>
        public bool IsAutoSend => ChkAutoSend.IsChecked == true;

        public CommonUsedListItem()
        {
            InitializeComponent();
        }

        private void TextBoxGetFocus(object sender, RoutedEventArgs e)
        {
            var box = sender as TextBox;
            if (box == null) return;

            box.Height = double.NaN;
        }

        private void TextBoxLostFocus(object sender, RoutedEventArgs e)
        {
            var box = sender as TextBox;
            if (box == null) return;

            box.Height = 30;
        }
    }
}
