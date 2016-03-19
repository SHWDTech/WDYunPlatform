using System;
using System.Reflection;
using log4net;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace SHWDTech.Platform.Utility
{
    public class LogService : ILogService
    {
        /// <summary>
        /// 日志服务实例
        /// </summary>
        public static ILogService Instance => new LogService();

        /// <summary>
        /// 日志提供器
        /// </summary>
        private static ILog Log
        {
            get
            {
                var type = MethodBase.GetCurrentMethod().DeclaringType;
                var log = LogManager.GetLogger(type);

                return log;
            }
        }

        /// <summary>
        /// 设置参数
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public virtual void SetParameter(string name, string value)
        {
            ThreadContext.Properties[name] = value;
        }

        /// <summary>
        /// 移除参数
        /// </summary>
        public virtual void ClearParameter()
        {
            ThreadContext.Properties.Clear();
        }

        /// <summary>
        /// 设置全局参数
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public virtual void SetGlobalParameter(string name, string value)
        {
            GlobalContext.Properties[name] = value;
        }

        /// <summary>
        /// 移除全局参数
        /// </summary>
        /// <param name="name"></param>
        public virtual void RemoveGlobalParameter(string name)
        {
            GlobalContext.Properties.Remove(name);
        }

        /// <summary>
        /// 准备Loger参数
        /// </summary>
        protected virtual void PrepareParameter()
        {

        }

        public void Debug(string message)
        {
            Debug(message, null);
        }

        public void Debug(string message, Exception ex)
        {
            PrepareParameter();
            Log.Debug(message, ex);
            ClearParameter();
        }

        public void Info(string message)
        {
            Info(message, null);
        }

        public void Info(string message, Exception ex)
        {
            PrepareParameter();
            Log.Info(message, ex);
            ClearParameter();
        }

        public void Warn(string message)
        {
            Warn(message, null);
        }

        public void Warn(string message, Exception ex)
        {
            PrepareParameter();
            Log.Warn(message, ex);
            ClearParameter();
        }

        public void Error(string message)
        {
            Error(message, null);
        }

        public void Error(string message, Exception ex)
        {
            PrepareParameter();
            Log.Error(message, ex);
            ClearParameter();
        }

        public void Fatal(string message)
        {
            Fatal(message, null);
        }

        public void Fatal(string message, Exception ex)
        {
            PrepareParameter();
            Log.Fatal(message, ex);
            ClearParameter();
        }
    }
}
