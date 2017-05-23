using System.Linq;
using System.Security.Principal;

namespace Supeng.Web.Common.Securities
{
    public class LogonPrincipal : IPrincipal
    {
        public LogonPrincipal(string userName)
        {
            Identity = new GenericIdentity(userName);
        }

        public int UserId { get; set; }

        public string UserCode { get; set; }

        public string UserName { get; set; }

        public string[] Roles { get; set; }

        public bool IsInRole(string role)
        {
            return Roles.Contains(role);
        }

        public IIdentity Identity { get; private set; }
    }
}