using System;
using SHWDTech.Platform.Model.Model;
using System.Collections.Generic;
using SHWDTech.Platform.Model.Enums;

namespace SHWDTech.Platform.Model.IModel
{
    /// <summary>
    /// 角色模型接口
    /// </summary>
    public interface IWdRole : ISysDomainModel
    {
        /// <summary>
        /// 角色所属父角色ID
        /// </summary>
        Guid? ParentRoleId { get; set; }

        /// <summary>
        /// 角色所属父角色
        /// </summary>
        WdRole ParentRole { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        string RoleName { get; set; }

        /// <summary>
        /// 属于该用户的角色
        /// </summary>
        ICollection<WdUser> Users { get; set; }

        /// <summary>
        /// 角色状态
        /// </summary>
        RoleStatus Status { get; set; }

        /// <summary>
        /// 角色拥有的权限
        /// </summary>
        ICollection<Permission> Permissions { get; set; }
    }
}