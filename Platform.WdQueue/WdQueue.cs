using System;
using System.Collections.Generic;

namespace Platform.WdQueue
{
    /// <summary>
    /// 消息队列管理
    /// </summary>
    public static class WdQueue
    {
        /// <summary>
        /// 任务集合
        /// </summary>
        private static readonly List<WdTechTask> TaskList = new List<WdTechTask>();

        /// <summary>
        /// 创建一个新的任务
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="messageType"></param>
        /// <returns></returns>
        public static WdTechTask CreateTask(string queueName, Type[] messageType)
        {
            var task = new WdTechTask(queueName, messageType);
            TaskList.Add(task);

            return task;
        }

        /// <summary>
        /// 结束所有任务
        /// </summary>
        public static void EndTasks()
        {
            foreach (var wdTechTask in TaskList)
            {
                wdTechTask.Close();
            }
        }
    }
}
