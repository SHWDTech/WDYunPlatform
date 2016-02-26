using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using SHWDTech.Platform.Common.Enum;
using SHWDTech.Platform.Utility;

namespace SHWDTech.Platform.Common.Component
{
    [Serializable]
    public class WdtException : Exception
    {
        public WdtException()
        {

        }

        public WdtException(Exception sex)
            : base(sex.Message, sex)
        {
            ExceptionType = PlatformExceptionType.UnKnownError;
        }

        public WdtException(string exceptionMessage)
            : base(exceptionMessage)
        {
            ExceptionType = PlatformExceptionType.UnKnownError;
        }

        public WdtException(PlatformExceptionType exceptionType)
        {
            ExceptionType = exceptionType;
        }

        public WdtException(string errorCode, string exceptionMessage)
            : this(PlatformExceptionType.CustomError, errorCode, exceptionMessage, null)
        {

        }

        public WdtException(string errorCode, string exceptionMessage, bool isSeriousLevel, params object[] processParam)
            : this(PlatformExceptionType.CustomError, errorCode, exceptionMessage, null, isSeriousLevel, processParam)
        {

        }

        public WdtException(PlatformExceptionType exceptionType, string errorCode, string exceptionMessage)
            : this(exceptionType, errorCode, exceptionMessage, null)
        {

        }

        public WdtException(PlatformExceptionType exceptionType, string errorCode, string exceptionMessage, object tag)
            : this(exceptionType, errorCode, exceptionMessage, tag, false, null)
        {

        }

        public WdtException(PlatformExceptionType exceptionType, string errorCode, string exceptionMessage, object tag,
            bool isSeriousLevel, params object[] processParam)
            : base(exceptionMessage)
        {
            ExceptionType = exceptionType;
            ErrorCode = errorCode;
            Tag = tag;
            NeedInterrupt = false;

            IsSeriousLevel = isSeriousLevel;
            ProcessParam = processParam;
        }

        protected WdtException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            ExceptionType = (PlatformExceptionType)info.GetInt32("ExceptionType");
            ErrorCode = info.GetString("ErrorCode");
            Tag = info.GetString("Tag");
            NeedInterrupt = info.GetBoolean("NeedInterrupt");
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue("Tag", JsonConvert.SerializeObject(Tag));
            info.AddValue("ErrorCode", ErrorCode);
            info.AddValue("ExceptionType", ExceptionType);
            info.AddValue("NeedInterrupt", NeedInterrupt);
        }

        private string _message;

        private string _exceptionId;


        public object Tag { get; set; }

        /// <summary>
        /// 标记是否严重级别的异常，默认不是，严重级别的异常错误，返回到前台会被替换成公共友好的消息
        /// </summary>
        public bool IsSeriousLevel { get; set; }

        /// <summary>
        /// 过程参数，用于记录日志
        /// </summary>
        public object[] ProcessParam { get; set; }

        /// <summary>
        /// 自定义消息，会在严重级别低的条件下显示
        /// </summary>
        public string CustomMessage { get; set; }

        public bool NeedInterrupt { get; set; }

        public string ErrorCode { get; set; }

        public override string Message
        {
            get
            {
                try
                {
                    if (_message == null)
                        _message = this.GetMessage(false);
                }
                catch (Exception)
                {
                    _message = base.Message;
                }

                return _message;
            }
        }

        public string OriMessage
            => base.Message;

        public WdtException AddTag(object tag)
        {
            Tag = tag;
            return this;
        }

        public PlatformExceptionType ExceptionType { get; set; }

        public string ExceptionId
        {
            get
            {
                if (string.IsNullOrEmpty(_exceptionId))
                {
                    _exceptionId = string.Concat(DateTime.Now.ToString("HHmmss"), "-", Globals.Random(4));
                }

                return _exceptionId;
            }
        }


        public static WdtException Instance(Exception ex, bool isSeriousLevel, string errorCode, string msg,
            params object[] processData)
        {
            var wdtex = ex as WdtException;
            if (wdtex?.IsSeriousLevel == isSeriousLevel && string.IsNullOrWhiteSpace(errorCode) && string.IsNullOrWhiteSpace(msg) && processData == null)
                return wdtex;

            var dtex = ex != null ? new WdtException(ex) : new WdtException();

            dtex.CustomMessage = msg;
            dtex.ErrorCode = errorCode;
            dtex.ExceptionType = PlatformExceptionType.CustomError;
            dtex.IsSeriousLevel = isSeriousLevel;
            dtex.ProcessParam = processData;

            return dtex;
        }

        #region 友好提示异常
        public static WdtException Info(Exception ex)
            => Instance(ex, false, null, null);

        public static WdtException Info(Exception ex, string msg)
            => Instance(ex, false, null, msg);

        public static WdtException Info(Exception ex, string errorCode, string msg)
            => Instance(ex, false, errorCode, msg);

        public static WdtException Info(Exception ex, string errorCode, string msg, params object[] processData)
            => Instance(ex, false, errorCode, msg, processData);

        public static WdtException Info(string msg)
            => Instance(null, false, null, msg);

        public static WdtException Info(string errorCode, string msg)
            => Instance(null, false, errorCode, msg);

        public static WdtException Info(string errorCode, string msg, params object[] processData)
            => Instance(null, false, errorCode, msg, processData);

        #endregion

        #region 严重错误提示异常

        public static WdtException Error(Exception ex)
            => Instance(ex, true, null, null);

        public static WdtException Error(string msg)
            => Instance(new Exception(msg), true, null, null);

        public static WdtException Error(Exception ex, string errorCode)
            => Instance(ex, true, errorCode, null);

        public static WdtException Error(Exception ex, string errorCode, string msg)
            => Instance(ex, true, errorCode, msg);

        public static WdtException Error(Exception ex, string errorCode, string msg, params object[] processData)
            => Instance(ex, true, errorCode, msg, processData);

        public static WdtException Error(string errorCode, string msg)
            => Instance(null, true, errorCode, msg);

        public static WdtException Error(string errorCode, string msg, params object[] processData)
            => Instance(null, true, errorCode, msg, processData);

        #endregion
    }
}
