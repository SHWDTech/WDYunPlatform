using SHWDTech.Platform.Model.Model;

namespace SHWDTech.Platform.Model.IModel
{
    public interface IUserConfig : ISysModel
    {
        /// <summary>
        /// 用户配置所属域
        /// </summary>
        SysDomain UserConfigDomain { get; set; }

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
    }
}
