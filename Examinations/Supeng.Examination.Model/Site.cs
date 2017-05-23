using System.ComponentModel.DataAnnotations;

namespace Supeng.Examination.Model
{
    public class Site
    {
        [Key]
        public int SiteId { get; set; }

        [MaxLength(10), Required, Display(Name = "网点代码")]
        public string SiteCode { get; set; }

        [MaxLength(50), Required, Display(Name = "网点名称")]
        public string Name { get; set; }

        [Display(Name = "备注")]
        public string Description { get; set; }
    }
}