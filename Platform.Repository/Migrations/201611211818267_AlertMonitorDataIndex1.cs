namespace SHWD.Platform.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlertMonitorDataIndex1 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.MonitorDatas", "Ix_UpdateTime");
        }
        
        public override void Down()
        {
            CreateIndex("dbo.MonitorDatas", "UpdateTime", name: "Ix_UpdateTime");
        }
    }
}
