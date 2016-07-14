namespace SHWD.Platform.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStatistics : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DataStatistics",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        CommandDataId = c.Guid(nullable: false),
                        ProjectId = c.Guid(),
                        DoubleValue = c.Double(),
                        BooleanValue = c.Boolean(),
                        IntegerValue = c.Int(),
                        UpdateTime = c.DateTime(nullable: false),
                        Type = c.Byte(nullable: false),
                        DomainId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CommandDatas", t => t.CommandDataId)
                .ForeignKey("dbo.Domains", t => t.DomainId)
                .ForeignKey("dbo.Project", t => t.ProjectId)
                .Index(t => t.CommandDataId)
                .Index(t => t.ProjectId)
                .Index(t => t.DomainId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DataStatistics", "ProjectId", "dbo.Project");
            DropForeignKey("dbo.DataStatistics", "DomainId", "dbo.Domains");
            DropForeignKey("dbo.DataStatistics", "CommandDataId", "dbo.CommandDatas");
            DropIndex("dbo.DataStatistics", new[] { "DomainId" });
            DropIndex("dbo.DataStatistics", new[] { "ProjectId" });
            DropIndex("dbo.DataStatistics", new[] { "CommandDataId" });
            DropTable("dbo.DataStatistics");
        }
    }
}
