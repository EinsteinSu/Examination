namespace Supeng.Examination.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TestPaperQuestions",
                c => new
                    {
                        TestPaperQuestionId = c.Int(nullable: false, identity: true),
                        TestPaperId = c.Int(nullable: false),
                        QuestionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TestPaperQuestionId)
                .ForeignKey("dbo.Questions", t => t.QuestionId, cascadeDelete: true)
                .ForeignKey("dbo.TestPapers", t => t.TestPaperId, cascadeDelete: true)
                .Index(t => t.TestPaperId)
                .Index(t => t.QuestionId);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        QuestionId = c.Int(nullable: false, identity: true),
                        Type = c.Int(nullable: false),
                        Content = c.String(nullable: false),
                        Score = c.Int(nullable: false),
                        CorrectAnswer = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.QuestionId);
            
            CreateTable(
                "dbo.OpitionalAnswers",
                c => new
                    {
                        AnswerId = c.Int(nullable: false, identity: true),
                        OrderNumber = c.String(nullable: false, maxLength: 2),
                        Content = c.String(nullable: false),
                        Question_QuestionId = c.Int(),
                    })
                .PrimaryKey(t => t.AnswerId)
                .ForeignKey("dbo.Questions", t => t.Question_QuestionId)
                .Index(t => t.Question_QuestionId);
            
            CreateTable(
                "dbo.TestPapers",
                c => new
                    {
                        TestPaperId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                        QuestionCount = c.Int(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.TestPaperId);
            
            CreateTable(
                "dbo.UserAnswers",
                c => new
                    {
                        UserAnswerId = c.Int(nullable: false, identity: true),
                        QuestionId = c.Int(nullable: false),
                        Answer = c.String(),
                        UserTestId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserAnswerId)
                .ForeignKey("dbo.Questions", t => t.QuestionId, cascadeDelete: true)
                .ForeignKey("dbo.UserTests", t => t.UserTestId, cascadeDelete: true)
                .Index(t => t.QuestionId)
                .Index(t => t.UserTestId);
            
            CreateTable(
                "dbo.UserTests",
                c => new
                    {
                        UserTestId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        TestId = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.UserTestId)
                .ForeignKey("dbo.Tests", t => t.TestId, cascadeDelete: true)
                .ForeignKey("dbo.UserProfiles", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.TestId);
            
            CreateTable(
                "dbo.Tests",
                c => new
                    {
                        TestId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 100),
                        StarTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        TestPaperId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TestId)
                .ForeignKey("dbo.TestPapers", t => t.TestPaperId, cascadeDelete: true)
                .Index(t => t.TestPaperId);
            
            CreateTable(
                "dbo.UserProfiles",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserCode = c.String(maxLength: 6),
                        Name = c.String(maxLength: 20),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserAnswers", "UserTestId", "dbo.UserTests");
            DropForeignKey("dbo.UserTests", "UserId", "dbo.UserProfiles");
            DropForeignKey("dbo.UserTests", "TestId", "dbo.Tests");
            DropForeignKey("dbo.Tests", "TestPaperId", "dbo.TestPapers");
            DropForeignKey("dbo.UserAnswers", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.TestPaperQuestions", "TestPaperId", "dbo.TestPapers");
            DropForeignKey("dbo.TestPaperQuestions", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.OpitionalAnswers", "Question_QuestionId", "dbo.Questions");
            DropIndex("dbo.Tests", new[] { "TestPaperId" });
            DropIndex("dbo.UserTests", new[] { "TestId" });
            DropIndex("dbo.UserTests", new[] { "UserId" });
            DropIndex("dbo.UserAnswers", new[] { "UserTestId" });
            DropIndex("dbo.UserAnswers", new[] { "QuestionId" });
            DropIndex("dbo.OpitionalAnswers", new[] { "Question_QuestionId" });
            DropIndex("dbo.TestPaperQuestions", new[] { "QuestionId" });
            DropIndex("dbo.TestPaperQuestions", new[] { "TestPaperId" });
            DropTable("dbo.UserProfiles");
            DropTable("dbo.Tests");
            DropTable("dbo.UserTests");
            DropTable("dbo.UserAnswers");
            DropTable("dbo.TestPapers");
            DropTable("dbo.OpitionalAnswers");
            DropTable("dbo.Questions");
            DropTable("dbo.TestPaperQuestions");
        }
    }
}
