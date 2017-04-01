namespace SHWD.Platform.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableLampblackRecord : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LampblackRecords",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ProjectIdentity = c.Long(nullable: false),
                        DeviceIdentity = c.Long(nullable: false),
                        ProtocolId = c.Long(nullable: false),
                        CleanerSwitch = c.Boolean(nullable: false),
                        CleanerCurrent = c.Int(nullable: false),
                        FanSwitch = c.Boolean(nullable: false),
                        FanCurrent = c.Int(nullable: false),
                        LampblackIn = c.Int(nullable: false),
                        LampblackOut = c.Int(nullable: false),
                        RecordDateTime = c.DateTime(nullable: false),
                        DomainId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id, clustered: false)
                .Index(t => new { t.ProjectIdentity, t.DeviceIdentity, t.RecordDateTime }, clustered: true, name: "Ix_Project_Device_RecordDateTime");
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.LampblackRecords", "Ix_Project_Device_RecordDateTime");
            DropTable("dbo.LampblackRecords");
        }
    }
}
