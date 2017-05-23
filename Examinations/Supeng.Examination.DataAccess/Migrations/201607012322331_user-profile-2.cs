namespace Supeng.Examination.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userprofile2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserProfiles", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserProfiles", "Description");
        }
    }
}
