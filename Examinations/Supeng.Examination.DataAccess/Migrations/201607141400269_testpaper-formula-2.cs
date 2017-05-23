namespace Supeng.Examination.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class testpaperformula2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TestPaperFormulas", "TestPaper_TestPaperId", "dbo.TestPapers");
            DropIndex("dbo.TestPaperFormulas", new[] { "TestPaper_TestPaperId" });
            RenameColumn(table: "dbo.TestPaperFormulas", name: "TestPaper_TestPaperId", newName: "TestPaperId");
            AlterColumn("dbo.TestPaperFormulas", "TestPaperId", c => c.Int(nullable: false));
            CreateIndex("dbo.TestPaperFormulas", "TestPaperId");
            AddForeignKey("dbo.TestPaperFormulas", "TestPaperId", "dbo.TestPapers", "TestPaperId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TestPaperFormulas", "TestPaperId", "dbo.TestPapers");
            DropIndex("dbo.TestPaperFormulas", new[] { "TestPaperId" });
            AlterColumn("dbo.TestPaperFormulas", "TestPaperId", c => c.Int());
            RenameColumn(table: "dbo.TestPaperFormulas", name: "TestPaperId", newName: "TestPaper_TestPaperId");
            CreateIndex("dbo.TestPaperFormulas", "TestPaper_TestPaperId");
            AddForeignKey("dbo.TestPaperFormulas", "TestPaper_TestPaperId", "dbo.TestPapers", "TestPaperId");
        }
    }
}
