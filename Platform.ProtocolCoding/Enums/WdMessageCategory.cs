using System;

namespace SHWDTech.Platform.ProtocolCoding.Enums
{
    /// <summary>
    /// 队列消息分类
    /// </summary>
    [Serializable]
    public enum WdMessageCategory
    {
        /// <summary>
        /// 指令发送请求
        /// </summary>
        CommandRequest,

        /// <summary>
        /// 指令发送结果
        /// </summary>
        CommandResult
    }
}
