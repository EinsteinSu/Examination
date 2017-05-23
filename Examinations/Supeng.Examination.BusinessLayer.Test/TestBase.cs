using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Supeng.Examination.DataAccess;
using Supeng.Examination.Model;

namespace Supeng.Examination.BusinessLayer.Test
{
    [TestClass]
    public class TestBase
    {
        protected readonly ExaDataContext Context = new ExaDataContext();

        [TestInitialize]
        public void Initialize()
        {
        }

        protected Model.Test CreateTest(string testName, bool startTimeIsNow = false)
        {
            var paper = AddTestPaper(20);

            var test = new Model.Test
            {
                Name = testName,
                StartTime = DateTime.Now.AddHours(1),
                EndTime = DateTime.Now.AddHours(2),
                TestPaperId = paper.TestPaperId
            };
            if (startTimeIsNow)
                test.StartTime = DateTime.Now.AddHours(-1);
            var error = new TestManager().Create(test);
            Assert.IsTrue(string.IsNullOrEmpty(error));
            return test;
        }

        protected Model.Test GenerateTestWithTester(string testName, int joinTesterCount, int testerCount = 20)
        {
            var test = CreateTest(testName);
            GenerateQuestionsAndAssociatWithTestPaper(test.TestPaperId);
            GenerateUsers(testerCount);
            var error = new TestManager().SaveSelectedTestUsers(test.TestId,
                0, Context.UserProfiles.Take(joinTesterCount).Select(s => s.UserId).ToList());
            Assert.IsTrue(string.IsNullOrEmpty(error));
            return test;
        }

        protected void GenerateQuestionsAndAssociatWithTestPaper(int testPaperId)
        {
            GenerateQuestions(100);
            var f1 = new TestPaperFormula
            {
                CategoryId = Context.QuestionCategories.ToList()[0].QuestionCategoryId,
                QuestionCount = 10,
            };
            new TestPaperManager().CreateFormula(testPaperId, f1);
            var f2 = new TestPaperFormula
            {
                CategoryId = Context.QuestionCategories.ToList()[1].QuestionCategoryId,
                QuestionCount = 20,
            };
            new TestPaperManager().CreateFormula(testPaperId, f2);
            var error = new TestPaperManager().GenerateTestPaper(testPaperId, true);
            Assert.IsTrue(string.IsNullOrEmpty(error));
            Assert.IsTrue(Context.TestPaperQuestions.Count(c => c.Question.CategoryId == f1.CategoryId) == f1.QuestionCount);
            Assert.IsTrue(Context.TestPaperQuestions.Count(c => c.Question.CategoryId == f2.CategoryId) == f2.QuestionCount);
            Assert.IsTrue(Context.TestPaperQuestions.Any(a => a.TestPaper.TestPaperId == testPaperId));
        }

        protected void GenerateSites(int count)
        {
            for (int i = 0; i < count; i++)
            {
                Context.Sites.Add(new Site
                {
                    SiteCode = i.ToString().PadLeft(3, '0'),
                    Name = "Site:" + (i + 1)
                });
            }
            Context.SaveChanges();
        }

        protected void GenerateQuestions(int count)
        {
            GenerateQuestionCategories(2);
            for (var i = 0; i < count; i++)
            {
                var category = Context.QuestionCategories.ToList()[i % 2];
                Context.Questions.Add(new Question
                {
                    Content = string.Format("Question {0}: Hello This is question {0}, Please choose an answer and click ok submit your answer. \r for multiple answer please choose one or more answers and submit your answer complete your test.", (i + 1)),
                    Type = QuestionType.多选,
                    Score = 2,
                    Category = category,
                    CorrectAnswer = "A",
                    OptionalAnswers = new List<OptionalAnswer>
                {
                    new OptionalAnswer{OrderNumber = "A",Content = "Answer A"},
                    new OptionalAnswer{OrderNumber = "B",Content = "Answer B"},
                    new OptionalAnswer{OrderNumber = "C",Content = "Answer C"},
                    new OptionalAnswer{OrderNumber = "D",Content = "Answer D"},
                }
                });
            }
            Context.SaveChanges();
        }

        protected void GenerateQuestionCategories(int count)
        {
            for (int i = 0; i < count; i++)
            {
                Context.QuestionCategories.Add(new QuestionCategory
                {
                    Name = "Category: " + (i + 1)
                });
            }
            Context.SaveChanges();
        }

        protected void GenerateUsers(int count)
        {
            var site = Context.Sites.Add(new Site { Name = "TestSite", SiteCode = "123" });
            for (int i = 0; i < count; i++)
            {
                var user = new UserProfile
                {
                    Name = string.Format("User: {0}", i + 1),
                    UserCode = (i + 1).ToString().PadLeft(3, '0'),
                    SiteId = site.SiteId,
                    Gender = 0,
                    Password = "123"
                };
                Context.UserProfiles.Add(user);
            }
            Context.SaveChanges();
        }

        protected TestPaper AddTestPaper(int questionCount)
        {
            var ex = new TestPaper
            {
                Name = "test",
                Description = "description"
            };
            Context.TestPapers.Add(ex);
            Context.SaveChanges();
            return ex;
        }

        [TestCleanup]
        public void Clean()
        {

            Context.Database.ExecuteSqlCommand("Delete from TechReports");
            Context.Database.ExecuteSqlCommand("Delete from SecurityRoleActions");
            Context.Database.ExecuteSqlCommand("Delete from SecurityRoles");
            Context.Database.ExecuteSqlCommand("Delete from SecurityActions");
            Context.Database.ExecuteSqlCommand("Delete from TestPaperQuestions");
            Context.Database.ExecuteSqlCommand("Delete from UserAnswers");
            Context.Database.ExecuteSqlCommand("Delete from UserTests");
            Context.Database.ExecuteSqlCommand("Delete from TestPapers");
            Context.Database.ExecuteSqlCommand("Delete from OptionalAnswers");
            Context.Database.ExecuteSqlCommand("Delete from questions");
            Context.Database.ExecuteSqlCommand("Delete from QuestionCategories");
            Context.Database.ExecuteSqlCommand("Delete from Userprofiles");
            Context.Database.ExecuteSqlCommand("Delete from Sites");
            Context.Database.ExecuteSqlCommand("Delete from tests");
        }
    }
}