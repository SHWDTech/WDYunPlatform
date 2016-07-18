namespace SHWD.Platform.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReMapDevice : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Device", new[] { "DeviceModelId" });
            AddColumn("dbo.RestaurantDevice", "DeviceModelId", c => c.Guid(nullable: false));
            AddColumn("dbo.RestaurantDevice", "ChannelCount", c => c.Int(nullable: false));
            CreateIndex("dbo.RestaurantDevice", "DeviceModelId");
            DropColumn("dbo.Device", "DeviceModelId");
            DropColumn("dbo.Device", "ChannelCount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Device", "ChannelCount", c => c.Int());
            AddColumn("dbo.Device", "DeviceModelId", c => c.Guid());
            DropIndex("dbo.RestaurantDevice", new[] { "DeviceModelId" });
            DropColumn("dbo.RestaurantDevice", "ChannelCount");
            DropColumn("dbo.RestaurantDevice", "DeviceModelId");
            CreateIndex("dbo.Device", "DeviceModelId");
        }
    }
}
