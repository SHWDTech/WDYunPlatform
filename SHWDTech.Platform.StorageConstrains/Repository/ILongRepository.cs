using SHWDTech.Platform.StorageConstrains.Model;

namespace SHWDTech.Platform.StorageConstrains.Repository
{
    public interface ILongRepository<T> : IRepository<T> where T : class, ILongKey, IModel, new()
    {
        /// <summary>
        /// 根据ID获取指定数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T GetModelById(long id);

        /// <summary>
        /// 根据ID获取包含指定导航属性的指定数据
        /// </summary>
        /// <param name="id"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        T GetModelIncludeById(long id, string[] includes);
    }
}
