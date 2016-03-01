using SHWD.Platform.Repository.IRepository;
using SHWD.Platform.Repository.Repository;

namespace SHWD.Platform.Repository
{
    /// <summary>
    /// Process调用类
    /// </summary>
    public class ProcessInvoke
    {
        public IRepositoryContext InvokeContext { get; }

        private ProcessInvoke()
        {
            
        }

        public ProcessInvoke(IRepositoryContext context) : this()
        {
            InvokeContext = context;
        }

        public T GetProcess<T>() where T: RepositoryBase, new()
        {
            var process = new T {Invoker =  this};

            return process;
        }
    }
}
