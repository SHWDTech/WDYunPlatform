using SHWD.Platform.Repository.Entities;
using SHWD.Platform.Repository.IRepository;
using SHWDTech.Platform.Model.Model;

namespace SHWD.Platform.Repository.Repository
{
    /// <summary>
    /// 固件集数据仓库
    /// </summary>
    public class FirmwareSetRepository : SysRepository<FirmwareSet>, IFirmwareSetRepository
    {
        public FirmwareSetRepository()
        {
            
        }

        public FirmwareSetRepository(RepositoryDbContext dbContext) : base(dbContext)
        {
            
        }
    }
}