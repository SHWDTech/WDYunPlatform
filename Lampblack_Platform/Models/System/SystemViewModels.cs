using MvcWebComponents.Model;
using PagedList;
using SHWDTech.Platform.Model.Model;

namespace Lampblack_Platform.Models.System
{
    /// <summary>
    /// 用户视图模型
    /// </summary>
    public class UserViewModel : PagedListViewModelBase
    {
        /// <summary>
        /// 油烟系统用户列表
        /// </summary>
        public IPagedList<LampblackUser> LampblackUsers { get; set; }
    }

    /// <summary>
    /// 部门视图模型
    /// </summary>
    public class DepartmentViewModel : PagedListViewModelBase
    {
        /// <summary>
        /// 部门列表
        /// </summary>
        public IPagedList<Department> Departments { get; set; }
    }

    /// <summary>
    /// 角色视图模型
    /// </summary>
    public class RoleViewModel : PagedListViewModelBase
    {
        /// <summary>
        /// 角色列表
        /// </summary>
        public IPagedList<WdRole> Roles { get; set; }
    }
}