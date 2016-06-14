using SHWD.Platform.Repository.Entities;
using SHWD.Platform.Repository.IRepository;
using SHWDTech.Platform.Model.Model;

namespace SHWD.Platform.Repository.Repository
{
    /// <summary>
    /// 监测数据仓库
    /// </summary>
    public class MonitorDataRepository : DataRepository<MonitorData>, IMonitorDataRepository
    {
        public MonitorDataRepository()
        {
            
        }

        public MonitorDataRepository(RepositoryDbContext dbContext) : base(dbContext)
        {
            
        }
    }
}