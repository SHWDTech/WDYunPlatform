using System.Configuration;
using System.Net;
using WdTech_Protocol_AdminTools.Enums;

namespace WdTech_Protocol_AdminTools.Models
{
    /// <summary>
    /// 系统全局设置
    /// </summary>
    public static class AppConfig
    {
        /// <summary>
        /// 服务器地址
        /// </summary>
        public static IPAddress ServerIpAddress { get; }

        /// <summary>
        /// 服务器端口号
        /// </summary>
        public static int ServerPort { get; }

        /// <summary>
        /// TCP接收Buffer大小
        /// </summary>
        public static int TcpBufferSize { get; }

        /// <summary>
        /// 服务开始时间显示格式
        /// </summary>
        public static string StartDateFormat { get; set; } = DateTimeViewFormat.DateTimeWithoutYear;

        static AppConfig()
        {
            ServerIpAddress = IPAddress.Parse(ConfigurationManager.AppSettings["ServerAddress"]);

            ServerPort = int.Parse(ConfigurationManager.AppSettings["ServerPort"]);

            TcpBufferSize = int.Parse(ConfigurationManager.AppSettings["TcpBufferSize"]);
        }

    }
}
