using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Supeng.Examination.Model;

namespace Supeng.Examination.BusinessLayer.Models.TestStatistics
{
    public class UserTestResultViewModel
    {
        public int UserTestId { get; set; }

        [Display(Name = "序号")]
        public int Sequence { get; set; }

        public int QuestionId { get; set; }

        [Display(Name = "试题")]
        public string QuestionContent { get; set; }

        [Display(Name = "正确答案")]
        public string CorrectAnswer { get; set; }

        [Display(Name = "考生答案")]
        public string Answer { get; set; }

        public virtual IList<OptionalAnswer> OptionalAnswers { get; set; }
    }
}