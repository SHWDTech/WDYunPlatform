using System;
using SHWDTech.Platform.Model.Model;
using System.Collections.Generic;
using System.Security.Principal;
using SHWDTech.Platform.Model.Enums;

namespace SHWDTech.Platform.Model.IModel
{
    /// <summary>
    /// 用户模型接口
    /// </summary>
    public interface IWdUser : ISysDomainModel, IPrincipal
    {
        /// <summary>
        /// 用户名
        /// </summary>
        string UserName { get; set; }

        /// <summary>
        /// 用户登录名
        /// </summary>
        string LoginName { get; set; }

        /// <summary>
        /// 用户真实姓名
        /// </summary>
        string UserIdentityName { get; set; }

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
        /// 用户最后登录时间
        /// </summary>
        DateTime? LastLoginDateTime { get; set; }

        /// <summary>
        /// 用户状态
        /// </summary>
        UserStatus Status { get; set; }

        /// <summary>
        /// 用户所属角色
        /// </summary>
        ICollection<WdRole> Roles { get; set; }

        /// <summary>
        /// 用户拥有的权限
        /// </summary>
        ICollection<Permission> Permissions { get; set; }
    }
}