using SHWDTech.Platform.Model.Model;

namespace SHWDTech.Platform.Model.IModel
{
    /// <summary>
    /// 菜单模型接口
    /// </summary>
    public interface IMenu : ISysDomainModel
    {
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
        /// 菜单关联的权限
        /// </summary>
        Permission Permission { get; set; }
    }
}
