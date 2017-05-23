using System.Web.Mvc;
using Supeng.Examination.BusinessLayer;

namespace Management.Controllers
{
    public class PasswordResetController : Controller
    {
        private readonly IUserProfileManager _manager;

        public PasswordResetController(IUserProfileManager manager)
        {
            _manager = manager;
        }

        public PasswordResetController()
        {
            _manager = new UserProfileManager();
        }

        // GET: PasswordReset
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult PasswordResetGridViewPartial()
        {
            return PartialView("_PasswordResetGridViewPartial", _manager.SelectList());
        }

        public ActionResult ResetPassword(int id)
        {
            return View("ResetPassword", _manager.SelectById(id));
        }

        public ActionResult ResetUserPassword(int id)
        {
            _manager.ResetPassword(id);
            return RedirectToAction("Index");
        }
    }
}