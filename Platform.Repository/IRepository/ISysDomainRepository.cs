using SHWDTech.Platform.Model.IModel;

namespace SHWD.Platform.Repository.IRepository
{
    /// <summary>
    /// 带有域信息的系统数据仓库基接口
    /// </summary>
    /// <typeparam name="T">数据仓库对应的模型类型，必须继承自ISysDomainModel</typeparam>
    public interface ISysDomainRepository<T> : ISysRepository<T> where T: class, ISysDomainModel
    {
    }
}
