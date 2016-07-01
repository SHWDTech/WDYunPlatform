using MvcWebComponents.Controllers;

namespace MvcWebComponents.Model
{
    /// <summary>
    /// 视图模型基类
    /// </summary>
    public class ViewModelBase : IBaseViewModel
    {
        public virtual WdContext Context { get; set; }
    }
}
