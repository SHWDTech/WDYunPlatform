using System.Configuration;
using System.Net;

namespace WdTech_Protocol_AdminTools.Models
{
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

        public static int DefaultTcpBufferSize { get; }

        static AppConfig()
        {
            ServerIpAddress = IPAddress.Parse(ConfigurationManager.AppSettings["ServerAddress"]);

            ServerPort = int.Parse(ConfigurationManager.AppSettings["ServerPort"]);

            DefaultTcpBufferSize = int.Parse(ConfigurationManager.AppSettings["DefaultTcpBufferSize"]);
        }

    }
}
