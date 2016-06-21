using System.Collections.Generic;
using SHWDTech.Platform.Model.Model;

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
        public List<Module> Menus { get; set; }
    }
}
