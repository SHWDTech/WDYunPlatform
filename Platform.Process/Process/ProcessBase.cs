using Platform.Process.IProcess;
using SHWD.Platform.Repository;
using SHWD.Platform.Repository.Entities;
using SHWD.Platform.Repository.IRepository;
using SHWD.Platform.Repository.Repository;

namespace Platform.Process.Process
{
    /// <summary>
    /// 处理程序基类
    /// </summary>
    public class ProcessBase : IProcessBase
    {
        public RepositoryDbContext DbContext { get; }

        public ProcessBase()
        {
            DbContext = string.IsNullOrWhiteSpace(DbRepository.ConnectionString)
                    ? new RepositoryDbContext()
                    : new RepositoryDbContext(DbRepository.ConnectionString);
        }

        protected ProcessBase(string connString) : this()
        {
            DbContext = new RepositoryDbContext(connString);
        }

        protected ProcessBase(RepositoryDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public T Repo<T>() where T : RepositoryBase, IRepository, new()
        {
            var repo = new T {DbContext = DbContext};
            repo.InitEntitySet();
            return repo;
        }

        protected int Commit()
            => DbContext.SaveChanges();
    }
}
