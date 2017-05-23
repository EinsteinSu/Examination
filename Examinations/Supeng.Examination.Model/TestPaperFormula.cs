using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Supeng.Examination.Model
{
    public class TestPaperFormula
    {
        [Key]
        public int FormulaId { get; set; }

        [ForeignKey("Category")]
        [Display(Name = "试题类型ID")]
        public int CategoryId { get; set; }

        public virtual QuestionCategory Category { get; set; }

        [Display(Name = "试题数量")]
        public int QuestionCount { get; set; }


        [ForeignKey("TestPaper")]
        public int TestPaperId { get; set; }

        public virtual TestPaper TestPaper { get; set; }

        public IList<Question> GenerateQuestions(IList<Question> questions)
        {
            if (questions.Count <= QuestionCount)
            {
                return new List<Question>();
            }
            var random = new Random();
            IList<Question> result = new List<Question>(QuestionCount);
            while (result.Count < QuestionCount)
            {
                var value = random.Next(0, questions.Count - 1);
                var item = questions[value];
                if (result.All(a => a.QuestionId != item.QuestionId))
                {
                    result.Add(item);
                }
            }
            return result;
        }
    }
}