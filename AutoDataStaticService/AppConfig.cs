using System;
using System.Configuration;

namespace AutoDataStaticService
{
    public static class AppConfig
    {
        /// <summary>
        /// 管理员账号
        /// </summary>
        public static readonly string ServerAccount;

        /// <summary>
        /// 默认计算开始时间
        /// </summary>
        public static readonly DateTime DefaultStartDate;

        /// <summary>
        /// 协议数据名称
        /// </summary>
        public static readonly string[] CommandDatas;

        static AppConfig()
        {
            ServerAccount = ConfigurationManager.AppSettings["ServerAccount"];

            DefaultStartDate = DateTime.Parse(ConfigurationManager.AppSettings["DefaultStartDate"]);

            CommandDatas = ConfigurationManager.AppSettings["CommandDatas"].Split(',');
        }
    }
}
