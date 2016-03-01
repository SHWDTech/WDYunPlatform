using SHWDTech.Platform.Model.IModel;

namespace SHWD.Platform.Repository.IRepository
{
    public interface IDataRepository<T> : IRepository<T> where T : class
    {
    }
}
