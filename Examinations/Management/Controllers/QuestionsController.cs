using System.Web.Mvc;
using DevExpress.Web.Mvc;
using Supeng.Examination.BusinessLayer;
using Supeng.Examination.BusinessLayer.Interfaces;
using Supeng.Examination.Model;
using Supeng.Web.Common.Securities;

namespace Management.Controllers
{
    [LogonAuthorize(Roles = "Management")]
    public class QuestionsController : Controller
    {
        private readonly IQuestionManager _manager;
        private readonly IQuestionCategoryManager _categoryManager;

        public QuestionsController(IQuestionManager manager,IQuestionCategoryManager categoryManager)
        {
            _manager = manager;
            _categoryManager = categoryManager;
        }

        public QuestionsController()
        {
            _manager = new QuestionManager();
            _categoryManager = new QuestionCategoryManager();
        }

        // GET: Questions
        public ActionResult Index()
        {
            return View();
        }

        #region Question

        [ValidateInput(false)]
        public ActionResult QuestionGridViewPartial()
        {
            ViewBag.CategoryList = _categoryManager.SelectList();
            return PartialView("_QuestionGridViewPartial", _manager.SelectList());
        }

        //public ActionResult QuestionPagging(GridViewPagerState pager)
        //{
        //    var pagging = _manager as IPagging<Question>;
        //    return PartialView("_QuestionGridViewPartial", pagging.SelectPaggingList(pager.PageIndex, pager.PageSize));
        //}

        [HttpPost, ValidateInput(false)]
        public ActionResult QuestionGridViewPartialAddNew(Question item)
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
            return PartialView("_QuestionGridViewPartial", _manager.SelectList());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult QuestionGridViewPartialUpdate(Question item)
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
                ViewData["EditError"] = "信息填写错误。";
            return PartialView("_QuestionGridViewPartial", _manager.SelectList());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult QuestionGridViewPartialDelete(int questionId)
        {
            if (questionId >= 0)
            {
                var error = _manager.Delete(questionId);
                if (!string.IsNullOrEmpty(error))
                {
                    ViewData["EditError"] = error;
                }
            }
            return PartialView("_QuestionGridViewPartial", _manager.SelectList());
        }

        #endregion

        #region Opitional Answer

        [ValidateInput(false)]
        public ActionResult QuestionDetailGridViewPartial(int questionId)
        {
            ViewData["QuestionId"] = questionId;
            return PartialView("_QuestionDetailGridViewPartial", _manager.SelectOptionalAnswers(questionId));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult QuestionDetailGridViewPartialAddNew(OptionalAnswer item, int questionId)
        {
            ViewData["QuestionId"] = questionId;
            if (ModelState.IsValid)
            {
                var error = _manager.CreateOptionalAnswer(questionId, item);
                if (!string.IsNullOrEmpty(error))
                {
                    ViewData["EditError"] = error;
                }
            }
            else
            {
                ViewData["EditError"] = "信息填写错误。";
            }
            return PartialView("_QuestionDetailGridViewPartial", _manager.SelectById(questionId).OptionalAnswers);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult QuestionDetailGridViewPartialUpdate(OptionalAnswer item, int questionId)
        {
            ViewData["QuestionId"] = questionId;
            if (ModelState.IsValid)
            {
                var error = _manager.ModifyOptionalAnswer(item);
                if (!string.IsNullOrEmpty(error))
                {
                    ViewData["EditError"] = error;
                }
            }
            else
            {
                ViewData["EditError"] = "信息填写错误。";
            }
            var question = _manager.SelectById(questionId);
            return PartialView("_QuestionDetailGridViewPartial", question.OptionalAnswers);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult QuestionDetailGridViewPartialDelete(int answerId, int questionId)
        {
            ViewData["QuestionId"] = questionId;
            if (answerId >= 0)
            {
                var error = _manager.DeleteOptionalAnswer(answerId);
                if (!string.IsNullOrEmpty(error))
                {
                    ViewData["EditError"] = error;
                }
            }
            var question = _manager.SelectById(questionId);
            return PartialView("_QuestionDetailGridViewPartial", question.OptionalAnswers);
        }

        #endregion
    }
}