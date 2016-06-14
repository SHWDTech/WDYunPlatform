using SHWD.Platform.Repository.Entities;
using SHWD.Platform.Repository.IRepository;
using SHWDTech.Platform.Model.Model;

namespace SHWD.Platform.Repository.Repository
{
    /// <summary>
    /// 报警信息数据仓库
    /// </summary>
    public class AlarmRepository : DataRepository<Alarm>, IAlarmRepository
    {
        public AlarmRepository()
        {
            
        }

        public AlarmRepository(RepositoryDbContext dbContext) : base(dbContext)
        {
            
        }
    }
}