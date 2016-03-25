using MisakaBanZai.Enums;
using MisakaBanZai.Services;

namespace MisakaBanZai.Models
{
    public interface IMisakaConnectionManagerWindow
    {
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
