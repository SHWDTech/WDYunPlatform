using System;
using System.Linq;
using Platform.Process.IProcess;
using SHWD.Platform.Repository.Repository;
using SHWDTech.Platform.Model.Enums;
using SHWDTech.Platform.Model.Model;

namespace Platform.Process.Process
{
    /// <summary>
    /// 任务处理程序
    /// </summary>
    public class CommandTaskProcess : ICommandTaskProcess
    {
        /// <summary>
        /// 任务数据仓库
        /// </summary>
        private readonly CommandTaskRepository _commandTaskRepository = new CommandTaskRepository();

        public CommandTask GetTaskByGuid(Guid taskId)
            => _commandTaskRepository.GetModels(task => task.Id == taskId).FirstOrDefault();

        public bool UPdateTaskStatus(CommandTask commandTask, TaskStatus status)
            => _commandTaskRepository.UpdateTaskStatus(commandTask, status);

        public bool UpdateExecuteStatus(CommandTask commandTask, TaskExceteStatus exceteStatus)
            => _commandTaskRepository.UpdateExecuteStatus(commandTask, exceteStatus);
    }
}
