using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Supeng.Examination.Model;

namespace Supeng.Examination.BusinessLayer.Test
{
    [TestClass]
    public class TestPaperManagerTest : TestBase
    {
        private readonly TestPaperManager _manager = new TestPaperManager();

        #region testpaper crud
        [TestMethod]
        public void SelectView()
        {
            Assert.IsTrue(!_manager.SelecTestPaperViewModels().Any());
        }

        [TestMethod]
        public void Select()
        {
            Add();
            var list = _manager.SelectList();
            Assert.IsTrue(list.Any());
        }

        [TestMethod]
        public void Create()
        {
            var ex = Add();
            Assert.IsTrue(ex.TestPaperId > 0);
        }

        //[TestMethod]
        //[Ignore]
        //public void CreateAndGenerateQuestions()
        //{
        //    GenerateQuestions(30);
        //    var ex = new TestPaper
        //    {
        //        Name = "test",
        //        Description = "description"
        //    };
        //    var error = _manager.CreateAndGenerateQuestions(ex);
        //    Assert.IsTrue(string.IsNullOrEmpty(error));
        //    Assert.IsTrue(Context.TestPaperQuestions.Count() == 20);
        //}

        ///// <summary>
        ///// the test paper's question count greater than stored question count, it will throw error exception
        ///// </summary>
        //[TestMethod]
        //[Ignore]
        //public void CreateAndGenerateQuestions_With_Error()
        //{
        //    var ex = new TestPaper
        //    {
        //        Name = "test",
        //        Description = "description"
        //    };
        //    var error = _manager.CreateAndGenerateQuestions(ex);
        //    Assert.IsTrue(!string.IsNullOrEmpty(error));
        //    Assert.IsTrue(!Context.TestPaperQuestions.Any());
        //}

        [TestMethod]
        public void Modify()
        {
            const string modify = "Test1";
            var ex = Add();
            ex.Name = modify;
            _manager.Modify(ex);
            ex = Context.TestPapers.FirstOrDefault(f => f.TestPaperId == ex.TestPaperId);
            Assert.IsNotNull(ex);
            Assert.AreEqual(modify, ex.Name);
        }

        [TestMethod]
        public void Delete()
        {
            var ex = Add();
            _manager.Delete(ex.TestPaperId);
            Assert.IsNull(Context.TestPapers.FirstOrDefault(f => f.TestPaperId == ex.TestPaperId));
        }
        #endregion

        [TestMethod]
        public void CreateFormula()
        {
            var ex = Add();
            GenerateQuestionCategories(2);
            var f1 = new TestPaperFormula
            {
                CategoryId = Context.QuestionCategories.ToList()[0].QuestionCategoryId,
                QuestionCount = 10,
            };
            var error = _manager.CreateFormula(ex.TestPaperId, f1);
            Assert.IsTrue(string.IsNullOrEmpty(error));
            var item = Context.TestPaperFormulas.FirstOrDefault(f => f.FormulaId == f1.FormulaId);
            Assert.IsNotNull(item);
            Assert.AreEqual(ex.TestPaperId, f1.TestPaperId);
        }

        [TestMethod]
        public void ModifyFormula()
        {
            const int count = 100;
            var ex = Add();
            GenerateQuestionCategories(2);
            var f1 = new TestPaperFormula
            {
                CategoryId = Context.QuestionCategories.ToList()[0].QuestionCategoryId,
                QuestionCount = 10,
            };
            var error = _manager.CreateFormula(ex.TestPaperId, f1);
            Assert.IsTrue(string.IsNullOrEmpty(error));
            f1.QuestionCount = count;
            error = _manager.ModifyFormula(f1);
            Assert.IsTrue(string.IsNullOrEmpty(error));
            var item = Context.TestPaperFormulas.FirstOrDefault(f => f.FormulaId == f1.FormulaId);
            Assert.IsNotNull(item);
            Assert.AreEqual(item.QuestionCount, count);
        }

        [TestMethod]
        public void DeleteFormula()
        {
            var ex = Add();
            GenerateQuestionCategories(2);
            var f1 = new TestPaperFormula
            {
                CategoryId = Context.QuestionCategories.ToList()[0].QuestionCategoryId,
                QuestionCount = 10,
            };
            var error = _manager.CreateFormula(ex.TestPaperId, f1);
            Assert.IsTrue(string.IsNullOrEmpty(error));
            error = _manager.DeleteFormula(f1.FormulaId);
            Assert.IsTrue(string.IsNullOrEmpty(error));
            var item = Context.TestPaperFormulas.FirstOrDefault(f => f.FormulaId == f1.FormulaId);
            Assert.IsNull(item);
        }

        [TestMethod]
        public void Generate()
        {
            var ex = Add();
            GenerateQuestions(100);
            var f1 = new TestPaperFormula
            {
                CategoryId = Context.QuestionCategories.ToList()[0].QuestionCategoryId,
                QuestionCount = 10,
            };
            _manager.CreateFormula(ex.TestPaperId, f1);
            var f2 = new TestPaperFormula
            {
                CategoryId = Context.QuestionCategories.ToList()[1].QuestionCategoryId,
                QuestionCount = 20,
            };
            _manager.CreateFormula(ex.TestPaperId, f2);
            var error = _manager.GenerateTestPaper(ex.TestPaperId, true);
            Assert.IsTrue(string.IsNullOrEmpty(error));
            Assert.IsTrue(Context.TestPaperQuestions.Count(c => c.Question.CategoryId == f1.CategoryId) == f1.QuestionCount);
            Assert.IsTrue(Context.TestPaperQuestions.Count(c => c.Question.CategoryId == f2.CategoryId) == f2.QuestionCount);
            Assert.IsTrue(Context.TestPaperQuestions.Any(a => a.TestPaper.TestPaperId == ex.TestPaperId));
        }

        [TestMethod]
        public void Generate_Error()
        {
            var ex = Add();
            GenerateQuestions(10);
            var f1 = new TestPaperFormula
            {
                CategoryId = Context.QuestionCategories.ToList()[0].QuestionCategoryId,
                QuestionCount = 10,
            };
            _manager.CreateFormula(ex.TestPaperId, f1);
            var error = _manager.GenerateTestPaper(ex.TestPaperId,true);
            Assert.IsFalse(string.IsNullOrEmpty(error));
        }

        protected TestPaper Add()
        {
            var ex = new TestPaper
            {
                Name = "test",
                Description = "description"
            };
            var error = _manager.Create(ex);
            Assert.IsTrue(string.IsNullOrEmpty(error));
            return ex;
        }
    }
}