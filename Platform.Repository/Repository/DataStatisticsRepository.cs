using SHWD.Platform.Repository.Entities;
using SHWD.Platform.Repository.IRepository;
using SHWDTech.Platform.Model.Model;

namespace SHWD.Platform.Repository.Repository
{
    public class DataStatisticsRepository : DataRepository<DataStatistics>, IDataStatisticsRepository
    {
        public DataStatisticsRepository()
        {
            
        }

        public DataStatisticsRepository(RepositoryDbContext dbContext) : base(dbContext)
        {

        }
    }
}
