using SHWDTech.Platform.Model.Enums;
using SHWDTech.Platform.Model.IModel;
using SHWDTech.Platform.Model.ModelBase;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Principal;

namespace SHWDTech.Platform.Model.Model
{
    /// <summary>
    /// 用户
    /// </summary>
    [Serializable]
    public class WdUser : SysDomainModelBase, IWdUser
    {
        [Display(Name = "用户名")]
        [MaxLength(25)]
        [Required]
        public virtual string UserName { get; set; }

        [Required]
        [Index(IsUnique = true)]
        [MaxLength(25)]
        [Display(Name = "用户登录名")]
        public virtual string LoginName { get; set; }

        [Display(Name = "密码")]
        [MaxLength(50)]
        [Required]
        [DataType(DataType.Password)]
        public virtual string Password { get; set; }

        [Display(Name = "邮箱地址")]
        [DataType(DataType.EmailAddress)]
        [MaxLength(50)]
        public virtual string Email { get; set; }

        [Display(Name = "电话号码")]
        [DataType(DataType.PhoneNumber)]
        [MaxLength(15)]
        public virtual string Telephone { get; set; }

        [Display(Name = "最后登录时间")]
        [DataType(DataType.DateTime)]
        public virtual DateTime? LastLoginDateTime { get; set; }

        [Required]
        [Display(Name = "用户状态")]
        public virtual UserStatus Status { get; set; }

        [Display(Name = "所属用户组")]
        public virtual ICollection<WdRole> Roles { get; set; }

        public virtual bool IsInRole(string role) => Roles.Any(item => item.RoleName == role);

        [NotMapped]
        public virtual IIdentity Identity { get; set; }
    }
}