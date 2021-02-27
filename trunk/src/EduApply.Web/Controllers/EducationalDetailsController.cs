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
    public class EducationalDetailsController : Controller
    {
        private IRegistrationService _registrationService;
        private IApplicationFormRepository _appForm;
        private IEventLogRepository _eventLogRepo;
        private IStateManager _stateManager;
        private IConfigurationService _configurationService;
        public EducationalDetailsController(IRegistrationService registrationService, IConfigurationService configurationService, IApplicationFormRepository appForm, IEventLogRepository eventLogRepo, IStateManager stateManager)
        {
            this._registrationService = registrationService;
            this._appForm = appForm;
            this._eventLogRepo = eventLogRepo;
            this._stateManager = stateManager;
            this._configurationService = configurationService;
        }
        //
        // GET: /EducationalDetails/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Delete(long id)
        {
            try
            {
                var educattionalDetail = _registrationService.GetEducationalDetail(id);
                _registrationService.DeleteEducationalDetail(educattionalDetail);
            }
            catch (Exception)
            {
                ViewBag.message = "Something went wrong";
                return View("ApplicationError");
            }
            return RedirectToAction("AddEducationalDetail");
        }

        public ActionResult Update(long id, string editSchName, string editQualification, string editClassOfDegree,string editGradMonth, int editEntryYear, int editGradYear)
        {
            var educationalDetail = _registrationService.GetEducationalDetail(id);
            educationalDetail.SchoolName = editSchName;
            educationalDetail.Qualification = editQualification;
            educationalDetail.ClassOfDegree = editClassOfDegree;
            educationalDetail.EntryYear = editEntryYear;
            educationalDetail.GraduationMonth = editGradMonth;
            educationalDetail.GraduationYear = editGradYear;
            _registrationService.SaveEducationalDetails(educationalDetail);
            return RedirectToAction("AddEducationalDetail");
        }

        [HttpGet]
        public ActionResult AddEducationalDetail()
        {
            var applicationId = Convert.ToInt64(Session["AppId"]);
            var application = _registrationService.GetApplicationDetails(applicationId);

            var templatesInAppForm = _appForm.GeTemplatesInApp(application.AppFormId).ToList();

            var isRightState = _stateManager.ConfirmFillStage(application, templatesInAppForm, "ED", Session["WorkFlowList"] as List<ApplicationFormWorkFlow>, "Fill");
            if (!isRightState || application.UserName != User.Identity.GetUserName())
            {
                application.FillStage = 0;
                application.WorkFlowStage = 0;
                _registrationService.SaveApplication(application);
                ViewBag.message = "Please follow the right process, Do not copy and paste URL's";
                return View("ApplicationError");
                //return RedirectToAction("WorkFlowManager", "Application");
            }


            var educationalDetails = _registrationService.GetEducationalDetails(applicationId).ToList();
            var classOfDegres = _configurationService.GetDegrees(application.AppFormId);
            Session["EducationalDetails"] = educationalDetails;
            Session["FormCog"] = classOfDegres;
            // var model = Mapper.Map<List<EducationalDetails>, List<EducationalDetailsModel>>(educationalDetails);

            var formTemplateList = Session["FormTemplates"] as List<TemplatesInAppForms> ?? new List<TemplatesInAppForms>();
            var educationalDetailsTemplate = formTemplateList[application.FillStage];
            var templateSettings = _registrationService.GetFormTemplateSettings(educationalDetailsTemplate.ApplicationFormId, educationalDetailsTemplate.FormTemplateId);
            ViewBag.maxEntry = templateSettings.MaxEntry;
            var model = new EducationalDetailsCollection()
            {
                MaxEntry = templateSettings.MaxEntry,
                EducationalDetails = educationalDetails,
                ClassOfDegrees = classOfDegres
            };
            return View(model);
        }
        [HttpPost]
        public ActionResult AddEducationalDetail(string schoolName, string qualification, string classOfDegree, string otherDegree, int entryYear,string graduationMonth, int graduationYear)
        {
            var applicationId = Convert.ToInt64(Session["AppId"]);
            var application = _registrationService.GetApplicationDetails(applicationId);

            //check that entry has not reached maximum entry.
            var formTemplateList = Session["FormTemplates"] as List<TemplatesInAppForms> ?? new List<TemplatesInAppForms>();
            var educationalDetailsTemplate = formTemplateList[application.FillStage];
            var templateSettings = _registrationService.GetFormTemplateSettings(educationalDetailsTemplate.ApplicationFormId,
                educationalDetailsTemplate.FormTemplateId);
            var educationalDetails = Session["EducationalDetails"] as List<EducationalDetails> ?? new List<EducationalDetails>();
            var eduDetailsModel = new EducationalDetailsCollection();

            var numOfEduDetailsEntered = educationalDetails.Count;

            if (templateSettings.MaxEntry == numOfEduDetailsEntered)
            {
                ModelState.AddModelError("", "You have already entered the maximum number of Educational Details allowed for this form,Please click continue to proceed with application");

                eduDetailsModel.MaxEntry = templateSettings.MaxEntry;
                eduDetailsModel.EducationalDetails = educationalDetails;
                eduDetailsModel.ClassOfDegrees = Session["FormCog"] as List<ClassOfDegree> ?? new List<ClassOfDegree>();
                return View(eduDetailsModel);
            }


            var eduDet = new EducationalDetails()
            {
                SchoolName = schoolName,
                Qualification = qualification,
                ClassOfDegree = classOfDegree != "Others" ? classOfDegree : otherDegree,
                EntryYear = entryYear,
                GraduationMonth = graduationMonth,
                GraduationYear = graduationYear,
                ApplicationId = applicationId
            };
            _registrationService.SaveEducationalDetails(eduDet);
            educationalDetails.Add(eduDet);
            Session["EducationalDetails"] = educationalDetails;

            eduDetailsModel.MaxEntry = templateSettings.MaxEntry;
            eduDetailsModel.EducationalDetails = educationalDetails;
            eduDetailsModel.ClassOfDegrees = Session["FormCog"] as List<ClassOfDegree> ?? new List<ClassOfDegree>();

            return View(eduDetailsModel);
        }
        public ActionResult ContinueFromEducationalDetails()
        {
            //check that entry is not less than minimum entry.
            var applicationId = Convert.ToInt64(Session["AppId"]);
            var application = _registrationService.GetApplicationDetails(applicationId);
            var formTemplateList = Session["FormTemplates"] as List<TemplatesInAppForms> ?? new List<TemplatesInAppForms>();

            var educationalDetailsTemplate = formTemplateList[application.FillStage];
            var templateSettings = _registrationService.GetFormTemplateSettings(educationalDetailsTemplate.ApplicationFormId, educationalDetailsTemplate.FormTemplateId);
            var educationalDetails = Session["EducationalDetails"] as List<EducationalDetails> ?? new List<EducationalDetails>();
            var numOfEduDetailsEntered = educationalDetails.Count;

            if (templateSettings.MinEntry > numOfEduDetailsEntered)
            {
                ModelState.AddModelError("", "You have to enter at least " + templateSettings.MinEntry + " educational detail");
                var errorModel = new EducationalDetailsCollection()
                {
                    MaxEntry = templateSettings.MaxEntry,
                    EducationalDetails = educationalDetails,
                    ClassOfDegrees = Session["FormCog"] as List<ClassOfDegree> ?? new List<ClassOfDegree>()
                };
                return View("AddEducationalDetail", errorModel);
            }

            //increase the fill stage
            var EdTemplt = _configurationService.GetFormTemplateByCode("ED");
            if (EdTemplt == null)
            {
                ViewBag.message = "An error occured, Please try again";
                return View("ApplicationError");
            }
            var currentFS = formTemplateList.FindIndex(x => x.FormTemplateId == EdTemplt.Id);
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
                Action = "added his/her Educational Details for" + applicationForm.Name,
                Timestamp = localTime,
                FormTemplateId = formTemplateId,
                AppNum = application.AppNum
            };
            _eventLogRepo.SaveEvent(eventLog);


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
                        return RedirectToAction("AddJambScore", "Fill");
                }
            }
            else
            {
                var app = _registrationService.GetApplicationDetails(applicationId);
                var currentWS = appFormWorkFlow.FindIndex(x => x.WorkFlowId == fiilWf.Id);
                app.WorkFlowStage = currentWS + 1;
                _registrationService.SaveApplication(app);

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
            }
            ViewBag.message = "Something went wrong";
            return View("ApplicationError");
        }
    }
}