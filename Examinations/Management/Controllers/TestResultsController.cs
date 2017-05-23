using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Supeng.Examination.BusinessLayer;

namespace Management.Controllers
{
    public class TestResultsController : Controller
    {
        private readonly IStatisticsManager _manager;

        public TestResultsController()
        {
            _manager = new StatisticsManager();
        }

        public TestResultsController(IStatisticsManager manager)
        {
            _manager = manager;
        }

        // GET: TestResults
        public ActionResult Index()
        {
            return View();
        }



        [ValidateInput(false)]
        public ActionResult TestResultsGridViewPartial()
        {
            return PartialView("_TestResultsGridViewPartial", _manager.TestResultSelect());
        }

        public ActionResult ViewDetails(int id)
        {
            ViewData["TestId"] = id;
            return View("TestResultDetails", _manager.TestResultSelect(id));
        }

        [ValidateInput(false)]
        public ActionResult DetailsGridViewPartial(int id)
        {
            ViewData["TestId"] = id;
            return PartialView("_DetailsGridViewPartial", _manager.TestResultDetailSelect(id));
        }

        public ActionResult Export(int id)
        {
            ViewData["TestId"] = id;
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename= TestResults.xlsx");
            using (MemoryStream stream = new MemoryStream(_manager.ExportTestResultDetails(id)))
            {
                stream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
            return View("TestResultDetails", _manager.TestResultSelect(id));
        }

        public ActionResult ExportAllUserTest(int id)
        {
            ViewData["TestId"] = id;
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename= AllUserTestResults.xlsx");
            using (MemoryStream stream = new MemoryStream(_manager.ExportAllUserTest(id)))
            {
                stream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
            return View("TestResultDetails", _manager.TestResultSelect(id));
        }

        public ActionResult ViewUserTestDetails(int testId, int userTestId)
        {
            ViewData["TestId"] = testId;
            ViewData["UserTestId"] = userTestId;
            return View("UserTestDetails",
                _manager.TestResultDetailSelect(testId, userTestId)
                .FirstOrDefault());
        }


        [ValidateInput(false)]
        public ActionResult UserTestDetailsGridViewPartial(int testId, int userTestId)
        {
            ViewData["TestId"] = testId;
            ViewData["UserTestId"] = userTestId;
            return PartialView("_UserTestDetailsGridViewPartial", _manager.UserTestResultSelect(userTestId));
        }

        public ActionResult ExportUserTestDetails(int testId, int userTestId)
        {
            ViewData["TestId"] = testId;
            ViewData["UserTestId"] = userTestId;
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename= TestResults.xlsx");
            using (MemoryStream stream = new MemoryStream(_manager.ExportUserTestDetals(userTestId)))
            {
                stream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
            return View("UserTestDetails",
               _manager.TestResultDetailSelect(testId, userTestId)
               .FirstOrDefault());
        }

      
    }
}