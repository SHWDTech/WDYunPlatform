using SHWDTech.Platform.Model.IModel;

namespace SHWD.Platform.Repository.IRepository
{
    public interface ISysDomainRepository<T> : ISysRepository<T> where T: class, ISysDomainModel
    {
    }
}
