using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EduApply.Logic.Interfaces;

namespace EduApply.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ConfigurationController : Controller
    {
        private IRegistrationService _registrationService;
        public ConfigurationController(IRegistrationService registrationService)
        {
            this._registrationService = registrationService;
        }
        // GET: Configuration
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Session");
        }
    }
}