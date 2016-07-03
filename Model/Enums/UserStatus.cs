using System.ComponentModel.DataAnnotations;

namespace SHWDTech.Platform.Model.Enums
{
    /// <summary>
    /// 用户状态
    /// </summary>
    public enum UserStatus : byte
    {
        /// <summary>
        /// 已启用
        /// </summary>
        [Display(Name = "已启用")]
        Enabled = 0x00,

        /// <summary>
        /// 已禁用
        /// </summary>
        [Display(Name = "已禁用")]
        Disabled = 0x01,

        /// <summary>
        /// 已停用
        /// </summary>
        [Display(Name = "已停用")]
        Stopped = 0x02,

        /// <summary>
        /// 已锁定
        /// </summary>
        [Display(Name = "已锁定")]
        Locked = 0x03
    }
}