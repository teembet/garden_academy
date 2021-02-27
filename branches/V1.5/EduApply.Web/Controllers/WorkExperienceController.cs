using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using EduApply.Data.Entities;
using EduApply.Logic.Interfaces;
using EduApply.Web.Models;

namespace EduApply.Web.Controllers
{
    public class WorkExperienceController : Controller
    {
        private IRegistrationService _registrationService;
        private IApplicationFormRepository _appForm;
        private IEventLogRepository _eventLogRepo;
        private IStateManager _stateManager;
        public WorkExperienceController(IRegistrationService registrationService, IApplicationFormRepository appForm, IEventLogRepository eventLogRepo, IStateManager stateManager)
        {
            this._registrationService = registrationService;
            this._appForm = appForm;
            this._eventLogRepo = eventLogRepo;
            this._stateManager = stateManager;
        }

        public ActionResult Delete(long wrkExpId)
        {
            try
            {
                var workExperience = _registrationService.GetWorkExperienceById(wrkExpId);
                _registrationService.DeleteWorkExperience(workExperience);
            }
            catch (Exception)
            {
                ViewBag.message = "Something went wrong";
                return View("ApplicationError");
            }
            return RedirectToAction("AddWorkExperience");
        }

        public ActionResult Update(long id, string editOrg, DateTime editFromDate, DateTime editToDate, string editPosition, string editJobDesc)
        {
            var workExperience = _registrationService.GetWorkExperienceById(id);
            workExperience.Organization = editOrg;
            workExperience.FromDate = editFromDate;
            workExperience.ToDate = editToDate;
            workExperience.Position = editPosition;
            workExperience.JobDescription = editJobDesc;
            _registrationService.SaveWorkExperience(workExperience);
            return RedirectToAction("AddWorkExperience");
        }

        //
        // GET: /WorkExperience/
        [HttpGet]
        public ActionResult AddWorkExperience()
        {
            var applicationId = Convert.ToInt64(Session["AppId"]);
            var application = _registrationService.GetApplicationDetails(applicationId);

            var templatesInAppForm = _appForm.GeTemplatesInApp(application.AppFormId).ToList();

            var isRightState = _stateManager.ConfirmFillStage(application, templatesInAppForm, "WE");
            if (!isRightState)
            {
                return RedirectToAction("WorkFlowManager", "Application");
            }
            var workExperience = _registrationService.GetWorkExperience(applicationId).ToList();
            Session["WorkExperience"] = workExperience;
            //var model = Mapper.Map<List<WorkExperience>, List<WorkExperienceModel>>(workExperience);

            var formTemplateList = Session["FormTemplates"] as List<TemplatesInAppForms> ?? new List<TemplatesInAppForms>();
            var workExpTemplate = formTemplateList[application.FillStage];
            var templateSettings = _registrationService.GetFormTemplateSettings(workExpTemplate.ApplicationFormId, workExpTemplate.FormTemplateId);
            ViewBag.maxEntry = templateSettings.MaxEntry;
            return View(workExperience);
        }
        [HttpPost]
        public ActionResult AddWorkExperience(string organization, DateTime startDate, DateTime? endDate, string position, string jobDescription, bool isCurrentJob = false)
        {
            var applicationId = Convert.ToInt64(Session["AppId"]);
            var application = _registrationService.GetApplicationDetails(applicationId);
            //check that entry has not reached maximum entry.
            var formTemplateList = Session["FormTemplates"] as List<TemplatesInAppForms> ?? new List<TemplatesInAppForms>();
            var workExpTemplate = formTemplateList[application.FillStage];
            var templateSettings = _registrationService.GetFormTemplateSettings(workExpTemplate.ApplicationFormId,
                workExpTemplate.FormTemplateId);
            var workExperiencesErrorModel = Session["WorkExperience"] as List<WorkExperience> ?? new List<WorkExperience>();
            var numOfwrkExpDetailsEntered = workExperiencesErrorModel.Count;

            if (templateSettings.MaxEntry == numOfwrkExpDetailsEntered)
            {
                ModelState.AddModelError("", "You have already entered the maximum number of Work Experience allowed for this form,Please click continue to proceed with application");
                //  var modelError = Mapper.Map<List<WorkExperience>, List<WorkExperienceModel>>(workExperiencesErrorModel);
                ViewBag.maxEntry = templateSettings.MaxEntry;
                return View(workExperiencesErrorModel);
            }



            var workExperiences = Session["WorkExperience"] as List<WorkExperience> ?? new List<WorkExperience>();
            var workExp = new WorkExperience()
            {
                Organization = organization,
                FromDate = startDate,
                ToDate = endDate,
                Position = position,
                JobDescription = jobDescription,
                IsCurrentJob = isCurrentJob,
                ApplicationId = applicationId
            };
            _registrationService.SaveWorkExperience(workExp);
            workExperiences.Add(workExp);
            Session["WorkExperience"] = workExperiences;
            //var model = Mapper.Map<List<WorkExperience>, List<WorkExperienceModel>>(workExperiences);
            ViewBag.maxEntry = templateSettings.MaxEntry;
            return View(workExperiences);
        }

        public ActionResult ContinueFromWorkExperience()
        {

            var applicationId = Convert.ToInt64(Session["AppId"]);
            var application = _registrationService.GetApplicationDetails(applicationId);
            //check that entry has not reached maximum entry.
            var formTemplateList = Session["FormTemplates"] as List<TemplatesInAppForms> ?? new List<TemplatesInAppForms>();
            var workExpTemplate = formTemplateList[application.FillStage];
            var templateSettings = _registrationService.GetFormTemplateSettings(workExpTemplate.ApplicationFormId,
                workExpTemplate.FormTemplateId);
            var workExperiencesErrorModel = Session["WorkExperience"] as List<WorkExperience> ?? new List<WorkExperience>();
            var numOfwrkExpDetailsEntered = workExperiencesErrorModel.Count;

            if (templateSettings.MinEntry > numOfwrkExpDetailsEntered)
            {
                ModelState.AddModelError("", "You have to enter at least " + templateSettings.MinEntry + " work experience(s) before you can proceed with application");
                //var modelError = Mapper.Map<List<WorkExperience>, List<WorkExperienceModel>>(workExperiencesErrorModel);
                //return View(modelError);
                ViewBag.maxEntry = templateSettings.MaxEntry;
                return View("AddWorkExperience", workExperiencesErrorModel);
            }


            //first save workExperiences
            //var workExperiences = Session["WorkExperience"] as List<WorkExperience>;
            //if (workExperiences != null && workExperiences.Any())
            //{
            //    foreach (var we in workExperiences)
            //    {
            //        _registrationService.SaveWorkExperience(we);
            //    }
            //}

            //Get the current Application Details
            //var applicationId = Convert.ToInt64(Session["AppId"]);
            //var application = _registrationService.GetApplicationDetails(applicationId);
            application.FillStage++;
            _registrationService.SaveApplication(application);

            //Get the current Application Form Details
            var appFormWorkFlow = Session["WorkFlowList"] as List<ApplicationFormWorkFlow>;
            var applicationForm = _appForm.GetAppForms(appFormWorkFlow.FirstOrDefault().ApplicationFormId);


            //save event in the Event Log
            var workflowId = appFormWorkFlow[application.WorkFlowStage].WorkFlowId;
            var formTemplateId = formTemplateList[application.FillStage - 1].FormTemplateId;
            var eventLog = new EventLog()
            {
                ApplicationFormId = applicationForm.Id,
                Username = User.Identity.Name,
                WorkFlowId = workflowId,
                Action = "added his/her work experience for form" + applicationForm.Name,
                Timestamp = DateTime.Now,
                FormTemplateId = formTemplateId,
                AppNum = application.AppNum
            };
            _eventLogRepo.SaveEvent(eventLog);

            // var formTemplateList = Session["FormTemplates"] as List<TemplatesInAppForms>;
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
                        return RedirectToAction("SaveProgram", "Fill");
                    case "REG":
                        return RedirectToAction("SaveRegNum", "Fill");
                    case "JR":
                        return RedirectToAction("AddJambScore", "Fill");
                }
            }
            else
            {
                var app = _registrationService.GetApplicationDetails(applicationId);
                app.WorkFlowStage++;
                _registrationService.SaveApplication(app);
                //this means we have completed all the templates in the application so we remove the fill workflow from the list of workflow
                //and redirect to the next workflow;
                // appFormWorkFlow.RemoveAt(0);
                if (application.WorkFlowStage < appFormWorkFlow.Count)
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
        public ActionResult Index()
        {
            return View();
        }
    }
}