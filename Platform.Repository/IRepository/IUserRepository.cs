using System;
using System.Collections.Generic;
using SHWDTech.Platform.Model.IModel;

namespace SHWD.Platform.Repository.IRepository
{
    /// <summary>
    /// 用户数据仓库接口
    /// </summary>
    public interface IUserRepository : ISysRepository<IWdUser>
    {
        /// <summary>
        /// 通过用户名称查找用户
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>符合要求的用户</returns>
        IWdUser GetUserByName(string userName);

        /// <summary>
        /// 通过用户ID查找用户
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <returns>符合要求的用户</returns>
        IWdUser GetUserById(Guid id);

        /// <summary>
        /// 通过用户姓名列表获取用户列表
        /// </summary>
        /// <param name="nameList">用户名称列表</param>
        /// <returns>符合要求的用户列表</returns>
        IList<IWdUser> GetUsersByNameList(IEnumerable<string> nameList);

        /// <summary>
        /// 通过用户ID列表获取用户列表
        /// </summary>
        /// <param name="idList">用户ID列表</param>
        /// <returns>符合要求的用户列表</returns>
        IList<IWdUser> GetUsersByIdList(IEnumerable<Guid> idList);
    }
}