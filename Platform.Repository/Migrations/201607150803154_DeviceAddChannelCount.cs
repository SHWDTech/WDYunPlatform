namespace SHWD.Platform.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeviceAddChannelCount : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RestaurantDevice1", "ChannelCount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RestaurantDevice1", "ChannelCount");
        }
    }
}
