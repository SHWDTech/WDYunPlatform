using SHWDTech.Platform.Model.IModel;
using SHWDTech.Platform.Model.ModelBase;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SHWDTech.Platform.Model.Model
{
    /// <summary>
    /// 域
    /// </summary>
    [Serializable]
    public class Domain : SysModelBase, IDomain
    {
        [Required]
        [Display(Name = "域名称")]
        [MaxLength(50)]
        public string DomainName { get; set; }

        [Display(Name = "域类型")]
        [MaxLength(25)]
        public string DomianType { get; set; }

        public virtual ICollection<Camera> Cameras { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }

        public virtual ICollection<Menu> Menus { get; set; }

        public virtual ICollection<Photo> Photos { get; set; }

        public virtual ICollection<Device> Devices { get; set; }

        public virtual ICollection<Project> Projects { get; set; }

        public virtual ICollection<WdRole> WdRoles { get; set; }

        public virtual ICollection<WdUser> WdUsers { get; set; }

        public virtual ICollection<Permission> Permissions { get; set; }

        public virtual ICollection<UserConfig> UserCOnfigs { get; set; }

        public virtual ICollection<UserDictionary> UserDictionaries { get; set; }
    }
}