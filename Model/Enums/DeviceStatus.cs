using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SHWDTech.Platform.Model.Enums
{
    public enum DeviceStatus : byte
    {
        /// <summary>
        /// 已经启用
        /// </summary>
        [Display(Name = "使用中")]
        [Description("使用中")]
        Enabled = 0x00,

        /// <summary>
        /// 未启用
        /// </summary>
        [Display(Name = "停用中")]
        [Description("停用中")]
        Disabled = 0x01,

        /// <summary>
        /// 维护中
        /// </summary>
        [Display(Name = "维护中")]
        [Description("维护中")]
        Maintenance = 0x02
    }
}
