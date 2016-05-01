using System;
using System.Messaging;

namespace Platform.WdQueue
{
    public class WdTechTask
    {
        public WdTechTask(string queueName, Type[] messageType)
        {
            _taskMessageQueue = new MessageQueue(queueName, QueueAccessMode.Receive)
            {
                Formatter = new XmlMessageFormatter(messageType)
            };

            _taskMessageQueue.ReceiveCompleted += OnReceiveCompleted;
        }

        private readonly MessageQueue _taskMessageQueue;

        /// <summary>
        /// 开始任务
        /// </summary>
        public void BeginReceive(TimeSpan timeout, object stateObject, AsyncCallback callback)
        {
            _taskMessageQueue.BeginReceive(timeout, stateObject, callback);
        }

        /// <summary>
        /// 完成异步接收操作
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public Message EndReceive(IAsyncResult result) => _taskMessageQueue.EndReceive(result);

        public void StopTask()
        {
            _taskMessageQueue.Close();
        }

        /// <summary>
        /// 从队列中移除消息后发生
        /// </summary>
        public event ReceiveCompletedEventHandler ReceiveCompleted;

        /// <summary>
        /// 消息接收事件
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void OnReceiveCompleted(object source, ReceiveCompletedEventArgs e)
        {
            ReceiveCompleted?.Invoke(source, e);
        }
    }
}
