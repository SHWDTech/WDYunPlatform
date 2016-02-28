using SHWDTech.Platform.Model.Model;

namespace SHWDTech.Platform.Model.IModel
{
    public interface IMenu
    {
        /// <summary>
        /// 菜单ID
        /// </summary>
        int MenuId { get; set; }

        /// <summary>
        /// 菜单所属域
        /// </summary>
        SysDomain MenuDomain { get; set; }

        /// <summary>
        /// 父菜单
        /// </summary>
        Menu ParentMenu { get; set; }

        /// <summary>
        /// 菜单所属层级
        /// </summary>
        int MenuLevel { get; set; }

        /// <summary>
        /// 菜单名称
        /// </summary>
        string MenuName { get; set; }

        /// <summary>
        /// 菜单所属控制器
        /// </summary>
        string Controller { get; set; }

        /// <summary>
        /// 菜单所属操作
        /// </summary>
        string Action { get; set; }

        /// <summary>
        /// 菜单是否启用
        /// </summary>
        bool IsEnabled { get; set; }

        /// <summary>
        /// 菜单关联的权限
        /// </summary>
        Permission Permission { get; set; }
    }
}
