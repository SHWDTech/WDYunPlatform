namespace SHWDTech.Platform.Common.Enum
{
    /// <summary>
    /// 云平台异常类型
    /// </summary>
    public enum PlatformExceptionType
    {
        /// <summary>
        /// 无效域编码
        /// </summary>
        InvalidDomainCode = 10,

        /// <summary>
        /// 无效域ID
        /// </summary>
        InvalidDomainId = 11,

        /// <summary>
        /// 登陆失败
        /// </summary>
        LoginFailed = 20,

        /// <summary>
        /// 登陆用户不存在
        /// </summary>
        LoginUserNotExists = 21,

        /// <summary>
        /// 登陆密码错误
        /// </summary>
        LoginPasswordError = 22,

        /// <summary>
        /// 登陆被拒绝
        /// </summary>
        LoginIsNotApproved = 23,

        /// <summary>
        /// 自定义异常
        /// </summary>
        CustomError = 888,

        /// <summary>
        /// 未知异常
        /// </summary>
        UnKnownError = 999,

        /// <summary>
        /// 未知数据库异常
        /// </summary>
        UnKnownDbError = 1999
    }
}