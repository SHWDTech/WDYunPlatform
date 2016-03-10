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
    }
}