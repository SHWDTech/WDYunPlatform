using System;
using System.Collections.Generic;
using System.Messaging;
using System.Threading.Tasks;
using SHWDTech.Platform.Utility;
using SHWDTech.Platform.ServiceHandler;

namespace SHWDTech.Platform.PlatformServices
{
    public abstract class PlatformService
    {
        /// <summary>
        /// 服务使用的消息队列
        /// </summary>
        protected MessageQueue ServiceMessageQueue { get; } = new MessageQueue();

        private static readonly Guid ServiceId = Guid.Empty;

        /// <summary>
        /// 服务是否在运行中
        /// </summary>
        public static bool IsRuning { get; private set; }

        public virtual void InitService(string path, List<Type> formaterTypes)
        {
            ServiceMessageQueue.Path = path;
            formaterTypes.Add(typeof(IServiceMessage));
            ServiceMessageQueue.Formatter = new XmlMessageFormatter(formaterTypes.ToArray());
        }

        public virtual void Start()
        {
            ServiceMessageQueue.PeekCompleted += MessagePeekCompleted;
            IsRuning = true;
            ServiceMessageQueue.BeginPeek();
        }

        public virtual void Stop()
        {
            ServiceMessageQueue.PeekCompleted -= MessagePeekCompleted;
            IsRuning = false;
        }

        public virtual void ReStart()
        {
            Stop();
            Start();
        }

        /// <summary>
        /// 检查消息队列路径
        /// </summary>
        /// <param name="path"></param>
        protected static void CheckPath(string path)
        {
            if (!MessageQueue.Exists(path))
            {
                MessageQueue.Create(path);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="asyncResult"></param>
        protected virtual void MessagePeekCompleted(object source, PeekCompletedEventArgs asyncResult)
        {
            var messageQueue = (MessageQueue)source;

            var message = messageQueue.EndPeek(asyncResult.AsyncResult);

            if (!(message.Body is IServiceMessage)) return;

            var messageContent = (IServiceMessage)message.Body;

            if (messageContent.SourceServiceGuid != ServiceId) return;

            Task.Run(() => ProcessMessage(messageContent));

            if (IsRuning)
            {
                messageQueue.BeginPeek();
            }
        }

        /// <summary>
        /// 处理服务消息
        /// </summary>
        /// <param name="message"></param>
        protected virtual void ProcessMessage(IServiceMessage message)
            => UnityFactory.Resolve<IServiceHandler>(message.MessageCommand).Execute(message.MessageParams, message.MessageType);
    }
}
