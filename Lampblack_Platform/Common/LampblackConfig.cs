using System;
using System.Collections.Generic;
using System.Configuration;
using StackExchange.Redis;

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
            LoginName = ConfigurationManager.AppSettings["LoginName"];
            District = ConfigurationManager.AppSettings["District"];
            RedisDbObject = ConnectionMultiplexer.Connect("139.224.105.103").GetDatabase();
        }

        /// <summary>
        /// 默认设备类型ID
        /// </summary>
        public static Guid DeviceTypeGuid { get; private set; }

        /// <summary>
        /// 默认固件集ID
        /// </summary>
        public static Guid FirmwareSetGuid { get; private set; }

        /// <summary>
        /// 指定登录用户
        /// </summary>
        public static string LoginName { get; private set; } = string.Empty;

        /// <summary>
        /// 所属区县
        /// </summary>
        public static string District { get; private set; } = string.Empty;

        /// <summary>
        /// Redis对象
        /// </summary>
        public static IDatabase RedisDbObject { get; private set; }
    }
}