using System;
using System.Collections.Generic;
using SHWDTech.Platform.Common.Enum;
using SHWDTech.Platform.Utility;

namespace SHWDTech.Platform.Common.Model
{
    /// <summary>
    /// 记录异常信息的简单类，便于在后台任务处理时传递运行过程中出现的异常
    /// 当异常信息中存在子异常时，同样在本类中的ChildMessage中体现
    /// 只反映WdtException的信息，常规的Exception不会提取到本实例中
    /// 在调试模式下，所有Exception的信息都会提取出来
    /// </summary>
    [Serializable]
    public class WdtMessage
    {
        /// <summary>
        /// 消息类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 消息编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 消息内容
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 消息标签
        /// </summary>
        public object Tag { get; set; }

        /// <summary>
        /// 子信息对象
        /// </summary>
        public List<WdtMessage> ChildMessage { get; set; }

        public override string ToString() => (Globals.IsNullOrEmpty(Code) ? string.Empty : string.Concat(Code, "::")) + Message;

        public static WdtMessage NewMessage(string type, string code, string message, object tag) => new WdtMessage
        {
            Type = type,
            Code = code,
            Message = message,
            Tag = tag
        };

        public static WdtMessage Error(string code, string message) 
            => NewMessage(WdtMessageType.Error, code, message, null);

        public static WdtMessage Error(string code, string message, object tag)
            => NewMessage(WdtMessageType.Error, code, message, tag);

        public static WdtMessage Warn(string code, string message)
            => NewMessage(WdtMessageType.Warn, code, message, null);

        public static WdtMessage Info(string code, string message)
            => NewMessage(WdtMessageType.Infomation, code, message, null);
    }
}
