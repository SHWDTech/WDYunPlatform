using System.Data.Entity;
using SHWDTech.Platform.Model.Model;

namespace SHWD.Platform.Process.Entities
{
    public class ProcessContext :DbContext
    {
        public ProcessContext() : base("DefaultConnection")
        {
            
        }

        public virtual DbSet<Alarm> Alarms { get; set; }

        public virtual DbSet<Camera> Cameras { get; set; }

        public virtual DbSet<Device> Devices { get; set; }

        public virtual DbSet<District> Districts { get; set; }

        public virtual DbSet<Menu> Menus { get; set; }

        public virtual DbSet<MonitorData> MonitorDatas { get; set; }

        public virtual DbSet<Permission> Permissions { get; set; }

        public virtual DbSet<Photo> Photos { get; set; }

        public virtual DbSet<Project> Projects { get; set; }

        public virtual DbSet<Protocol> Protocols { get; set; }

        public virtual DbSet<Role> Roles { get; set; }

        public virtual DbSet<SysConfig> SysConfigs { get; set; }

        public virtual DbSet<SysDomain> SysDomains { get; set; }

        public virtual DbSet<Task> Tasks { get; set; }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<UserConfig> UserConfigs { get; set; }
    }
}
