using MvcWebComponents.Controllers;

namespace MvcWebComponents.Model
{
    /// <summary>
    /// 视图模型基类
    /// </summary>
    public class ViewModelBase
    {
        /// <summary>
        /// 菜单列表
        /// </summary>
        public WdContext Context { get; set; }
    }
}
