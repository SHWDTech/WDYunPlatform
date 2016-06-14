using SHWD.Platform.Repository.Entities;
using SHWD.Platform.Repository.IRepository;
using SHWDTech.Platform.Model.Model;

namespace SHWD.Platform.Repository.Repository
{
    /// <summary>
    /// 用户配置数据仓库
    /// </summary>
    public class UserConfigRepository : SysDomainRepository<UserConfig>, IUserConfigRepository
    {
        public UserConfigRepository()
        {
            
        }

        public UserConfigRepository(RepositoryDbContext dbContext) : base(dbContext)
        {
            
        }
    }
}