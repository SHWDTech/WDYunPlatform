namespace SHWD.Platform.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserRoleVisiable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WdUser", "IsVisiable", c => c.Boolean(nullable: false, defaultValue: true));
            AddColumn("dbo.WdRoles", "IsVisiable", c => c.Boolean(nullable: false, defaultValue: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.WdRoles", "IsVisiable");
            DropColumn("dbo.WdUser", "IsVisiable");
        }
    }
}
