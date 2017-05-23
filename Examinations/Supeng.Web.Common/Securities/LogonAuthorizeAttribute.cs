using System.Configuration;
using System.Web;
using System.Web.Mvc;

namespace Supeng.Web.Common.Securities
{
    public class LogonAuthorizeAttribute : AuthorizeAttribute
    {
        public string UserConfigKey { get; set; }

        public string RoleConfigKey { get; set; }

        protected virtual LogonPrincipal CurrentUser
        {
            get
            {
                return HttpContext.Current.User as LogonPrincipal;
            }
        }

        //protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        //{
        //    filterContext.Result = new ContentResult { Content = "您没有权限访问该页面，请联系系统管理员！" };
        //}

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAuthenticated)
            {
                var users = ConfigurationManager.AppSettings[UserConfigKey];
                var roles = ConfigurationManager.AppSettings[RoleConfigKey];
                Users = string.IsNullOrEmpty(Users) ? users : Users;
                Roles = string.IsNullOrEmpty(Roles) ? roles : Roles;
                if (!string.IsNullOrEmpty(Users) && !Users.Contains(CurrentUser.UserId.ToString()))
                {
                    //return to login page
                    base.OnAuthorization(filterContext);
                }
                //if (string.IsNullOrEmpty(Roles))
                //{
                //    HandleUnauthorizedRequest(filterContext);
                //}
                if (!string.IsNullOrEmpty(Roles) && !CurrentUser.IsInRole(Roles))
                {
                    base.OnAuthorization(filterContext);
                }
            }
            base.OnAuthorization(filterContext);
        }
    }
}
