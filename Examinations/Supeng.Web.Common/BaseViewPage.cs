using System.Web.Mvc;
using Supeng.Web.Common.Securities;

namespace Supeng.Web.Common
{
    public abstract class BaseViewPage : WebViewPage
    {
        public virtual LogonPrincipal CurrentUser
        {
            get { return User as LogonPrincipal; }
        }
    }
}