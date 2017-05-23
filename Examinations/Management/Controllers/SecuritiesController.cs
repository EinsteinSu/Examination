using System.Collections.Generic;
using System.Web.Mvc;
using DevExpress.Web.Mvc;
using Supeng.Examination.BusinessLayer;
using Supeng.Examination.BusinessLayer.Models;
using Supeng.Examination.Model;
using Supeng.Web.Common.Securities;

namespace Management.Controllers
{
    [LogonAuthorize(Roles = "Management")]
    public class SecuritiesController : Controller
    {
        private readonly ISecurityRoleManager _manager;

        public SecuritiesController(ISecurityRoleManager securityRoleManager)
        {
            _manager = securityRoleManager;
        }

        public SecuritiesController()
        {
            _manager = new SecurityRoleManager();
        }

        // GET: Securities
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult SecurityGridViewPartial()
        {
            var model = _manager.SelectList();
            return PartialView("_SecurityGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult SecurityGridViewPartialAddNew(SecurityRole item)
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
            return PartialView("_SecurityGridViewPartial", _manager.SelectList());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult SecurityGridViewPartialUpdate(SecurityRole item)
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
            return PartialView("_SecurityGridViewPartial", _manager.SelectList());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult SecurityGridViewPartialDelete(int securityRoleId)
        {
            if (securityRoleId >= 0)
            {
                var error = _manager.Delete(securityRoleId);
                if (!string.IsNullOrEmpty(error))
                {
                    ViewData["EditError"] = error;
                    //todo: it can be optimize that make a html display this error
                    return new ContentResult { Content = error };
                }
            }
            return PartialView("_SecurityGridViewPartial", _manager.SelectList());
        }

        public ActionResult SecurityRoleActionEdit(int id)
        {
            return View("SecurityRoleActionSetting", _manager.SelectSecurityRoleActionViewModel(id));
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SecurityRoleActionEdit(
            [ModelBinder(typeof(DevExpressEditorsBinder))]SecurityRoleActionViewModel model,
            int roleId)
        {
            var list = CheckBoxListExtension.GetSelectedValues<int>("actionList") ?? new int[0];
            var error = _manager.UpdateSecurity(roleId, list);
            if (!string.IsNullOrEmpty(error))
            {
                ViewData["Error"] = error;
                return View("SecurityRoleActionSetting", _manager.SelectSecurityRoleActionViewModel(roleId));
            }
            return RedirectToAction("Index");
        }
    }
}