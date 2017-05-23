using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supeng.Examination.BusinessLayer.Models.TestStatistics
{
    public class TestResultDetailViewModel
    {
        public int TestId { get; set; }

        public int UserTestId { get; set; }

        public int UserId { get; set; }

        [Display(Name = "姓名")]
        public string Name { get; set; }

        [Display(Name = "网点")]
        public string SiteName { get; set; }

        [Display(Name = "分数")]
        public int Score { get; set; }
    }
}
