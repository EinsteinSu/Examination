using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Transactions;
using Supeng.Examination.BusinessLayer.Interfaces;
using Supeng.Examination.BusinessLayer.Models;
using Supeng.Examination.DataAccess;
using Supeng.Examination.Model;

namespace Supeng.Examination.BusinessLayer
{
    public interface ITestPaperManager : ICrud<TestPaper>, IDisposable
    {
        IList<TestPaperFormula> SelectFormulas(int testPaperId);

        TestPaperFormula SeleTestPaperFormulaById(int id);

        string CreateFormula(int testPaperId, TestPaperFormula formula);

        string ModifyFormula(TestPaperFormula formula);

        string DeleteFormula(int formulaId);

        IList<TestPaperViewModel> SelecTestPaperViewModels();

        string GenerateTestPaper(int testPaperId, bool useForTests = false);

        TestPaperDetailViewModel GetPaperDetailViewModel(int testPaperId);

        IList<Question> GetTestQuestions(int testPaperId);

        string DeleteTestQuestion(int questionId, int testPaperId);

        bool CanEdit(int testPaperId);
    }

    public class TestPaperManager : ITestPaperManager
    {
        private readonly ExaDataContext _context = new ExaDataContext();

        #region ICruds
        public IList<TestPaper> SelectList()
        {
            //todo: statistics the score and question count
            return _context.TestPapers.OrderByDescending(o => o.TestPaperId).ToList();
        }

        public TestPaper SelectById(int id)
        {
            return _context.TestPapers.FirstOrDefault(f => f.TestPaperId == id);
        }

        public string Create(TestPaper examination)
        {
            try
            {
                _context.TestPapers.Add(examination);
                _context.SaveChanges();
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string Modify(TestPaper examination)
        {
            try
            {
                _context.Entry(examination).State = EntityState.Modified;
                _context.SaveChanges();
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string Delete(int id)
        {
            try
            {
                var ex = SelectById(id);
                if (ex == null)
                {
                    throw new KeyNotFoundException("Not Found");
                }
                _context.TestPapers.Remove(ex);
                _context.SaveChanges();
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        #endregion

        #region Formulas Cruds
        public IList<TestPaperFormula> SelectFormulas(int testPaperId)
        {
            var item = _context.TestPapers.FirstOrDefault(f => f.TestPaperId == testPaperId);
            if (item == null)
            {
                throw new KeyNotFoundException("Test paper could not found");
            }
            return item.Formulas;
        }

        public TestPaperFormula SeleTestPaperFormulaById(int id)
        {
            return _context.TestPaperFormulas.FirstOrDefault(f => f.FormulaId == id);
        }

        public string CreateFormula(int testPaperId, TestPaperFormula formula)
        {
            try
            {
                formula.TestPaperId = testPaperId;
                _context.TestPaperFormulas.Add(formula);
                _context.SaveChanges();
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string ModifyFormula(TestPaperFormula formula)
        {
            try
            {
                _context.Entry(formula).State = EntityState.Modified;
                _context.SaveChanges();
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string DeleteFormula(int formulaId)
        {
            try
            {
                var item = SeleTestPaperFormulaById(formulaId);
                if (item == null)
                {
                    throw new KeyNotFoundException("Could not found the test paper formula.");
                }
                _context.TestPaperFormulas.Remove(item);
                _context.SaveChanges();
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        #endregion

        public IList<TestPaperViewModel> SelecTestPaperViewModels()
        {
            return _context.Database.SqlQuery<TestPaperViewModel>("TestPaperSelect").ToList();
        }

        public string GenerateTestPaper(int testPaperId, bool useForTest = false)
        {
            try
            {
                var testPaper = SelectById(testPaperId);
                var error = CheckTheQuestionCount(testPaper.Formulas);
                if (!string.IsNullOrEmpty(error))
                {
                    return error;
                }
                if (!useForTest && IsAssociatedToTest(testPaperId))
                {
                    return "该试卷已经用于其他考试，不能再进行更改，请重新创建新的试卷。";
                }
                var questions = new List<Question>();
                foreach (var f in testPaper.Formulas)
                {
                    questions.AddRange(f.GenerateQuestions(_context.Questions.Where(w => w.CategoryId == f.CategoryId && !w.Deleted).ToList()));
                }
                _context.Database.ExecuteSqlCommand("TestPaperQuestionDelete @p0", testPaperId);
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    foreach (var question in questions)
                    {
                        var eq = new TestPaperQuestion
                        {
                            TestPaper = testPaper,
                            Question = question
                        };
                        _context.TestPaperQuestions.Add(eq);
                    }
                    _context.SaveChanges();
                    scope.Complete();
                    return string.Empty;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private bool IsAssociatedToTest(int testPaperId)
        {
            return _context.Tests.Any(a => a.TestPaperId == testPaperId);
        }

        private string CheckTheQuestionCount(IList<TestPaperFormula> formulas)
        {
            return formulas.Where(f => f.QuestionCount >=
                _context.Questions.Count(c => c.CategoryId == f.CategoryId))
                .Aggregate(string.Empty, (current, f) =>
                    current + string.Format("试题类型:{0} 数量大于题库数量!", f.Category.Name));
        }

        public TestPaperDetailViewModel GetPaperDetailViewModel(int testPaperId)
        {
            var testpaper = SelectById(testPaperId);
            var detail = new TestPaperDetailViewModel
            {
                Id = testpaper.TestPaperId,
                Title = string.Format("{0} 试题", testpaper.Name),
                QuestionCount = testpaper.QuestionCount,
                CanGenerate = !_context.TestPaperQuestions.Any(w => w.TestPaperId == testPaperId),
                Validated =
                    _context.TestPaperQuestions.Count(w => w.TestPaperId == testPaperId) == testpaper.QuestionCount,

            };
            detail.Score = (from tq in _context.TestPaperQuestions
                            join q in _context.Questions on tq.QuestionId equals q.QuestionId
                            where tq.TestPaperId == testPaperId
                            select (int?)q.Score).Sum() ?? 0;
            return detail;
        }

        public IList<Question> GetTestQuestions(int testPaperId)
        {
            return _context.TestPaperQuestions.Where(w => w.TestPaperId == testPaperId).Select(s => s.Question).ToList();
        }

        public string DeleteTestQuestion(int questionId, int testPaperId)
        {
            if (IsAssociatedToTest(testPaperId))
            {
                return "该试卷已经用于其他考试，不能再进行更改，请重新创建新的试卷。";
            }
            try
            {
                var first = _context.TestPaperQuestions.FirstOrDefault(f => f.QuestionId == questionId
                                                                            && f.TestPaperId == testPaperId);
                if (first != null)
                {
                    _context.TestPaperQuestions.Remove(first);
                    _context.SaveChanges();
                    return string.Empty;
                }
                return "Cannot found this question in test paper.";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public bool CanEdit(int testPaperId)
        {
            var list = _context.Tests.Where(w => w.TestPaperId == testPaperId).ToList();
            return !list.Any(a => a.TestDone);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}