using System.ComponentModel.DataAnnotations;

namespace Lampblack_Platform.Models.Account
{
    /// <summary>
    /// 用户登录模型
    /// </summary>
    public class LoginViewModel
    {
        /// <summary>
        /// 用户登陆名
        /// </summary>
        [Required]
        [Display(Name = "用户名")]
        public string LoginName { get; set; }

        /// <summary>
        /// 登陆密码
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        /// <summary>
        /// 是否记住我的账号
        /// </summary>
        [Display(Name = "记住我的账号？")]
        public bool RememberMe { get; set; }
    }
}