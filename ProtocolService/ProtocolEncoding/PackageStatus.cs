using System.ComponentModel.DataAnnotations;

namespace SHWDTech.Platform.ProtocolService.ProtocolEncoding
{
    /// <summary>
    /// 协议包状态
    /// </summary>
    public enum PackageStatus : byte
    {
        /// <summary>
        /// 未完成
        /// </summary>
        [Display(Name = "未完成")]
        UnFinalized = 0x00,

        /// <summary>
        /// 无效的协议头
        /// </summary>
        [Display(Name = "无效的协议头")]
        InvalidHead = 0x01,

        /// <summary>
        /// 无效的指令
        /// </summary>
        [Display(Name = "无效的指令")]
        InvalidCommand = 0x02,

        /// <summary>
        /// 缓存字节不足一个协议包
        /// </summary>
        [Display(Name = "缓存字节不足一个协议包")]
        NoEnoughBuffer = 0x03,

        /// <summary>
        /// 无效的数据包
        /// </summary>
        [Display(Name = "无效的数据包")]
        InvalidPackage = 0x04,

        /// <summary>
        /// 数据校验失败
        /// </summary>
        [Display(Name = "数据校验失败")]
        ValidationFailed = 0x05,

        /// <summary>
        /// 已完成
        /// </summary>
        [Display(Name = "已完成")]
        Finalized = 0xFF
    }
}
