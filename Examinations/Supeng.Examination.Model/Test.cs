using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Supeng.Examination.Model
{
    public class Test
    {
        [Key]
        public int TestId { get; set; }

        [Display(Name = "考试名称")]
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Display(Name = "考试开始时间")]
        [Required]
        public DateTime StartTime { get; set; }

        [Display(Name = "考试结束时间")]
        [Required]
        public DateTime EndTime { get; set; }

        public bool Generated { get; set; }

        public DateTime? GenerateTime { get; set; }

        public bool TestStarted
        {
            get { return DateTime.Now > StartTime; }
        }

        public bool TestDone
        {
            get
            {
                if (DateTime.Now > EndTime)
                {
                    return true;
                }
                return false;
            }
        }

        [Display(Name = "试卷ID")]
        [ForeignKey("TestPaper")]
        public int TestPaperId { get; set; }

        public virtual TestPaper TestPaper { get; set; }

        [Display(Name = "备注")]
        public string Description { get; set; }
    }

}