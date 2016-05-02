using SHWD.Platform.Repository.IRepository;
using SHWDTech.Platform.Model.Enums;
using SHWDTech.Platform.Model.Model;

namespace SHWD.Platform.Repository.Repository
{
    /// <summary>
    /// 任务数据仓库
    /// </summary>
    public class TaskRepository : SysDomainRepository<Task>, ITaskRepository
    {
        public bool UpdateExecuteStatus(Task task, TaskExceteStatus status)
        {
            task.ExecuteStatus = status;
            return (DbContext.SaveChanges() == 1);
        }
    }
}