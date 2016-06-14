using SHWD.Platform.Repository.Entities;
using SHWD.Platform.Repository.IRepository;
using SHWDTech.Platform.Model.Model;

namespace SHWD.Platform.Repository.Repository
{
    public class ProtocolCommandRepository : SysRepository<ProtocolCommand>, IProtocolCommandRepository
    {
        public ProtocolCommandRepository()
        {
            
        }

        public ProtocolCommandRepository(RepositoryDbContext dbContext) : base(dbContext)
        {
            
        }
    }
}
