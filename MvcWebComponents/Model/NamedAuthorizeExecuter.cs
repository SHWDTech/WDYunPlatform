using System.Web.Mvc;
using MvcWebComponents.Attributes;

namespace MvcWebComponents.Model
{
    public class NamedAuthorizeExecuter
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

        /// <summary>
        /// 控制器命名授权属性
        /// </summary>
        private NamedAuthAttribute _controllerAuthAttribute;

        /// <summary>
        /// 操作命名授权属性
        /// </summary>
        private NamedAuthAttribute _actionAuthAttribute;

        private NamedAuthorizeExecuter()
        {
            
        }

        /// <summary>
        /// 创建新的命名授权执行器
        /// </summary>
        /// <param name="filterContext"></param>
        public NamedAuthorizeExecuter(ActionExecutingContext filterContext) : this()
        {
            GetAuthAttribute(filterContext);
        }

        /// <summary>
        /// 获取授权特性
        /// </summary>
        /// <param name="filterContext"></param>
        private void GetAuthAttribute(ActionExecutingContext filterContext)
        {
            var controllerAttribute =
                filterContext.ActionDescriptor.ControllerDescriptor.GetCustomAttributes(typeof(NamedAuthAttribute),
                    true);
            if (controllerAttribute.Length > 0)
            {
                _controllerAuthAttribute = (NamedAuthAttribute)controllerAttribute[0];
            }

            var actionAttribute =
                filterContext.ActionDescriptor.GetCustomAttributes(typeof(NamedAuthAttribute), true);
            if (actionAttribute.Length > 0)
            {
                _actionAuthAttribute = (NamedAuthAttribute)actionAttribute[0];
            }

            GetNamedAuthResult();
        }

        /// <summary>
        /// 获取命名授权特性结果
        /// </summary>
        private void GetNamedAuthResult()
        {
            if (_controllerAuthAttribute != null)
            {
                ControllerModule = _controllerAuthAttribute.Modules;
                ControllerRequired = _controllerAuthAttribute.Required;
            }
            else
            {
                ControllerModule = string.Empty;
                ControllerRequired = true;
            }

            if (_actionAuthAttribute != null)
            {
                ActionModule = _actionAuthAttribute.Modules;
                ActionRequired = _actionAuthAttribute.Required;
            }
            else
            {
                ActionModule = string.Empty;
                ActionRequired = true;
            }
        }

        /// <summary>
        /// 根据命名特性结果调整授权模块信息
        /// </summary>
        /// <param name="filterContext"></param>
        public void AdjustModule(ActionExecutingContext filterContext)
        {
            if (string.IsNullOrWhiteSpace(ControllerModule))
            {
                ControllerModule = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            }

            if (string.IsNullOrWhiteSpace(ActionModule))
            {
                ActionModule = filterContext.ActionDescriptor.ActionName;
            }
        }
    }
}
