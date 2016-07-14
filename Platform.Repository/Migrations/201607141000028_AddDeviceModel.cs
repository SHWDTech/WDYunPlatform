namespace SHWD.Platform.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDeviceModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DeviceModels",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 200),
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
                "dbo.RestaurantDevice1",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DeviceModelId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DeviceModels", t => t.DeviceModelId)
                .Index(t => t.DeviceModelId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RestaurantDevice1", "DeviceModelId", "dbo.DeviceModels");
            DropForeignKey("dbo.DeviceModels", "DomainId", "dbo.Domains");
            DropIndex("dbo.RestaurantDevice1", new[] { "DeviceModelId" });
            DropIndex("dbo.DeviceModels", new[] { "DomainId" });
            DropTable("dbo.RestaurantDevice1");
            DropTable("dbo.DeviceModels");
        }
    }
}
