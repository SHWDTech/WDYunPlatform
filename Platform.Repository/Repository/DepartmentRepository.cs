using SHWD.Platform.Repository.Entities;
using SHWD.Platform.Repository.IRepository;
using SHWDTech.Platform.Model.Model;

namespace SHWD.Platform.Repository.Repository
{
    /// <summary>
    /// 系统部门数据仓库
    /// </summary>
    public class DepartmentRepository : SysDomainRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository()
        {

        }

        public DepartmentRepository(RepositoryDbContext dbContext) : base(dbContext)
        {

        }
    }
}
