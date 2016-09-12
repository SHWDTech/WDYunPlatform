using System;
using System.Linq;
using System.Linq.Expressions;
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
        public static Expression<Func<UserDictionary, bool>> Filter { get; set; }

        public UserDictionaryRepository()
        {
            
        }

        public UserDictionaryRepository(RepositoryDbContext dbContext) : base(dbContext)
        {
            
        }

        public override void InitEntitySet()
        {
            base.InitEntitySet();
            if (Filter != null)
            {
                EntitySet = EntitySet.Where(Filter);
            }
        }
    }
}