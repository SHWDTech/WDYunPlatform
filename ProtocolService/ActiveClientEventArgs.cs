using System;
using SHWDTech.Platform.ProtocolService.DataBase;

namespace SHWDTech.Platform.ProtocolService
{
    public class ActiveClientEventArgs : EventArgs
    {
        /// <summary>
        /// 来源客户端
        /// </summary>
        public IActiveClient SourceActiveClient { get; private set; }

        /// <summary>
        /// 包含的异常
        /// </summary>
        public Exception Exception { get; private set; }

        /// <summary>
        /// 协议数据
        /// </summary>
        public IProtocolData ProtocolData { get; set; }

        /// <summary>
        /// 异常的追加描述信息
        /// </summary>
        public string ExceptionMessage { get; }

        public ActiveClientEventArgs(IActiveClient sourceClient)
        {
            SourceActiveClient = sourceClient;
        }

        public ActiveClientEventArgs(IActiveClient sourceClient, Exception exception) : this(sourceClient)
        {
            Exception = exception;
        }

        public ActiveClientEventArgs(IActiveClient sourceClient, Exception exception, string message)
            : this(sourceClient, exception)
        {
            ExceptionMessage = message;
        }
    }
}
