namespace SHWD.Platform.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRunningTIme : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RunningTimes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        RunningTimeTicks = c.Long(nullable: false),
                        Type = c.Byte(nullable: false),
                        ProjectId = c.Guid(nullable: false),
                        DeviceId = c.Guid(nullable: false),
                        UpdateTime = c.DateTime(nullable: false),
                        DomainId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Device", t => t.DeviceId)
                .ForeignKey("dbo.Domains", t => t.DomainId)
                .ForeignKey("dbo.Project", t => t.ProjectId)
                .Index(t => t.ProjectId)
                .Index(t => t.DeviceId)
                .Index(t => t.DomainId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RunningTimes", "ProjectId", "dbo.Project");
            DropForeignKey("dbo.RunningTimes", "DomainId", "dbo.Domains");
            DropForeignKey("dbo.RunningTimes", "DeviceId", "dbo.Device");
            DropIndex("dbo.RunningTimes", new[] { "DomainId" });
            DropIndex("dbo.RunningTimes", new[] { "DeviceId" });
            DropIndex("dbo.RunningTimes", new[] { "ProjectId" });
            DropTable("dbo.RunningTimes");
        }
    }
}
