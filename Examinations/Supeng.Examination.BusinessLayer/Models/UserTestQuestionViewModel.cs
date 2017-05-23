using System.ComponentModel.DataAnnotations;
using Supeng.Examination.Model;

namespace Supeng.Examination.BusinessLayer.Models
{
    public class UserTestQuestionViewModel : Question
    {
        public int UserTestId { get; set; }

        [Display(Name = "我的回答")]
        public string Answer { get; set; }
    }
}