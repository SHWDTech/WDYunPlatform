using System;
using System.Messaging;

namespace Platform.WdQueue
{
    public class WdTechTask : MessageQueue
    {
        public WdTechTask(string queueName, Type[] messageType) : base(queueName)
        {
            Formatter = new XmlMessageFormatter(messageType);
        }
    }
}
