namespace Platform.Process.Enums
{
    /// <summary>
    /// 登陆状态
    /// </summary>
    public enum SignInStatus
    {
        /// <summary>
        /// 成功
        /// </summary>
        Success,

        /// <summary>
        /// 已锁定
        /// </summary>
        LockedOut,

        /// <summary>
        /// 需要二次验证
        /// </summary>
        RequiresVerification,

        /// <summary>
        /// 失败
        /// </summary>
        Failure
    }
}
