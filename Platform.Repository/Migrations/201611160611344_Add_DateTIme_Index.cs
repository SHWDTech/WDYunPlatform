namespace SHWD.Platform.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_DateTIme_Index : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.MonitorDatas", new[] { "ProtocolDataId" });
            DropIndex("dbo.ProtocolDatas", new[] { "DeviceId" });
            CreateIndex("dbo.MonitorDatas", new[] { "ProtocolDataId", "UpdateTime" }, name: "Ix_ProtocolData_UpdateTime");
            CreateIndex("dbo.MonitorDatas", "UpdateTime", name: "Ix_UpdateTime");
            CreateIndex("dbo.ProtocolDatas", new[] { "DeviceId", "UpdateTime" }, name: "Ix_Device_UpdateTime");
            CreateIndex("dbo.ProtocolDatas", "UpdateTime", name: "Ix_UpdateTime");
        }
        
        public override void Down()
        {
            DropIndex("dbo.ProtocolDatas", "Ix_UpdateTime");
            DropIndex("dbo.ProtocolDatas", "Ix_Device_UpdateTime");
            DropIndex("dbo.MonitorDatas", "Ix_UpdateTime");
            DropIndex("dbo.MonitorDatas", "Ix_ProtocolData_UpdateTime");
            CreateIndex("dbo.ProtocolDatas", "DeviceId");
            CreateIndex("dbo.MonitorDatas", "ProtocolDataId");
        }
    }
}
