using System;
using System.Data.Entity;
using System.Linq;
using SHWDTech.Platform.StorageConstrains.Model;

namespace SHWDTech.Platform.StorageConstrains.Repository
{
    public class GuidRepository<T> : Repository<T>, IGuidRepository<T> where T : class, IGuidModel, new()
    {
        public GuidRepository()
        {
            
        }

        public GuidRepository(DbContext dbContext) : base(dbContext)
        {
            
        }

        public T GetModelById(Guid id)
            => EntitySet.FirstOrDefault(obj => obj.Id == id);

        public T GetModelIncludeById(Guid id, string[] includes)
        {
            var query = includes.Aggregate(EntityQuery, (current, include) => current.Include(include));

            return query.FirstOrDefault(obj => obj.Id == id);
        }
    }
}
