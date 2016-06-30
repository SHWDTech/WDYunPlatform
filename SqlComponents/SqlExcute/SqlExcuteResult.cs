using System;

namespace SqlComponents.SqlExcute
{
    /// <summary>
    /// SQL执行结果
    /// </summary>
    public class SqlExcuteResult
    {
        /// <summary>
        /// 异常
        /// </summary>
        public Exception Exception { get; set; }

        /// <summary>
        /// 执行是否成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 错误码
        /// </summary>
        public int ErrorNumber { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }
    }
}
