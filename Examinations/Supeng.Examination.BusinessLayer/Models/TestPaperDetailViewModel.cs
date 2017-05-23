using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supeng.Examination.BusinessLayer.Models
{
    public class TestPaperDetailViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int QuestionCount { get; set; }

        public int Score { get; set; }

        public bool CanGenerate { get; set; }

        public bool Validated { get; set; }

        public string Error { get; set; }
    }
}
