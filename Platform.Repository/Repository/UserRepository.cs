using System;
using System.Collections.Generic;
using System.Linq;
using SHWD.Platform.Repository.IRepository;
using SHWDTech.Platform.Model.Enums;
using SHWDTech.Platform.Model.IModel;

namespace SHWD.Platform.Repository.Repository
{
    /// <summary>
    /// 用户数据仓库
    /// </summary>
    public class UserRepository : SysRepository<IWdUser>, IUserRepository
    {
        public override IWdUser CreateDefaultModel()
        {
            var model =  base.CreateDefaultModel();
            model.Status = UserStatus.Enabled;

            return model;
        }

        public IWdUser GetUserByName(string userName) => GetModels(obj => obj.UserName == userName).FirstOrDefault();

        public IWdUser GetUserById(Guid id) => GetModels(obj => obj.Id == id).FirstOrDefault();

        public IList<IWdUser> GetUsersByNameList(IEnumerable<string> nameList) => nameList.Select(GetUserByName).Where(user => user != null).ToList();

        public IList<IWdUser> GetUsersByIdList(IEnumerable<Guid> idList) => idList.Select(GetUserById).Where(user => user != null).ToList();
    }
}