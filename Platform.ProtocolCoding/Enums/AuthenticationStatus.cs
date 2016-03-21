namespace SHWDTech.Platform.ProtocolCoding.Enums
{
    /// <summary>
    /// 设备授权状态
    /// </summary>
    public enum AuthenticationStatus
    {
        /// <summary>
        /// 未授权
        /// </summary>
        NotAuthed,

        /// <summary>
        /// 已授权
        /// </summary>
        Authed,

        /// <summary>
        /// 已回复授权信息
        /// </summary>
        AuthReplyed,

        /// <summary>
        /// 已确认授权
        /// </summary>
        AuthConfirmed
    }
}
