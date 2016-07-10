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
        UnFinalized,

        /// <summary>
        /// 无效的协议头
        /// </summary>
        InvalidHead,

        /// <summary>
        /// 缓存字节不足一个协议包
        /// </summary>
        NoEnoughBuffer,

        /// <summary>
        /// 无效的数据包
        /// </summary>
        InvalidPackage,

        /// <summary>
        /// 已完成
        /// </summary>
        Finalized
    }
}
