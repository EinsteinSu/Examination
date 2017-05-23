using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Supeng.Examination.Model
{
    public class TestPaper
    {
        public TestPaper()
        {
            Formulas = new List<TestPaperFormula>();
        }

        [Key]
        public int TestPaperId { get; set; }

        [MaxLength(20), Required]
        [Display(Name = "试卷名称")]
        public string Name { get; set; }

        //[Required]
        //[Display(Name = "试题数量")]
        //public int QuestionCount { get; set; }

        [Display(Name = "试题数量")]
        public int QuestionCount
        {
            get { return Formulas.Sum(s => s.QuestionCount); }
        }

        public virtual IList<TestPaperFormula> Formulas { get; set; }


        [Display(Name = "备注")]
        public string Description { get; set; }


    }
}