using System.Collections.Generic;
using System.Linq;
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

        public WdRole CreateDefaultModel()
        {
            var model = base.CreateDefaultModel();
            model.Status = RoleStatus.Enabled;

            return model;
        }

        public List<WdRole> GetPermissions()
            => DbContext.Set<WdRole>().Include("Permissions").ToList();
    }
}