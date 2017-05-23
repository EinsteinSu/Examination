using System.Web.Mvc;
using Supeng.Common;
using Supeng.Examination.BusinessLayer;
using Supeng.Examination.BusinessLayer.Models;
using Supeng.Examination.Model;
using Supeng.Web.Common.Securities;

namespace Management.Controllers
{
    [LogonAuthorize(Roles = "Management")]
    public class TestPapersController : Controller
    {
        private readonly IQuestionCategoryManager _categoryManager;
        private readonly ITestPaperManager _manager;
        private readonly IQuestionManager _questionManager;

        public TestPapersController(ITestPaperManager manager, IQuestionManager questionManager,
            IQuestionCategoryManager categoryManager)
        {
            _manager = manager;
            _questionManager = questionManager;
            _categoryManager = categoryManager;
        }

        public TestPapersController()
        {
            _manager = new TestPaperManager();
            _questionManager = new QuestionManager();
            _categoryManager = new QuestionCategoryManager();
        }

        public ActionResult Index()
        {
            return View();
        }


        [ValidateInput(false)]
        public ActionResult TestPaperGridViewPartial()
        {
            return PartialView("_TestPaperGridViewPartial", _manager.SelecTestPaperViewModels());
        }


        public ActionResult QuestionDetails(int id, string error)
        {
            ViewData["Error"] = error;
            ViewBag.TestPaperId = id;
            var model = _manager.GetPaperDetailViewModel(id);
            return View("QuestionDetails", model);
        }

        [ValidateInput(false)]
        public ActionResult QuestionGridViewPartial(int id)
        {
            ViewData["TestPaperId"] = id;
            return PartialView("_QuestionGridViewPartial", _manager.GetTestQuestions(id));
        }

        [ValidateInput(false)]
        public ActionResult OptionalAnswerGridViewPartial(int questionId)
        {
            return PartialView("_OptionalAnswerGridViewPartial", _questionManager.SelectOptionalAnswers(questionId));
        }

        public ActionResult GenerateQuestions(int testPaperId)
        {
            var error = _manager.GenerateTestPaper(testPaperId);
            return RedirectToAction("QuestionDetails", new { id = testPaperId, error });
        }

        #region Formulas Cruds

        [ValidateInput(false)]
        public ActionResult FormulaGridViewPartial(int testPaperId)
        {
            ViewData["TestPaperId"] = testPaperId;
            ViewBag.Categories = _categoryManager.SelectList();
            return PartialView("_FormulaGridViewPartial", _manager.SelectFormulas(testPaperId));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult FormulaGridViewPartialAddNew(TestPaperFormula item, int testPaperId)
        {
            ViewData["TestPaperId"] = testPaperId;
            if (ModelState.IsValid)
            {
                var error = _manager.CreateFormula(testPaperId, item);
                if (!string.IsNullOrEmpty(error))
                {
                    ViewData["EditError"] = error;
                }
            }
            else
            {
                ViewData["EditError"] = "信息填写错误。";
            }
            return PartialView("_FormulaGridViewPartial", _manager.SelectFormulas(testPaperId));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult FormulaGridViewPartialUpdate(TestPaperFormula item, int testPaperId)
        {
            ViewData["TestPaperId"] = testPaperId;
            if (ModelState.IsValid)
            {
                var error = _manager.ModifyFormula(item);
                if (!string.IsNullOrEmpty(error))
                {
                    ViewData["EditError"] = error;
                }
            }
            else
            {
                ViewData["EditError"] = "信息填写错误。";
            }
            return PartialView("_FormulaGridViewPartial", _manager.SelectFormulas(testPaperId));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult FormulaGridViewPartialDelete(int formulaId, int testPaperId)
        {
            ViewData["TestPaperId"] = testPaperId;
            if (formulaId >= 0)
            {
                var error = _manager.DeleteFormula(formulaId);
                if (!string.IsNullOrEmpty(error))
                {
                    ViewData["EditError"] = error;
                }
            }
            return PartialView("_FormulaGridViewPartial", _manager.SelectFormulas(testPaperId));
        }

        #endregion

        #region TestPaper Crud

        [HttpPost, ValidateInput(false)]
        public ActionResult TestPaperGridViewPartialAddNew(TestPaperViewModel item)
        {
            if (ModelState.IsValid)
            {
                var error = _manager.Create(item.CloneDataToTarget<TestPaperViewModel, TestPaper>());
                if (!string.IsNullOrEmpty(error))
                {
                    ViewData["EditError"] = error;
                }
            }
            else
            {
                ViewData["EditError"] = "信息填写错误。";
            }
            return PartialView("_TestPaperGridViewPartial", _manager.SelecTestPaperViewModels());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult TestPaperGridViewPartialUpdate(TestPaperViewModel item)
        {
            if (!_manager.CanEdit(item.TestPaperId))
            {
                ModelState.AddModelError("CannotEdit", "考试已经开始");
            }
            if (ModelState.IsValid)
            {
                var error = _manager.Modify(item.CloneDataToTarget<TestPaperViewModel, TestPaper>());
                if (!string.IsNullOrEmpty(error))
                {
                    ViewData["EditError"] = error;
                }
            }
            else
            {
                if (ModelState["CannotEdit"] != null)
                {
                    ViewData["EditError"] = "考试已经开始";
                }
                else
                {
                    ViewData["EditError"] = "信息填写错误。";
                }
            }
            return PartialView("_TestPaperGridViewPartial", _manager.SelecTestPaperViewModels());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult TestPaperGridViewPartialDelete(int testPaperId)
        {
            if (!_manager.CanEdit(testPaperId))
            {
                return new ContentResult { Content = "考试已经开始，不能删除考试！" };
            }

            if (testPaperId >= 0)
            {
                var error = _manager.Delete(testPaperId);
                if (!string.IsNullOrEmpty(error))
                {
                    return new ContentResult { Content = error };
                }
            }
            return PartialView("_TestPaperGridViewPartial", _manager.SelecTestPaperViewModels());
        }

        #endregion

        public ActionResult QuestionDelete(int questionId, int testPaperId)
        {
            ViewBag.TestPaperId = testPaperId;
            var error = _manager.DeleteTestQuestion(questionId, testPaperId);
            if (!string.IsNullOrEmpty(error))
            {
                return Content(error);
            }
            var model = _manager.GetPaperDetailViewModel(testPaperId);
            return RedirectToAction("QuestionDetails", model);
        }
    }
}