namespace SHWD.Platform.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDataChannel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MonitorDatas", "DataChannel", c => c.Short(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MonitorDatas", "DataChannel");
        }
    }
}
