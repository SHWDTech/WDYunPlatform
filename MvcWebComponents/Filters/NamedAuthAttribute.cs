using System;
using System.Web.Mvc;

namespace MvcWebComponents.Filters
{
    /// <summary>
    /// 命名授权模块
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class NamedAuthAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 模块名称
        /// </summary>
        public string Modules { get; set; }

        /// <summary>
        /// 是否需要特定权限
        /// </summary>
        public bool Required { get; set; } = true;
    }
}
