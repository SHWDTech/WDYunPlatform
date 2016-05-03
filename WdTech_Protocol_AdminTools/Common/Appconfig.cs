﻿using System.Configuration;
using System.Linq;
using System.Net;
using Platform.Process;
using Platform.Process.Process;
using SHWDTech.Platform.Model.Enums;
using WdTech_Protocol_AdminTools.Enums;

namespace WdTech_Protocol_AdminTools.Common
{
    public static class AppConfig
    {
        /// <summary>
        /// TCP接收Buffer大小
        /// </summary>
        public static readonly int TcpBufferSize;

        /// <summary>
        /// 完整时间格式
        /// </summary>
        public const string FullDateFormat = "yyyy-MM-dd HH:mm:ss fff";

        /// <summary>
        /// 短时间格式
        /// </summary>
        public const string ShortDateFormat = "HH:mm:ss fff";

        /// <summary>
        /// 选择全部连接
        /// </summary>
        public const string SelectAllConnection = "选择全部";

        /// <summary>
        /// 服务器地址
        /// </summary>
        public static IPAddress ServerIpAddress { get; }

        /// <summary>
        /// 服务器端口号
        /// </summary>
        public static int ServerPort { get; }

        /// <summary>
        /// 管理员账号
        /// </summary>
        public static readonly string ServerAccount;

        /// <summary>
        /// 指令消息名称
        /// </summary>
        public static readonly string CommandQueue;

        /// <summary>
        /// 服务开始时间显示格式
        /// </summary>
        public static string StartDateFormat { get; set; } = DateTimeViewFormat.DateTimeWithoutYear;

        public static readonly string CommandMessageQueueCategory;

        static AppConfig()
        {
            TcpBufferSize = int.Parse(ConfigurationManager.AppSettings["TcpBufferSize"]);

            ServerIpAddress = IPAddress.Parse(ConfigurationManager.AppSettings["ServerAddress"]);

            ServerPort = int.Parse(ConfigurationManager.AppSettings["ServerPort"]);

            ServerAccount = ConfigurationManager.AppSettings["ServerAccount"];

            var configs = ProcessInvoke.GetInstance<SysConfigProcess>().GetSysConfigsByType(SysConfigType.ProtocolAdminTools);

            CommandQueue = configs.FirstOrDefault(obj => obj.SysConfigName == "CommandMessageQueueName")?.SysConfigValue;

            CommandMessageQueueCategory = ConfigurationManager.AppSettings["CommandMessageQueueCategory"];
        }
    }
}
