using System.Collections.Generic;
using SHWDTech.Platform.Model.Model;

namespace SHWDTech.Platform.Model.IModel
{
    /// <summary>
    /// 带有域的模型接口
    /// </summary>
    public interface IDomain : ISysModel
    {
        /// <summary>
        /// 域名称
        /// </summary>
        string DomainName { get; set; }

        /// <summary>
        /// 域类型
        /// </summary>
        string DomianType { get; set; }

        ICollection<Camera> Cameras { get; set; }

        ICollection<Task> Tasks { get; set; }

        ICollection<Menu> Menus { get; set; }

        ICollection<Photo> Photos { get; set; }

        ICollection<Device> Devices { get; set; }

        ICollection<Project> Projects { get; set; }

        ICollection<WdRole> WdRoles { get; set; }

        ICollection<WdUser> WdUsers { get; set; }

        ICollection<Permission> Permissions { get; set; }

        ICollection<UserConfig> UserCOnfigs { get; set; }

        ICollection<UserDictionary> UserDictionaries { get; set; }
    }
}