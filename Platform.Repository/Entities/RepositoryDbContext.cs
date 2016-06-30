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
        public RepositoryDbContext() : base($"name=Lampblack_Platform")
        {
        }

        /// <summary>
        /// 创建默认的DbContext
        /// </summary>
        /// <param name="connString">连接字符串或连接字符串名称</param>
        public RepositoryDbContext(string connString) : base(connString)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Entity<WdUser>()
                .HasMany(s => s.Roles)
                .WithMany(c => c.Users)
                .Map(cs =>
                {
                    cs.MapLeftKey("UserId");
                    cs.MapRightKey("RoleId");
                    cs.ToTable("UserRole");
                });

            modelBuilder.Entity<Permission>()
                .HasMany(s => s.Roles)
                .WithMany(c => c.Permissions)
                .Map(cs =>
                {
                    cs.MapLeftKey("PermissionId");
                    cs.MapRightKey("RoleId");
                    cs.ToTable("RolePermission");
                });

            modelBuilder.Entity<Permission>()
                .HasMany(s => s.Users)
                .WithMany(c => c.Permissions)
                .Map(cs =>
                {
                    cs.MapLeftKey("PermissionId");
                    cs.MapRightKey("UserId");
                    cs.ToTable("UserPermission");
                });


            modelBuilder.Entity<RestaurantDevice>()
                .Map(m =>
                {
                    m.Properties(p => new
                    {
                        p.CreateDateTime,
                        p.CreateUserId,
                        p.LastUpdateDateTime,
                        p.LastUpdateUserId,
                        p.IsDeleted,
                        p.IsEnabled,
                        p.DomainId,
                        p.DeviceTypeId,
                        p.OriginalDeviceId,
                        p.DeviceCode,
                        p.StatCode,
                        p.DevicePassword,
                        p.DeviceModuleGuid,
                        p.DeviceNodeId,
                        p.FirmwareSetId,
                        p.ProjectId,
                        p.StartTime,
                        p.PreEndTime,
                        p.EndTime,
                        p.Status,
                        p.CameraId
                    });
                    m.ToTable("Device");
                }).Map(m =>
                {
                    m.Properties(p => new
                    {
                        p.ProductionDateTime,
                        p.Telephone,
                        p.CollectFrequency,
                        p.CleanerName,
                        p.CleanerTypeId,
                        p.CleanerModel,
                        p.CleanerManufacturer,
                        p.CleanerRatedVoltage,
                        p.CleanerMaxCurrent,
                        p.CleanerRatedCurrent,
                        p.CleanerMinCurrent,
                        p.FanType,
                        p.FanRatedVoltage,
                        p.FanDeliveryRate,
                        p.FanManufacturer,
                        p.FanMaxCurrent,
                        p.FanRatedCurrent,
                        p.FanMinCurrent,
                        p.Comment
                    });
                    m.ToTable("RestaurantDevice");
                });

            modelBuilder.Entity<ParticulateMatterProject>()
                .Map(m =>
                {
                    m.Properties(p => new
                    {
                        p.CreateDateTime,
                        p.CreateUserId,
                        p.LastUpdateDateTime,
                        p.LastUpdateUserId,
                        p.IsDeleted,
                        p.IsEnabled,
                        p.DomainId,
                        p.ProjectCode,
                        p.ProjectName,
                        p.ChargeMan,
                        p.Telephone,
                        p.Longitude,
                        p.Latitude,
                        p.Comment
                    });
                    m.ToTable("Project");
                })
                .Map(m =>
                {
                    m.Properties(p => new
                    {
                        p.StatCode,
                        p.StatBJH,
                        p.ProjectOutCode,
                        p.StatName,
                        p.Department,
                        p.Compnany,
                        p.Address,
                        p.Country,
                        p.Street,
                        p.DistrictId,
                        p.Square,
                        p.ProStartDate,
                        p.Stage,
                        p.ProjectStageId,
                        p.StartDate,
                        p.TypeId,
                        p.AlarmTypeId
                    });

                    m.ToTable("ParticulateMatterProject");
                });

            modelBuilder.Entity<HotelRestaurant>()
               .Map(m =>
               {
                   m.Properties(p => new
                   {
                       p.CreateDateTime,
                       p.CreateUserId,
                       p.LastUpdateDateTime,
                       p.LastUpdateUserId,
                       p.IsDeleted,
                       p.IsEnabled,
                       p.DomainId,
                       p.ProjectCode,
                       p.ProjectName,
                       p.ChargeMan,
                       p.Telephone,
                       p.Longitude,
                       p.Latitude,
                       p.Comment
                   });
                   m.ToTable("Project");
               })
               .Map(m =>
               {
                   m.Properties(p => new
                   {
                       p.RaletedCompanyId,
                       p.RegisterDateTime,
                       p.Email,
                       p.DistrictId,
                       p.StreetId,
                       p.AddressId,
                       p.Status,
                       p.OpeningDateTime,
                       p.StopDateTIme,
                       p.CookStoveNumber,
                       p.AddressDetail
                   });

                   m.ToTable("HotelRestaurant");
               });

            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// 报警信息
        /// </summary>
        public virtual DbSet<Alarm> Alarms { get; set; }

        /// <summary>
        /// 摄像头
        /// </summary>
        public virtual DbSet<Camera> Cameras { get; set; }

        /// <summary>
        /// 设备
        /// </summary>
        public virtual DbSet<Device> Devices { get; set; }

        /// <summary>
        /// 设备类型
        /// </summary>
        public virtual DbSet<DeviceType> DeviceTypes { get; set; }

        /// <summary>
        /// 模块
        /// </summary>
        public virtual DbSet<Module> Modules { get; set; }

        /// <summary>
        /// 监测数据
        /// </summary>
        public virtual DbSet<MonitorData> MonitorDatas { get; set; }

        /// <summary>
        /// 系统权限
        /// </summary>
        public virtual DbSet<Permission> Permissions { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        public virtual DbSet<Photo> Photos { get; set; }

        /// <summary>
        /// 项目
        /// </summary>
        public virtual DbSet<Project> Projects { get; set; }

        /// <summary>
        /// 扬尘项目
        /// </summary>
        public virtual DbSet<ParticulateMatterProject> ParticulateMatterProject { get; set; }

        /// <summary>
        /// 固件
        /// </summary>
        public virtual DbSet<Firmware> Firmwares { get; set; }

        /// <summary>
        /// 固件集
        /// </summary>
        public virtual DbSet<FirmwareSet> FirmwareSets { get; set; }

        /// <summary>
        /// 协议信息
        /// </summary>
        public virtual DbSet<Protocol> Protocols { get; set; }

        /// <summary>
        /// 协议数据包
        /// </summary>
        public virtual DbSet<ProtocolData> ProtocolDatas { get; set; }

        /// <summary>
        /// 系统角色
        /// </summary>
        public virtual DbSet<WdRole> Roles { get; set; }

        /// <summary>
        /// 系统配置
        /// </summary>
        public virtual DbSet<SysConfig> SysConfigs { get; set; }

        /// <summary>
        /// 系统域
        /// </summary>
        public virtual DbSet<Domain> SysDomains { get; set; }

        /// <summary>
        /// 任务
        /// </summary>
        public virtual DbSet<CommandTask> Tasks { get; set; }

        /// <summary>
        /// 用户
        /// </summary>
        public virtual DbSet<WdUser> Users { get; set; }

        /// <summary>
        /// 用户配置
        /// </summary>
        public virtual DbSet<UserConfig> UserConfigs { get; set; }

        /// <summary>
        /// 系统自定义词典
        /// </summary>
        public virtual DbSet<SysDictionary> SysDictionaries { get; set; }

        /// <summary>
        /// 用户自定义词典
        /// </summary>
        public virtual DbSet<UserDictionary> UserDictionaries { get; set; }

        /// <summary>
        /// 协议结构
        /// </summary>
        public virtual DbSet<ProtocolStructure> ProtocolStructures { get; set; }

        /// <summary>
        /// 协议指令
        /// </summary>
        public virtual DbSet<ProtocolCommand> ProtocolCommands { get; set; }

        /// <summary>
        /// 指令数据
        /// </summary>
        public virtual DbSet<CommandData> CommandDatas { get; set; }

        /// <summary>
        /// 指令定义数据
        /// </summary>
        public virtual DbSet<CommandDefinition> CommandDefinitions { get; set; }

        /// <summary>
        /// 餐饮企业
        /// </summary>
        public virtual DbSet<CateringCompany> Restaurants { get; set; }

        /// <summary>
        /// 餐饮企业设备
        /// </summary>
        public virtual DbSet<RestaurantDevice> RestaurantDevices { get; set; }
    }
}