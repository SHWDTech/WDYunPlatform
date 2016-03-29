namespace SHWDTech.Platform.ProtocolCoding.Authentication
{
    /// <summary>
    /// 认证结果类型
    /// </summary>
    public enum AuthResultType
    {
        /// <summary>
        /// 无效的帧头
        /// </summary>
        InvaildHead,

        /// <summary>
        /// 无效的数据包
        /// </summary>
        InvalidPackage,

        /// <summary>
        /// 认证成功
        /// </summary>
        Success
    }
}
