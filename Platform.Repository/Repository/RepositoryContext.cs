using SHWD.Platform.Repository.IRepository;
using SHWDTech.Platform.Model.IModel;

namespace SHWD.Platform.Repository.Repository
{
    public class RepositoryContext : IRepositoryContext
    {
        public IUser CurrentUser { get; set; }

        public ISysDomain CurrentDomain { get; set; }
    }
}
