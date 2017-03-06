namespace SHWD.Platform.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveDomainFromIndex : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.MonitorDatas", "Ix_Domain_Project_ProtocolData_UpdateTime");
            CreateIndex("dbo.MonitorDatas", new[] { "ProjectId", "ProtocolDataId", "UpdateTime" }, name: "Ix_Project_ProtocolData_UpdateTime");
            CreateIndex("dbo.MonitorDatas", "DomainId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.MonitorDatas", new[] { "DomainId" });
            DropIndex("dbo.MonitorDatas", "Ix_Project_ProtocolData_UpdateTime");
            CreateIndex("dbo.MonitorDatas", new[] { "DomainId", "ProjectId", "ProtocolDataId", "UpdateTime" }, name: "Ix_Domain_Project_ProtocolData_UpdateTime");
        }
    }
}
