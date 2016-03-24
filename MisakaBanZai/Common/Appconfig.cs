using System.Configuration;

namespace MisakaBanZai.Common
{
    public static class Appconfig
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

        static Appconfig()
        {
            TcpBufferSize = int.Parse(ConfigurationManager.AppSettings["TcpBufferSize"]);
        }
    }
}
