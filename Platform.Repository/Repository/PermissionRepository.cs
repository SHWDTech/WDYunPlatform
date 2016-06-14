using SHWD.Platform.Repository.Entities;
using SHWD.Platform.Repository.IRepository;
using SHWDTech.Platform.Model.Model;

namespace SHWD.Platform.Repository.Repository
{
    /// <summary>
    /// 系统权限数据仓库
    /// </summary>
    public class PermissionRepository : SysDomainRepository<Permission>, IPermissionRepository
    {
        public PermissionRepository()
        {
            
        }

        public PermissionRepository(RepositoryDbContext dbContext) : base(dbContext)
        {
            
        }
    }
}