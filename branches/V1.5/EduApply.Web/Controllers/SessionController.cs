using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using EduApply.Data.Entities;
using EduApply.Logic.Interfaces;
using EduApply.Logic.Service;
using EduApply.Logic.Utility;
using EduApply.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace EduApply.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SessionController : Controller
    {
        public const string Success = "Success";
        private IConfigurationService _config;
        private IAuditTrailRepository _auditTrailRepository;
        public SessionController(IConfigurationService config, IAuditTrailRepository auditTrailRepository)
        {
            this._config = config;
            this._auditTrailRepository = auditTrailRepository;
        }
        // GET: Session
        public ActionResult Index()
        {
            var sessions = _config.GetSessions();
            var model = Mapper.Map<IEnumerable<Session>, IEnumerable<SessionModel>>(sessions);
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Session session)
        {
            try
            {

                var sessions = _config.GetSessions(session.Name);
                if (sessions.Any())
                {
                    AddModelError("Session name entered has already been used for another session");
                    return View();
                }
                if (session.EndDate < session.StartDate)
                {
                    AddModelError("Start Date cannot be greater than End Date");
                    return View();
                }
                //var allSessions = _config.GetSessions();
                //if (allSessions.Any(x => (x.StartDate <= session.StartDate && x.EndDate >= session.EndDate) || (session.StartDate <= x.StartDate && session.EndDate >= x.EndDate) || (session.StartDate <= x.StartDate && session.EndDate > x.StartDate && session.EndDate <= x.EndDate) || (x.StartDate <= session.StartDate && x.EndDate > session.StartDate && x.EndDate <= session.EndDate)))
                //{
                //    AddModelError("The start and end date entered overlaps with another session");
                //    return View();
                //}
                _config.SaveSession(session);
                var userRole = UserManager.GetRoles(User.Identity.GetUserId());
                var auditTrail = new AuditTrail()
                {
                    UserId = User.Identity.GetUserId(),
                    Username = User.Identity.GetUserName(),
                    AuditActionId = Convert.ToInt32(AuditTrailActions.AddSession),
                    Details = "created Session \'" + session.Name + "\'",
                    TimeStamp = DateTime.Now,
                    UserRole = userRole.First(),
                    UserIp = UtilityService.GetIp(System.Web.HttpContext.Current)
                };
                _auditTrailRepository.SaveAuditTrail(auditTrail);
                TempData["Created"] = Success;
                return RedirectToAction("Index");


            }
            catch (Exception)
            {
                TempData["Created"] = "Failed";
                throw;
            }


        }

        [HttpGet]
        public ActionResult Edit(int sessionId)
        {
            var session = _config.GetSession(sessionId);
            var model = Mapper.Map<Session, SessionModel>(session);
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(Session session)
        {
            try
            {
                if (session.EndDate < session.StartDate)
                {
                    AddModelError("Start Date cannot be greater than End Date");
                    var model = Mapper.Map<Session, SessionModel>(session);
                    return View(model);
                }
                var sessions = _config.GetSessions(session.Name).Where(x => x.Id != session.Id).ToList();
                if (sessions.Any())
                {
                    AddModelError("Session name entered has already been used for another session");
                    var model = Mapper.Map<Session, SessionModel>(session);
                    return View(model);
                }
                //var allSessions = _config.GetSessions().Where(x => x.Id != session.Id);
                //if (allSessions.Any(x => (x.StartDate <= session.StartDate && x.EndDate >= session.EndDate) || (session.StartDate <= x.StartDate && session.EndDate >= x.EndDate) || (session.StartDate <= x.StartDate && session.EndDate > x.StartDate && session.EndDate <= x.EndDate) || (x.StartDate <= session.StartDate && x.EndDate > session.StartDate && x.EndDate <= session.EndDate)))
                //{
                //    AddModelError("The start and end date entered overlaps with another session");
                //    var model = Mapper.Map<Session, SessionModel>(session);
                //    return View(model);
                //}

                var sessionToUpdate = _config.GetSession(session.Id);
                sessionToUpdate.Name = session.Name;
                sessionToUpdate.StartDate = session.StartDate;
                sessionToUpdate.EndDate = session.EndDate;
                _config.SaveSession(sessionToUpdate);
                TempData["Edited"] = Success;
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                TempData["Edited"] = "Failed";
                throw;
            }

        }

        public void AddModelError(string error)
        {
            ModelState.AddModelError("", error);
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