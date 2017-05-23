using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Supeng.Examination.Model
{
    public class UserAnswer
    {
        [Key]
        public int UserAnswerId { get; set; }

        [ForeignKey("Question")]
        public int QuestionId { get; set; }

        public virtual Question Question { get; set; }

        public string Answer { get; set; }

        [ForeignKey("UserTest")]
        public int UserTestId { get; set; }

        public virtual UserTest UserTest { get; set; }

        public int Score
        {
            get
            {
                if (!string.IsNullOrEmpty(Answer))
                {
                    if (Answer == Question.CorrectAnswer ||
                        Question.CorrectAnswer == Answer.Replace(",", ""))
                    {
                        return Question.Score;
                    }
                    return 0;
                }
                return 0;
            }
        }

    }

    public static class UserAnswerHelper
    {
        public static bool CanGetScore(string answer, string correctAnswer)
        {
            if (answer == correctAnswer || answer.Replace(",", "") == correctAnswer)
            {
                return true;
            }
            return false;
        }
    }
}