using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace SHWDTech.Platform.StorageConstrains.Repository
{
    public class RepositoryBase : IRepositoryBase
    {
        public bool AutoCommit { get; set; }

        public DbContext DbContext { get; set; }

        public Database DataBase => DbContext.Database;

        public DbContextConfiguration Configuration => DbContext.Configuration;
    }
}
