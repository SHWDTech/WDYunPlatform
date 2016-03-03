using SHWDTech.Platform.Model.IModel;
using SHWDTech.Platform.Model.ModelBase;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SHWDTech.Platform.Model.Model
{
    /// <summary>
    /// 用户
    /// </summary>
    [Serializable]
    public class User : SysDomainModelBase, IUser
    {
        [Display(Name = "用户名")]
        [MaxLength(25)]
        [Required]
        public string UserName { get; set; }

        [Required]
        [Index(IsUnique = true)]
        [MaxLength(25)]
        [Display(Name = "用户登录名")]
        public string LoginName { get; set; }

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

        [Display(Name = "最后登录时间")]
        [DataType(DataType.DateTime)]
        public DateTime LastLoginDateTime { get; set; }

        [Display(Name = "所属用户组")]
        public List<Role> Roles { get; set; }
    }
}