using SHWDTech.Platform.Model.Model;
using System;
using System.Collections.Generic;
using System.Security.Principal;

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
        DateTime LastLoginDateTime { get; set; }

        /// <summary>
        /// 用户所属角色
        /// </summary>
        List<WdRole> Roles { get; set; }
    }
}