using SHWD.Platform.Repository.Entities;
using SHWD.Platform.Repository.IRepository;
using SHWDTech.Platform.Model.Enums;
using SHWDTech.Platform.Model.Model;

namespace SHWD.Platform.Repository.Repository
{
    /// <summary>
    /// 系统角色数据仓库
    /// </summary>
    public class RoleRepository : SysDomainRepository<WdRole>, IRoleRepository
    {
        public RoleRepository()
        {
            
        }

        public RoleRepository(RepositoryDbContext dbContext) : base(dbContext)
        {
            
        }

        public new static WdRole CreateDefaultModel()
        {
            var model = SysDomainRepository<WdRole>.CreateDefaultModel();
            model.Status = RoleStatus.Enabled;

            return model;
        }
    }
}