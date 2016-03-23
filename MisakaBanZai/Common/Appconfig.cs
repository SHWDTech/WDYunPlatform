using System.Configuration;

namespace MisakaBanZai.Common
{
    public static class Appconfig
    {
        /// <summary>
        /// TCP接收Buffer大小
        /// </summary>
        public static int TcpBufferSize { get; set; }

        static Appconfig()
        {
            TcpBufferSize = int.Parse(ConfigurationManager.AppSettings["TcpBufferSize"]);
        }
    }
}
