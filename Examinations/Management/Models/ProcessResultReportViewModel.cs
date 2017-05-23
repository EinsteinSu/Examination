using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Models
{
    public class ProcessResultReportViewModel
    {
        public string Title { get; set; }

        public ProcessResultType Type { get; set; }

        public string Content { get; set; }

        public object RouteValues { get; set; }
    }

    public enum ProcessResultType
    {
        Success, Failed, Warning
    }
}
