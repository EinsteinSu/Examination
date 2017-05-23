using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Supeng.Examination.BusinessLayer;
using Supeng.Web.Common.Securities;

namespace Management.Controllers
{
    [LogonAuthorize(Roles = "Management")]
    public class SitesController : Controller
    {
        private ISiteManager manager;
        public SitesController()
        {
            manager = new SiteManager();
        }

        // GET: Sites
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult SiteGridViewPartial()
        {
            return PartialView("_SiteGridViewPartial", manager.SelectList());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult SiteGridViewPartialAddNew(Supeng.Examination.Model.Site item)
        {

            if (ModelState.IsValid)
            {
                var error = manager.Create(item);
                if (!string.IsNullOrEmpty(error))
                {
                    ViewData["EditError"] = error;
                }
            }
            else
                ViewData["EditError"] = "信息填写错误。";
            return PartialView("_SiteGridViewPartial", manager.SelectList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult SiteGridViewPartialUpdate(Supeng.Examination.Model.Site item)
        {
            if (ModelState.IsValid)
            {
                var error = manager.Modify(item);
                if (!string.IsNullOrEmpty(error))
                {
                    ViewData["EditError"] = error;
                }
            }
            else
                ViewData["EditError"] = "信息填写错误。";
            return PartialView("_SiteGridViewPartial", manager.SelectList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult SiteGridViewPartialDelete(int siteId)
        {
            if (siteId >= 0)
            {
                var error = manager.Delete(siteId);
                if (!string.IsNullOrEmpty(error))
                {
                    ViewData["EditError"] = error;
                }
            }
            return PartialView("_SiteGridViewPartial", manager.SelectList());
        }
    }
}