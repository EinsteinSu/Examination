using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Supeng.Examination.Model;

namespace Supeng.Examination.BusinessLayer.Models
{
    public class TestPaperViewModel : TestPaper
    {
        public int Score { get; set; }

        [Display(Name = "试题数量")]
        public int RealQuestionCount { get; set; }

        public bool CanGenerate
        {
            get { return !(Score > 0); }
        }

        public string ScoreDescription
        {
            get
            {
                if (Score <= 0)
                {
                    return "该试卷还未生成题目";
                }
                return string.Empty;
            }
        }
    }
}
