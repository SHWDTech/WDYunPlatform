using System;
using System.Collections.Generic;

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
        /// 消息类型
        /// </summary>
        string MessageType { get; set; }

        /// <summary>
        /// 消息参数列表
        /// </summary>
        List<object> MessageParams { get; set; }

        /// <summary>
        /// 消息指令
        /// </summary>
        string MessageCommand { get; set; }
    }
}
