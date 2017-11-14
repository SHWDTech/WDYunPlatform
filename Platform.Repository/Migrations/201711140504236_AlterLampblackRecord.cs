namespace SHWD.Platform.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterLampblackRecord : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.LampblackRecords", "CleanerCurrent", c => c.Double(nullable: false));
            AlterColumn("dbo.LampblackRecords", "FanCurrent", c => c.Double(nullable: false));
            AlterColumn("dbo.LampblackRecords", "LampblackIn", c => c.Double(nullable: false));
            AlterColumn("dbo.LampblackRecords", "LampblackOut", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.LampblackRecords", "LampblackOut", c => c.Int(nullable: false));
            AlterColumn("dbo.LampblackRecords", "LampblackIn", c => c.Int(nullable: false));
            AlterColumn("dbo.LampblackRecords", "FanCurrent", c => c.Int(nullable: false));
            AlterColumn("dbo.LampblackRecords", "CleanerCurrent", c => c.Int(nullable: false));
        }
    }
}
