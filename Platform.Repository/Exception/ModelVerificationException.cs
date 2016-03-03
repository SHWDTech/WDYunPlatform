using System;

namespace SHWD.Platform.Repository.Exception
{
    /// <summary>
    /// 数据模型验证异常
    /// </summary>
    [Serializable]
    public class ModelVerificationException : System.Exception
    {
        /// <summary>
        /// 验证异常对应的属性值
        /// </summary>
        public string ExceptionKey { get; }

        /// <summary>
        /// 创建新的模型验证异常
        /// </summary>
        /// <param name="key">验证异常对应的属性值</param>
        /// <param name="message">异常消息</param>
        public ModelVerificationException(string key, string message) : base(message)
        {
            ExceptionKey = key;
        }
    }
}