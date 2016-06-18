using System.Windows;

namespace SHWDTech.Platform.Utility.ExtensionMethod
{
    public static class WpfWindowExtrensionMethod
    {
        public static void ShowAtPosition(this Window window, double left, double top)
        {
            window.Left = left;
            if (Globals.IsWindows10()) window.Left = window.Left - 14;
            window.Top = top;
            window.Show();
        }
    }
}
