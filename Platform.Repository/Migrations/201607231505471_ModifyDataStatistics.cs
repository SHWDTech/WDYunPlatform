namespace SHWD.Platform.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyDataStatistics : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DataStatistics", "DeviceId", c => c.Guid(nullable: false));
            CreateIndex("dbo.DataStatistics", "DeviceId");
            AddForeignKey("dbo.DataStatistics", "DeviceId", "dbo.Device", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DataStatistics", "DeviceId", "dbo.Device");
            DropIndex("dbo.DataStatistics", new[] { "DeviceId" });
            DropColumn("dbo.DataStatistics", "DeviceId");
        }
    }
}
