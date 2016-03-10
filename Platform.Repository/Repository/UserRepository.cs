using System;
using System.Collections.Generic;
using System.Linq;
using SHWD.Platform.Repository.IRepository;
using SHWDTech.Platform.Model.Enums;
using SHWDTech.Platform.Model.Model;

namespace SHWD.Platform.Repository.Repository
{
    /// <summary>
    /// 用户数据仓库
    /// </summary>
    public class UserRepository : SysRepository<WdUser>, IUserRepository
    {
        public override WdUser CreateDefaultModel()
        {
            var model =  base.CreateDefaultModel();
            model.Status = UserStatus.Enabled;

            return model;
        }

        public WdUser GetUserByName(string userName) => GetModels(obj => obj.UserName == userName).FirstOrDefault();

        public WdUser GetUserById(Guid id) => GetModels(obj => obj.Id == id).FirstOrDefault();

        public IList<WdUser> GetUsersByNameList(IEnumerable<string> nameList) => nameList.Select(GetUserByName).Where(user => user != null).ToList();

        public IList<WdUser> GetUsersByIdList(IEnumerable<Guid> idList) => idList.Select(GetUserById).Where(user => user != null).ToList();
    }
}