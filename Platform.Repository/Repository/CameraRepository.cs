using SHWD.Platform.Repository.Entities;
using SHWD.Platform.Repository.IRepository;
using SHWDTech.Platform.Model.Model;

namespace SHWD.Platform.Repository.Repository
{
    /// <summary>
    /// 摄像头数据仓库
    /// </summary>
    public class CameraRepository : SysDomainRepository<Camera>, ICameraRepository
    {
        public CameraRepository()
        {
            
        }

        public CameraRepository(RepositoryDbContext dbContext) : base(dbContext)
        {
            
        }
    }
}