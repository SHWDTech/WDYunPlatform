namespace SHWD.Platform.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModigyDeviceStartDate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Device", "StartTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Device", "StartTime", c => c.DateTime(nullable: false));
        }
    }
}
