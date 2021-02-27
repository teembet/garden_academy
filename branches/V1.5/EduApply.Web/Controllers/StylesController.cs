using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EduApply.Web.Infrastructure;

namespace EduApply.Web.Controllers
{
    public class StylesController : Controller
    {
        // GET: Theme
        public ActionResult Index()
        {
            return Content("Styles folder");
        }

        protected override void HandleUnknownAction(string actionName)
        {
            if (actionName.ToLower().Contains(".css"))
            {
                var res = this.CssFromView(actionName);
                res.ExecuteResult(ControllerContext);
            }
        }


    }
}