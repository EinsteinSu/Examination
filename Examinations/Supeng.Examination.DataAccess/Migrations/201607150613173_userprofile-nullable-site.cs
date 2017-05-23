namespace Supeng.Examination.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userprofilenullablesite : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserProfiles", "SiteId", "dbo.Sites");
            DropIndex("dbo.UserProfiles", new[] { "SiteId" });
            AlterColumn("dbo.UserProfiles", "SiteId", c => c.Int());
            CreateIndex("dbo.UserProfiles", "SiteId");
            AddForeignKey("dbo.UserProfiles", "SiteId", "dbo.Sites", "SiteId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserProfiles", "SiteId", "dbo.Sites");
            DropIndex("dbo.UserProfiles", new[] { "SiteId" });
            AlterColumn("dbo.UserProfiles", "SiteId", c => c.Int(nullable: false));
            CreateIndex("dbo.UserProfiles", "SiteId");
            AddForeignKey("dbo.UserProfiles", "SiteId", "dbo.Sites", "SiteId", cascadeDelete: true);
        }
    }
}
