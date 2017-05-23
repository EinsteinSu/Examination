using System;
using System.ComponentModel.DataAnnotations;

namespace Supeng.Examination.BusinessLayer.Models.TestStatistics
{
    public class TestStatisticsViewModelBase
    {
        public int TestId { get; set; }
        [Display(Name = "名称")]
        public string TestName { get; set; }
        [Display(Name = "开始时间")]
        public DateTime? StartTime { get; set; }

        [Display(Name = "结束时间")]
        public DateTime? EndTime { get; set; }

        [Display(Name = "试卷名称")]
        public string TestPaperName { get; set; }
    }
}