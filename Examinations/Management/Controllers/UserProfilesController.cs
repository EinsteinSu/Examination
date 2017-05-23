using System.Linq;
using System.Web.Mvc;
using Supeng.Examination.BusinessLayer;
using Supeng.Examination.Model;
using Supeng.Web.Common.Securities;

namespace Management.Controllers
{
    [LogonAuthorize(Roles = "Management")]
    public class UserProfilesController : Controller
    {
        private readonly IUserProfileManager _manager;
        private readonly ISiteManager _siteManager;
        private readonly ISecurityRoleManager _securityRoleManager;
        public UserProfilesController(IUserProfileManager manager, ISiteManager siteManager, ISecurityRoleManager securityRoleManager)
        {
            _manager = manager;
            _siteManager = siteManager;
            _securityRoleManager = securityRoleManager;
        }

        public UserProfilesController()
        {
            _manager = new UserProfileManager();
            _siteManager = new SiteManager();
            _securityRoleManager = new SecurityRoleManager();
        }

        // GET: UserProfiles
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult UserProfileGridViewPartial()
        {
            ViewBag.SiteList = _siteManager.SelectList();
            ViewBag.RoleList = _securityRoleManager.SelectList();
            return PartialView("_UserProfileGridViewPartial", _manager.SelectList());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult UserProfileGridViewPartialAddNew(UserProfile item)
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
            var model = _manager.SelectList();
            return PartialView("_UserProfileGridViewPartial", model.ToList());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult UserProfileGridViewPartialUpdate(UserProfile item)
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
            var model = _manager.SelectList();
            return PartialView("_UserProfileGridViewPartial", model.ToList());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult UserProfileGridViewPartialDelete(int UserId)
        {
            if (UserId >= 0)
            {
                var error = _manager.Delete(UserId);
                if (!string.IsNullOrEmpty(error))
                {
                    ViewData["EditError"] = error;
                }
            }
            var model = _manager.SelectList();
            return PartialView("_UserProfileGridViewPartial", model.ToList());
        }

        public ActionResult InlineEditingAddNewPartial(int id)
        {
            var model = _manager.SelectList();
            return PartialView("_UserProfileGridViewPartial", model.ToList());
        }
    }
}