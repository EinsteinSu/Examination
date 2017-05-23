using System.Web.Mvc;
using Supeng.Web.Common.Securities;

namespace Management.Controllers
{
    [LogonAuthorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}

public enum HeaderViewRenderMode
{
    Full,
    Title
}