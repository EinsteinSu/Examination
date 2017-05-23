using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supeng.Examination.BusinessLayer.Models
{
    public class TestUserSelectionViewModel
    {
        public int TestId { get; set; }

        public int SiteId { get; set; }

        public IList<int> SelectedItems { get; set; }
    }

    public class UserSelectionViewModel
    {
        public int UserId { get; set; }

        [Display(Name = "所属网点")]
        public string SiteName { get; set; }

        [Display(Name = "用户代码")]
        public string UserCode { get; set; }

        [Display(Name = "姓名")]
        public string UserName { get; set; }

        public string DisplayName
        {
            get { return string.Format("({0}){1}-{2}", UserCode, UserName, SiteName); }
        }
    }
}
