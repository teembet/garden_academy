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

namespace EduApply.Web.Controllers
{

    [Authorize]
    public class ReferenceController : Controller
    {
        private IRegistrationService _registrationService;
        private IApplicationFormRepository _appForm;
        private IEventLogRepository _eventLogRepo;
        private IStateManager _stateManager;
        private IConfigurationService _configurationService;
        public ReferenceController(IRegistrationService registrationService,IConfigurationService configurationService, IApplicationFormRepository appForm, IEventLogRepository eventLogRepo, IStateManager stateManager)
        {
            this._registrationService = registrationService;
            this._appForm = appForm;
            this._eventLogRepo = eventLogRepo;
            this._stateManager = stateManager;
            this._configurationService = configurationService;
        }

        public ActionResult Delete(long id)
        {
            try
            {
                var reference = _registrationService.GetReference(id);
                _registrationService.DeleteReference(reference);
            }
            catch (Exception)
            {
                ViewBag.message = "Something went wrong";
                return View("ApplicationError");
            }
            return RedirectToAction("AddReference");
        }
        [HttpPost]
        public ActionResult Update(long id, string editRefName, string editRefOccupation, string editRefAddress, string editRefPhone, string editRefEmail)
        {
            var reference = _registrationService.GetReference(id);
            reference.Name = editRefName;
            reference.Occupation = editRefOccupation;
            reference.Address = editRefAddress;
            reference.PhoneNumber = editRefPhone;
            reference.Email = editRefEmail;
            _registrationService.SaveReference(reference);
            return RedirectToAction("AddReference");
        }
        [HttpGet]
        public ActionResult AddReference()
        {
            var applicationId = Convert.ToInt64(Session["AppId"]);

            var application = _registrationService.GetApplicationDetails(applicationId);
            var templatesInAppForm = _appForm.GeTemplatesInApp(application.AppFormId).ToList();

            var isRightState = _stateManager.ConfirmFillStage(application, templatesInAppForm, "REF", Session["WorkFlowList"] as List<ApplicationFormWorkFlow>, "Fill");
            if (!isRightState || application.UserName != User.Identity.GetUserName())
            {
                application.FillStage = 0;
                application.WorkFlowStage = 0;
                _registrationService.SaveApplication(application);
                ViewBag.message = "Please follow the right process, Do not copy and paste URL's";
                return View("ApplicationError");
                //return RedirectToAction("WorkFlowManager", "Application");
            }

            var references = _registrationService.GetReferences(applicationId).ToList();
            Session["References"] = references;

            //check that entry has not reached maximum entry.
            var formTemplateList = Session["FormTemplates"] as List<TemplatesInAppForms> ?? new List<TemplatesInAppForms>();
            var refTemplate = formTemplateList[application.FillStage];

            //get the settings for the formtemplate to see its min and max entry
            var templateSettings = _registrationService.GetFormTemplateSettings(refTemplate.ApplicationFormId, refTemplate.FormTemplateId);
            ViewBag.maxEntry = templateSettings.MaxEntry;
            //var model = Mapper.Map<List<Reference>, List<ReferenceModel>>(references);
            return View(references);
        }
        [HttpPost]
        public ActionResult AddReference(string refName, string refOccupation, string refAddress, string refPhone, string refEmail)
        {
            var applicationId = Convert.ToInt64(Session["AppId"]);
            var application = _registrationService.GetApplicationDetails(applicationId);

            //check that entry has not reached maximum entry.
            var formTemplateList = Session["FormTemplates"] as List<TemplatesInAppForms> ?? new List<TemplatesInAppForms>();
            var refTemplate = formTemplateList[application.FillStage];

            //get the settings for the formtemplate to see its min and max entry
            var templateSettings = _registrationService.GetFormTemplateSettings(refTemplate.ApplicationFormId, refTemplate.FormTemplateId);
            var referenceErrorModel = Session["References"] as List<Reference> ?? new List<Reference>();
            var numOfrefDetailsEntered = referenceErrorModel.Count;

            if (templateSettings.MaxEntry == numOfrefDetailsEntered)
            {
                ModelState.AddModelError("", "You have already entered the maximum number of References allowed for this form, Please click continue to proceed with application");
                //var modelError = Mapper.Map<List<Reference>, List<ReferenceModel>>(referenceErrorModel);
                ViewBag.maxEntry = templateSettings.MaxEntry;
                return View(referenceErrorModel);
            }

            var references = Session["References"] as List<Reference> ?? new List<Reference>();
            var referee = new Reference()
            {
                Name = refName,
                Occupation = refOccupation,
                Address = refAddress,
                PhoneNumber = refPhone,
                Email = refEmail,
                ApplicationId = applicationId
            };
            _registrationService.SaveReference(referee);
            references.Add(referee);
            Session["References"] = references;
            // var model = Mapper.Map<List<Reference>, List<ReferenceModel>>(references);
            ViewBag.maxEntry = templateSettings.MaxEntry;
            return View(references);
        }

        public ActionResult ContinueFromReference()
        {
            var applicationId = Convert.ToInt64(Session["AppId"]);
            var application = _registrationService.GetApplicationDetails(applicationId);
            //check that entry is greater than minimu value
            var formTemplateList = Session["FormTemplates"] as List<TemplatesInAppForms> ?? new List<TemplatesInAppForms>();
            var refTemplate = formTemplateList[application.FillStage];
            var templateSettings = _registrationService.GetFormTemplateSettings(refTemplate.ApplicationFormId,
                refTemplate.FormTemplateId);
            var referenceErrorModel = Session["References"] as List<Reference> ?? new List<Reference>();
            var numOfrefDetailsEntered = referenceErrorModel.Count;

            if (templateSettings.MinEntry > numOfrefDetailsEntered)
            {
                ModelState.AddModelError("", "You have to enter at least " + templateSettings.MinEntry + " reference(s) to coontinue application");
                // var modelError = Mapper.Map<List<Reference>, List<ReferenceModel>>(referenceErrorModel);
                ViewBag.maxEntry = templateSettings.MaxEntry;
                return View("AddReference", referenceErrorModel);
            }

            //first save References
            //var references = Session["References"] as List<Reference>;
            //if (references != null && references.Any())
            //{
            //    foreach (var referee in references)
            //    {
            //        _registrationService.SaveReference(referee);
            //    }
            //}

            //Get the current Application Details
            //var applicationId = Convert.ToInt64(Session["AppId"]);
            //var application = _registrationService.GetApplicationDetails(applicationId);
            var RefTemplt = _configurationService.GetFormTemplateByCode("REF");
            if (RefTemplt == null)
            {
                ViewBag.message = "An error occured, Please try again";
                return View("ApplicationError");
            }
            var currentFS = formTemplateList.FindIndex(x => x.FormTemplateId == RefTemplt.Id);
            application.FillStage = currentFS + 1;
            _registrationService.SaveApplication(application);

            //Get the current Application Form Details
            var appFormWorkFlow = Session["WorkFlowList"] as List<ApplicationFormWorkFlow>;
            var applicationForm = _appForm.GetAppForms(appFormWorkFlow.FirstOrDefault().ApplicationFormId);
           
            var fiilWf = _configurationService.GetWorkFlowbyActionName("Fill");
            if (fiilWf == null)
            {
                ViewBag.message = "An error occured, please try again";
                return View("ApplicationError");
            }

            //save event in the Event Log
            var workflowId = fiilWf.Id;
            var formTemplateId = formTemplateList[application.FillStage - 1].FormTemplateId;
            var localTime = _configurationService.GetCurrentWestAfricanDateTime();
            var eventLog = new EventLog()
            {
                ApplicationFormId = applicationForm.Id,
                Username = User.Identity.Name,
                WorkFlowId = workflowId,
                Action = "added his/her references to form" + applicationForm.Name,
                Timestamp = localTime,
                FormTemplateId = formTemplateId,
                AppNum = application.AppNum
            };
            _eventLogRepo.SaveEvent(eventLog);

            //  var formTemplateList = Session["FormTemplates"] as List<TemplatesInAppForms>;
            // formTemplateList.RemoveAt(0);
            if (application.FillStage < formTemplateList.Count)
            {
                var nextTemplateInApp = formTemplateList[application.FillStage];
                var formTemplate = _appForm.GetFormTemplate(nextTemplateInApp.FormTemplateId);
                switch (formTemplate.Code)
                {
                    case "BD":
                        return RedirectToAction("SaveBiodata", "Fill");
                    case "OLR":
                        return RedirectToAction("SaveOLevelResult", "Fill");
                    case "ED":
                        return RedirectToAction("AddEducationalDetail", "EducationalDetails");
                    case "WE":
                        return RedirectToAction("AddWorkExperience", "WorkExperience");
                    case "REF":
                        return RedirectToAction("AddReference", "Reference");
                    case "PU":
                        return RedirectToAction("SavePassport", "Fill");
                    case "CU":
                        return RedirectToAction("SaveCertificate", "Fill");
                    case "PC":
                        return RedirectToAction("AddProgramCourse", "ApplicantsProgramCourse");
                    case "REG":
                        return RedirectToAction("SaveRegNum", "Fill");
                    case "JR":
                        return RedirectToAction("AddJambScore","Fill");
                }
            }
            else
            {
                var app = _registrationService.GetApplicationDetails(applicationId);
                var currentWS = appFormWorkFlow.FindIndex(x => x.WorkFlowId == fiilWf.Id);
                app.WorkFlowStage = currentWS + 1;
                _registrationService.SaveApplication(app);
                //this means we have completed all the templates in the application so we remove the fill workflow from the list of workflow
                //and redirect to the next workflow;
                //  appFormWorkFlow.RemoveAt(0);
                if (app.WorkFlowStage < appFormWorkFlow.Count)
                {
                    var nextWorkFlow = appFormWorkFlow[application.WorkFlowStage];
                    var workFlowItem = _appForm.GetWorkFlowItem(nextWorkFlow.WorkFlowId);
                    Session["WorkFlowList"] = appFormWorkFlow;
                    switch (workFlowItem.Name)
                    {
                        case "Pay":
                            return RedirectToAction("ValidatePayment", "Payment");
                        case "Fill":
                            return RedirectToAction("Fill", "Fill");
                        case "Validate Jamb Score":
                            return RedirectToAction("ValidateJamb", "Validation");
                        case "Validate Application Result":
                            return RedirectToAction("ValidateApplicationResult", "Validation");
                        case "Validate Non-Application Result":
                            return RedirectToAction("ValidateNonApplicationResult", "Validation");
                        case "Validate Admission Status":
                            return RedirectToAction("CheckAdmissionStatus", "Validation");
                        case "Venue Assignment":
                            return RedirectToAction("VenueAssignment", "Validation");
                        case "Validate Direct Entry":
                            return RedirectToAction("DirectEntryValidation", "Validation");

                    }
                }
                else
                {
                    return RedirectToAction("ApplicationPreview", "Application");
                }

                //then select nextworkflow and redirect appropriately
            }
            ViewBag.message = "Something went wrong";
            return View("ApplicationError");
        }
        //
        // GET: /Reference/
        public ActionResult Index()
        {
            return View();
        }
    }
}