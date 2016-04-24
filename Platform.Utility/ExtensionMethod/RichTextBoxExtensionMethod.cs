using System;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace SHWDTech.Platform.Utility.ExtensionMethod
{
    public static class RichTextBoxExtensionMethod
    {
        /// <summary>
        /// 追加文本到文本控件
        /// </summary>
        /// <param name="box">文本控件</param>
        /// <param name="text">追加的文本</param>
        /// <param name="color">文本的颜色</param>
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
