using SHWD.Platform.Repository.Entities;
using SHWD.Platform.Repository.IRepository;
using SHWDTech.Platform.Model.Model;

namespace SHWD.Platform.Repository.Repository
{
    public class CommandDataRepository : SysRepository<CommandData>, ICommandDataRepository
    {
        public CommandDataRepository()
        {
            
        }

        public CommandDataRepository(RepositoryDbContext dbContext) : base(dbContext)
        {
            
        }
    }
}
