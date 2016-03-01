using SHWD.Platform.Repository.IRepository;
using SHWDTech.Platform.Model.IModel;

namespace SHWD.Platform.Repository.Repository
{
    public class SysDomainRepository <T> : SysRepository<T>, ISysDomainRepository<T> where T: class, ISysDomainModel
    {
        public override T CreateDefaultModel()
        {
            var model = base.CreateDefaultModel();
            model.Domain = RepositoryContext.CurrentDomain;

            return model;
        }
    }
}
