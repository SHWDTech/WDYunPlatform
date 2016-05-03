using SHWDTech.Platform.Model.Enums;
using SHWDTech.Platform.Model.Model;

namespace SHWD.Platform.Repository.IRepository
{
    /// <summary>
    /// 任务数据仓库接口
    /// </summary>
    public interface ICommandTaskRepository : ISysDomainRepository<CommandTask>
    {
        /// <summary>
        /// 更新任务状态
        /// </summary>
        /// <param name="commandTask"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        /// <returns></returns>
        bool UpdateTaskStatus(CommandTask commandTask, TaskStatus status);

        /// <summary>
        /// 更新任务执行状态
        /// </summary>
        /// <param name="commandTask"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        bool UpdateExecuteStatus(CommandTask commandTask, TaskExceteStatus status);
    }
}