using System;
using SHWDTech.Platform.Model.Model;

namespace SHWDTech.Platform.Model.IModel
{
    /// <summary>
    /// 用户配置模型接口
    /// </summary>
    public interface IUserConfig : ISysDomainModel
    {
        /// <summary>
        /// 用户配置名称
        /// </summary>
        string UserConfigName { get; set; }

        /// <summary>
        /// 用户配置类型
        /// </summary>
        string UserConfigType { get; set; }

        /// <summary>
        /// 用户配置值
        /// </summary>
        string UserConfigValue { get; set; }

        /// <summary>
        /// 用户配置所属父级配置项ID
        /// </summary>
        Guid ParentUserConfigId { get; set; }

        /// <summary>
        /// 用户配置所属父级配置项
        /// </summary>
        UserConfig ParentUserConfig { get; set; }
    }
}