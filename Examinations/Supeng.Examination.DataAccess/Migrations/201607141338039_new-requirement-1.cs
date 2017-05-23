namespace Supeng.Examination.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newrequirement1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.QuestionCategories",
                c => new
                    {
                        QuestionCategoryId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.QuestionCategoryId);
            
            CreateTable(
                "dbo.Sites",
                c => new
                    {
                        SiteId = c.Int(nullable: false, identity: true),
                        SiteCode = c.String(nullable: false, maxLength: 3),
                        Name = c.String(nullable: false, maxLength: 20),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.SiteId);
            
            AddColumn("dbo.Questions", "CategoryId", c => c.Int(nullable: false));
            AddColumn("dbo.UserProfiles", "SiteId", c => c.Int(nullable: false));
            CreateIndex("dbo.Questions", "CategoryId");
            CreateIndex("dbo.UserProfiles", "SiteId");
            AddForeignKey("dbo.Questions", "CategoryId", "dbo.QuestionCategories", "QuestionCategoryId", cascadeDelete: true);
            AddForeignKey("dbo.UserProfiles", "SiteId", "dbo.Sites", "SiteId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserProfiles", "SiteId", "dbo.Sites");
            DropForeignKey("dbo.Questions", "CategoryId", "dbo.QuestionCategories");
            DropIndex("dbo.UserProfiles", new[] { "SiteId" });
            DropIndex("dbo.Questions", new[] { "CategoryId" });
            DropColumn("dbo.UserProfiles", "SiteId");
            DropColumn("dbo.Questions", "CategoryId");
            DropTable("dbo.Sites");
            DropTable("dbo.QuestionCategories");
        }
    }
}
