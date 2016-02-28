using System;
using System.Collections.Generic;
using SHWDTech.Platform.Model.IModel;

namespace SHWD.Platform.Process.IProcess
{
    public interface IUserProcess
    {
        IUser GetUserById(Guid userId);

        IEnumerable<IUser> GetUser(Func<IUser, bool> exp);

        bool AddOrUpdate(IUser user);

        int AddOrUpdate(IEnumerable<IUser> users);

        bool Delete(IUser user);

        bool MarkDelete(IUser user);
    }
}
