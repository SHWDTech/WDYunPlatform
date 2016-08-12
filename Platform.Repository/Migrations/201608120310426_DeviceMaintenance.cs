namespace SHWD.Platform.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeviceMaintenance : DbMigration
    {
        public override void Up()
        {
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DeviceMaintenances", "MaintenanceUserId", "dbo.WdUser");
            DropForeignKey("dbo.DeviceMaintenances", "DomainId", "dbo.Domains");
            DropForeignKey("dbo.DeviceMaintenances", "DeviceId", "dbo.Device");
            DropIndex("dbo.DeviceMaintenances", new[] { "DomainId" });
            DropIndex("dbo.DeviceMaintenances", new[] { "DeviceId" });
            DropIndex("dbo.DeviceMaintenances", new[] { "MaintenanceUserId" });
            DropTable("dbo.DeviceMaintenances");
        }
    }
}
