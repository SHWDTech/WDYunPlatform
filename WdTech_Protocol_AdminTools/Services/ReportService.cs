using System;
using System.Collections.Generic;
using System.Linq;
using SHWDTech.Platform.Utility;
using WdTech_Protocol_AdminTools.Enums;

namespace WdTech_Protocol_AdminTools.Services
{
    public class ReportService
    {
        /// <summary>
        /// ReportService 的实例
        /// </summary>
        public static ReportService Instance { get; }= new ReportService();

        /// <summary>
        /// 记录时带上时间戳
        /// </summary>
        public static bool AppendTimeStamp { get; set; } = false;

        /// <summary>
        /// 报告数据
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
        /// <param name="type"></param>
        private void AddReportMessage(string message, ReportMessageType type)
        {
            if (AppendTimeStamp)
            {
                message = $"[{DateTime.Now}]{message}\r\n";
            }

            var reportMessage = new ReportMessage(type, message);
            Instance.ReportData.Add(reportMessage);
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
                case ReportMessageType.Fatal:
                    Fatal(message);
                    break;
            }
        }

        /// <summary>
        /// 普通消息
        /// </summary>
        /// <param name="message"></param>
        public void Info(string message)
        {
            AddReportMessage(message, ReportMessageType.Info);
        }

        /// <summary>
        /// 普通消息并记录异常
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public void Info(string message, Exception ex)
        {
            Info(message);
            LogService.Instance.Info(message, ex);
        }

        /// <summary>
        /// 错误消息
        /// </summary>
        /// <param name="message"></param>
        public void Error(string message)
        {
            AddReportMessage(message, ReportMessageType.Error);
        }

        /// <summary>
        /// 错误消息并记录异常
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public void Error(string message, Exception ex)
        {
            Error(message);
            LogService.Instance.Error(message, ex);
        }

        /// <summary>
        /// 报警消息
        /// </summary>
        /// <param name="message"></param>
        public void Warning(string message)
        {
            AddReportMessage(message, ReportMessageType.Warning);
        }

        /// <summary>
        /// 报警消息并记录日志
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public void Warning(string message, Exception ex)
        {
            Warning(message);
            LogService.Instance.Warn(message, ex);
        }

        /// <summary>
        /// 严重错误消息
        /// </summary>
        /// <param name="message"></param>
        public void Fatal(string message)
        {
            AddReportMessage(message, ReportMessageType.Fatal);
        }

        /// <summary>
        /// 严重错误消息并记录日志
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public void Fatal(string message, Exception ex)
        {
            Fatal(message);
            LogService.Instance.Fatal(message, ex);
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
