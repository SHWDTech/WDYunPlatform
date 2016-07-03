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
    public class CommandTaskProcess : ProcessBase, ICommandTaskProcess
    {
        public CommandTask GetTaskByGuid(Guid taskId)
        {
            using (var repo = Repo<CommandTaskRepository>())
            {
                return repo.GetModels(task => task.Id == taskId).FirstOrDefault();
            }
        }

        public bool UPdateTaskStatus(CommandTask commandTask, TaskStatus status)
        {
            using (var repo = Repo<CommandTaskRepository>())
            {
                return repo.UpdateTaskStatus(commandTask, status);
            }
        }

        public bool UpdateExecuteStatus(CommandTask commandTask, TaskExceteStatus exceteStatus)
        {
            using (var repo = Repo<CommandTaskRepository>())
            {
                return repo.UpdateExecuteStatus(commandTask, exceteStatus);
            }
        }
    }
}
