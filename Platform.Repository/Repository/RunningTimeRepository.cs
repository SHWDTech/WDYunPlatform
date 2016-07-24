using SHWD.Platform.Repository.Entities;
using SHWD.Platform.Repository.IRepository;
using SHWDTech.Platform.Model.Model;

namespace SHWD.Platform.Repository.Repository
{
    /// <summary>
    /// 运行时间模型
    /// </summary>
    public class RunningTimeRepository : DataRepository<RunningTime>, IRunningTimeRepository
    {
        public RunningTimeRepository()
        {

        }

        public RunningTimeRepository(RepositoryDbContext dbContext) : base(dbContext)
        {

        }
    }
}
