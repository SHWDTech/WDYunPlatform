using System;
using System.Collections.Generic;
using System.Linq;
using Platform.Process;
using Platform.Process.Process;
using SHWDTech.Platform.Model.Enums;
using SHWDTech.Platform.Model.Model;

namespace WdTech_Protocol_AdminTools.TcpCore
{
    /// <summary>
    /// 任务管理器
    /// </summary>
    public static class TaskManager
    {
        /// <summary>
        /// 等待处理的任务
        /// </summary>
        private static readonly List<CommandTask> ResponingTasks = new List<CommandTask>();

        /// <summary>
        /// 更新任务状态
        /// </summary>
        /// <param name="taskGuid">任务GUID</param>
        /// <param name="status">任务状态</param>
        public static void UpdateTaskStatus(Guid taskGuid, TaskStatus status)
        {
            var task = GetTask(taskGuid);

            ProcessInvoke.Instance<CommandTaskProcess>().UPdateTaskStatus(task, status);
        }

        /// <summary>
        /// 更新任务执行状态
        /// </summary>
        /// <param name="taskGuid">任务GUID</param>
        /// <param name="exceteStatus">任务执行状态</param>
        public static void UpdateTaskExceteStatus(Guid taskGuid, TaskExceteStatus exceteStatus)
        {
            var task = GetTask(taskGuid);

            ProcessInvoke.Instance<CommandTaskProcess>().UpdateExecuteStatus(task, exceteStatus);
        }

        private static CommandTask GetTask(Guid taskGuid)
        {
            var task = ResponingTasks.FirstOrDefault(obj => obj.Id == taskGuid);

            if (task != null) return task;
            task = ProcessInvoke.Instance<CommandTaskProcess>().GetTaskByGuid(taskGuid);
            ResponingTasks.Add(task);

            return task;
        }
    }
}
