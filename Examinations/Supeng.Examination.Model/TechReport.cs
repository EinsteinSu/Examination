using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supeng.Examination.Model
{
    public class TechReport
    {
        [Key]
        public int TechReportId { get; set; }

        [Display(Name = "主题"), Required, MaxLength(200)]
        public string Subject { get; set; }

        [Display(Name = "描述"), Required]
        public string Description { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public virtual UserProfile User { get; set; }
    }
}
