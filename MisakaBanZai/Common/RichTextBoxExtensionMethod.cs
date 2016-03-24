using System;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace MisakaBanZai.Common
{
    public static class RichTextBoxExtensionMethod
    {
        public static void AppendText(this RichTextBox box, string text, string color)
        {
            var brushConverter = new BrushConverter();
            var textRange = new TextRange(box.Document.ContentEnd, box.Document.ContentEnd) { Text = text };

            var colorObj = brushConverter.ConvertFromString(color);
            if (colorObj == null) throw new ArgumentException("错误的颜色值！");
            textRange.ApplyPropertyValue(TextElement.ForegroundProperty, colorObj);
        }
    }
}
