using System;

namespace SHWDTech.Platform.PlatformServices
{
    /// <summary>
    /// 服务消息接口
    /// </summary>
    public interface IServiceMessage
    {
        /// <summary>
        /// 消息源GUID
        /// </summary>
        Guid SourceServiceGuid { get; }

        /// <summary>
        /// 消息内容JSON对象
        /// </summary>
        string MessageObjectJson { get; }
    }
}
