using SHWDTech.Platform.Model.IModel;

namespace SHWD.Platform.Repository.IRepository
{
    /// <summary>
    /// 数据类型仓库基接口
    /// </summary>
    /// <typeparam name="T">数据仓库对应的模型类型，必须继承自IDataModel</typeparam>
    public interface IDataRepository<T> : IRepository<T> where T : class, IDataModel
    {
    }
}