namespace SHWD.Platform.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlertMonitorDataIndex : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.MonitorDatas", "Ix_ProtocolData_UpdateTime");
            DropIndex("dbo.MonitorDatas", new[] { "ProjectId" });
            CreateIndex("dbo.MonitorDatas", new[] { "ProjectId", "ProtocolDataId", "UpdateTime" }, name: "Ix_Project_ProtocolData_UpdateTime");
        }
        
        public override void Down()
        {
            DropIndex("dbo.MonitorDatas", "Ix_Project_ProtocolData_UpdateTime");
            CreateIndex("dbo.MonitorDatas", "ProjectId");
            CreateIndex("dbo.MonitorDatas", new[] { "ProtocolDataId", "UpdateTime" }, name: "Ix_ProtocolData_UpdateTime");
        }
    }
}
