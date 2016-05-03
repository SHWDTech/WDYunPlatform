using System;

namespace Platform.WdQueue
{
    /// <summary>
    /// 队列消息
    /// </summary>
    [Serializable]
    public class WdMessage : IWdMessage
    {
        /// <summary>
        /// 消息分类
        /// </summary>
        public string MessageCategory { get; set; }

        /// <summary>
        /// 消息对象JSON字符串
        /// </summary>
        public string MessageObjectJson { get; set; }
    }
}
