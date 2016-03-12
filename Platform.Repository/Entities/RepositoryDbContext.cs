using SHWDTech.Platform.Model.Model;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace SHWD.Platform.Repository.Entities
{
    /// <summary>
    /// 数据仓库基础环境类
    /// </summary>
    public class RepositoryDbContext : DbContext
    {
        /// <summary>
        /// 创建默认的DbContext
        /// </summary>
        public RepositoryDbContext() : base("DefaultConnection")
        {
            Configuration.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// 报警信息
        /// </summary>
        public DbSet<Alarm> Alarms { get; set; }

        /// <summary>
        /// 摄像头
        /// </summary>
        public DbSet<Camera> Cameras { get; set; }

        /// <summary>
        /// 设备
        /// </summary>
        public DbSet<Device> Devices { get; set; }

        /// <summary>
        /// 菜单
        /// </summary>
        public DbSet<Menu> Menus { get; set; }

        /// <summary>
        /// 监测数据
        /// </summary>
        public DbSet<MonitorData> MonitorDatas { get; set; }

        /// <summary>
        /// 系统权限
        /// </summary>
        public DbSet<Permission> Permissions { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        public DbSet<Photo> Photos { get; set; }

        /// <summary>
        /// 项目
        /// </summary>
        public DbSet<Project> Projects { get; set; }

        /// <summary>
        /// 协议数据包
        /// </summary>
        public DbSet<ProtocolData> ProtocolDatas { get; set; }

        /// <summary>
        /// 系统角色
        /// </summary>
        public virtual DbSet<WdRole> Roles { get; set; }

        /// <summary>
        /// 系统配置
        /// </summary>
        public DbSet<SysConfig> SysConfigs { get; set; }

        /// <summary>
        /// 系统域
        /// </summary>
        public DbSet<Domain> SysDomains { get; set; }

        /// <summary>
        /// 任务
        /// </summary>
        public DbSet<Task> Tasks { get; set; }

        /// <summary>
        /// 用户
        /// </summary>
        public DbSet<WdUser> Users { get; set; }

        /// <summary>
        /// 用户配置
        /// </summary>
        public DbSet<UserConfig> UserConfigs { get; set; }

        /// <summary>
        /// 系统自定义词典
        /// </summary>
        public DbSet<SysDictionary> SysDictionaries { get; set; }

        /// <summary>
        /// 用户自定义词典
        /// </summary>
        public DbSet<UserDictionary> UserDictionaries { get; set; }

        /// <summary>
        /// 协议结构
        /// </summary>
        public DbSet<ProtocolStructure> ProtocolStructures { get; set; }
    }
}