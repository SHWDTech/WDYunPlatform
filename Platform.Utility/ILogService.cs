using System;

namespace SHWDTech.Platform.Utility
{
    /// <summary>
    /// 日志服务接口
    /// </summary>
    public interface ILogService
    {
        /// <summary>
        /// 调试信息
        /// </summary>
        /// <param name="message">错误消息</param>
        void Debug(string message);

        /// <summary>
        /// 调试信息
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="ex">异常</param>
        void Debug(string message, Exception ex);

        /// <summary>
        /// 信息级别日志
        /// </summary>
        /// <param name="message">错误消息</param>
        void Info(string message);

        /// <summary>
        /// 信息级别日志
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="ex">异常</param>
        void Info(string message, Exception ex);



        /// <summary>
        /// 警告级别日志
        /// </summary>
        /// <param name="message">消息</param>
        void Warn(string message);

        /// <summary>
        /// 警告级别日志
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="ex">异常</param>
        void Warn(string message, Exception ex);



        /// <summary>
        /// 错误级别日志
        /// </summary>
        /// <param name="message">消息</param>
        void Error(string message);

        /// <summary>
        /// 错误级别日志
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="ex">异常</param>
        void Error(string message, Exception ex);



        /// <summary>
        /// 严重错误级别日志
        /// </summary>
        /// <param name="message">消息</param>
        void Fatal(string message);

        /// <summary>
        /// 严重错误级别日志
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="ex">异常</param>
        void Fatal(string message, Exception ex);
    }
}
