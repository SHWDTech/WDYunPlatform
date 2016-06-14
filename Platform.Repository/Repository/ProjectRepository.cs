using SHWD.Platform.Repository.Entities;
using SHWD.Platform.Repository.IRepository;
using SHWDTech.Platform.Model.Model;

namespace SHWD.Platform.Repository.Repository
{
    /// <summary>
    /// 项目数据仓库
    /// </summary>
    public class ProjectRepository : SysDomainRepository<Project>, IProjectRepository
    {
        public ProjectRepository()
        {
            
        }

        public ProjectRepository(RepositoryDbContext dbContext) : base(dbContext)
        {
            
        }
    }
}