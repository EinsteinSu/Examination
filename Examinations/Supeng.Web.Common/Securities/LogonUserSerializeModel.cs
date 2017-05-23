using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supeng.Web.Common.Securities
{
    /// <summary>
    /// used for serialize to cookie and deserialize and convert to user
    /// </summary>
    public class LogonUserSerializeModel
    {
        public int UserId { get; set; }

        public string UserCode { get; set; }

        public string UserName { get; set; }

        public string[] Roles { get; set; }
    }
}
