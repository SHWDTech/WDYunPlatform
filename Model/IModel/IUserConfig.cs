namespace SHWDTech.Platform.Model.IModel
{
    public interface IUserConfig : ISysModel, IDomainModel
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
