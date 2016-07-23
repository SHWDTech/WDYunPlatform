namespace SHWD.Platform.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModiryDataStatistic : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DataStatistics", "DataChannel", c => c.Short(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DataStatistics", "DataChannel");
        }
    }
}
