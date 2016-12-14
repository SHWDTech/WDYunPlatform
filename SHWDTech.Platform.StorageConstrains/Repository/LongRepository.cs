using System.Data.Entity;
using System.Linq;
using SHWDTech.Platform.StorageConstrains.Model;

namespace SHWDTech.Platform.StorageConstrains.Repository
{
    public class LongRepository<T> : Repository<T>, ILongRepository<T> where T : class, ILongModel, new()
    {
        public LongRepository(DbContext dbContext) : base(dbContext)
        {

        }

        public T GetModelById(long id)
            => EntitySet.FirstOrDefault(obj => obj.Id == id);

        public T GetModelIncludeById(long id, string[] includes)
        {
            var query = includes.Aggregate(EntityQuery, (current, include) => current.Include(include));

            return query.FirstOrDefault(obj => obj.Id == id);
        }
    }
}
