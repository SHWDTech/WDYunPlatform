namespace SHWD.Platform.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProtocolFrimware : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.FirmwareFirmwareSets", newName: "FirmwareSetFirmware");
            DropForeignKey("dbo.Protocols", "Firmware_Id", "dbo.Firmwares");
            DropIndex("dbo.Protocols", new[] { "Firmware_Id" });
            RenameColumn(table: "dbo.FirmwareSetFirmware", name: "Firmware_Id", newName: "FirmwareSetId");
            RenameColumn(table: "dbo.FirmwareSetFirmware", name: "FirmwareSet_Id", newName: "FirmwareId");
            RenameIndex(table: "dbo.FirmwareSetFirmware", name: "IX_FirmwareSet_Id", newName: "IX_FirmwareId");
            RenameIndex(table: "dbo.FirmwareSetFirmware", name: "IX_Firmware_Id", newName: "IX_FirmwareSetId");
            DropPrimaryKey("dbo.FirmwareSetFirmware");
            CreateTable(
                "dbo.ProtocolFirmware",
                c => new
                    {
                        ProtocolId = c.Guid(nullable: false),
                        FirmwareId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProtocolId, t.FirmwareId })
                .ForeignKey("dbo.Protocols", t => t.ProtocolId, cascadeDelete: true)
                .ForeignKey("dbo.Firmwares", t => t.FirmwareId, cascadeDelete: true)
                .Index(t => t.ProtocolId)
                .Index(t => t.FirmwareId);
            
            AddPrimaryKey("dbo.FirmwareSetFirmware", new[] { "FirmwareId", "FirmwareSetId" });
            DropColumn("dbo.Protocols", "Firmware_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Protocols", "Firmware_Id", c => c.Guid());
            DropForeignKey("dbo.ProtocolFirmware", "FirmwareId", "dbo.Firmwares");
            DropForeignKey("dbo.ProtocolFirmware", "ProtocolId", "dbo.Protocols");
            DropIndex("dbo.ProtocolFirmware", new[] { "FirmwareId" });
            DropIndex("dbo.ProtocolFirmware", new[] { "ProtocolId" });
            DropPrimaryKey("dbo.FirmwareSetFirmware");
            DropTable("dbo.ProtocolFirmware");
            AddPrimaryKey("dbo.FirmwareSetFirmware", new[] { "Firmware_Id", "FirmwareSet_Id" });
            RenameIndex(table: "dbo.FirmwareSetFirmware", name: "IX_FirmwareSetId", newName: "IX_Firmware_Id");
            RenameIndex(table: "dbo.FirmwareSetFirmware", name: "IX_FirmwareId", newName: "IX_FirmwareSet_Id");
            RenameColumn(table: "dbo.FirmwareSetFirmware", name: "FirmwareId", newName: "FirmwareSet_Id");
            RenameColumn(table: "dbo.FirmwareSetFirmware", name: "FirmwareSetId", newName: "Firmware_Id");
            CreateIndex("dbo.Protocols", "Firmware_Id");
            AddForeignKey("dbo.Protocols", "Firmware_Id", "dbo.Firmwares", "Id");
            RenameTable(name: "dbo.FirmwareSetFirmware", newName: "FirmwareFirmwareSets");
        }
    }
}
