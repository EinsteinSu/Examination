using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Supeng.Examination.Model
{
    public class UserTest
    {
        public UserTest()
        {
            StartTime = DateTime.Now;
        }

        [Key]
        public int UserTestId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public virtual UserProfile User { get; set; }

        [ForeignKey("Test")]
        public int TestId { get; set; }

        public virtual Test Test { get; set; }

        [Display(Name = "状态")]
        [Required]
        public TestStatus Status { get; set; }

        [Display(Name = "考试开始时间")]
        public DateTime? StartTime { get; set; }

        [Display(Name = "考试完成时间")]
        public DateTime? EndTime { get; set; }

        public IList<UserAnswer> UserAnswers { get; set; }

        [Display(Name = "完成百分比")]
        public string Percent
        {
            get
            {
                if (UserAnswers != null)
                {
                    var total = UserAnswers.Count;
                    var completed = UserAnswers.Count(c => !string.IsNullOrEmpty(c.Answer));
                    return ((decimal)completed / total).ToString("P");
                }
                return "0.00%";
            }
        }

        [Display(Name = "分数")]
        public int Score
        {
            get
            {
                //test completed
                if (Test.TestDone && UserAnswers != null && UserAnswers.Any())
                {
                    return UserAnswers.Sum(s => s.Score);
                }
                return 0;
            }
        }
    }

    public enum TestStatus
    {
        考试未开始, 考试开始, 考试暂停, 考试结束
    }
}