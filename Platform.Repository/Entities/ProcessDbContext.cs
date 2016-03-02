using System.Data.Entity;
using SHWDTech.Platform.Model.Model;

namespace SHWD.Platform.Repository.Entities
{
    public class RepositoryDbContext : DbContext
    {
        public RepositoryDbContext() : base("DefaultConnection")
        {
            
        }

        public virtual DbSet<Alarm> Alarms { get; set; }

        public virtual DbSet<Camera> Cameras { get; set; }

        public virtual DbSet<Device> Devices { get; set; }

        public virtual DbSet<Menu> Menus { get; set; }

        public virtual DbSet<MonitorData> MonitorDatas { get; set; }

        public virtual DbSet<Permission> Permissions { get; set; }

        public virtual DbSet<Photo> Photos { get; set; }

        public virtual DbSet<Project> Projects { get; set; }

        public virtual DbSet<ProtocolData> Protocols { get; set; }

        public virtual DbSet<Role> Roles { get; set; }

        public virtual DbSet<SysConfig> SysConfigs { get; set; }

        public virtual DbSet<SysDomain> SysDomains { get; set; }

        public virtual DbSet<Task> Tasks { get; set; }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<UserConfig> UserConfigs { get; set; }
    }
}
