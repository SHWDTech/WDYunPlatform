using SHWD.Platform.Repository.Entities;
using SHWD.Platform.Repository.IRepository;
using SHWDTech.Platform.Model.Model;

namespace SHWD.Platform.Repository.Repository
{
    public class ProtocolStructureRepository : SysRepository<ProtocolStructure>, IProtocolStructureRepository
    {
        public ProtocolStructureRepository()
        {
            
        }

        public ProtocolStructureRepository(RepositoryDbContext dbContext) : base(dbContext)
        {
            
        }
    }
}