namespace SHWDTech.Platform.ProtocolCoding.Enums
{
    /// <summary>
    /// 协议包状态
    /// </summary>
    public enum PackageStatus
    {
        /// <summary>
        /// 未完成
        /// </summary>
        UnFinalized = 0x00,

        /// <summary>
        /// 无效的协议头
        /// </summary>
        InvalidHead = 0x01,

        /// <summary>
        /// 无效的指令
        /// </summary>
        InvalidCommand = 0x02,

        /// <summary>
        /// 缓存字节不足一个协议包
        /// </summary>
        NoEnoughBuffer = 0x03,

        /// <summary>
        /// 无效的数据包
        /// </summary>
        InvalidPackage = 0x04,

        /// <summary>
        /// 已完成
        /// </summary>
        Finalized = 0xFF
    }
}
