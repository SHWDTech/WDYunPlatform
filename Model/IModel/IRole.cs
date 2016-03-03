﻿using SHWDTech.Platform.Model.Model;
using System.Collections.Generic;

namespace SHWDTech.Platform.Model.IModel
{
    /// <summary>
    /// 角色模型接口
    /// </summary>
    public interface IRole : ISysDomainModel
    {
        /// <summary>
        /// 角色所属父角色
        /// </summary>
        Role ParentRole { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        string RoleName { get; set; }

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