using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Web.Mvc;
using System.Web.Mvc;
using Supeng.Examination.BusinessLayer;
using Supeng.Examination.BusinessLayer.Models;
using Supeng.Examination.Model;
using Supeng.Web.Common.Securities;

namespace Management.Controllers
{
    [LogonAuthorize(Roles = "Management")]
    public class TestsController : Controller
    {
        private readonly ITestManager _manager;
        private readonly ITestPaperManager _testPaperManager;
        private readonly IUserProfileManager _userProfileManager;
        private readonly ISiteManager _siteManager;
        public TestsController()
        {
            _manager = new TestManager();
            _testPaperManager = new TestPaperManager();
            _userProfileManager = new UserProfileManager();
            _siteManager = new SiteManager();
        }

        public TestsController(ITestManager manager, ITestPaperManager testPaperManager
            , IUserProfileManager userProfileManager, ISiteManager siteManager)
        {
            _manager = manager;
            _testPaperManager = testPaperManager;
            _userProfileManager = userProfileManager;
            _siteManager = siteManager;
        }

        // GET: Tests
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult TestUserDetails(int id)
        {
            ViewBag.TestId = id;
            return View(_manager.SelecTestDetailViewModel(id));
        }

        #region Test Crud
        [ValidateInput(false)]
        public ActionResult TestGridViewPartial()
        {
            ViewBag.TestPaperList = _testPaperManager.SelectList();
            return PartialView("_TestGridViewPartial", _manager.SelectList());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult TestGridViewPartialAddNew(Test item)
        {
            if (ModelState.IsValid)
            {
                var error = _manager.Create(item);
                if (!string.IsNullOrEmpty(error))
                {
                    ViewData["EditError"] = error;
                }
            }
            else
            {
                ViewData["EditError"] = "信息填写错误。";
            }
            return PartialView("_TestGridViewPartial", _manager.SelectList());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult TestGridViewPartialUpdate(Test item)
        {
            if (ModelState.IsValid)
            {
                var error = _manager.Modify(item);
                if (!string.IsNullOrEmpty(error))
                {
                    ViewData["EditError"] = error;
                }
            }
            else
            {
                ViewData["EditError"] = "信息填写错误。";
            }
            return PartialView("_TestGridViewPartial", _manager.SelectList());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult TestGridViewPartialDelete(int testId)
        {
            if (testId >= 0)
            {
                var error = _manager.Delete(testId);
                if (!string.IsNullOrEmpty(error))
                {
                    ViewData["EditError"] = error;
                    return new ContentResult { Content = error };
                }
            }
            return PartialView("_TestGridViewPartial", _manager.SelectList());
        }
        #endregion

        [ValidateInput(false)]
        public ActionResult UserSelectionGridViewPartial(int id, int siteId)
        {
            ViewBag.SelectionUsers = _userProfileManager.SelectTestUserList(siteId);
            ViewBag.Sites = _siteManager.SelectSitesIncludeEmpty();
            return PartialView("_UserSelectionGridViewPartial", _userProfileManager.SelectUserSelectViewModel(id, siteId));
        }

        [ValidateInput(false)]
        public ActionResult SelectUserConfirm(int id, int siteId)
        {
            var items = CheckBoxListExtension.GetSelectedValues<int>("cblUser");
            if (items != null)
            {
                var error = _manager.SaveSelectedTestUsers(id, siteId, items);
                if (!string.IsNullOrEmpty(error))
                {
                    throw new Exception(error);
                }
            }
            return View("TestUserDetails", _manager.SelecTestDetailViewModel(id));
        }

        [ValidateInput(false)]
        public ActionResult SelectConfirm(MVCxGridViewBatchUpdateValues<TestUserSelectionViewModel, int> list, int id)
        {
            ViewBag.TestId = id;
            //var selected = list.Update.Where(w => w.Selected).Select(s => s.UserId).ToList();
            //var error = _manager.GenerateTest(id, selected);
            //if (!string.IsNullOrEmpty(error))
            //{
            //    throw new Exception("生成考试失败:" + error);
            //}
            return RedirectToAction("TestUserDetails", new { id = id });
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial(int id)
        {
            ViewBag.TestId = id;
            return PartialView("_GridViewPartial", _manager.SelectUserTestListByTestId(id));
        }
    }
}