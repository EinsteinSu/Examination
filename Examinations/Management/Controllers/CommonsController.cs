using System.Web.Mvc;

namespace Management.Controllers
{
    public class CommonsController : Controller
    {
        // GET: Commons
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProcessResultReport(int? id)
        {
            return View();
        }
    }
}