namespace SHWD.Platform.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyDeviceModel : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.DeviceModels", newName: "LampblackDeviceModels");
            AddColumn("dbo.LampblackDeviceModels", "Fail", c => c.Int(nullable: false));
            AddColumn("dbo.LampblackDeviceModels", "Worse", c => c.Int(nullable: false));
            AddColumn("dbo.LampblackDeviceModels", "Qualified", c => c.Int(nullable: false));
            AddColumn("dbo.LampblackDeviceModels", "Good", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.LampblackDeviceModels", "Good");
            DropColumn("dbo.LampblackDeviceModels", "Qualified");
            DropColumn("dbo.LampblackDeviceModels", "Worse");
            DropColumn("dbo.LampblackDeviceModels", "Fail");
            RenameTable(name: "dbo.LampblackDeviceModels", newName: "DeviceModels");
        }
    }
}
