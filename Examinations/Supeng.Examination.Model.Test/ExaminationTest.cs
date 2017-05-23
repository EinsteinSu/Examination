using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Supeng.Examination.Model.Test
{
    [TestClass]
    public class ExaminationTest
    {
        [TestMethod]
        public void GenerateQuestion()
        {
            TestPaper ex = new TestPaper();
            ex.Formulas.Add(new TestPaperFormula
            {
                CategoryId = 1,
                QuestionCount = 3
            });
            ex.Formulas.Add(new TestPaperFormula
            {
                CategoryId = 2,
                QuestionCount = 5
            });
            var list = new List<Question>();
            for (int i = 0; i < 6; i++)
            {
                list.Add(new Question { QuestionId = i + 1, Content = "Question:" + (i + 1), CategoryId = 1 });
                list.Add(new Question { QuestionId = i + 1, Content = "Question:" + (i + 1), CategoryId = 2 });
            }

            foreach (var f in ex.Formulas)
            {
                var result = f.GenerateQuestions(list.Where(w => w.CategoryId == f.CategoryId).ToList());
                Assert.IsTrue(result.Count == f.QuestionCount);
                Console.WriteLine(String.Join(",", result.Select(s => s.QuestionId).ToList()));
            }
        }
    }
}
