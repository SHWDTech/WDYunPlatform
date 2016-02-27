using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SHWDTech.Platform.Model.Interface;
using SHWDTech.Platform.Model.ModelBase;

namespace SHWDTech.Platform.Model.Model
{
    [Serializable]
    public class User : SysModelBase , IUser
    {
        [Key]
        public Guid UserId { get; set; }

        [Required]
        public SysDomain UserDomain { get; set; }

        [Display(Name = "用户名")]
        [MaxLength(25)]
        [Required]
        public string UserName { get; set; }

        [Display(Name = "密码")]
        [MaxLength(50)]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "邮箱地址")]
        [DataType(DataType.EmailAddress)]
        [MaxLength(50)]
        public string Email { get; set; }

        [Display(Name = "电话号码")]
        [DataType(DataType.PhoneNumber)]
        [MaxLength(15)]
        public string Telephone { get; set; }

        public bool IsEnabled { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime LastLoginDateTime { get; set; }

        [Display(Name = "所属用户组")]
        public List<Role> Roles { get; set; }
    }
}
