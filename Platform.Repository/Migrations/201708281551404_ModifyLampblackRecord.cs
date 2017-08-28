namespace SHWD.Platform.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyLampblackRecord : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.LampblackRecords", "Ix_Project_Device_RecordDateTime");
            AddColumn("dbo.LampblackRecords", "Channel", c => c.Int(nullable: false));
            CreateIndex("dbo.LampblackRecords", new[] { "DomainId", "ProjectIdentity", "DeviceIdentity", "RecordDateTime" }, clustered: true, name: "Ix_Domain_Project_Device_RecordDateTime");
        }
        
        public override void Down()
        {
            DropIndex("dbo.LampblackRecords", "Ix_Domain_Project_Device_RecordDateTime");
            DropColumn("dbo.LampblackRecords", "Channel");
            CreateIndex("dbo.LampblackRecords", new[] { "ProjectIdentity", "DeviceIdentity", "RecordDateTime" }, clustered: true, name: "Ix_Project_Device_RecordDateTime");
        }
    }
}
