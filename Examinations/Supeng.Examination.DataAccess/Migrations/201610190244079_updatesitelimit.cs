namespace Supeng.Examination.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatesitelimit : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Sites", "SiteCode", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.Sites", "Name", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Sites", "Name", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Sites", "SiteCode", c => c.String(nullable: false, maxLength: 3));
        }
    }
}
