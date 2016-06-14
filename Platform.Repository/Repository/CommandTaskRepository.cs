using SHWD.Platform.Repository.Entities;
using SHWD.Platform.Repository.IRepository;
using SHWDTech.Platform.Model.Enums;
using SHWDTech.Platform.Model.Model;

namespace SHWD.Platform.Repository.Repository
{
    /// <summary>
    /// 任务数据仓库
    /// </summary>
    public class CommandTaskRepository : SysDomainRepository<CommandTask>, ICommandTaskRepository
    {
        public CommandTaskRepository()
        {
            
        }

        public CommandTaskRepository(RepositoryDbContext dbContext) : base(dbContext)
        {
            
        }

        public bool UpdateTaskStatus(CommandTask commandTask, TaskStatus status)
        {
            commandTask.TaskStatus = status;
            return (DbContext.SaveChanges() == 1);
        }

        public bool UpdateExecuteStatus(CommandTask commandTask, TaskExceteStatus status)
        {
            commandTask.ExecuteStatus = status;
            return (DbContext.SaveChanges() == 1);
        }
    }
}