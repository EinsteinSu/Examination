namespace Supeng.Examination.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class opitionalanswer1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OpitionalAnswers", "Question_QuestionId", "dbo.Questions");
            DropIndex("dbo.OpitionalAnswers", new[] { "Question_QuestionId" });
            RenameColumn(table: "dbo.OpitionalAnswers", name: "Question_QuestionId", newName: "QuestionId");
            AlterColumn("dbo.OpitionalAnswers", "QuestionId", c => c.Int(nullable: false));
            CreateIndex("dbo.OpitionalAnswers", "QuestionId");
            AddForeignKey("dbo.OpitionalAnswers", "QuestionId", "dbo.Questions", "QuestionId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OpitionalAnswers", "QuestionId", "dbo.Questions");
            DropIndex("dbo.OpitionalAnswers", new[] { "QuestionId" });
            AlterColumn("dbo.OpitionalAnswers", "QuestionId", c => c.Int());
            RenameColumn(table: "dbo.OpitionalAnswers", name: "QuestionId", newName: "Question_QuestionId");
            CreateIndex("dbo.OpitionalAnswers", "Question_QuestionId");
            AddForeignKey("dbo.OpitionalAnswers", "Question_QuestionId", "dbo.Questions", "QuestionId");
        }
    }
}
