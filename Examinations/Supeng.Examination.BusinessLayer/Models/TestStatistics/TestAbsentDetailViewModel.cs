using System.ComponentModel.DataAnnotations;

namespace Supeng.Examination.BusinessLayer.Models.TestStatistics
{
    public class TestAbsentDetailViewModel
    {
        public int TestId { get; set; }

        public int UserTestId { get; set; }

        [Display(Name = "姓名")]
        public string Name { get; set; }

        [Display(Name = "网点名称")]
        public string SiteName { get; set; }
    }
}