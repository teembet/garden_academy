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

    [Authorize]
    public class ExamVenueController : Controller
    {
        public const string Success = "Success";
        private IApplicationFormRepository _applicationFormRepository;
        private IConfigurationService _configurationService;
        private IAuditTrailRepository _auditTrailRepository;

        private IVenueAssignmentService _venueService;

        public ExamVenueController(IApplicationFormRepository applicationFormRepository,
            IVenueAssignmentService venueService,
            IConfigurationService configurationService, IAuditTrailRepository auditTrailRepository)
        {
            this._applicationFormRepository = applicationFormRepository;
            this._configurationService = configurationService;
            this._auditTrailRepository = auditTrailRepository;

            this._venueService = venueService;
        }
        public ActionResult Index()
        {
            var examVenues = _venueService.GetExamVenues();
            return View(examVenues);
        }
        [HttpGet]
        public ActionResult Create()
        {
            var examVenue = new ExamVenueModel();
            examVenue.Venues = _venueService.GetVenues().Where(x => x.Active);
            return View(examVenue);
        }
        [HttpPost]
        public ActionResult Create(ExamVenue examVenue)
        {
            //remember to add restriction to using the same venue within a 4 hours time difference
            _venueService.SaveExamVenue(examVenue);
            var venue = _venueService.GetVenue(examVenue.VenueId);
            TempData["Created"] = Success;
            var IUtilityService = EngineContext.Resolve<IUtilityService>();

            //Add to Audit Trail
            var userRole = UserManager.GetRoles(User.Identity.GetUserId());
            var localTime = _configurationService.GetCurrentWestAfricanDateTime();
            var userId = User.Identity.GetUserId();
            var userName = User.Identity.GetUserName();
            var auditTrail = new AuditTrail()
            {
                UserId = userId,
                Username = userName,
                AuditActionId = Convert.ToInt32(AuditTrailActions.AddExamVenue),
                Details = "Scheduled Venue  \'" + venue.Name + "\' for " + examVenue.ExamDate.ToString("dd-MMM-yyyy h:mm tt"),
                TimeStamp = localTime,
                UserRole = userRole.First(),
                UserIp = IUtilityService.GetIp()
            };
            _auditTrailRepository.SaveAuditTrail(auditTrail);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int examVenueId)
        {
            var examVenue = _venueService.GetExamVenue(examVenueId);
            var examVenueModel = Mapper.Map<ExamVenue, ExamVenueModel>(examVenue);
            examVenueModel.Venues = _venueService.GetVenues().Where(x => x.Active);
            return View(examVenueModel);

        }
        public ActionResult Edit(ExamVenue examVenue)
        {
            var examVenueToUpdate = _venueService.GetExamVenue(examVenue.Id);
            examVenueToUpdate.ExamDate = examVenue.ExamDate;
            examVenueToUpdate.IsActive = examVenue.IsActive;
            examVenueToUpdate.VenueId = examVenue.VenueId;
            _venueService.SaveExamVenue(examVenueToUpdate);
            TempData["Edited"] = Success;
            var IUtilityService = EngineContext.Resolve<IUtilityService>();
            //Add to Audit Trail

            var userRole = UserManager.GetRoles(User.Identity.GetUserId());
            var localTime = _configurationService.GetCurrentWestAfricanDateTime();
            var auditTrail = new AuditTrail()
            {
                UserId = User.Identity.GetUserId(),
                Username = User.Identity.GetUserName(),
                AuditActionId = Convert.ToInt32(AuditTrailActions.AddExamVenue),
                Details = "edited schedule for  \'" + examVenueToUpdate.Venue.Name + "\'",
                TimeStamp = localTime,
                UserRole = userRole.First(),
                UserIp = IUtilityService.GetIp()
            };
            _auditTrailRepository.SaveAuditTrail(auditTrail);
            return RedirectToAction("Index");
        }
        public ActionResult Activate(int id)
        {
            var examVenue = _venueService.GetExamVenue(id);
            examVenue.IsActive = true;
            _venueService.SaveExamVenue(examVenue);
            TempData["Activate"] = Success;
            return RedirectToAction("Index");
        }
        public ActionResult Deactivate(int id)
        {
            var examVenue = _venueService.GetExamVenue(id);
            examVenue.IsActive = false;
            _venueService.SaveExamVenue(examVenue);
            TempData["Deactivate"] = Success;
            return RedirectToAction("Index");
        }
        private ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }
    }
}