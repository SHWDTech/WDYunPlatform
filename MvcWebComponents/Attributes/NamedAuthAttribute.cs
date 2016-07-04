using System;

namespace MvcWebComponents.Attributes
{
    /// <summary>
    /// 命名授权特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class NamedAuthAttribute : Attribute
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
