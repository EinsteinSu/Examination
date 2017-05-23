using System.IO;
using System.Web.Mvc;
using Supeng.Examination.BusinessLayer;

namespace Management.Controllers
{
    public class TestAbsentsController : Controller
    {
        private readonly IStatisticsManager _manager;

        public TestAbsentsController()
        {
            _manager = new StatisticsManager();
        }

        public TestAbsentsController(IStatisticsManager manager)
        {
            _manager = manager;
        }

        // GET: TestAbsents
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult TestAbsentGridViewPartial()
        {
            return PartialView("_TestAbsentGridViewPartial", _manager.TestAbsentSelect());
        }

        public ActionResult ViewDetails(int id)
        {
            ViewData["TestId"] = id;
            return View("TestAbsentDetails", _manager.TestResultSelect(id));
        }

        [ValidateInput(false)]
        public ActionResult TestAbsentDetailsGridViewPartial(int id)
        {
            ViewData["TestId"] = id;
            return PartialView("_TestAbsentDetailsGridViewPartial", _manager.TestAbsentDetailSelect(id));
        }

        public ActionResult Export(int id)
        {
            ViewData["TestId"] = id;
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename= TestAbsents.xlsx");
            using (var stream = new MemoryStream(_manager.ExportTestAbsentDetails(id)))
            {
                stream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
            return View("TestAbsentDetails", _manager.TestResultSelect(id));
        }
    }
}