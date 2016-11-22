using System;
using System.Data.Entity;
using System.Linq;
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
        public RepositoryDbContext DbContext { get; private set; }

        public IRepositoryContext RepositoryContext { get; set; }

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
            var repo = new T {DbContext = DbContext, ContextLocal = RepositoryContext };
            repo.InitEntitySet();
            return repo;
        }

        public T Invoke<T>() where T : ProcessBase, new()
        {
            var process = new T {RepositoryContext = RepositoryContext};
            return process;
        }

        protected int Commit()
            => DbContext.SaveChanges();

        public void RenewDbContext()
        {
            if(HasUnsaedChanges()) throw new InvalidOperationException("数据库上下文存在未提交的操作！");

            DbContext = string.IsNullOrWhiteSpace(DbRepository.ConnectionString)
                    ? new RepositoryDbContext()
                    : new RepositoryDbContext(DbRepository.ConnectionString);
        }

        /// <summary>
        /// 当前数据上下文是否有未提交的操作
        /// </summary>
        /// <returns></returns>
        private bool HasUnsaedChanges()
            => DbContext.ChangeTracker.Entries().Any(obj => obj.State == EntityState.Added
                                                         || obj.State == EntityState.Modified
                                                         || obj.State == EntityState.Deleted);
    }
}
