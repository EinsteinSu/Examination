using System.Linq;
using System.Web.Mvc;
using DevExpress.Web.Mvc;
using Supeng.Examination.BusinessLayer;
using Supeng.Examination.BusinessLayer.Models;
using Supeng.Web.Common;
using Supeng.Web.Common.Securities;

namespace Tester.Controllers
{
    [LogonAuthorize]
    public class HomeController : BaseController
    {
        private readonly IUserUserTestManager _manager;
        private readonly IQuestionManager _questionManager;

        public HomeController(IUserUserTestManager manager, IQuestionManager questionManager)
        {
            _manager = manager;
            _questionManager = questionManager;
        }

        public HomeController()
        {
            _manager = new UserTestManager();
            _questionManager = new QuestionManager();
        }

        public ActionResult Index()
        {
            return View();
        }


        [ValidateInput(false)]
        public ActionResult GridViewPartial()
        {
            return PartialView("_GridViewPartial", _manager.SelectUserTestByUserId(User.UserId));
        }

        public ActionResult TestQuestions(int id, int? index)
        {
            ViewData["index"] = index;
            var test = _manager.SelectTestByUserTestId(id);
            if (test.TestDone)
            {
                return View("CannotTestView");
            }
            return View(test);
        }

        [ValidateInput(false)]
        public ActionResult QuestionGridViewPartial(int id, int? index)
        {
            ViewBag.TestId = id;
            ViewData["index"] = index;

            return PartialView("_QuestionGridViewPartial", _manager.SelectQuestionListByUserTestId(id));
        }

        public ActionResult AnswerQuestions(int id, int testId, int? index)
        {
            ViewBag.TestId = testId;
            ViewData["index"] = index;
            var test = _manager.SelectTestByUserTestId(testId);
            if (test.TestDone)
            {
                return View("CannotTestView");
            }
            return View(_manager.SelecTestQuestionViewModelByQuestionId(testId, id));
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AnswerQuestions(
            [ModelBinder(typeof(DevExpressEditorsBinder))] UserTestQuestionViewModel model, int userTestId,
            int questionId, int? index)
        {
            ViewData["index"] = index;
            var items = CheckBoxListExtension.GetSelectedValues<string>("MAnswer");
            string answer;
            if (items != null && items.Any())
            {
                answer = string.Join(",", items);
            }
            else
            {
                answer = model.Answer;
            }
            var error = _manager.AnswerQuestion(userTestId, questionId, answer);
            if (!string.IsNullOrEmpty(error))
            {
                return View();
            }
            return RedirectToAction("TestQuestions", new { id = userTestId, index = index });
        }
    }
}