using WdTech_Protocol_AdminTools.Enums;

namespace WdTech_Protocol_AdminTools.WorkApp
{
    /// <summary>
    /// 界面原色显示格式
    /// </summary>
    public class UiElementViewFormat
    {
        /// <summary>
        /// 服务开始时间显示格式
        /// </summary>
        public static string StartDateFormat { get; set; } = DateTimeViewFormat.DateTimeWithoutYear;
    }
}
