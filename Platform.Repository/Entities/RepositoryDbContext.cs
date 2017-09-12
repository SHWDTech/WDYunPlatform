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
        public RepositoryDbContext() : base("Lampblack_Platform")
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

            modelBuilder.Entity<FirmwareSet>()
                .HasMany(s => s.Firmwares)
                .WithMany(c => c.FirmwareSets)
                .Map(cs =>
                {
                    cs.MapLeftKey("FirmwareId");
                    cs.MapRightKey("FirmwareSetId");
                    cs.ToTable("FirmwareSetFirmware");
                });

            modelBuilder.Entity<Protocol>()
                .HasMany(s => s.Firmwares)
                .WithMany(c => c.Protocols)
                .Map(cs =>
                {
                    cs.MapLeftKey("ProtocolId");
                    cs.MapRightKey("FirmwareId");
                    cs.ToTable("ProtocolFirmware");
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
                        p.DeviceName,
                        p.CleanerType,
                        p.DeviceTerminalCode,
                        p.Photo,
                        p.IpAddress,
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
                        p.Comment,
                        p.DeviceModelId,
                        p.ChannelCount
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
                       p.StopDateTime,
                       p.CookStoveNumber,
                       p.AddressDetail
                   });

                   m.ToTable("HotelRestaurant");
               });

            modelBuilder.Entity<LampblackUser>()
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
                       p.UserName,
                       p.LoginName,
                       p.UserIdentityName,
                       p.Password,
                       p.Email,
                       p.Telephone,
                       p.LastLoginDateTime,
                       p.Status
                   });
                   m.ToTable("WdUser");
               })
               .Map(m =>
               {
                   m.Properties(p => new
                   {
                       p.DepartmentId,
                       p.CateringCompanyId
                   });

                   m.ToTable("LampblackUser");
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

        /// <summary>
        /// 油烟系统用户
        /// </summary>
        public virtual DbSet<LampblackUser> LampblackUsers { get; set; }

        /// <summary>
        /// 公司部门
        /// </summary>
        public virtual DbSet<Department> Departments { get; set; }

        /// <summary>
        /// 统计数据
        /// </summary>
        public virtual DbSet<DataStatistics> DataStatisticses { get; set; }

        /// <summary>
        /// 运行时间
        /// </summary>
        public virtual DbSet<RunningTime> RunningTimes { get; set; }

        /// <summary>
        /// 设备维护记录
        /// </summary>
        public virtual DbSet<DeviceMaintenance> DeviceMaintenances { get; set; }

        /// <summary>
        /// 油烟设备模型
        /// </summary>
        public virtual DbSet<LampblackDeviceModel> LampblackDeviceModels { get; set; }

        /// <summary>
        /// 油烟监控记录
        /// </summary>
        public virtual DbSet<LampblackRecord> LampblackRecords { get; set; }

        /// <summary>
        /// 设备平台接入记录
        /// </summary>
        public virtual DbSet<PlatformAccess> PlatformAccesses { get; set; }
    }
}