using System;
using System.Collections.Generic;
using System.Messaging;

namespace SHWDTech.Platform.PlatformServices
{
    public abstract class PlatformService : IPlatformService
    {
        /// <summary>
        /// 服务使用的消息队列
        /// </summary>
        protected MessageQueue ServiceMessageQueue { get; set; }

        /// <summary>
        /// 唯一实例
        /// </summary>
        protected static List<IPlatformService> InstancesDictionary;

        public abstract Guid ServiceGuid { get; }

        public abstract void Start(string path, Type[] formaterTypes);

        public abstract void Stop();

        public abstract void ReStart();

        /// <summary>
        /// 检查消息队列路径
        /// </summary>
        /// <param name="path"></param>
        protected void CheckPath(string path)
        {
            if (!MessageQueue.Exists(path))
            {
                MessageQueue.Create(path);
            }
        } 
    }
}
