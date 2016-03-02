using SHWD.Platform.Repository.IRepository;
using SHWDTech.Platform.Model.IModel;

namespace SHWD.Platform.Repository.Repository
{
    /// <summary>
    /// 任务数据仓库
    /// </summary>
    public class TaskRepository : SysDomainRepository<ITask>, ITaskRepository
    {
    }
}
