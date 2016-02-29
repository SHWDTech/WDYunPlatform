using System;
using System.Collections.Generic;
using SHWDTech.Platform.Model.Model;

namespace SHWDTech.Platform.Model.IModel
{
    public interface IUser : ISysModel
    {
        /// <summary>
        /// 用户所属域
        /// </summary>
        SysDomain UserDomain { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        string UserName { get; set; }

        /// <summary>
        /// 用户密码
        /// </summary>
        string Password { get; set; }

        /// <summary>
        /// 用户邮箱地址
        /// </summary>
        string Email { get; set; }

        /// <summary>
        /// 用户电话号码
        /// </summary>
        string Telephone { get; set; }

        /// <summary>
        /// 是否启用状态
        /// </summary>
        bool IsEnabled { get; set; }

        /// <summary>
        /// 用户最后登录时间
        /// </summary>
        DateTime LastLoginDateTime { get; set; }

        /// <summary>
        /// 用户所属角色
        /// </summary>
        List<Role> Roles { get; set; }
    }
}
