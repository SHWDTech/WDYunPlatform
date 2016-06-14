using SHWD.Platform.Repository.Entities;
using SHWD.Platform.Repository.IRepository;
using SHWDTech.Platform.Model.Model;

namespace SHWD.Platform.Repository.Repository
{
    /// <summary>
    /// 指令定义数据仓库
    /// </summary>
    public class CommandDefinitionRepository : SysRepository<CommandDefinition>, ICommandDefinitionRepository
    {
        public CommandDefinitionRepository()
        {
            
        }

        public CommandDefinitionRepository(RepositoryDbContext dbContext) : base(dbContext)
        {
            
        }
    }
}
