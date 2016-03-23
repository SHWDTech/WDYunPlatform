using System;
using System.Collections.Generic;
using System.Linq;

namespace MisakaBanZai.Services
{
    public class ReportService
    {
        private IList<string> ReportData { get; } = new List<string>();

        /// <summary>
        /// 报告数据添加事件
        /// </summary>
        public event ReportDataAddedEventHandler ReportDataAdded;

        /// <summary>
        /// 添加报告数据
        /// </summary>
        /// <param name="report"></param>
        public void AddReportData(string report)
        {
            ReportData.Add(report);
            OnReportDataAdded(EventArgs.Empty);
        }

        /// <summary>
        /// 从报告数据中取出最早压入的报告
        /// </summary>
        /// <returns></returns>
        public string PopupReport()
        {
            var report = ReportData.FirstOrDefault();
            if (report != null) ReportData.RemoveAt(0);

            return report;
        }

        /// <summary>
        /// 添加报告数据时触发
        /// </summary>
        /// <param name="e"></param>
        private void OnReportDataAdded(EventArgs e)
        {
            ReportDataAdded?.Invoke(e);
        }
    }
}
