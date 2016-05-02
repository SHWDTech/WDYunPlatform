using SHWDTech.Platform.Model.Enums;
using SHWDTech.Platform.Model.Model;

namespace SHWD.Platform.Repository.IRepository
{
    /// <summary>
    /// 任务数据仓库接口
    /// </summary>
    public interface ITaskRepository : ISysDomainRepository<Task>
    {
        /// <summary>
        /// 更新任务执行状态
        /// </summary>
        /// <param name="task"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        bool UpdateExecuteStatus(Task task, TaskExceteStatus status);
    }
}