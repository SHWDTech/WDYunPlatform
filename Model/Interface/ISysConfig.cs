namespace SHWDTech.Platform.Model.Interface
{
    internal interface ISysConfig
    {
        /// <summary>
        /// 系统设置ID
        /// </summary>
        int SysConfigId { get; set; }

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
