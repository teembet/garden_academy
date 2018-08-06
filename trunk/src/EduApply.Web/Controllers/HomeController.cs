using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using EduApply.Data.Entities;
using EduApply.Logic.Interfaces;
using EduApply.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace EduApply.Web.Controllers
{

    [Authorize]
    public class HomeController : Controller
    {
        private IApplicationFormRepository _appForm;
        private IRegistrationService _registrationService;
        private IRepository _repo;
        private ILocationRepository _lga;
        private IApplicationFormRepository _appFormRepo;
        private IConfigurationService _configurationService;

        public HomeController(IApplicationFormRepository appForm, IRepository repo, ILocationRepository lga, IRegistrationService registrationService, IApplicationFormRepository appFormRepo, IConfigurationService configurationService)
        {
            this._appForm = appForm;
            this._repo = repo;
            this._lga = lga;
            this._registrationService = registrationService;
            this._appFormRepo = appFormRepo;
            this._configurationService = configurationService;
        }
        [Authorize(Roles = "Student")]
        public ActionResult Index()
        {
            var localTime = _configurationService.GetCurrentWestAfricanDateTime();
            // var _ssss = _appForm.GetAppForms();
            var appForms = _appForm.GetAppForms().Where(x => x.StartDate <= localTime && x.EndDate >= localTime).OrderByDescending(x => x.Id);
            var appFormsModel = Mapper.Map<IEnumerable<ApplicationForm>, IEnumerable<ApplicationFormModel>>(appForms);



            var applicationViewModelList = new List<ApplicationViewModel>();
            var submittedApplicationList = new List<SubmittedApplicationViewModel>();

            var applications = _registrationService.GetApplicationDetails(User.Identity.GetUserName()).ToList();
            var savedApplications = applications.Where(x => x.IsSubmitted == false).OrderByDescending(x => x.Id);
            var submittedApplications = applications.Where(x => x.IsSubmitted).OrderByDescending(x => x.Id);
            foreach (var item in savedApplications)
            {
                var appForm = _appFormRepo.GetAppForms(item.AppFormId);
                if (appForm.EndDate > localTime)
                {
                    var applicationViewModel = new ApplicationViewModel();
                    applicationViewModel.ApplicationId = item.Id;
                    applicationViewModel.ApplicationClosingDate = appForm.EndDate;
                    applicationViewModel.AppFormName = appForm.Name;
                    applicationViewModel.ApplicationFormId = appForm.Id;

                    applicationViewModelList.Add(applicationViewModel);
                }
            }
            foreach (var item in submittedApplications)
            {
                var appForm = _appFormRepo.GetAppForms(item.AppFormId);
                var submittedApplicationViewModel = new SubmittedApplicationViewModel();
                submittedApplicationViewModel.ApplicationId = item.Id;
                submittedApplicationViewModel.AppFormName = appForm.Name;
                submittedApplicationViewModel.ApplicationFormId = appForm.Id;

                submittedApplicationList.Add(submittedApplicationViewModel);
            }
            var applyActions = new ApplicantsViewModel();
            applyActions.OpenApplicationForms = appFormsModel;
            applyActions.SavedApplications = applicationViewModelList;
            applyActions.SubmittedApplications = submittedApplicationList;//new List<SubmittedApplicationViewModel>();

            return View(applyActions);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}