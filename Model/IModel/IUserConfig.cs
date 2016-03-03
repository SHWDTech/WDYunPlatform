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
    }
}