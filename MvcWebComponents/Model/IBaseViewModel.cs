using MvcWebComponents.Controllers;

namespace MvcWebComponents.Model
{
    public interface IBaseViewModel
    {
        /// <summary>
        /// 菜单列表
        /// </summary>
        WdContext Context { get; set; }
    }
}
