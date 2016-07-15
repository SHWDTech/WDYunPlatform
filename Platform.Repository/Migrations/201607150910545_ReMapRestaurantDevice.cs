namespace SHWD.Platform.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReMapRestaurantDevice : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.RestaurantDevice1", new[] { "DeviceModelId" });
            AddColumn("dbo.Device", "DeviceModelId", c => c.Guid());
            AddColumn("dbo.Device", "ChannelCount", c => c.Int());
            CreateIndex("dbo.Device", "DeviceModelId");
            DropTable("dbo.RestaurantDevice1");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.RestaurantDevice1",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DeviceModelId = c.Guid(nullable: false),
                        ChannelCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropIndex("dbo.Device", new[] { "DeviceModelId" });
            DropColumn("dbo.Device", "ChannelCount");
            DropColumn("dbo.Device", "DeviceModelId");
            CreateIndex("dbo.RestaurantDevice1", "DeviceModelId");
        }
    }
}
