using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Supeng.Examination.Model
{
    public class Question
    {
        public Question()
        {
            Score = 2;
            Type = QuestionType.单选;
        }

        [Key]
        public int QuestionId { get; set; }

        [Display(Name = "类型"), Required]
        public QuestionType Type { get; set; }

        [ForeignKey("Category")]
        [Display(Name = "试题分类ID")]
        public int CategoryId { get; set; }

        public virtual QuestionCategory Category { get; set; }

        [Display(Name = "内容"), Required]
        public string Content { get; set; }

        [Display(Name = "该题分数"), Required]
        public int Score { get; set; }

        [MaxLength(100)]
        [Display(Name = "正确答案")]
        public string CorrectAnswer { get; set; }

        public virtual IList<OptionalAnswer> OptionalAnswers { get; set; }

        public bool Deleted { get; set; }
    }

    public enum QuestionType
    {
        单选, 多选
    }
}