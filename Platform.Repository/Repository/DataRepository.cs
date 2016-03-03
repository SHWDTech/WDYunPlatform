using SHWD.Platform.Repository.IRepository;
using SHWDTech.Platform.Model.IModel;

namespace SHWD.Platform.Repository.Repository
{
    /// <summary>
    /// 数据类型仓库基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DataRepository<T> : Repository<T>, IDataRepository<T> where T : class, IDataModel
    {
        public override T CreateDefaultModel()
        {
            var model = base.CreateDefaultModel();

            model.Domain = ContextLocal.Value.CurrentDomain;

            return model;
        }

        public override T ParseModel(string jsonString)
        {
            var model = base.ParseModel(jsonString);
            model.Domain = ContextLocal.Value.CurrentDomain;

            return model;
        }
    }
}