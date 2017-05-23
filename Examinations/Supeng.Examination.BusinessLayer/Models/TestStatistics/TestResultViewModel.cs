using System.ComponentModel.DataAnnotations;

namespace Supeng.Examination.BusinessLayer.Models.TestStatistics
{
    public class TestResultViewModel : TestStatisticsViewModelBase
    {
        [Display(Name = "参考人数")]
        public int TesterCount { get; set; }
    }
}