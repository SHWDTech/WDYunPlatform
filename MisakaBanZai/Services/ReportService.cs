using System;
using System.Collections.Generic;
using System.Linq;
using MisakaBanZai.Enums;

namespace MisakaBanZai.Services
{
    public class ReportService
    {
        /// <summary>
        /// 报告数据列表
        /// </summary>
        private IList<ReportMessage> ReportData { get; } = new List<ReportMessage>();

        /// <summary>
        /// 报告数据添加事件
        /// </summary>
        public event ReportDataAddedEventHandler ReportDataAdded;

        /// <summary>
        /// 添加报告数据
        /// </summary>
        /// <param name="message"></param>
        private void AddReportMessage(ReportMessage message)
        {
            ReportData.Add(message);
            OnReportDataAdded(EventArgs.Empty);
        }

        /// <summary>
        /// 从报告数据中取出最早压入的报告
        /// </summary>
        /// <returns></returns>
        public ReportMessage PopupReport()
        {
            var message = ReportData.FirstOrDefault();
            if (message != null) ReportData.RemoveAt(0);

            return message;
        }

        /// <summary>
        /// 添加报告消息
        /// </summary>
        /// <param name="type"></param>
        /// <param name="message"></param>
        public void AddReportMessage(ReportMessageType type, string message)
        {
            switch (type)
            {
                case ReportMessageType.Info:
                    Info(message);
                    break;
                case ReportMessageType.Error:
                    Error(message);
                    break;
                case ReportMessageType.Warning:
                    Warning(message);
                    break;
                case ReportMessageType.Danger:
                    Danger(message);
                    break;
            }
        }

        /// <summary>
        /// 普通消息
        /// </summary>
        /// <param name="message"></param>
        public void Info(string message)
        {
            var repoMessage = new ReportMessage(ReportMessageType.Info, message);
            AddReportMessage(repoMessage);
        }

        /// <summary>
        /// 错误消息
        /// </summary>
        /// <param name="message"></param>
        public void Error(string message)
        {
            var repoMessage = new ReportMessage(ReportMessageType.Error, message);
            AddReportMessage(repoMessage);
        }

        /// <summary>
        /// 报警消息
        /// </summary>
        /// <param name="message"></param>
        public void Warning(string message)
        {
            var repoMessage = new ReportMessage(ReportMessageType.Warning, message);
            AddReportMessage(repoMessage);
        }

        /// <summary>
        /// 严重错误消息
        /// </summary>
        /// <param name="message"></param>
        public void Danger(string message)
        {
            var repoMessage = new ReportMessage(ReportMessageType.Danger, message);
            AddReportMessage(repoMessage);
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
