namespace Supeng.Examination.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userprofile1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserProfiles", "Gender", c => c.Int(nullable: false));
            AddColumn("dbo.UserProfiles", "Mobile", c => c.String(maxLength: 11));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserProfiles", "Mobile");
            DropColumn("dbo.UserProfiles", "Gender");
        }
    }
}
