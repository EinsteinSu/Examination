using System.ComponentModel.DataAnnotations;

namespace Supeng.Examination.BusinessLayer.Models.TestStatistics
{
    public class TestAbsentViewModel : TestResultViewModel
    {
        [Display(Name = "缺考人数")]
        public int AbsentTesterCount { get; set; }
    }
}
