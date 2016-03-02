using SHWD.Platform.Repository.IRepository;
using SHWDTech.Platform.Model.IModel;

namespace SHWD.Platform.Repository.Repository
{
    /// <summary>
    /// 用户自定义词典数据仓库
    /// </summary>
    internal class UserDictionaryRepository : SysDomainRepository<IUserDictionary>, IUserDictionaryRepository
    {
    }
}
