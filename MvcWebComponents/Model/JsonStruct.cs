using System;

namespace MvcWebComponents.Model
{
    /// <summary>
    /// Ajax请求标准返回格式
    /// </summary>
    [Serializable]
    public class JsonStruct
    {
        /// <summary>
        /// 请求的响应
        /// </summary>
        public object Result { get; set; }

        /// <summary>
        /// 响应附带的消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 需要执行POST请求的form名称
        /// </summary>
        public string PostForm { get; set; }

        /// <summary>
        /// 请求的异常信息
        /// </summary>
        public string Exception { get; set; }
    }
}
