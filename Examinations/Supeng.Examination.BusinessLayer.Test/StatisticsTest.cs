using System.Data.Entity;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Supeng.Examination.Model;

namespace Supeng.Examination.BusinessLayer.Test
{
    [TestClass]
    public class StatisticsTest : TestBase
    {
        private readonly IStatisticsManager manager = new StatisticsManager();

        [TestMethod]
        public void TestSelect()
        {
            var test = GenerateTestWithTester("test", 10);
            var result = manager.TestResultSelect();
            Assert.IsNotNull(result);
            Assert.IsTrue(result.First().TesterCount == 10);
        }

        [TestMethod]
        public void TestResultDetailSelect()
        {
            var test = GenerateTestWithTester("test", 10);
            var result = manager.TestResultSelect();
            Assert.IsNotNull(result);
            var userTest = Context.UserTests.Include(i => i.UserAnswers).FirstOrDefault();
            Assert.IsNotNull(userTest);
            var answer = userTest.UserAnswers.FirstOrDefault();
            Assert.IsNotNull(answer);
            var question = Context.Questions.FirstOrDefault(f => f.QuestionId == answer.QuestionId);
            Assert.IsNotNull(question);
            answer.Answer = question.CorrectAnswer;
            Context.SaveChanges();

            var detail = manager.TestResultDetailSelect(test.TestId);
            var first = detail.FirstOrDefault(f => f.UserId == userTest.UserId);
            Assert.IsNotNull(first);
            Assert.AreEqual(first.Score, 2);
        }

        [TestMethod]
        public void TestResultExport()
        {
            var test = GenerateTestWithTester("test", 10);
            var result = manager.TestResultSelect();
            Assert.IsNotNull(result);
            var userTest = Context.UserTests.Include(i => i.UserAnswers).FirstOrDefault();
            Assert.IsNotNull(userTest);
            var answer = userTest.UserAnswers.FirstOrDefault();
            Assert.IsNotNull(answer);
            var question = Context.Questions.FirstOrDefault(f => f.QuestionId == answer.QuestionId);
            Assert.IsNotNull(question);
            answer.Answer = question.CorrectAnswer;
            Context.SaveChanges();
            var buffer = manager.ExportTestResultDetails(test.TestId);
            File.WriteAllBytes("D:\\test.xlsx", buffer);
        }

        [TestMethod]
        public void UserTestResultSelect()
        {
            var test = GenerateTestWithTester("test", 10);
            var result = manager.TestResultSelect();
            Assert.IsNotNull(result);
            var userTest = Context.UserTests.Include(i => i.UserAnswers).FirstOrDefault();
            Assert.IsNotNull(userTest);
            var testDetails = manager.UserTestResultSelect(userTest.UserTestId);
            //the reuslt is 30 that dependent on how many question I created for testpaper
            Assert.AreEqual(testDetails.Count, 30);
        }

        [TestMethod]
        public void TestAbsentSelect()
        {
            TestResultDetailSelect();
            var user = Context.UserTests.FirstOrDefault();
            Assert.IsNotNull(user);
            user.Status = TestStatus.考试开始;
            Context.SaveChanges();
            var result = manager.TestAbsentSelect();
            var first = result.FirstOrDefault();
            Assert.IsNotNull(first);
            Assert.AreEqual(first.AbsentTesterCount, first.TesterCount - 1);
        }
    }
}