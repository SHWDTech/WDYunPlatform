using SHWD.Platform.Repository.Entities;
using SHWD.Platform.Repository.IRepository;
using SHWDTech.Platform.Model.Model;

namespace SHWD.Platform.Repository.Repository
{
    /// <summary>
    /// 系统域数据仓库
    /// </summary>
    public class DomainRepository : SysRepository<Domain>, IDomainRepository
    {
        public DomainRepository()
        {
            
        }

        public DomainRepository(RepositoryDbContext dbContext) : base(dbContext)
        {
            
        }
    }
}