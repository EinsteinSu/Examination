using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Supeng.Examination.Model
{
    public class OptionalAnswer
    {
        [Key]
        public int AnswerId { get; set; }

        [ForeignKey("Question")]
        public int QuestionId { get; set; }

        public virtual Question Question { get; set; }

        [MaxLength(2), Required]
        [Display(Name = "编号")]
        public string OrderNumber { get; set; }

        [Display(Name = "内容"), Required]
        public string Content { get; set; }

        public string DisplayContent
        {
            get { return string.Format("{0} : {1}", OrderNumber, Content); }
        }
    }
}