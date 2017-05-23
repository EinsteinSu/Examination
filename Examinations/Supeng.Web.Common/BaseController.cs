using System;
using System.Web.Mvc;
using Supeng.Web.Common.Securities;

namespace Supeng.Web.Common
{
    public class BaseController : Controller
    {
        public new virtual LogonPrincipal User
        {
            get { return HttpContext.User as LogonPrincipal; }
        }

        public DateTime CurrentTime
        {
            get
            {
                ViewData["time"] = DateTime.Now;
                return DateTime.Now;
            }
        }
    }
}