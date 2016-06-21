using SHWD.Platform.Repository.Entities;
using SHWD.Platform.Repository.IRepository;
using SHWDTech.Platform.Model.Model;

namespace SHWD.Platform.Repository.Repository
{
    /// <summary>
    /// 模块数据仓库
    /// </summary>
    public class ModuleRepository : SysDomainRepository<Module>, IModuleRepository
    {
        public ModuleRepository()
        {
            
        }

        public ModuleRepository(RepositoryDbContext dbContext) : base(dbContext)
        {
            
        }
    }
}