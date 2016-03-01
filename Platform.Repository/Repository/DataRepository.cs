using SHWD.Platform.Repository.IRepository;
using SHWDTech.Platform.Model.IModel;

namespace SHWD.Platform.Repository.Repository
{
    public class DataRepository<T> : Repository<T>, IDataRepository<T> where T : class, IDataModel
    {

    }
}
