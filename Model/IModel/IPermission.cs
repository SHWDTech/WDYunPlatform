using System;
using System.Collections.Generic;
using SHWDTech.Platform.Model.Enums;
using SHWDTech.Platform.Model.Model;

namespace SHWDTech.Platform.Model.IModel
{
    /// <summary>
    /// 权限模型接口
    /// </summary>
    public interface IPermission : ISysDomainModel
    {
        /// <summary>
        /// 权限名称
        /// </summary>
        string PermissionName { get; set; }

        /// <summary>
        /// 所属父级权限ID
        /// </summary>
        Guid? ParentPermissionId { get; set; }

        /// <summary>
        /// 所属父级权限
        /// </summary>
        Permission ParentPermission { get; set; }

        /// <summary>
        /// 拥有权限的角色
        /// </summary>
        ICollection<WdRole> Roles { get; set; }

        /// <summary>
        /// 拥有权限的用户
        /// </summary>
        ICollection<WdUser> Users { get; set; }
    }
}