using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Supeng.Web.Common.Securities;

namespace Entry.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            // DXCOMMENT: Pass a data model for GridView
            return View();    
        }
        
     
    
    }
}