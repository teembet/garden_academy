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
    public class EducationalDetailsController : Controller
    {
        private IRegistrationService _registrationService;
        private IApplicationFormRepository _appForm;
        private IEventLogRepository _eventLogRepo;
        private IStateManager _stateManager;
        public EducationalDetailsController(IRegistrationService registrationService, IApplicationFormRepository appForm, IEventLogRepository eventLogRepo, IStateManager stateManager)
        {
            this._registrationService = registrationService;
            this._appForm = appForm;
            this._eventLogRepo = eventLogRepo;
            this._stateManager = stateManager;
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

        public ActionResult Update(long id, string editSchName, string editQualification, string editClassOfDegree, int editEntryYear, int editGradYear)
        {
            var educationalDetail = _registrationService.GetEducationalDetail(id);
            educationalDetail.SchoolName = editSchName;
            educationalDetail.Qualification = editQualification;
            educationalDetail.ClassOfDegree = editClassOfDegree;
            educationalDetail.EntryYear = editEntryYear;
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

            var isRightState = _stateManager.ConfirmFillStage(application, templatesInAppForm, "ED");
            if (!isRightState)
            {
                return RedirectToAction("WorkFlowManager", "Application");
            }


            var educationalDetails = _registrationService.GetEducationalDetails(applicationId).ToList();
            Session["EducationalDetails"] = educationalDetails;
           // var model = Mapper.Map<List<EducationalDetails>, List<EducationalDetailsModel>>(educationalDetails);

            var formTemplateList = Session["FormTemplates"] as List<TemplatesInAppForms> ?? new List<TemplatesInAppForms>();
            var educationalDetailsTemplate = formTemplateList[application.FillStage];
            var templateSettings = _registrationService.GetFormTemplateSettings(educationalDetailsTemplate.ApplicationFormId, educationalDetailsTemplate.FormTemplateId);
            ViewBag.maxEntry = templateSettings.MaxEntry;
            return View(educationalDetails);
        }
        [HttpPost]
        public ActionResult AddEducationalDetail(string schoolName, string qualification, string classOfDegree, string otherDegree, int entryYear, int graduationYear)
        {
            var applicationId = Convert.ToInt64(Session["AppId"]);
            var application = _registrationService.GetApplicationDetails(applicationId);

            //check that entry has not reached maximum entry.
            var formTemplateList = Session["FormTemplates"] as List<TemplatesInAppForms> ?? new List<TemplatesInAppForms>();
            var educationalDetailsTemplate = formTemplateList[application.FillStage];
            var templateSettings = _registrationService.GetFormTemplateSettings(educationalDetailsTemplate.ApplicationFormId,
                educationalDetailsTemplate.FormTemplateId);
            var eduDetailsErrorModel = Session["EducationalDetails"] as List<EducationalDetails> ?? new List<EducationalDetails>();
            var numOfEduDetailsEntered = eduDetailsErrorModel.Count;

            if (templateSettings.MaxEntry == numOfEduDetailsEntered)
            {
                ModelState.AddModelError("", "You have already entered the maximum number of Educational Details allowed for this form,Please click continue to proceed with application");
                //var modelError = Mapper.Map<List<EducationalDetails>, List<EducationalDetailsModel>>(eduDetailsErrorModel);
                ViewBag.maxEntry = templateSettings.MaxEntry;
                return View(eduDetailsErrorModel);
            }
            
            var educationalDetails = Session["EducationalDetails"] as List<EducationalDetails> ?? new List<EducationalDetails>();
            var eduDet = new EducationalDetails()
            {
                SchoolName = schoolName,
                Qualification = qualification,
                ClassOfDegree = classOfDegree != "Others" ? classOfDegree : otherDegree,
                EntryYear = entryYear,
                GraduationYear = graduationYear,
                ApplicationId = applicationId
            };
            _registrationService.SaveEducationalDetails(eduDet);
            educationalDetails.Add(eduDet);
            Session["EducationalDetails"] = educationalDetails;
            //var model = Mapper.Map<List<EducationalDetails>, List<EducationalDetailsModel>>(educationalDetails);
            ViewBag.maxEntry = templateSettings.MaxEntry;
            return View(educationalDetails);

        }

        public ActionResult ContinueFromEducationalDetails()
        {
            //check that entry is not less than minimum entry.
            var applicationId = Convert.ToInt64(Session["AppId"]);
            var application = _registrationService.GetApplicationDetails(applicationId);
            var formTemplateList = Session["FormTemplates"] as List<TemplatesInAppForms> ?? new List<TemplatesInAppForms>();

            var educationalDetailsTemplate = formTemplateList[application.FillStage];
            var templateSettings = _registrationService.GetFormTemplateSettings(educationalDetailsTemplate.ApplicationFormId,
                educationalDetailsTemplate.FormTemplateId);
            var eduDetailsErrorModel = Session["EducationalDetails"] as List<EducationalDetails> ?? new List<EducationalDetails>();
            var numOfEduDetailsEntered = eduDetailsErrorModel.Count;

            if (templateSettings.MinEntry > numOfEduDetailsEntered)
            {
                ModelState.AddModelError("", "You have to enter at least "+templateSettings.MinEntry+" educational detail");
              //  var modelError = Mapper.Map<List<EducationalDetails>, List<EducationalDetailsModel>>(eduDetailsErrorModel);
                ViewBag.maxEntry = templateSettings.MaxEntry;
                return View("AddEducationalDetail", eduDetailsErrorModel);
            }


            //var educationalDetails = Session["EducationalDetails"] as List<EducationalDetails>;
            //if (educationalDetails != null && educationalDetails.Any())
            //{
            //    foreach (var ed in educationalDetails)
            //    {
            //        _registrationService.SaveEducationalDetails(ed);
            //    }
            //}

            //increase the fill stage

            application.FillStage++;
            _registrationService.SaveApplication(application);

            //Get the current Application Form Details
            var appFormWorkFlow = Session["WorkFlowList"] as List<ApplicationFormWorkFlow>;
            var applicationForm = _appForm.GetAppForms(appFormWorkFlow.FirstOrDefault().ApplicationFormId);


            //save event in the Event Log
            var workflowId = appFormWorkFlow.FirstOrDefault().WorkFlowId;
            var formTemplateId = formTemplateList[application.FillStage - 1].FormTemplateId;
            var eventLog = new EventLog()
            {
                ApplicationFormId = applicationForm.Id,
                Username = User.Identity.Name,
                WorkFlowId = workflowId,
                Action = "added his/her Educational Details for" + applicationForm.Name,
                Timestamp = DateTime.Now,
                FormTemplateId = formTemplateId,
                AppNum = application.AppNum
            };
            _eventLogRepo.SaveEvent(eventLog);

         //   var formTemplateList = Session["FormTemplates"] as List<TemplatesInAppForms>;
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
                        return RedirectToAction("AddJambScore","Fill");
                }
            }
            else
            {
                var app = _registrationService.GetApplicationDetails(applicationId);
                app.WorkFlowStage++;
                _registrationService.SaveApplication(app);
                //this means we have completed all the templates in the application so we remove the fill workflow from the list of workflow
                //and redirect to the next workflow;
                //  appFormWorkFlow.RemoveAt(0);
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
    }
}