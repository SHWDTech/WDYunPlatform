using MisakaBanZai.Services;
using SHWDTech.Platform.Utility;
using SHWDTech.Platform.Utility.Enum;

namespace MisakaBanZai.Models
{
    /// <summary>
    /// TCP连接管理窗口
    /// </summary>
    public interface IMisakaConnectionManagerWindow
    {
        /// <summary>
        /// 连接变动事件
        /// </summary>
        event ConnectionModefiedEventHandler ConnectionModefied;

        /// <summary>
        /// 报告服务
        /// </summary>
        ReportService ReportService { get; set; }

        /// <summary>
        /// 添加报告数据
        /// </summary>
        /// <param name="type"></param>
        /// <param name="str"></param>
        void DispatcherAddReportData(ReportMessageType type, string str);

        /// <summary>
        /// 获取连接名称
        /// </summary>
        /// <returns></returns>
        string GetConnectionName();

        /// <summary>
        /// 关闭窗口
        /// </summary>
        void DoClose();
    }
}
