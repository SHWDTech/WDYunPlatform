using System.ComponentModel.DataAnnotations;

namespace Web_Cloud_Platform.Models
{
    public class AccountLoginViewModel : IBaseViewModel
    {
        [Display(Name = "登录名称")]
        [MaxLength(50)]
        [Required(ErrorMessage = "请输入登录名")]
        public string LoginName { get; set; }

        [Required(ErrorMessage = "请输入登陆密码")]
        [MinLength(6, ErrorMessage = "密码长度应当是6-16为字母或数字")]
        [MaxLength(16, ErrorMessage = "密码长度应当是6-16为字母或数字")]
        [Display(Name = "登录密码")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}