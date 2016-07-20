namespace SHWD.Platform.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyProject : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Project", "Longitude", c => c.Single());
            AlterColumn("dbo.Project", "Latitude", c => c.Single());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Project", "Latitude", c => c.Single(nullable: false));
            AlterColumn("dbo.Project", "Longitude", c => c.Single(nullable: false));
        }
    }
}
