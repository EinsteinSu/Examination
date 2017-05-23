using System.Web.Mvc;
using Supeng.Examination.BusinessLayer;
using Supeng.Examination.Model;
using Supeng.Web.Common.Securities;

namespace Management.Controllers
{
    [LogonAuthorize(Roles = "Management")]
    public class QuestionCategoriesController : Controller
    {
        private readonly IQuestionCategoryManager _manager;

        public QuestionCategoriesController(IQuestionCategoryManager manager)
        {
            _manager = manager;
        }

        public QuestionCategoriesController()
        {
            _manager = new QuestionCategoryManager();
        }

        // GET: QuestionCategories
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult QuestionCategoryGridViewPartial()
        {
            return PartialView("_QuestionCategoryGridViewPartial", _manager.SelectList());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult QuestionCategoryGridViewPartialAddNew(QuestionCategory item)
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
            return PartialView("_QuestionCategoryGridViewPartial", _manager.SelectList());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult QuestionCategoryGridViewPartialUpdate(QuestionCategory item)
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
            return PartialView("_QuestionCategoryGridViewPartial", _manager.SelectList());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult QuestionCategoryGridViewPartialDelete(int questionCategoryId)
        {
            if (questionCategoryId >= 0)
            {
                var error = _manager.Delete(questionCategoryId);
                if (!string.IsNullOrEmpty(error))
                {
                    ViewData["EditError"] = error;
                }
            }
            return PartialView("_QuestionCategoryGridViewPartial", _manager.SelectList());
        }
    }
}