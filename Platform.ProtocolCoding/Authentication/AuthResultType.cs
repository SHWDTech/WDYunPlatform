namespace SHWDTech.Platform.ProtocolCoding.Authentication
{
    /// <summary>
    /// 认证结果类型
    /// </summary>
    public enum AuthResultType
    {
        /// <summary>
        /// 协议包解码失败
        /// </summary>
        DecodedFailed,

        /// <summary>
        /// 设备未注册
        /// </summary>
        DeviceNotRegisted,

        /// <summary>
        /// 认证成功
        /// </summary>
        Success
    }
}
