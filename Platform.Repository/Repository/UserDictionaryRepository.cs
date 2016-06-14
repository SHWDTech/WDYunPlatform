using SHWD.Platform.Repository.Entities;
using SHWD.Platform.Repository.IRepository;
using SHWDTech.Platform.Model.Model;

namespace SHWD.Platform.Repository.Repository
{
    /// <summary>
    /// 用户自定义词典数据仓库
    /// </summary>
    public class UserDictionaryRepository : SysDomainRepository<UserDictionary>, IUserDictionaryRepository
    {
        public UserDictionaryRepository()
        {
            
        }

        public UserDictionaryRepository(RepositoryDbContext dbContext) : base(dbContext)
        {
            
        }
    }
}