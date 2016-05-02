using System;
using System.Collections.Generic;

namespace SHWDTech.Platform.ProtocolCoding.MessageQueueModel
{
    /// <summary>
    /// 指令消息
    /// </summary>
    [Serializable]
    public class CommandMessage
    {
        /// <summary>
        /// 设备ID
        /// </summary>
        public Guid DeviceGuid { get; set; }

        /// <summary>
        /// 指令ID
        /// </summary>
        public Guid CommandGuid { get; set; }

        /// <summary>
        /// 任务ID
        /// </summary>
        public Guid TaskGuid { get; set; }

        /// <summary>
        /// 指令参数集合
        /// </summary>
        public List<CommandParam> Params { get; set; }
    }

    /// <summary>
    /// 指令参数
    /// </summary>
    [Serializable]
    public class CommandParam
    {
        /// <summary>
        /// 参数名称
        /// </summary>
        public string ParamName { get; set; }

        /// <summary>
        /// 参数字节数组
        /// </summary>
        public byte[] ParamBytes { get; set; }
    }
}
