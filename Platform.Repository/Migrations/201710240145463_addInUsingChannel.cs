namespace SHWD.Platform.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addInUsingChannel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RestaurantDevice", "InUsingChannelString", c => c.String());
            DropColumn("dbo.RestaurantDevice", "ChannelCount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RestaurantDevice", "ChannelCount", c => c.Int(nullable: false));
            DropColumn("dbo.RestaurantDevice", "InUsingChannelString");
        }
    }
}
