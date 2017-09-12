using SHWD.Platform.Repository.Entities;
using SHWD.Platform.Repository.IRepository;
using SHWDTech.Platform.Model.Model;

namespace SHWD.Platform.Repository.Repository
{
    public class PlatformRepository : SysDomainRepository<PlatformAccess>, IPlatfromAccessRepository
    {
        public PlatformRepository()
        {

        }

        public PlatformRepository(RepositoryDbContext dbContext) : base(dbContext)
        {

        }
    }
}
