using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using Supeng.Common;
using Supeng.Web.Common.Securities;

namespace Management
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            ModelBinders.Binders.DefaultBinder = new DevExpressEditorsBinder();

            ASPxWebControl.CallbackError += Application_Error;
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            var exception = HttpContext.Current.Server.GetLastError();
            //TODO: Handle Exception
        }

        protected void Application_PostAuthenticateRequest(object sender, EventArgs e)
        {
            var cookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (cookie != null)
            {
                var ticket = FormsAuthentication.Decrypt(cookie.Value);
                if (ticket != null)
                {
                    var serializeUser = ticket.UserData.StringToObject<LogonUserSerializeModel>();
                    var user = new LogonPrincipal(serializeUser.UserName)
                    {
                        UserId = serializeUser.UserId,
                        UserCode = serializeUser.UserCode,
                        UserName = serializeUser.UserName,
                        Roles = serializeUser.Roles
                    };
                    HttpContext.Current.User = user;
                }
            }
        }
    }
}