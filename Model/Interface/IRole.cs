using System.Collections.Generic;
using SHWDTech.Platform.Model.Model;

namespace SHWDTech.Platform.Model.Interface
{
    internal interface IRole
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        int RoleId { get; set; }

        /// <summary>
        /// 角色所属域ID
        /// </summary>
        SysDomain RoleDomain { get; set; }

        /// <summary>
        /// 角色所属父角色
        /// </summary>
        Role ParentRole { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        string RoleName { get; set; }

        /// <summary>
        /// 角色是否启用
        /// </summary>
        bool IsEnabled { get; set; }

        /// <summary>
        /// 属于该用户的角色
        /// </summary>
        List<User> Users { get; set; } 

        /// <summary>
        /// 角色拥有的权限
        /// </summary>
        List<Permission> Permissions { get; set; } 
    }
}
