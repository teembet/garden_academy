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
    public class ExamVenueController : Controller
    {
        public const string Success = "Success";
        private IApplicationFormRepository _applicationFormRepository;
        private IConfigurationService _configurationService;
        private IAuditTrailRepository _auditTrailRepository;

        public ExamVenueController(IApplicationFormRepository applicationFormRepository, IConfigurationService configurationService, IAuditTrailRepository auditTrailRepository)
        {
            this._applicationFormRepository = applicationFormRepository;
            this._configurationService = configurationService;
            this._auditTrailRepository = auditTrailRepository;
        }
        public ActionResult Index()
        {
            var examVenues = _configurationService.GetExamVenues();
            return View(examVenues);
        }
        [HttpGet]
        public ActionResult Create()
        {
            var examVenue = new ExamVenueModel();
            examVenue.Venues = _configurationService.GetVenues().Where(x=>x.Active);
            return View(examVenue);
        }
        [HttpPost]
        public ActionResult Create(ExamVenue examVenue)
        {
            //remember to add restriction to using the same venue within a 4 hours time difference
            _configurationService.SaveExamVenue(examVenue);
            TempData["Created"] = Success;
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int examVenueId)
        {
            var examVenue = _configurationService.GetExamVenue(examVenueId);
            var examVenueModel = Mapper.Map<ExamVenue, ExamVenueModel>(examVenue);
            examVenueModel.Venues = _configurationService.GetVenues().Where(x=>x.Active);
            return View(examVenueModel);

        }
        public ActionResult Edit(ExamVenue examVenue)
        {
            var examVenueToUpdate = _configurationService.GetExamVenue(examVenue.Id);
            examVenueToUpdate.ExamDate = examVenue.ExamDate;
            examVenueToUpdate.IsActive = examVenue.IsActive;
            examVenueToUpdate.VenueId = examVenue.VenueId;
            _configurationService.SaveExamVenue(examVenueToUpdate);
            TempData["Edited"] = Success;
            return RedirectToAction("Index");
        }
        public ActionResult Activate(int id)
        {
            var examVenue = _configurationService.GetExamVenue(id);
            examVenue.IsActive = true;
            _configurationService.SaveExamVenue(examVenue);
            TempData["Activate"] = Success;
            return RedirectToAction("Index");
        }
        public ActionResult Deactivate(int id)
        {
            var examVenue = _configurationService.GetExamVenue(id);
            examVenue.IsActive = false;
            _configurationService.SaveExamVenue(examVenue);
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