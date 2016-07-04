using MvcWebComponents.Filters;

namespace MvcWebComponents.Model
{
    /// <summary>
    /// 授权模块
    /// </summary>
    public class AuthModules
    {
        /// <summary>
        /// 控制器模块名称
        /// </summary>
        public string ControllerModule { get; private set; }

        /// <summary>
        /// 是否要求控制器特定权限
        /// </summary>
        public bool ControllerRequired { get; private set; }

        /// <summary>
        /// 操作模块名称
        /// </summary>
        public string ActionModule { get; private set; }

        /// <summary>
        /// 是否要求操作特定权限
        /// </summary>
        public bool ActionRequired { get; private set; }

        private AuthModules()
        {
            
        }

        /// <summary>
        /// 创建新的授权模块对象
        /// </summary>
        /// <param name="controllerModule">控制器模块名称</param>
        /// <param name="actionModule">操作模块名称</param>
        public AuthModules(NamedAuthAttribute controllerModule, NamedAuthAttribute actionModule) : this()
        {
            if (controllerModule != null)
            {
                ControllerModule = controllerModule.Modules;
                ControllerRequired = controllerModule.Required;
            }
            else
            {
                ControllerModule = string.Empty;
                ControllerRequired = true;
            }

            if (actionModule != null)
            {
                ActionModule = actionModule.Modules;
                ActionRequired = actionModule.Required;
            }
            else
            {
                ActionModule = string.Empty;
                ActionRequired = true;
            }
        }
    }
}
