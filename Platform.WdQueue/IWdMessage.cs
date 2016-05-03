namespace Platform.WdQueue
{
    /// <summary>
    /// 队列消息接口
    /// </summary>
    public interface IWdMessage
    {
        /// <summary>
        /// 消息分类
        /// </summary>
        string MessageCategory { get; set; }

        /// <summary>
        /// 消息对象JSON字符串
        /// </summary>
        string MessageObjectJson { get; set; }
    }
}
