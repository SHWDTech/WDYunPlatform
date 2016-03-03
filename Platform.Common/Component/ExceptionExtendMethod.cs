using Newtonsoft.Json;
using SHWDTech.Platform.Common.Interface;
using SHWDTech.Platform.Common.Model;
using SHWDTech.Platform.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace SHWDTech.Platform.Common.Component
{
    public static class ExceptionExtendMethod
    {
        #region 扩展Exception / void

        public static WdtException Info(this Exception ex, string msg)
            => WdtException.Info(ex, null, msg);

        public static WdtException Info(this Exception ex, string errorCode, string msg)
            => WdtException.Info(ex, errorCode, msg);

        public static WdtException Info(this Exception ex, string errorCode, string msg, params object[] processData)
            => WdtException.Info(ex, errorCode, msg, processData);

        /// <summary>
        /// 输出严重异常
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>

        public static WdtException Error(this Exception ex)
            => WdtException.Error(ex);

        public static WdtException Error(this Exception ex, string msg)
            => WdtException.Error(ex, null, msg);

        public static WdtException Error(this Exception ex, string errorCode, string msg)
            => WdtException.Error(ex, errorCode, msg);

        public static WdtException Error(this Exception ex, string errorCode, string msg, params object[] processData)
            => WdtException.Error(ex, errorCode, msg, processData);

        public static string GetMessage(this Exception ex, bool writeLog = true, bool includeDebugMessage = false)
            => UnityFactory.Resolve<IErrorHandle>().GetMessage(ex, writeLog, includeDebugMessage);

        public static WdtMessage GetMessage(this Exception ex)
        {
            var wdtMsg = UnityFactory.Resolve<IErrorHandle>().GetWdtMessage(ex);

            return wdtMsg;
        }

        public static string GetErrorJson(this Exception ex)
            => UnityFactory.Resolve<IErrorHandle>().GetErrorJson(ex);

        public static object GetExceptionObject(this Exception ex)
            => UnityFactory.Resolve<IErrorHandle>().GetExceptionObject(ex);

        public static IEnumerable<Exception> GetExceptionList(this Exception ex)
            => UnityFactory.Resolve<IErrorHandle>().GetExceptionList(ex);

        /// <summary>
        /// 输出到日志中的消息
        /// </summary>
        /// <returns></returns>
        public static string ToLogMessage(this Exception ex)
        {
            if (ex is WdtException)
            {
                var tsex = ex as WdtException;

                var msg = new StringBuilder();
                msg.AppendLine();
                msg.AppendLine();
                msg.AppendFormat("异常ID: {0} ", tsex.ExceptionId);
                msg.AppendLine();

                msg.AppendFormat("异常消息: {0}", tsex.OriMessage);
                msg.AppendLine();

                if (tsex.ProcessParam != null && tsex.ProcessParam.Length > 0)
                {
                    msg.AppendFormat("过程参数: ");

                    for (int i = 0; i < tsex.ProcessParam.Length; i++)
                    {
                        var o = tsex.ProcessParam[i];

                        msg.AppendFormat("{{{0}}} => type:{1}  |  data:{2}",
                                i,
                                o.GetType(),
                                JsonConvert.SerializeObject(o)
                            );

                        msg.AppendLine();
                    }
                }

                if (string.IsNullOrEmpty(tsex.ErrorCode) == false)
                {
                    msg.AppendFormat("错误码: {0}", tsex.ErrorCode);
                    msg.AppendLine();
                }

                msg.AppendFormat("严重与否: {0}", tsex.IsSeriousLevel);
                msg.AppendLine();

                msg.AppendFormat("自定义消息: {0}", tsex.CustomMessage);
                msg.AppendLine();

                msg.AppendFormat("异常堆栈: {0}", ex.StackTrace);
                msg.AppendLine();

                return msg.ToString();
            }
            else
            {
                var msg = new StringBuilder();
                msg.AppendLine();
                msg.AppendLine();
                msg.AppendLine();

                msg.AppendFormat("异常消息: {0}", ex.Message);
                msg.AppendLine();

                msg.AppendFormat("异常堆栈: {0}", ex.StackTrace);
                msg.AppendLine();

                return msg.ToString();
            }
        }

        #endregion 扩展Exception / void
    }
}