namespace Supeng.Examination.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class testpaperformula1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TestPaperFormulas",
                c => new
                    {
                        FormulaId = c.Int(nullable: false, identity: true),
                        CategoryId = c.Int(nullable: false),
                        QuestionCount = c.Int(nullable: false),
                        TestPaper_TestPaperId = c.Int(),
                    })
                .PrimaryKey(t => t.FormulaId)
                .ForeignKey("dbo.QuestionCategories", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.TestPapers", t => t.TestPaper_TestPaperId)
                .Index(t => t.CategoryId)
                .Index(t => t.TestPaper_TestPaperId);
            
            DropColumn("dbo.TestPapers", "QuestionCount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TestPapers", "QuestionCount", c => c.Int(nullable: false));
            DropForeignKey("dbo.TestPaperFormulas", "TestPaper_TestPaperId", "dbo.TestPapers");
            DropForeignKey("dbo.TestPaperFormulas", "CategoryId", "dbo.QuestionCategories");
            DropIndex("dbo.TestPaperFormulas", new[] { "TestPaper_TestPaperId" });
            DropIndex("dbo.TestPaperFormulas", new[] { "CategoryId" });
            DropTable("dbo.TestPaperFormulas");
        }
    }
}
