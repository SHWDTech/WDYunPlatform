using SHWDTech.Platform.Model.Model;

namespace SHWDTech.Platform.Model.IModel
{
    public interface IPermission
    {
        /// <summary>
        /// 权限ID
        /// </summary>
        int PermissionId { get; set; }

        /// <summary>
        /// 权限所属域
        /// </summary>
        SysDomain PermissionDomain { get; set; }

        /// <summary>
        /// 权限名称
        /// </summary>
        string PermissionName { get; set; }
    }
}
