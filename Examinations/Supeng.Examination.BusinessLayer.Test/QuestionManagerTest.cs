using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Supeng.Examination.Model;

namespace Supeng.Examination.BusinessLayer.Test
{
    [TestClass]
    public class QuestionManagerTest : TestBase
    {
        private readonly QuestionManager _manager = new QuestionManager();

        [TestMethod]
        public void SelectTest()
        {
            var list = _manager.SelectList();
            Assert.IsTrue(list.Count == 0);
        }

        [TestMethod]
        public void Add()
        {
            var id = AddQuestion();

            Assert.IsTrue(id > 0);
        }

        [TestMethod]
        public void Modify()
        {
            var id = AddQuestion();
            var question = _manager.SelectById(id);
            Assert.IsNotNull(question);
            question.OptionalAnswers[0].Content = "Test";
            var str = _manager.Modify(question);
            Assert.IsTrue(string.IsNullOrEmpty(str));
        }

        [TestMethod]
        public void Delete()
        {
            var id = AddQuestion();
            _manager.Delete(id);
            var item = Context.Questions.FirstOrDefault(a => a.QuestionId == id);
            Assert.IsNotNull(item);
            Assert.IsTrue(item.Deleted);
        }

        [TestMethod]
        public void SelectOptionalAnswer()
        {
            var item = AddQuestion();
            var q = Context.Questions.FirstOrDefault(f => f.QuestionId == item);
            Assert.IsNotNull(q);
            Assert.IsTrue(q.OptionalAnswers.Count == 2);
        }

        [TestMethod]
        public void AddOptionalAnswer()
        {
            var item = AddQuestion();
            OptionalAnswer answer = new OptionalAnswer
            {
                OrderNumber = "A",
                Content = "This is a optional answer content"
            };
            var error = _manager.CreateOptionalAnswer(item, answer);
            Assert.IsTrue(string.IsNullOrEmpty(error));
            var q = Context.Questions.FirstOrDefault(f => f.QuestionId == item);
            Assert.IsNotNull(q);
            Assert.IsTrue(q.OptionalAnswers.Count == 3);
        }

        [TestMethod]
        public void ModifyOptionalAnswer()
        {
            const string content = "test modify";
            var item = AddQuestion();
            var q = Context.Questions.FirstOrDefault(f => f.QuestionId == item);
            Assert.IsNotNull(q);
            var answer = q.OptionalAnswers[0];
            answer.Content = content;
            _manager.ModifyOptionalAnswer(answer);
            answer = Context.OpitionalAnswers.FirstOrDefault(f => f.AnswerId == answer.AnswerId);
            Assert.IsNotNull(answer);
            Assert.AreEqual(content, answer.Content);
        }

        [TestMethod]
        public void DeleteOptionalAnswer()
        {
            var item = AddQuestion();
            var q = Context.Questions.FirstOrDefault(f => f.QuestionId == item);
            Assert.IsNotNull(q);
            var answer = q.OptionalAnswers[0];
            _manager.DeleteOptionalAnswer(answer.AnswerId);
            Assert.IsNull(Context.OpitionalAnswers.FirstOrDefault(f => f.AnswerId == answer.AnswerId));
        }

        private int AddQuestion()
        {
            GenerateQuestionCategories(2);
            var question = new Question
            {
                Content = "Test content",
                Type = QuestionType.单选,
                CorrectAnswer = "A",
                Category = Context.QuestionCategories.First(),
                OptionalAnswers = new List<OptionalAnswer>
                {
                    new OptionalAnswer
                    {
                        OrderNumber = "A",
                        Content = "A is a correct answer."
                    },
                    new OptionalAnswer
                    {
                        OrderNumber = "B",
                        Content = "B is a correct answer."
                    }
                }
            };
            _manager.Create(question);
            return question.QuestionId;
        }
    }
}