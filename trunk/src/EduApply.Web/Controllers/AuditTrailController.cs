using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EduApply.Logic.Interfaces;
using EduApply.Web.Models;
using Microsoft.AspNet.Identity.Owin;

namespace EduApply.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AuditTrailController : Controller
    {
        private IAuditTrailRepository _auditTrailRepository;
        public AuditTrailController(IAuditTrailRepository auditTrailRepository)
        {
            this._auditTrailRepository = auditTrailRepository;
        }
        //
        // GET: /AuditTrail/

        //public ActionResult GetAuditTrail(int? auditSectionId, int? auditActionId, DateTime? startDate, DateTime? endDate)
        //{

        //}
        public ActionResult GetAuditActions(int? auditSectionId)
        {

            var auditTrails = _auditTrailRepository.GetAuditActions(auditSectionId);
            var result = from a in auditTrails
                         select new
                         {
                             id = a.Id,
                             name = a.Name
                         };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAuditTrailList(int? auditSectionId, int? auditActionId, DateTime? startDate, DateTime? endDate, string userRole, string keyword)
        {
            // var auditSections = _auditTrailRepository.GetAuditSections();
            var auditTrails = _auditTrailRepository.GetAuditTrails(auditSectionId, auditActionId, startDate, endDate, userRole, keyword).OrderByDescending(x => x.TimeStamp);
            var result = from s in auditTrails
                         select new
                         {
                             //auditSection = "section",
                             trailId = s.Id,
                             username = s.Username,
                             auditAction = _auditTrailRepository.GetAuditAction(s.AuditActionId).Name,
                             details = s.Details,
                             userIp = s.UserIp,
                             userRole = s.UserRole,
                             timestamp = s.TimeStamp.ToString("dd-MMM-yyyy h:mm tt")
                         };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Index()
        {
            var model = new AuditTrailModel()
            {
                AuditSections = _auditTrailRepository.GetAuditSections(),
                AuditActions = _auditTrailRepository.GetAuditActions(),
                Roles = RoleManager.Roles
            };
            return View(model);
        }

        private ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        private ApplicationRoleManager RoleManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationRoleManager>();
            }
        }
    }
}