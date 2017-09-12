namespace SHWD.Platform.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPlatfromAccess : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PlatformAccesses",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        PlatformName = c.String(maxLength: 256),
                        DeviceGuid = c.Guid(nullable: false),
                        AccessTime = c.DateTime(nullable: false),
                        DomainId = c.Guid(nullable: false),
                        CreateDateTime = c.DateTime(nullable: false),
                        CreateUserId = c.Guid(nullable: false),
                        LastUpdateDateTime = c.DateTime(),
                        LastUpdateUserId = c.Guid(),
                        IsDeleted = c.Boolean(nullable: false),
                        IsEnabled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id, clustered:false)
                .ForeignKey("dbo.Domains", t => t.DomainId)
                .Index(t => t.PlatformName, clustered: true, name: "Index_PlatformName")
                .Index(t => t.DomainId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PlatformAccesses", "DomainId", "dbo.Domains");
            DropIndex("dbo.PlatformAccesses", new[] { "DomainId" });
            DropIndex("dbo.PlatformAccesses", "Index_PlatformName");
            DropTable("dbo.PlatformAccesses");
        }
    }
}
