﻿using System;
using System.Collections.Generic;

namespace SHWDTech.Platform.ProtocolCoding.MessageQueueModel
{
    /// <summary>
    /// 指令消息
    /// </summary>
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
        /// 指令参数
        /// </summary>
        public Dictionary<string, byte[]> Params { get; set; }
    }
}
