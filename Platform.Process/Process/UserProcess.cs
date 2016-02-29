using System;
using SHWD.Platform.Process.IProcess;
using SHWDTech.Platform.Model.IModel;

namespace SHWD.Platform.Process.Process
{
    public class UserProcess : SysProcessBase<IUser>, IUserProcess<IUser>
    {
        public override Guid AddOrUpdate(IUser model)
        {
            model.LastUpdateDateTime = DateTime.Now;
            return base.AddOrUpdate(model);
        }
    }
}
