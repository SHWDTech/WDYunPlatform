using System;
using System.Collections.Generic;

namespace Lampblack_Platform.Common
{
    /// <summary>
    /// 油烟系统配置
    /// </summary>
    public class LampblackConfig
    {
        public static void InitConfig(Dictionary<string, object> configs)
        {
            DeviceTypeGuid = (Guid) configs["deviceTypeGuid"];
            FirmwareSetGuid = (Guid) configs["firmwareSetGuid"];
        }

        /// <summary>
        /// 默认设备类型ID
        /// </summary>
        public static Guid DeviceTypeGuid { get; private set; }

        /// <summary>
        /// 默认固件集ID
        /// </summary>
        public static Guid FirmwareSetGuid { get; private set; }
    }
}