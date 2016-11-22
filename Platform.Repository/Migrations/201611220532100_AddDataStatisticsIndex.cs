namespace SHWD.Platform.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDataStatisticsIndex : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.DataStatistics", new[] { "ProjectId" });
            CreateIndex("dbo.DataStatistics", new[] { "ProjectId", "UpdateTime" }, name: "Ix_Project_Device_UpdateTime");
        }
        
        public override void Down()
        {
            DropIndex("dbo.DataStatistics", "Ix_Project_Device_UpdateTime");
            CreateIndex("dbo.DataStatistics", "ProjectId");
        }
    }
}
