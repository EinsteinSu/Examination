using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Supeng.Examination.Model
{
    public class TestPaperQuestion
    {
        [Key]
        public int TestPaperQuestionId { get; set; }

        [ForeignKey("TestPaper")]
        public int TestPaperId { get; set; }

        public virtual TestPaper TestPaper { get; set; }

        [ForeignKey("Question")]
        public int QuestionId { get; set; }

        public virtual Question Question { get; set; }
    }
}