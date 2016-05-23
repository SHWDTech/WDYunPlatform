using System;
using System.Collections.Generic;
using System.Messaging;

namespace SHWDTech.Platform.PlatformServices
{
    /// <summary>
    /// 平台服务接口
    /// </summary>
    public interface IPlatformService
    {
        /// <summary>
        /// 服务GUID
        /// </summary>
        Guid ServiceGuid { get; }

        /// <summary>
        /// 初始化服务
        /// </summary>
        /// <param name="path"></param>
        /// <param name="formaterTypes"></param>
        void InitService(string path, List<Type> formaterTypes);

        /// <summary>
        /// 启动服务
        /// </summary>
        void Start();

        /// <summary>
        /// 停止服务
        /// </summary>
        void Stop();

        /// <summary>
        /// 重启服务
        /// </summary>
        void ReStart();

        void SendMessage(object message);

        void SendMessage(object message, MessageQueueTransaction transaction);

        void SendMessage(object message, MessageQueueTransactionType transationType);

        void SendMessage(object message, string label);

        void SendMessage(object message, string label, MessageQueueTransaction transaction);

        void SendMessage(object message, string label, MessageQueueTransactionType transationType);
    }
}
