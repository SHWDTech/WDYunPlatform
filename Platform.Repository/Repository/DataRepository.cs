using SHWD.Platform.Repository.IRepository;
using SHWDTech.Platform.Model.IModel;

namespace SHWD.Platform.Repository.Repository
{
    public class DataRepository<T> : Repository<T>, IDataRepository<T> where T : class, IDataModel
    {
        protected DataRepository()
        {

        }

        public override T CreateDefaultModel()
        {
            var model = base.CreateDefaultModel();

            model.Domain = RepositoryContext.CurrentDomain;

            return model;
        }

        public override T ParseModel(string jsonString)
        {
            var model = base.ParseModel(jsonString);
            model.Domain = RepositoryContext.CurrentDomain;

            return model;
        }
    }
}
