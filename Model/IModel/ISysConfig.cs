namespace SHWDTech.Platform.Model.IModel
{
    public interface ISysConfig
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
    }
}
