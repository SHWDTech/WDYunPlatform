namespace Platform.Process.Enums
{
    /// <summary>
    /// 登陆结果
    /// </summary>
    public class SignInResult
    {
        /// <summary>
        /// 登陆结果状态
        /// </summary>
        public SignInStatus Status { get; set; }

        /// <summary>
        /// 错误消息
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// 错误元素
        /// </summary>
        public string ErrorElement { get; set; }
    }
}
