using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Supeng.Examination.Model
{
    public class QuestionCategory
    {
        [Key]
        public int QuestionCategoryId { get; set; }

        [Display(Name = "试题分类名称"), Required, MaxLength(20)]
        public string Name { get; set; }

        [Display(Name = "备注")]
        public string Description { get; set; }

        public virtual IList<Question> Questions { get; set; }
    }
}