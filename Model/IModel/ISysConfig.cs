using SHWDTech.Platform.Model.Model;

namespace SHWDTech.Platform.Model.IModel
{
    /// <summary>
    /// 系统配置模型接口
    /// </summary>
    public interface ISysConfig : ISysModel
    {
        /// <summary>
        /// 系统设置名称
        /// </summary>
        string SysConfigName { get; set; }

        /// <summary>
        /// 系统设置类型
        /// </summary>
        string SysConfigType { get; set; }

        /// <summary>
        /// 系统设置值
        /// </summary>
        string SysConfigValue { get; set; }

        /// <summary>
        /// 父级系统设置项
        /// </summary>
        SysConfig ParentSysConfig { get; set; }
    }
}