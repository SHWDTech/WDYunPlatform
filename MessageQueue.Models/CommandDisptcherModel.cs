using System;

namespace Platform.MessageQueue.Models
{
    public class CommandDisptcherModel
    {
        /// <summary>
        /// 设备ID
        /// </summary>
        public Guid DeviceGuid { get; set; }

        /// <summary>
        /// 指令ID
        /// </summary>
        public Guid CommandGuid { get; set; }
    }
}
