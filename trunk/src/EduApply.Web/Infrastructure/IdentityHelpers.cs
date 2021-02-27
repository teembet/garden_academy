using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EduApply.Data.Entities;
using Microsoft.AspNet.Identity.Owin;

namespace EduApply.Web.Infrastructure
{
    public static class IdentityHelpers
    {
        static IdentityHelpers()
        {
            
        }
        public static MvcHtmlString GetUserName(this HtmlHelper html, string id)
        {
            ApplicationUserManager mgr = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            return new MvcHtmlString(mgr.FindByIdAsync(id).Result.UserName);
        }

    }
}