namespace SHWD.Platform.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyColumnNameOfPlatformAccess : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PlatformAccesses", "TargetGuid", c => c.Guid(nullable: false));
            DropColumn("dbo.PlatformAccesses", "DeviceGuid");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PlatformAccesses", "DeviceGuid", c => c.Guid(nullable: false));
            DropColumn("dbo.PlatformAccesses", "TargetGuid");
        }
    }
}
