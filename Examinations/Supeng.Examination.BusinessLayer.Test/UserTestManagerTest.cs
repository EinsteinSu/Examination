using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Supeng.Examination.Model;

namespace Supeng.Examination.BusinessLayer.Test
{
    [TestClass]
    public class UserTestManagerTest : TestBase
    {
        private readonly UserTestManager manager = new UserTestManager();

        [TestMethod]
        public void SelectUserTestByUserId()
        {
            var test = GenerateTestWithTester("test", 20);
            var user = Context.UserTests.FirstOrDefault();
            Assert.IsNotNull(user);
            var questionCount = Context.TestPaperQuestions.Count(c => c.TestPaperId == test.TestPaperId);
            var t = Context.Tests.FirstOrDefault();
            Assert.IsNotNull(t);
            t.StartTime = DateTime.Now.AddSeconds(-1);
            t.EndTime = DateTime.Now.AddSeconds(-2);
            Context.SaveChanges();
            var list = manager.SelectUserTestByUserId(user.UserId);
            var userTest = list.FirstOrDefault();
            Assert.IsNotNull(userTest);
            Assert.IsTrue(userTest.Status == TestStatus.考试未开始);
            Assert.AreEqual(userTest.UserAnswers.Count, questionCount);
        }

        [TestMethod]
        public void AnswerQuestionTest()
        {
            var test = GenerateTestWithTester("test", 20);
            var user = Context.UserTests.FirstOrDefault();
            Assert.IsNotNull(user);
            var questionCount = Context.TestPaperQuestions.Count(c => c.TestPaperId == test.TestPaperId);
            var t = Context.Tests.FirstOrDefault();
            Assert.IsNotNull(t);
            t.StartTime = DateTime.Now.AddSeconds(-1);
            t.EndTime = DateTime.Now.AddSeconds(-2);
            Context.SaveChanges();
            var list = manager.SelectUserTestByUserId(user.UserId);
            var userTest = list.FirstOrDefault();
            Assert.IsNotNull(userTest);
            var question = Context.UserAnswers.FirstOrDefault(f => f.UserTestId == userTest.UserTestId);
            Assert.IsNotNull(question);
            manager.AnswerQuestion(userTest.UserTestId, question.QuestionId, "A");
            Assert.IsTrue(userTest.Status == TestStatus.考试开始);
            Assert.AreEqual(userTest.UserAnswers.Count, questionCount);
        }
    }
}