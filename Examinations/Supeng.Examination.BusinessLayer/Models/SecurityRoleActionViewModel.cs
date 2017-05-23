using System.Collections.Generic;
using Supeng.Examination.Model;

namespace Supeng.Examination.BusinessLayer.Models
{
    public class SecurityRoleActionViewModel
    {
        public int RoleId { get; set; }
        public IList<SecurityAction> AllActions { get; set; }

        public IList<int> ExistsActionIds { get; set; }
    }
}