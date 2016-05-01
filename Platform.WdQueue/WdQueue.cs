using System;
using System.Collections.Generic;

namespace Platform.WdQueue
{
    /// <summary>
    /// 消息队列管理
    /// </summary>
    public static class WdQueue
    {
        private static readonly List<WdTechTask> TaskList = new List<WdTechTask>();

        public static WdTechTask CreateTask(string queueName, Type[] messageType)
        {
            var task = new WdTechTask(queueName, messageType);
            TaskList.Add(task);

            return task;
        }

        public static void EndTasks()
        {
            foreach (var wdTechTask in TaskList)
            {
                wdTechTask.StopTask();
            }
        }
    }
}
