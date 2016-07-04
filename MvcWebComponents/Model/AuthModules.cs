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
        /// 操作模块名称
        /// </summary>
        public string ActionModule { get; private set; }

        private AuthModules()
        {
            
        }

        /// <summary>
        /// 创建新的授权模块对象
        /// </summary>
        /// <param name="controllerModule">控制器模块名称</param>
        /// <param name="actionModule">操作模块名称</param>
        public AuthModules(string controllerModule, string actionModule) : this()
        {
            ControllerModule = controllerModule;
            ActionModule = actionModule;
        }
    }
}
