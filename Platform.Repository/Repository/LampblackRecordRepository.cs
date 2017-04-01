using SHWD.Platform.Repository.Entities;
using SHWDTech.Platform.Model.Model;

namespace SHWD.Platform.Repository.Repository
{
    public class LampblackRecordRepository : DataRepository<LampblackRecord>
    {
        public LampblackRecordRepository()
        {

        }

        public LampblackRecordRepository(RepositoryDbContext dbContext) : base(dbContext)
        {

        }
    }
}
