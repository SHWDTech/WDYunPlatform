using System;
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
    }
}