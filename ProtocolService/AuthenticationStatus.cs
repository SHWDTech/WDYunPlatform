namespace SHWDTech.Platform.ProtocolService
{
    /// <summary>
    /// 客户端授权状态
    /// </summary>
    public enum AuthenticationStatus : byte
    {
        /// <summary>
        /// 尚未授权
        /// </summary>
        NotAuthed = 0x00,

        /// <summary>
        /// 授权失败
        /// </summary>
        AuthFailed = 0x01,

        /// <summary>
        /// 客户端未注册
        /// </summary>
        ClientNotRegistered = 0x02,

        /// <summary>
        /// 授权成功
        /// </summary>
        Authed = 0xFF
    }
}
