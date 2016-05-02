using System;
using System.Linq;
using Platform.Process.IProcess;
using SHWD.Platform.Repository.Repository;
using SHWDTech.Platform.Model.Enums;
using Task = SHWDTech.Platform.Model.Model.Task;

namespace Platform.Process.Process
{
    /// <summary>
    /// 任务处理程序
    /// </summary>
    public class TaskProcess : ITaskProcess
    {
        /// <summary>
        /// 任务数据仓库
        /// </summary>
        private readonly TaskRepository _taskRepository = new TaskRepository();

        public Task GetTaskByGuid(Guid taskId)
            => _taskRepository.GetModels(task => task.Id == taskId).FirstOrDefault();

        public bool UpdateExecuteStatus(Task task, TaskExceteStatus status)
            => _taskRepository.UpdateExecuteStatus(task, status);
    }
}
