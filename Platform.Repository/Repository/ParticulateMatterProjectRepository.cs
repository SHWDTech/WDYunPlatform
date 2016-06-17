using SHWD.Platform.Repository.Entities;
using SHWD.Platform.Repository.IRepository;
using SHWDTech.Platform.Model.Model;

namespace SHWD.Platform.Repository.Repository
{
    /// <summary>
    /// 项目数据仓库
    /// </summary>
    public class ParticulateMatterProjectRepository : SysDomainRepository<ParticulateMatterProject>, IParticulateMatterProjectRepository
    {
        public ParticulateMatterProjectRepository()
        {
            
        }

        public ParticulateMatterProjectRepository(RepositoryDbContext dbContext) : base(dbContext)
        {
            
        }
    }
}