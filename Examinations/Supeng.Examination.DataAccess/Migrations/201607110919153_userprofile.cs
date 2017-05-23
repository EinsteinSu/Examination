namespace Supeng.Examination.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userprofile : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserProfiles", "UserCode", c => c.String(nullable: false, maxLength: 6));
            AlterColumn("dbo.UserProfiles", "Name", c => c.String(nullable: false, maxLength: 20));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserProfiles", "Name", c => c.String(maxLength: 20));
            AlterColumn("dbo.UserProfiles", "UserCode", c => c.String(maxLength: 6));
        }
    }
}
