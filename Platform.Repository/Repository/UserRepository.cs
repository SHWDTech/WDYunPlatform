using System;
using System.Collections.Generic;
using System.Linq;
using SHWD.Platform.Repository.Entities;
using SHWD.Platform.Repository.IRepository;
using SHWDTech.Platform.Model.Enums;
using SHWDTech.Platform.Model.Model;

namespace SHWD.Platform.Repository.Repository
{
    /// <summary>
    /// 用户数据仓库
    /// </summary>
    public class UserRepository : SysDomainRepository<WdUser>, IUserRepository
    {
        public UserRepository()
        {
            
        }

        public UserRepository(RepositoryDbContext dbContext) : base(dbContext)
        {
            
        }

        public static WdUser CreateDefaultModel()
        {
            var model = SysDomainRepository<WdUser>.CreateDefaultModel();
            model.Status = UserStatus.Enabled;

            return model;
        }

        public List<WdUser> GetUserByName(string userName) => GetModels(obj => obj.UserName == userName).ToList();

        //用户未登录状态时，获取用户的相关信息。
        public WdUser GetUserByLoginName(string loginName) 
            => DbContext.Set<WdUser>().FirstOrDefault(user => user.LoginName == loginName);

        public WdUser GetUserById(Guid id) => GetModels(obj => obj.Id == id).FirstOrDefault();

        public IList<WdUser> GetUsersByNameList(IEnumerable<string> nameList)
        {
            var list = new List<WdUser>();
            foreach (var users in nameList.Select(GetUserByName))
            {
                list.AddRange(users);
            }

            return list;
        }

        public IList<WdUser> GetUsersByIdList(IEnumerable<Guid> idList) => idList.Select(GetUserById).Where(user => user != null).ToList();

        public void UpdateLoginInfo(string loginName)
        {
            var user = GetUserByLoginName(loginName);
            user.LastLoginDateTime = DateTime.Now;
            DbContext.SaveChanges();
        }
    }
}