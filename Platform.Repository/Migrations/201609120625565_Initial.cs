namespace SHWD.Platform.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Alarms",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        AlarmDeviceId = c.Guid(nullable: false),
                        AlarmValue = c.Double(nullable: false),
                        AlarmType = c.Int(nullable: false),
                        AlarmCode = c.Int(nullable: false),
                        UpdateTime = c.DateTime(nullable: false),
                        DomainId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Device", t => t.AlarmDeviceId)
                .ForeignKey("dbo.Domains", t => t.DomainId)
                .Index(t => t.AlarmDeviceId)
                .Index(t => t.DomainId);
            
            CreateTable(
                "dbo.Device",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DeviceTypeId = c.Guid(nullable: false),
                        OriginalDeviceId = c.Guid(),
                        DeviceCode = c.String(nullable: false, maxLength: 25),
                        StatCode = c.Int(),
                        DevicePassword = c.String(maxLength: 25),
                        DeviceModuleGuid = c.Guid(),
                        DeviceNodeId = c.String(maxLength: 16),
                        FirmwareSetId = c.Guid(nullable: false),
                        ProjectId = c.Guid(),
                        StartTime = c.DateTime(),
                        PreEndTime = c.DateTime(),
                        EndTime = c.DateTime(),
                        Status = c.Byte(nullable: false),
                        CameraId = c.Guid(),
                        DomainId = c.Guid(nullable: false),
                        CreateDateTime = c.DateTime(nullable: false),
                        CreateUserId = c.Guid(nullable: false),
                        LastUpdateDateTime = c.DateTime(),
                        LastUpdateUserId = c.Guid(),
                        IsDeleted = c.Boolean(nullable: false),
                        IsEnabled = c.Boolean(nullable: false),
                        CleanerType = c.Byte(),
                        DeviceName = c.String(maxLength: 50),
                        DeviceTerminalCode = c.String(maxLength: 50),
                        Photo = c.String(),
                        IpAddress = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cameras", t => t.CameraId)
                .ForeignKey("dbo.DeviceTypes", t => t.DeviceTypeId)
                .ForeignKey("dbo.Domains", t => t.DomainId)
                .ForeignKey("dbo.FirmwareSets", t => t.FirmwareSetId)
                .ForeignKey("dbo.Device", t => t.OriginalDeviceId)
                .ForeignKey("dbo.Project", t => t.ProjectId)
                .Index(t => t.DeviceTypeId)
                .Index(t => t.OriginalDeviceId)
                .Index(t => t.FirmwareSetId)
                .Index(t => t.ProjectId)
                .Index(t => t.CameraId)
                .Index(t => t.DomainId);
            
            CreateTable(
                "dbo.Cameras",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CameraOutId = c.String(maxLength: 50),
                        AccessName = c.String(maxLength: 25),
                        AccessPassword = c.String(maxLength: 50),
                        AccessUrl = c.String(maxLength: 200),
                        AccessPort = c.Int(),
                        AccessTypeId = c.Guid(),
                        Compnany = c.String(maxLength: 50),
                        ExtraInformation = c.String(maxLength: 2000),
                        CameraStatus = c.Byte(nullable: false),
                        DomainId = c.Guid(nullable: false),
                        CreateDateTime = c.DateTime(nullable: false),
                        CreateUserId = c.Guid(nullable: false),
                        LastUpdateDateTime = c.DateTime(),
                        LastUpdateUserId = c.Guid(),
                        IsDeleted = c.Boolean(nullable: false),
                        IsEnabled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SysDictionaries", t => t.AccessTypeId)
                .ForeignKey("dbo.Domains", t => t.DomainId)
                .Index(t => t.AccessTypeId)
                .Index(t => t.DomainId);
            
            CreateTable(
                "dbo.SysDictionaries",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ItemName = c.String(nullable: false),
                        ItemKey = c.String(nullable: false),
                        ItemValue = c.String(nullable: false),
                        ItemLevel = c.Byte(nullable: false),
                        ParentDictionaryId = c.Guid(),
                        CreateDateTime = c.DateTime(nullable: false),
                        CreateUserId = c.Guid(nullable: false),
                        LastUpdateDateTime = c.DateTime(),
                        LastUpdateUserId = c.Guid(),
                        IsDeleted = c.Boolean(nullable: false),
                        IsEnabled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SysDictionaries", t => t.ParentDictionaryId)
                .Index(t => t.ParentDictionaryId);
            
            CreateTable(
                "dbo.Domains",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DomainName = c.String(nullable: false, maxLength: 50),
                        DomianType = c.String(maxLength: 50),
                        DomainStatus = c.Byte(nullable: false),
                        CreateDateTime = c.DateTime(nullable: false),
                        CreateUserId = c.Guid(nullable: false),
                        LastUpdateDateTime = c.DateTime(),
                        LastUpdateUserId = c.Guid(),
                        IsDeleted = c.Boolean(nullable: false),
                        IsEnabled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DeviceTypes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FieldId = c.Guid(nullable: false),
                        SubFieldId = c.Guid(nullable: false),
                        CustomerInfo = c.String(),
                        Version = c.String(nullable: false),
                        ReleaseDateTime = c.DateTime(nullable: false),
                        DeviceTypeCode = c.String(nullable: false),
                        CreateDateTime = c.DateTime(nullable: false),
                        CreateUserId = c.Guid(nullable: false),
                        LastUpdateDateTime = c.DateTime(),
                        LastUpdateUserId = c.Guid(),
                        IsDeleted = c.Boolean(nullable: false),
                        IsEnabled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SysDictionaries", t => t.FieldId)
                .ForeignKey("dbo.SysDictionaries", t => t.SubFieldId)
                .Index(t => t.FieldId)
                .Index(t => t.SubFieldId);
            
            CreateTable(
                "dbo.FirmwareSets",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FirmwareSetName = c.String(nullable: false),
                        CreateDateTime = c.DateTime(nullable: false),
                        CreateUserId = c.Guid(nullable: false),
                        LastUpdateDateTime = c.DateTime(),
                        LastUpdateUserId = c.Guid(),
                        IsDeleted = c.Boolean(nullable: false),
                        IsEnabled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Firmwares",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FirmwareName = c.String(nullable: false),
                        CreateDateTime = c.DateTime(nullable: false),
                        CreateUserId = c.Guid(nullable: false),
                        LastUpdateDateTime = c.DateTime(),
                        LastUpdateUserId = c.Guid(),
                        IsDeleted = c.Boolean(nullable: false),
                        IsEnabled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Protocols",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FieldId = c.Guid(nullable: false),
                        SubFieldId = c.Guid(nullable: false),
                        ProtocolName = c.String(nullable: false),
                        ProtocolModule = c.String(nullable: false),
                        CustomerInfo = c.String(),
                        Version = c.String(nullable: false),
                        Head = c.Binary(nullable: false),
                        Tail = c.Binary(nullable: false),
                        ReleaseDateTime = c.DateTime(nullable: false),
                        CheckType = c.String(nullable: false),
                        CreateDateTime = c.DateTime(nullable: false),
                        CreateUserId = c.Guid(nullable: false),
                        LastUpdateDateTime = c.DateTime(),
                        LastUpdateUserId = c.Guid(),
                        IsDeleted = c.Boolean(nullable: false),
                        IsEnabled = c.Boolean(nullable: false),
                        Firmware_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SysDictionaries", t => t.FieldId)
                .ForeignKey("dbo.SysDictionaries", t => t.SubFieldId)
                .ForeignKey("dbo.Firmwares", t => t.Firmware_Id)
                .Index(t => t.FieldId)
                .Index(t => t.SubFieldId)
                .Index(t => t.Firmware_Id);
            
            CreateTable(
                "dbo.ProtocolCommands",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CommandTypeCode = c.Binary(),
                        CommandCode = c.Binary(nullable: false),
                        SendBytesLength = c.Int(nullable: false),
                        ReceiveBytesLength = c.Int(nullable: false),
                        ReceiceMaxBytesLength = c.Int(nullable: false),
                        CommandCategory = c.String(nullable: false, maxLength: 50),
                        ProtocolId = c.Guid(nullable: false),
                        DataOrderType = c.Byte(nullable: false),
                        CreateDateTime = c.DateTime(nullable: false),
                        CreateUserId = c.Guid(nullable: false),
                        LastUpdateDateTime = c.DateTime(),
                        LastUpdateUserId = c.Guid(),
                        IsDeleted = c.Boolean(nullable: false),
                        IsEnabled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Protocols", t => t.ProtocolId)
                .Index(t => t.ProtocolId);
            
            CreateTable(
                "dbo.CommandDatas",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DataIndex = c.Int(nullable: false),
                        DataLength = c.Int(nullable: false),
                        DataName = c.String(nullable: false),
                        DataConvertType = c.String(nullable: false),
                        DataValueType = c.Byte(nullable: false),
                        DataFlag = c.Byte(nullable: false),
                        ValidFlagIndex = c.Int(nullable: false),
                        CreateDateTime = c.DateTime(nullable: false),
                        CreateUserId = c.Guid(nullable: false),
                        LastUpdateDateTime = c.DateTime(),
                        LastUpdateUserId = c.Guid(),
                        IsDeleted = c.Boolean(nullable: false),
                        IsEnabled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CommandDefinitions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CommandGuid = c.Guid(nullable: false),
                        StructureName = c.String(),
                        ContentBytes = c.Binary(),
                        CreateDateTime = c.DateTime(nullable: false),
                        CreateUserId = c.Guid(nullable: false),
                        LastUpdateDateTime = c.DateTime(),
                        LastUpdateUserId = c.Guid(),
                        IsDeleted = c.Boolean(nullable: false),
                        IsEnabled = c.Boolean(nullable: false),
                        Command_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProtocolCommands", t => t.Command_Id)
                .Index(t => t.Command_Id);
            
            CreateTable(
                "dbo.SysConfigs",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        SysConfigName = c.String(nullable: false, maxLength: 25),
                        SysConfigType = c.String(nullable: false, maxLength: 25),
                        SysConfigValue = c.String(nullable: false, maxLength: 200),
                        ParentSysConfigId = c.Guid(),
                        CreateDateTime = c.DateTime(nullable: false),
                        CreateUserId = c.Guid(nullable: false),
                        LastUpdateDateTime = c.DateTime(),
                        LastUpdateUserId = c.Guid(),
                        IsDeleted = c.Boolean(nullable: false),
                        IsEnabled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SysConfigs", t => t.ParentSysConfigId)
                .Index(t => t.ParentSysConfigId);
            
            CreateTable(
                "dbo.ProtocolStructures",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ProtocolId = c.Guid(nullable: false),
                        DataType = c.String(nullable: false),
                        StructureName = c.String(nullable: false, maxLength: 50),
                        StructureIndex = c.Int(nullable: false),
                        StructureDataLength = c.Int(nullable: false),
                        DefaultBytes = c.Binary(nullable: false),
                        CreateDateTime = c.DateTime(nullable: false),
                        CreateUserId = c.Guid(nullable: false),
                        LastUpdateDateTime = c.DateTime(),
                        LastUpdateUserId = c.Guid(),
                        IsDeleted = c.Boolean(nullable: false),
                        IsEnabled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Protocols", t => t.ProtocolId)
                .Index(t => t.ProtocolId);
            
            CreateTable(
                "dbo.Project",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ProjectCode = c.String(nullable: false, maxLength: 200),
                        ProjectName = c.String(nullable: false, maxLength: 200),
                        ChargeMan = c.String(nullable: false, maxLength: 200),
                        Telephone = c.String(maxLength: 50),
                        Longitude = c.Single(),
                        Latitude = c.Single(),
                        Comment = c.String(maxLength: 2000),
                        DomainId = c.Guid(nullable: false),
                        CreateDateTime = c.DateTime(nullable: false),
                        CreateUserId = c.Guid(nullable: false),
                        LastUpdateDateTime = c.DateTime(),
                        LastUpdateUserId = c.Guid(),
                        IsDeleted = c.Boolean(nullable: false),
                        IsEnabled = c.Boolean(nullable: false),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Domains", t => t.DomainId)
                .Index(t => t.DomainId);
            
            CreateTable(
                "dbo.UserDictionaries",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ItemName = c.String(nullable: false),
                        ItemKey = c.String(nullable: false),
                        ItemValue = c.String(),
                        ItemLevel = c.Int(nullable: false),
                        ParentDictionaryId = c.Guid(),
                        DomainId = c.Guid(nullable: false),
                        CreateDateTime = c.DateTime(nullable: false),
                        CreateUserId = c.Guid(nullable: false),
                        LastUpdateDateTime = c.DateTime(),
                        LastUpdateUserId = c.Guid(),
                        IsDeleted = c.Boolean(nullable: false),
                        IsEnabled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Domains", t => t.DomainId)
                .ForeignKey("dbo.UserDictionaries", t => t.ParentDictionaryId)
                .Index(t => t.ParentDictionaryId)
                .Index(t => t.DomainId);
            
            CreateTable(
                "dbo.CateringCompanies",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CompanyName = c.String(nullable: false, maxLength: 200),
                        CompanyCode = c.String(nullable: false, maxLength: 50),
                        ChargeMan = c.String(nullable: false, maxLength: 50),
                        Telephone = c.String(),
                        Email = c.String(),
                        Address = c.String(),
                        RegisterDateTime = c.DateTime(nullable: false),
                        DomainId = c.Guid(nullable: false),
                        CreateDateTime = c.DateTime(nullable: false),
                        CreateUserId = c.Guid(nullable: false),
                        LastUpdateDateTime = c.DateTime(),
                        LastUpdateUserId = c.Guid(),
                        IsDeleted = c.Boolean(nullable: false),
                        IsEnabled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Domains", t => t.DomainId)
                .Index(t => t.DomainId);
            
            CreateTable(
                "dbo.LampblackDeviceModels",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 200),
                        Fail = c.Int(nullable: false),
                        Worse = c.Int(nullable: false),
                        Qualified = c.Int(nullable: false),
                        Good = c.Int(nullable: false),
                        DomainId = c.Guid(nullable: false),
                        CreateDateTime = c.DateTime(nullable: false),
                        CreateUserId = c.Guid(nullable: false),
                        LastUpdateDateTime = c.DateTime(),
                        LastUpdateUserId = c.Guid(),
                        IsDeleted = c.Boolean(nullable: false),
                        IsEnabled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Domains", t => t.DomainId)
                .Index(t => t.DomainId);
            
            CreateTable(
                "dbo.DataStatistics",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        CommandDataId = c.Guid(nullable: false),
                        DataChannel = c.Short(nullable: false),
                        ProjectId = c.Guid(),
                        DeviceId = c.Guid(nullable: false),
                        DoubleValue = c.Double(),
                        BooleanValue = c.Boolean(),
                        IntegerValue = c.Int(),
                        UpdateTime = c.DateTime(nullable: false),
                        Type = c.Byte(nullable: false),
                        DomainId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CommandDatas", t => t.CommandDataId)
                .ForeignKey("dbo.Device", t => t.DeviceId)
                .ForeignKey("dbo.Domains", t => t.DomainId)
                .ForeignKey("dbo.Project", t => t.ProjectId)
                .Index(t => t.CommandDataId)
                .Index(t => t.ProjectId)
                .Index(t => t.DeviceId)
                .Index(t => t.DomainId);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        Comment = c.String(),
                        DomainId = c.Guid(nullable: false),
                        CreateDateTime = c.DateTime(nullable: false),
                        CreateUserId = c.Guid(nullable: false),
                        LastUpdateDateTime = c.DateTime(),
                        LastUpdateUserId = c.Guid(),
                        IsDeleted = c.Boolean(nullable: false),
                        IsEnabled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Domains", t => t.DomainId)
                .Index(t => t.DomainId);
            
            CreateTable(
                "dbo.DeviceMaintenances",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        MaintenanceUserId = c.Guid(nullable: false),
                        DeviceId = c.Guid(nullable: false),
                        MaintenanceDateTime = c.DateTime(nullable: false),
                        BeforeMaintenance = c.Int(nullable: false),
                        AfterMaintenance = c.Int(nullable: false),
                        Comment = c.String(maxLength: 2000),
                        DomainId = c.Guid(nullable: false),
                        CreateDateTime = c.DateTime(nullable: false),
                        CreateUserId = c.Guid(nullable: false),
                        LastUpdateDateTime = c.DateTime(),
                        LastUpdateUserId = c.Guid(),
                        IsDeleted = c.Boolean(nullable: false),
                        IsEnabled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Device", t => t.DeviceId)
                .ForeignKey("dbo.Domains", t => t.DomainId)
                .ForeignKey("dbo.WdUser", t => t.MaintenanceUserId)
                .Index(t => t.MaintenanceUserId)
                .Index(t => t.DeviceId)
                .Index(t => t.DomainId);
            
            CreateTable(
                "dbo.WdUser",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 25),
                        LoginName = c.String(nullable: false, maxLength: 25),
                        UserIdentityName = c.String(maxLength: 25),
                        Password = c.String(nullable: false, maxLength: 50),
                        Email = c.String(maxLength: 50),
                        Telephone = c.String(maxLength: 15),
                        LastLoginDateTime = c.DateTime(),
                        Status = c.Byte(nullable: false),
                        DomainId = c.Guid(nullable: false),
                        CreateDateTime = c.DateTime(nullable: false),
                        CreateUserId = c.Guid(nullable: false),
                        LastUpdateDateTime = c.DateTime(),
                        LastUpdateUserId = c.Guid(),
                        IsDeleted = c.Boolean(nullable: false),
                        IsEnabled = c.Boolean(nullable: false),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Domains", t => t.DomainId)
                .Index(t => t.LoginName, unique: true)
                .Index(t => t.DomainId);
            
            CreateTable(
                "dbo.Permissions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        PermissionName = c.String(nullable: false, maxLength: 50),
                        PermissionDisplayName = c.String(nullable: false, maxLength: 50),
                        ParentPermissionId = c.Guid(),
                        DomainId = c.Guid(nullable: false),
                        CreateDateTime = c.DateTime(nullable: false),
                        CreateUserId = c.Guid(nullable: false),
                        LastUpdateDateTime = c.DateTime(),
                        LastUpdateUserId = c.Guid(),
                        IsDeleted = c.Boolean(nullable: false),
                        IsEnabled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Domains", t => t.DomainId)
                .ForeignKey("dbo.Permissions", t => t.ParentPermissionId)
                .Index(t => t.ParentPermissionId)
                .Index(t => t.DomainId);
            
            CreateTable(
                "dbo.WdRoles",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ParentRoleId = c.Guid(),
                        RoleName = c.String(nullable: false, maxLength: 25),
                        Status = c.Int(nullable: false),
                        Comments = c.String(),
                        DomainId = c.Guid(nullable: false),
                        CreateDateTime = c.DateTime(nullable: false),
                        CreateUserId = c.Guid(nullable: false),
                        LastUpdateDateTime = c.DateTime(),
                        LastUpdateUserId = c.Guid(),
                        IsDeleted = c.Boolean(nullable: false),
                        IsEnabled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Domains", t => t.DomainId)
                .ForeignKey("dbo.WdRoles", t => t.ParentRoleId)
                .Index(t => t.ParentRoleId)
                .Index(t => t.DomainId);
            
            CreateTable(
                "dbo.Modules",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ParentModuleId = c.Guid(),
                        IsMenu = c.Boolean(nullable: false),
                        ModuleLevel = c.Int(nullable: false),
                        ModuleIndex = c.Int(nullable: false),
                        IconString = c.String(),
                        ModuleName = c.String(nullable: false, maxLength: 25),
                        Controller = c.String(maxLength: 25),
                        Action = c.String(maxLength: 25),
                        PermissionId = c.Guid(),
                        DomainId = c.Guid(nullable: false),
                        CreateDateTime = c.DateTime(nullable: false),
                        CreateUserId = c.Guid(nullable: false),
                        LastUpdateDateTime = c.DateTime(),
                        LastUpdateUserId = c.Guid(),
                        IsDeleted = c.Boolean(nullable: false),
                        IsEnabled = c.Boolean(nullable: false, defaultValue:true),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Domains", t => t.DomainId)
                .ForeignKey("dbo.Modules", t => t.ParentModuleId)
                .ForeignKey("dbo.Permissions", t => t.PermissionId)
                .Index(t => t.ParentModuleId)
                .Index(t => t.PermissionId)
                .Index(t => t.DomainId);
            
            CreateTable(
                "dbo.MonitorDatas",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        ProtocolDataId = c.Guid(nullable: false),
                        CommandDataId = c.Guid(nullable: false),
                        DataChannel = c.Short(nullable: false),
                        ProjectId = c.Guid(),
                        DoubleValue = c.Double(),
                        BooleanValue = c.Boolean(),
                        IntegerValue = c.Int(),
                        UpdateTime = c.DateTime(nullable: false),
                        DataIsValid = c.Boolean(nullable: false),
                        DomainId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CommandDatas", t => t.CommandDataId)
                .ForeignKey("dbo.Domains", t => t.DomainId)
                .ForeignKey("dbo.Project", t => t.ProjectId)
                .ForeignKey("dbo.ProtocolDatas", t => t.ProtocolDataId)
                .Index(t => t.ProtocolDataId)
                .Index(t => t.CommandDataId)
                .Index(t => t.ProjectId)
                .Index(t => t.DomainId);
            
            CreateTable(
                "dbo.ProtocolDatas",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        DeviceId = c.Guid(nullable: false),
                        ProtocolContent = c.Binary(nullable: false),
                        Length = c.Int(nullable: false),
                        ProtocolId = c.Guid(nullable: false),
                        ProtocolTime = c.DateTime(nullable: false),
                        UpdateTime = c.DateTime(nullable: false),
                        DomainId = c.Guid(nullable: false),
                        CommandTask_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Device", t => t.DeviceId)
                .ForeignKey("dbo.Domains", t => t.DomainId)
                .ForeignKey("dbo.Protocols", t => t.ProtocolId)
                .ForeignKey("dbo.CommandTasks", t => t.CommandTask_Id)
                .Index(t => t.DeviceId)
                .Index(t => t.ProtocolId)
                .Index(t => t.DomainId)
                .Index(t => t.CommandTask_Id);
            
            CreateTable(
                "dbo.Photos",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DeviceId = c.Guid(),
                        PhotoTag = c.String(),
                        PhotoUrl = c.String(nullable: false, maxLength: 2000),
                        PhotoTypeId = c.Guid(nullable: false),
                        DomainId = c.Guid(nullable: false),
                        CreateDateTime = c.DateTime(nullable: false),
                        CreateUserId = c.Guid(nullable: false),
                        LastUpdateDateTime = c.DateTime(),
                        LastUpdateUserId = c.Guid(),
                        IsDeleted = c.Boolean(nullable: false),
                        IsEnabled = c.Boolean(nullable: false),
                        PhtotDevice_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Device", t => t.DeviceId)
                .ForeignKey("dbo.Domains", t => t.DomainId)
                .ForeignKey("dbo.SysDictionaries", t => t.PhotoTypeId)
                .ForeignKey("dbo.Device", t => t.PhtotDevice_Id)
                .Index(t => t.DeviceId)
                .Index(t => t.PhotoTypeId)
                .Index(t => t.DomainId)
                .Index(t => t.PhtotDevice_Id);
            
            CreateTable(
                "dbo.RunningTimes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        RunningTimeTicks = c.Long(nullable: false),
                        Type = c.Byte(nullable: false),
                        ProjectId = c.Guid(nullable: false),
                        DeviceId = c.Guid(nullable: false),
                        UpdateTime = c.DateTime(nullable: false),
                        DomainId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Device", t => t.DeviceId)
                .ForeignKey("dbo.Domains", t => t.DomainId)
                .ForeignKey("dbo.Project", t => t.ProjectId)
                .Index(t => t.ProjectId)
                .Index(t => t.DeviceId)
                .Index(t => t.DomainId);
            
            CreateTable(
                "dbo.CommandTasks",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TaskDeviceId = c.Guid(nullable: false),
                        TaskCode = c.String(maxLength: 25),
                        TaskType = c.Int(nullable: false),
                        TaskStatus = c.Byte(nullable: false),
                        ExecuteStatus = c.Byte(nullable: false),
                        SetUpDateTime = c.DateTime(nullable: false),
                        DomainId = c.Guid(nullable: false),
                        CreateDateTime = c.DateTime(nullable: false),
                        CreateUserId = c.Guid(nullable: false),
                        LastUpdateDateTime = c.DateTime(),
                        LastUpdateUserId = c.Guid(),
                        IsDeleted = c.Boolean(nullable: false),
                        IsEnabled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Domains", t => t.DomainId)
                .ForeignKey("dbo.Device", t => t.TaskDeviceId)
                .Index(t => t.TaskDeviceId)
                .Index(t => t.DomainId);
            
            CreateTable(
                "dbo.UserConfigs",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserConfigName = c.String(nullable: false, maxLength: 25),
                        UserConfigType = c.String(nullable: false, maxLength: 25),
                        UserConfigValue = c.String(nullable: false, maxLength: 200),
                        ParentUserConfigId = c.Guid(),
                        DomainId = c.Guid(nullable: false),
                        CreateDateTime = c.DateTime(nullable: false),
                        CreateUserId = c.Guid(nullable: false),
                        LastUpdateDateTime = c.DateTime(),
                        LastUpdateUserId = c.Guid(),
                        IsDeleted = c.Boolean(nullable: false),
                        IsEnabled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Domains", t => t.DomainId)
                .ForeignKey("dbo.UserConfigs", t => t.ParentUserConfigId)
                .Index(t => t.ParentUserConfigId)
                .Index(t => t.DomainId);
            
            CreateTable(
                "dbo.FirmwareFirmwareSets",
                c => new
                    {
                        Firmware_Id = c.Guid(nullable: false),
                        FirmwareSet_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Firmware_Id, t.FirmwareSet_Id })
                .ForeignKey("dbo.Firmwares", t => t.Firmware_Id, cascadeDelete: true)
                .ForeignKey("dbo.FirmwareSets", t => t.FirmwareSet_Id, cascadeDelete: true)
                .Index(t => t.Firmware_Id)
                .Index(t => t.FirmwareSet_Id);
            
            CreateTable(
                "dbo.CommandDataProtocolCommands",
                c => new
                    {
                        CommandData_Id = c.Guid(nullable: false),
                        ProtocolCommand_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.CommandData_Id, t.ProtocolCommand_Id })
                .ForeignKey("dbo.CommandDatas", t => t.CommandData_Id, cascadeDelete: true)
                .ForeignKey("dbo.ProtocolCommands", t => t.ProtocolCommand_Id, cascadeDelete: true)
                .Index(t => t.CommandData_Id)
                .Index(t => t.ProtocolCommand_Id);
            
            CreateTable(
                "dbo.SysConfigProtocolCommands",
                c => new
                    {
                        SysConfig_Id = c.Guid(nullable: false),
                        ProtocolCommand_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.SysConfig_Id, t.ProtocolCommand_Id })
                .ForeignKey("dbo.SysConfigs", t => t.SysConfig_Id, cascadeDelete: true)
                .ForeignKey("dbo.ProtocolCommands", t => t.ProtocolCommand_Id, cascadeDelete: true)
                .Index(t => t.SysConfig_Id)
                .Index(t => t.ProtocolCommand_Id);
            
            CreateTable(
                "dbo.RolePermission",
                c => new
                    {
                        PermissionId = c.Guid(nullable: false),
                        RoleId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.PermissionId, t.RoleId })
                .ForeignKey("dbo.Permissions", t => t.PermissionId, cascadeDelete: true)
                .ForeignKey("dbo.WdRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.PermissionId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.UserPermission",
                c => new
                    {
                        PermissionId = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.PermissionId, t.UserId })
                .ForeignKey("dbo.Permissions", t => t.PermissionId, cascadeDelete: true)
                .ForeignKey("dbo.WdUser", t => t.UserId, cascadeDelete: true)
                .Index(t => t.PermissionId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserRole",
                c => new
                    {
                        UserId = c.Guid(nullable: false),
                        RoleId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.WdUser", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.WdRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.LampblackUser",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DepartmentId = c.Guid(),
                        CateringCompanyId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departments", t => t.DepartmentId)
                .ForeignKey("dbo.CateringCompanies", t => t.CateringCompanyId)
                .ForeignKey("dbo.WdUser", t => t.Id)
                .Index(t => t.Id)
                .Index(t => t.DepartmentId)
                .Index(t => t.CateringCompanyId);
            
            CreateTable(
                "dbo.ParticulateMatterProject",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        StatCode = c.Int(nullable: false, identity: true),
                        StatBJH = c.String(nullable: false, maxLength: 20),
                        ProjectOutCode = c.String(maxLength: 25),
                        StatName = c.String(nullable: false, maxLength: 20),
                        Department = c.String(nullable: false, maxLength: 30),
                        Compnany = c.String(),
                        Address = c.String(maxLength: 50),
                        Country = c.String(maxLength: 20),
                        Street = c.String(maxLength: 20),
                        DistrictId = c.Guid(nullable: false),
                        Square = c.Short(nullable: false),
                        ProStartDate = c.DateTime(nullable: false),
                        Stage = c.String(maxLength: 20),
                        ProjectStageId = c.Guid(),
                        StartDate = c.DateTime(nullable: false),
                        TypeId = c.Guid(),
                        AlarmTypeId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SysDictionaries", t => t.DistrictId)
                .ForeignKey("dbo.SysDictionaries", t => t.ProjectStageId)
                .ForeignKey("dbo.SysDictionaries", t => t.TypeId)
                .ForeignKey("dbo.SysDictionaries", t => t.AlarmTypeId)
                .ForeignKey("dbo.Project", t => t.Id)
                .Index(t => t.Id)
                .Index(t => t.DistrictId)
                .Index(t => t.ProjectStageId)
                .Index(t => t.TypeId)
                .Index(t => t.AlarmTypeId);
            
            CreateTable(
                "dbo.RestaurantDevice",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ProductionDateTime = c.DateTime(nullable: false),
                        Telephone = c.String(nullable: false),
                        CollectFrequency = c.Int(nullable: false),
                        CleanerName = c.String(maxLength: 50),
                        CleanerModel = c.String(maxLength: 200),
                        CleanerManufacturer = c.String(maxLength: 200),
                        CleanerRatedVoltage = c.Double(nullable: false),
                        CleanerMaxCurrent = c.Double(nullable: false),
                        CleanerRatedCurrent = c.Double(nullable: false),
                        CleanerMinCurrent = c.Double(nullable: false),
                        FanType = c.String(),
                        FanManufacturer = c.String(),
                        FanRatedVoltage = c.Double(nullable: false),
                        FanMaxCurrent = c.Double(nullable: false),
                        FanRatedCurrent = c.Double(nullable: false),
                        FanMinCurrent = c.Double(nullable: false),
                        FanDeliveryRate = c.Double(nullable: false),
                        Comment = c.String(maxLength: 2000),
                        DeviceModelId = c.Guid(nullable: false),
                        ChannelCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LampblackDeviceModels", t => t.DeviceModelId)
                .ForeignKey("dbo.Device", t => t.Id)
                .Index(t => t.Id)
                .Index(t => t.DeviceModelId);
            
            CreateTable(
                "dbo.HotelRestaurant",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        RaletedCompanyId = c.Guid(nullable: false),
                        RegisterDateTime = c.DateTime(nullable: false),
                        Email = c.String(),
                        DistrictId = c.Guid(nullable: false),
                        StreetId = c.Guid(nullable: false),
                        AddressId = c.Guid(nullable: false),
                        Status = c.Byte(nullable: false),
                        OpeningDateTime = c.DateTime(),
                        StopDateTime = c.DateTime(),
                        CookStoveNumber = c.Int(nullable: false),
                        AddressDetail = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CateringCompanies", t => t.RaletedCompanyId)
                .ForeignKey("dbo.UserDictionaries", t => t.DistrictId)
                .ForeignKey("dbo.UserDictionaries", t => t.StreetId)
                .ForeignKey("dbo.UserDictionaries", t => t.AddressId)
                .ForeignKey("dbo.Project", t => t.Id)
                .Index(t => t.Id)
                .Index(t => t.RaletedCompanyId)
                .Index(t => t.DistrictId)
                .Index(t => t.StreetId)
                .Index(t => t.AddressId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.HotelRestaurant", "Id", "dbo.Project");
            DropForeignKey("dbo.HotelRestaurant", "AddressId", "dbo.UserDictionaries");
            DropForeignKey("dbo.HotelRestaurant", "StreetId", "dbo.UserDictionaries");
            DropForeignKey("dbo.HotelRestaurant", "DistrictId", "dbo.UserDictionaries");
            DropForeignKey("dbo.HotelRestaurant", "RaletedCompanyId", "dbo.CateringCompanies");
            DropForeignKey("dbo.RestaurantDevice", "Id", "dbo.Device");
            DropForeignKey("dbo.RestaurantDevice", "DeviceModelId", "dbo.LampblackDeviceModels");
            DropForeignKey("dbo.ParticulateMatterProject", "Id", "dbo.Project");
            DropForeignKey("dbo.ParticulateMatterProject", "AlarmTypeId", "dbo.SysDictionaries");
            DropForeignKey("dbo.ParticulateMatterProject", "TypeId", "dbo.SysDictionaries");
            DropForeignKey("dbo.ParticulateMatterProject", "ProjectStageId", "dbo.SysDictionaries");
            DropForeignKey("dbo.ParticulateMatterProject", "DistrictId", "dbo.SysDictionaries");
            DropForeignKey("dbo.LampblackUser", "Id", "dbo.WdUser");
            DropForeignKey("dbo.LampblackUser", "CateringCompanyId", "dbo.CateringCompanies");
            DropForeignKey("dbo.LampblackUser", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.UserConfigs", "ParentUserConfigId", "dbo.UserConfigs");
            DropForeignKey("dbo.UserConfigs", "DomainId", "dbo.Domains");
            DropForeignKey("dbo.ProtocolDatas", "CommandTask_Id", "dbo.CommandTasks");
            DropForeignKey("dbo.CommandTasks", "TaskDeviceId", "dbo.Device");
            DropForeignKey("dbo.CommandTasks", "DomainId", "dbo.Domains");
            DropForeignKey("dbo.RunningTimes", "ProjectId", "dbo.Project");
            DropForeignKey("dbo.RunningTimes", "DomainId", "dbo.Domains");
            DropForeignKey("dbo.RunningTimes", "DeviceId", "dbo.Device");
            DropForeignKey("dbo.Photos", "PhtotDevice_Id", "dbo.Device");
            DropForeignKey("dbo.Photos", "PhotoTypeId", "dbo.SysDictionaries");
            DropForeignKey("dbo.Photos", "DomainId", "dbo.Domains");
            DropForeignKey("dbo.Photos", "DeviceId", "dbo.Device");
            DropForeignKey("dbo.MonitorDatas", "ProtocolDataId", "dbo.ProtocolDatas");
            DropForeignKey("dbo.ProtocolDatas", "ProtocolId", "dbo.Protocols");
            DropForeignKey("dbo.ProtocolDatas", "DomainId", "dbo.Domains");
            DropForeignKey("dbo.ProtocolDatas", "DeviceId", "dbo.Device");
            DropForeignKey("dbo.MonitorDatas", "ProjectId", "dbo.Project");
            DropForeignKey("dbo.MonitorDatas", "DomainId", "dbo.Domains");
            DropForeignKey("dbo.MonitorDatas", "CommandDataId", "dbo.CommandDatas");
            DropForeignKey("dbo.Modules", "PermissionId", "dbo.Permissions");
            DropForeignKey("dbo.Modules", "ParentModuleId", "dbo.Modules");
            DropForeignKey("dbo.Modules", "DomainId", "dbo.Domains");
            DropForeignKey("dbo.DeviceMaintenances", "MaintenanceUserId", "dbo.WdUser");
            DropForeignKey("dbo.UserRole", "RoleId", "dbo.WdRoles");
            DropForeignKey("dbo.UserRole", "UserId", "dbo.WdUser");
            DropForeignKey("dbo.UserPermission", "UserId", "dbo.WdUser");
            DropForeignKey("dbo.UserPermission", "PermissionId", "dbo.Permissions");
            DropForeignKey("dbo.RolePermission", "RoleId", "dbo.WdRoles");
            DropForeignKey("dbo.RolePermission", "PermissionId", "dbo.Permissions");
            DropForeignKey("dbo.WdRoles", "ParentRoleId", "dbo.WdRoles");
            DropForeignKey("dbo.WdRoles", "DomainId", "dbo.Domains");
            DropForeignKey("dbo.Permissions", "ParentPermissionId", "dbo.Permissions");
            DropForeignKey("dbo.Permissions", "DomainId", "dbo.Domains");
            DropForeignKey("dbo.WdUser", "DomainId", "dbo.Domains");
            DropForeignKey("dbo.DeviceMaintenances", "DomainId", "dbo.Domains");
            DropForeignKey("dbo.DeviceMaintenances", "DeviceId", "dbo.Device");
            DropForeignKey("dbo.Departments", "DomainId", "dbo.Domains");
            DropForeignKey("dbo.DataStatistics", "ProjectId", "dbo.Project");
            DropForeignKey("dbo.DataStatistics", "DomainId", "dbo.Domains");
            DropForeignKey("dbo.DataStatistics", "DeviceId", "dbo.Device");
            DropForeignKey("dbo.DataStatistics", "CommandDataId", "dbo.CommandDatas");
            DropForeignKey("dbo.Alarms", "DomainId", "dbo.Domains");
            DropForeignKey("dbo.Alarms", "AlarmDeviceId", "dbo.Device");
            DropForeignKey("dbo.LampblackDeviceModels", "DomainId", "dbo.Domains");
            DropForeignKey("dbo.Device", "ProjectId", "dbo.Project");
            DropForeignKey("dbo.CateringCompanies", "DomainId", "dbo.Domains");
            DropForeignKey("dbo.UserDictionaries", "ParentDictionaryId", "dbo.UserDictionaries");
            DropForeignKey("dbo.UserDictionaries", "DomainId", "dbo.Domains");
            DropForeignKey("dbo.Project", "DomainId", "dbo.Domains");
            DropForeignKey("dbo.Device", "OriginalDeviceId", "dbo.Device");
            DropForeignKey("dbo.Device", "FirmwareSetId", "dbo.FirmwareSets");
            DropForeignKey("dbo.Protocols", "Firmware_Id", "dbo.Firmwares");
            DropForeignKey("dbo.Protocols", "SubFieldId", "dbo.SysDictionaries");
            DropForeignKey("dbo.ProtocolStructures", "ProtocolId", "dbo.Protocols");
            DropForeignKey("dbo.ProtocolCommands", "ProtocolId", "dbo.Protocols");
            DropForeignKey("dbo.SysConfigProtocolCommands", "ProtocolCommand_Id", "dbo.ProtocolCommands");
            DropForeignKey("dbo.SysConfigProtocolCommands", "SysConfig_Id", "dbo.SysConfigs");
            DropForeignKey("dbo.SysConfigs", "ParentSysConfigId", "dbo.SysConfigs");
            DropForeignKey("dbo.CommandDefinitions", "Command_Id", "dbo.ProtocolCommands");
            DropForeignKey("dbo.CommandDataProtocolCommands", "ProtocolCommand_Id", "dbo.ProtocolCommands");
            DropForeignKey("dbo.CommandDataProtocolCommands", "CommandData_Id", "dbo.CommandDatas");
            DropForeignKey("dbo.Protocols", "FieldId", "dbo.SysDictionaries");
            DropForeignKey("dbo.FirmwareFirmwareSets", "FirmwareSet_Id", "dbo.FirmwareSets");
            DropForeignKey("dbo.FirmwareFirmwareSets", "Firmware_Id", "dbo.Firmwares");
            DropForeignKey("dbo.Device", "DomainId", "dbo.Domains");
            DropForeignKey("dbo.Device", "DeviceTypeId", "dbo.DeviceTypes");
            DropForeignKey("dbo.DeviceTypes", "SubFieldId", "dbo.SysDictionaries");
            DropForeignKey("dbo.DeviceTypes", "FieldId", "dbo.SysDictionaries");
            DropForeignKey("dbo.Device", "CameraId", "dbo.Cameras");
            DropForeignKey("dbo.Cameras", "DomainId", "dbo.Domains");
            DropForeignKey("dbo.Cameras", "AccessTypeId", "dbo.SysDictionaries");
            DropForeignKey("dbo.SysDictionaries", "ParentDictionaryId", "dbo.SysDictionaries");
            DropIndex("dbo.HotelRestaurant", new[] { "AddressId" });
            DropIndex("dbo.HotelRestaurant", new[] { "StreetId" });
            DropIndex("dbo.HotelRestaurant", new[] { "DistrictId" });
            DropIndex("dbo.HotelRestaurant", new[] { "RaletedCompanyId" });
            DropIndex("dbo.HotelRestaurant", new[] { "Id" });
            DropIndex("dbo.RestaurantDevice", new[] { "DeviceModelId" });
            DropIndex("dbo.RestaurantDevice", new[] { "Id" });
            DropIndex("dbo.ParticulateMatterProject", new[] { "AlarmTypeId" });
            DropIndex("dbo.ParticulateMatterProject", new[] { "TypeId" });
            DropIndex("dbo.ParticulateMatterProject", new[] { "ProjectStageId" });
            DropIndex("dbo.ParticulateMatterProject", new[] { "DistrictId" });
            DropIndex("dbo.ParticulateMatterProject", new[] { "Id" });
            DropIndex("dbo.LampblackUser", new[] { "CateringCompanyId" });
            DropIndex("dbo.LampblackUser", new[] { "DepartmentId" });
            DropIndex("dbo.LampblackUser", new[] { "Id" });
            DropIndex("dbo.UserRole", new[] { "RoleId" });
            DropIndex("dbo.UserRole", new[] { "UserId" });
            DropIndex("dbo.UserPermission", new[] { "UserId" });
            DropIndex("dbo.UserPermission", new[] { "PermissionId" });
            DropIndex("dbo.RolePermission", new[] { "RoleId" });
            DropIndex("dbo.RolePermission", new[] { "PermissionId" });
            DropIndex("dbo.SysConfigProtocolCommands", new[] { "ProtocolCommand_Id" });
            DropIndex("dbo.SysConfigProtocolCommands", new[] { "SysConfig_Id" });
            DropIndex("dbo.CommandDataProtocolCommands", new[] { "ProtocolCommand_Id" });
            DropIndex("dbo.CommandDataProtocolCommands", new[] { "CommandData_Id" });
            DropIndex("dbo.FirmwareFirmwareSets", new[] { "FirmwareSet_Id" });
            DropIndex("dbo.FirmwareFirmwareSets", new[] { "Firmware_Id" });
            DropIndex("dbo.UserConfigs", new[] { "DomainId" });
            DropIndex("dbo.UserConfigs", new[] { "ParentUserConfigId" });
            DropIndex("dbo.CommandTasks", new[] { "DomainId" });
            DropIndex("dbo.CommandTasks", new[] { "TaskDeviceId" });
            DropIndex("dbo.RunningTimes", new[] { "DomainId" });
            DropIndex("dbo.RunningTimes", new[] { "DeviceId" });
            DropIndex("dbo.RunningTimes", new[] { "ProjectId" });
            DropIndex("dbo.Photos", new[] { "PhtotDevice_Id" });
            DropIndex("dbo.Photos", new[] { "DomainId" });
            DropIndex("dbo.Photos", new[] { "PhotoTypeId" });
            DropIndex("dbo.Photos", new[] { "DeviceId" });
            DropIndex("dbo.ProtocolDatas", new[] { "CommandTask_Id" });
            DropIndex("dbo.ProtocolDatas", new[] { "DomainId" });
            DropIndex("dbo.ProtocolDatas", new[] { "ProtocolId" });
            DropIndex("dbo.ProtocolDatas", new[] { "DeviceId" });
            DropIndex("dbo.MonitorDatas", new[] { "DomainId" });
            DropIndex("dbo.MonitorDatas", new[] { "ProjectId" });
            DropIndex("dbo.MonitorDatas", new[] { "CommandDataId" });
            DropIndex("dbo.MonitorDatas", new[] { "ProtocolDataId" });
            DropIndex("dbo.Modules", new[] { "DomainId" });
            DropIndex("dbo.Modules", new[] { "PermissionId" });
            DropIndex("dbo.Modules", new[] { "ParentModuleId" });
            DropIndex("dbo.WdRoles", new[] { "DomainId" });
            DropIndex("dbo.WdRoles", new[] { "ParentRoleId" });
            DropIndex("dbo.Permissions", new[] { "DomainId" });
            DropIndex("dbo.Permissions", new[] { "ParentPermissionId" });
            DropIndex("dbo.WdUser", new[] { "DomainId" });
            DropIndex("dbo.WdUser", new[] { "LoginName" });
            DropIndex("dbo.DeviceMaintenances", new[] { "DomainId" });
            DropIndex("dbo.DeviceMaintenances", new[] { "DeviceId" });
            DropIndex("dbo.DeviceMaintenances", new[] { "MaintenanceUserId" });
            DropIndex("dbo.Departments", new[] { "DomainId" });
            DropIndex("dbo.DataStatistics", new[] { "DomainId" });
            DropIndex("dbo.DataStatistics", new[] { "DeviceId" });
            DropIndex("dbo.DataStatistics", new[] { "ProjectId" });
            DropIndex("dbo.DataStatistics", new[] { "CommandDataId" });
            DropIndex("dbo.LampblackDeviceModels", new[] { "DomainId" });
            DropIndex("dbo.CateringCompanies", new[] { "DomainId" });
            DropIndex("dbo.UserDictionaries", new[] { "DomainId" });
            DropIndex("dbo.UserDictionaries", new[] { "ParentDictionaryId" });
            DropIndex("dbo.Project", new[] { "DomainId" });
            DropIndex("dbo.ProtocolStructures", new[] { "ProtocolId" });
            DropIndex("dbo.SysConfigs", new[] { "ParentSysConfigId" });
            DropIndex("dbo.CommandDefinitions", new[] { "Command_Id" });
            DropIndex("dbo.ProtocolCommands", new[] { "ProtocolId" });
            DropIndex("dbo.Protocols", new[] { "Firmware_Id" });
            DropIndex("dbo.Protocols", new[] { "SubFieldId" });
            DropIndex("dbo.Protocols", new[] { "FieldId" });
            DropIndex("dbo.DeviceTypes", new[] { "SubFieldId" });
            DropIndex("dbo.DeviceTypes", new[] { "FieldId" });
            DropIndex("dbo.SysDictionaries", new[] { "ParentDictionaryId" });
            DropIndex("dbo.Cameras", new[] { "DomainId" });
            DropIndex("dbo.Cameras", new[] { "AccessTypeId" });
            DropIndex("dbo.Device", new[] { "DomainId" });
            DropIndex("dbo.Device", new[] { "CameraId" });
            DropIndex("dbo.Device", new[] { "ProjectId" });
            DropIndex("dbo.Device", new[] { "FirmwareSetId" });
            DropIndex("dbo.Device", new[] { "OriginalDeviceId" });
            DropIndex("dbo.Device", new[] { "DeviceTypeId" });
            DropIndex("dbo.Alarms", new[] { "DomainId" });
            DropIndex("dbo.Alarms", new[] { "AlarmDeviceId" });
            DropTable("dbo.HotelRestaurant");
            DropTable("dbo.RestaurantDevice");
            DropTable("dbo.ParticulateMatterProject");
            DropTable("dbo.LampblackUser");
            DropTable("dbo.UserRole");
            DropTable("dbo.UserPermission");
            DropTable("dbo.RolePermission");
            DropTable("dbo.SysConfigProtocolCommands");
            DropTable("dbo.CommandDataProtocolCommands");
            DropTable("dbo.FirmwareFirmwareSets");
            DropTable("dbo.UserConfigs");
            DropTable("dbo.CommandTasks");
            DropTable("dbo.RunningTimes");
            DropTable("dbo.Photos");
            DropTable("dbo.ProtocolDatas");
            DropTable("dbo.MonitorDatas");
            DropTable("dbo.Modules");
            DropTable("dbo.WdRoles");
            DropTable("dbo.Permissions");
            DropTable("dbo.WdUser");
            DropTable("dbo.DeviceMaintenances");
            DropTable("dbo.Departments");
            DropTable("dbo.DataStatistics");
            DropTable("dbo.LampblackDeviceModels");
            DropTable("dbo.CateringCompanies");
            DropTable("dbo.UserDictionaries");
            DropTable("dbo.Project");
            DropTable("dbo.ProtocolStructures");
            DropTable("dbo.SysConfigs");
            DropTable("dbo.CommandDefinitions");
            DropTable("dbo.CommandDatas");
            DropTable("dbo.ProtocolCommands");
            DropTable("dbo.Protocols");
            DropTable("dbo.Firmwares");
            DropTable("dbo.FirmwareSets");
            DropTable("dbo.DeviceTypes");
            DropTable("dbo.Domains");
            DropTable("dbo.SysDictionaries");
            DropTable("dbo.Cameras");
            DropTable("dbo.Device");
            DropTable("dbo.Alarms");
        }
    }
}
