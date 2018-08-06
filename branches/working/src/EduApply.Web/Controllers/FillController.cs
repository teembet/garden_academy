using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using EduApply.Data.Entities;
using EduApply.Logic.Interfaces;
using EduApply.Web.Models;
using iTextSharp.text;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace EduApply.Web.Controllers
{
    [Authorize(Roles = "Student")]

    [Authorize]
    public class FillController : Controller
    {
        public const string Success = "Success";
        private IApplicationFormRepository _appForm;
        private IRegistrationService _registrationService;
        private IEventLogRepository _eventLogRepo;
        private ILocationRepository _locationRepository;
        private IConfigurationService _configurationService;
        private IStateManager _stateManager;
        public FillController(IApplicationFormRepository appForm, IRegistrationService registrationService, IEventLogRepository eventLogRepo, ILocationRepository locationRepository, IConfigurationService configurationService, IStateManager stateManager)
        {
            this._appForm = appForm;
            this._registrationService = registrationService;
            this._eventLogRepo = eventLogRepo;
            this._locationRepository = locationRepository;
            this._configurationService = configurationService;
            this._stateManager = stateManager;
        }
        // GET: FillStage
        public ActionResult UploadCertificate(HttpPostedFileBase certificate)
        {
            var file = Request.Files;
            return Content("Got here");
        }
        public ActionResult BackApplication()
        {
            var applicationId = Convert.ToInt64(Session["AppId"]);
            var application = _registrationService.GetApplicationDetails(applicationId);

            if (application.FillStage > 0)
            {
                application.FillStage--;
                _registrationService.SaveApplication(application);

                var formTemplateList = Session["FormTemplates"] as List<TemplatesInAppForms>;
                var prevFormTemplate = formTemplateList[application.FillStage];
                var formTemplate = _appForm.GetFormTemplate(prevFormTemplate.FormTemplateId);
                switch (formTemplate.Code)
                {
                    case "BD":
                        return RedirectToAction("SaveBiodata");
                    case "OLR":
                        return RedirectToAction("SaveOLevelResult");
                    case "ED":
                        return RedirectToAction("AddEducationalDetail", "EducationalDetails");
                    case "WE":
                        return RedirectToAction("AddWorkExperience", "WorkExperience");
                    case "REF":
                        return RedirectToAction("AddReference", "Reference");
                    case "PU":
                        return RedirectToAction("SavePassport");
                    case "CU":
                        return RedirectToAction("SaveCertificate");
                    case "PC":
                        if (application.FillStage > 0)
                        {
                            var applicationForm = _appForm.GetAppForms(application.AppFormId);
                            if (applicationForm.UseProgramCourseFromJamb)
                            {
                                return RedirectToAction("BackApplication");
                            }
                        }

                        return RedirectToAction("AddProgramCourse", "ApplicantsProgramCourse");
                    case "REG":
                        return RedirectToAction("SaveRegNum");
                    case "JR":
                        return RedirectToAction("AddJambScore");
                }

            }
            else
            {
                //this returns it to the previous workflow stage and if fill was the first it takes it back to zero
                //because once fill is a stage it increases even before it goes to any fill stage unlike other workk flow stages that increments only
                //when the workflow has finished and is abt to move to the next
                //application.WorkFlowStage--;
                //_registrationService.SaveApplication(application);
                //if we get here it means we reached the first template under fill and back was again pressed,
                //Therefore we go back one workflow
                return RedirectToAction("BackApplication", "Application");
            }

            ViewBag.message = "Ooops!, Something went wrong";
            return View("ApplicationError");
        }
        public ActionResult Fill()
        {
            var appFormWorkFlow = Session["WorkFlowList"] as List<ApplicationFormWorkFlow>;
            //the appFormWorkFlow above is a list of workflow in the order application is to follow, each item
            //in the list has a column for theapplicationFormId, in the line below we just use the first items appformId
            //to get the application form
            var applicationForm = _appForm.GetAppForms(appFormWorkFlow.FirstOrDefault().ApplicationFormId);

            //Get all form Templates
            var allFormTemplates = _appForm.GetFormTemplates();

            //Get details of current application to be used to determine what stage we are
            var applicationId = Convert.ToInt64(Session["AppId"]);
            var application = _registrationService.GetApplicationDetails(applicationId);


            //the following code snippet validates that user is in the right stage
            var isRightState = _stateManager.ConfirmWorkFlowStage(application, appFormWorkFlow, "Fill");
            if (!isRightState || application.UserName != User.Identity.GetUserName())
            {
                application.FillStage = 0;
                application.WorkFlowStage = 0;
                _registrationService.SaveApplication(application);
                ViewBag.message = "Please follow the right process, Do not copy and paste URL's";
                return View("ApplicationError");
                //  return RedirectToAction("WorkFlowManager", "Application");
            }



            //Get the Templates for the current Application Form
            var tempInApp = _appForm.GeTemplatesInApp(applicationForm.Id);
            var formTemplateIdz = tempInApp.Select(x => x.FormTemplateId).ToArray();
            List<TemplatesInAppForms> formTemplateList = tempInApp.ToList();
            //save TemplateForThisAppForm to Session
            Session["FormTemplates"] = formTemplateList;

            var firstTemplate = _appForm.GetFormTemplate(formTemplateIdz[application.FillStage]);
            switch (firstTemplate.Code)
            {
                case "BD":
                    return RedirectToAction("SaveBiodata");
                case "OLR":
                    return RedirectToAction("SaveOLevelResult");
                case "ED":
                    return RedirectToAction("AddEducationalDetail", "EducationalDetails");
                case "WE":
                    return RedirectToAction("AddWorkExperience", "WorkExperience");
                case "REF":
                    return RedirectToAction("AddReference", "Reference");
                case "PU":
                    return RedirectToAction("SavePassport");
                case "CU":
                    return RedirectToAction("SaveCertificate");
                case "PC":
                    return RedirectToAction("AddProgramCourse", "ApplicantsProgramCourse");
                case "REG":
                    return RedirectToAction("SaveRegNum");
                case "JR":
                    return RedirectToAction("AddJambScore");
            }
            //if we get here then something went wrong
            ViewBag.message = "Something went wrong";
            return View("ApplicationError");
        }
        [HttpGet]
        public ActionResult SaveBiodata()
        {
            var applicationId = Convert.ToInt64(Session["AppId"]);
            var application = _registrationService.GetApplicationDetails(applicationId);

            var templatesInAppForm = _appForm.GeTemplatesInApp(application.AppFormId).ToList();

            var isRightState = _stateManager.ConfirmFillStage(application, templatesInAppForm, "BD", Session["WorkFlowList"] as List<ApplicationFormWorkFlow>, "Fill");
            if (!isRightState || application.UserName != User.Identity.GetUserName())
            {
                application.FillStage = 0;
                application.WorkFlowStage = 0;
                _registrationService.SaveApplication(application);
                ViewBag.message = "Please follow the right process, Do not copy and paste URL's";
                return View("ApplicationError");
                //return RedirectToAction("WorkFlowManager", "Application");
            }
            var personalInformation = _registrationService.GetPersonalInformation(User.Identity.GetUserId()) ?? new PersonalInformation();
            personalInformation.Email = User.Identity.GetUserName();
            personalInformation.Countries = _locationRepository.GetCountries();
            personalInformation.ResidentStates = _locationRepository.GetStates();
            personalInformation.States = personalInformation.StateOfOrigin > 0 ? _locationRepository.GetStates() : new List<State>() { new State() { Code = "N/A", Id = -1, Name = "N/A" } };
            var lgaz = _locationRepository.GetLgas(personalInformation.StateOfOrigin).ToList();
            personalInformation.Lgaz = lgaz.Any() ? lgaz : new List<LocalGovernmentArea>() { new LocalGovernmentArea() { Code = "N/A", Id = -1, Name = "N/A" } };
            if (!string.IsNullOrEmpty(application.RegNum))
            {
                var applicantsJambResult = _configurationService.GetJambBreakDown(application.RegNum);
                if (applicantsJambResult != null)
                {
                    TempData["JambResultPresent"] = true;
                    personalInformation.LastName = applicantsJambResult.LastName;
                    personalInformation.FirstName = applicantsJambResult.FirstName;
                    personalInformation.MiddleName = applicantsJambResult.MiddleName;
                }

            }

            var appForm = _appForm.GetAppForms(application.AppFormId);
            if (appForm.UseDetailsFromJambBiodata)
            {
                var applicantsBiodata = _configurationService.GetJambBiodata(application.RegNum);
                if (applicantsBiodata != null)
                {
                    TempData["JambResultPresent"] = true;
                    personalInformation.LastName = applicantsBiodata.LastName;
                    personalInformation.FirstName = applicantsBiodata.FirstName;
                    personalInformation.MiddleName = applicantsBiodata.MiddleName;
                }
            }

            var model = Mapper.Map<PersonalInformation, PersonalInformationModel>(personalInformation);
            return View(model);
        }
        [HttpPost]
        public ActionResult SaveBiodata(PersonalInformation personalInformation)
        {

            var formTemplateList = Session["FormTemplates"] as List<TemplatesInAppForms>;
            var applicationId = Convert.ToInt64(Session["AppId"]);
            var application = _registrationService.GetApplicationDetails(applicationId);
            var biodataTemplt = _configurationService.GetFormTemplateByCode("BD");
            if (biodataTemplt == null)
            {
                ViewBag.message = "An error occured, Please try again";
                return View("ApplicationError");
            }
            var currentFS = formTemplateList.FindIndex(x => x.FormTemplateId == biodataTemplt.Id);
            application.FillStage = currentFS + 1;


            var applicantsJambResult = _configurationService.GetJambBreakDown(application.RegNum);
            //This will save both personalInfo and changes made to application
            personalInformation.RegNum = application.RegNum;
            if (applicantsJambResult != null)
            {
                personalInformation.LastName = applicantsJambResult.LastName;
                personalInformation.FirstName = applicantsJambResult.FirstName;
                personalInformation.MiddleName = applicantsJambResult.MiddleName;
            }
            personalInformation.Id = User.Identity.GetUserId();

            var appForm = _appForm.GetAppForms(application.AppFormId);
            if (appForm.UseDetailsFromJambBiodata)
            {
                var applicantsBiodata = _configurationService.GetJambBiodata(application.RegNum);
                if (applicantsBiodata != null)
                {
                    TempData["JambResultPresent"] = true;
                    personalInformation.LastName = applicantsBiodata.LastName;
                    personalInformation.FirstName = applicantsBiodata.FirstName;
                    personalInformation.MiddleName = applicantsBiodata.MiddleName;
                    personalInformation.Gender = applicantsBiodata.Gender == "M" ? "Male" : "Female";

                    var state = _locationRepository.GetState(applicantsBiodata.StateOfOrigin);
                    if (state != null)
                    {
                        personalInformation.StateOfOrigin = state.Id;
                    }

                    var lga = _locationRepository.GetLocalGovernmentArea(applicantsBiodata.LGA);
                    if (lga != null)
                    {
                        personalInformation.LocalGovernment = lga.Id;
                    }
                }
            }
            _registrationService.UpdatePersonalInformation(personalInformation);

            //saving this back to session is really not necessary, i am just being paranoid
            Session["AppId"] = applicationId;

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
                Action = "filled his/her Biodata for form: " + applicationForm.Name,
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
                        return RedirectToAction("SaveBiodata");
                    case "OLR":
                        return RedirectToAction("SaveOLevelResult");
                    case "ED":
                        return RedirectToAction("AddEducationalDetail", "EducationalDetails");
                    case "WE":
                        return RedirectToAction("AddWorkExperience", "WorkExperience");
                    case "REF":
                        return RedirectToAction("AddReference", "Reference");
                    case "PU":
                        return RedirectToAction("SavePassport");
                    case "CU":
                        return RedirectToAction("SaveCertificate");
                    case "PC":
                        return RedirectToAction("AddProgramCourse", "ApplicantsProgramCourse");
                    case "REG":
                        return RedirectToAction("SaveRegNum");
                    case "JR":
                        return RedirectToAction("AddJambScore");
                }
            }
            else
            {

                var app = _registrationService.GetApplicationDetails(applicationId);
                var currentWFS = appFormWorkFlow.FindIndex(x => x.WorkFlowId == fiilWf.Id);
                app.WorkFlowStage = currentWFS + 1;
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
        [HttpGet]
        public ActionResult SaveRegNum()
        {
            var regNumValidationModel = new RegNumValidationModel();
            var applicationId = Convert.ToInt64(Session["AppId"]);
            var application = _registrationService.GetApplicationDetails(applicationId);

            var templatesInAppForm = _appForm.GeTemplatesInApp(application.AppFormId).ToList();

            var isRightState = _stateManager.ConfirmFillStage(application, templatesInAppForm, "REG", Session["WorkFlowList"] as List<ApplicationFormWorkFlow>, "Fill");
            if (!isRightState || application.UserName != User.Identity.GetUserName())
            {
                application.FillStage = 0;
                application.WorkFlowStage = 0;
                _registrationService.SaveApplication(application);
                ViewBag.message = "Please follow the right process, Do not copy and paste URL's";
                return View("ApplicationError");
                //return RedirectToAction("WorkFlowManager", "Application");
            }


            if (!string.IsNullOrEmpty(application.RegNum))
            {
                regNumValidationModel.RegNum = application.RegNum;
            }
            return View(regNumValidationModel);
        }
        [HttpPost]
        public ActionResult SaveRegNum(RegNumValidationModel regNumModel)
        {
            var formTemplateList = Session["FormTemplates"] as List<TemplatesInAppForms>;
            var loggedOnUser = User.Identity.Name;
            var applicationId = Convert.ToInt64(Session["AppId"]);
            var application = _registrationService.GetApplicationDetails(applicationId);
            var anyApplicationWithRegNumEntered = _configurationService.GetApplicationsByRegNum(regNumModel.RegNum).FirstOrDefault();
            if (anyApplicationWithRegNumEntered != null && anyApplicationWithRegNumEntered.UserName != loggedOnUser)
            {
                ModelState.AddModelError("", "The registration number entered does not belong to you");
                return View(regNumModel);
            }
            application.RegNum = regNumModel.RegNum;
            var RegNumTemplt = _configurationService.GetFormTemplateByCode("REG");
            if (RegNumTemplt == null)
            {
                ViewBag.message = "Please follow the right process, Do not copy and paste URL's";
                return View("ApplicationError");
            }
            var currentFS = formTemplateList.FindIndex(x => x.FormTemplateId == RegNumTemplt.Id);
            application.FillStage = currentFS + 1;
            _registrationService.SaveApplication(application);

            Session["AppId"] = applicationId;

            var appFormWorkFlow = Session["WorkFlowList"] as List<ApplicationFormWorkFlow>;
            var applicationForm = _appForm.GetAppForms(appFormWorkFlow.FirstOrDefault().ApplicationFormId);


            //save event in the Event Log
            var fiilWf = _configurationService.GetWorkFlowbyActionName("Fill");
            if (fiilWf == null)
            {
                ViewBag.message = "An error occured, please try again";
                return View("ApplicationError");
            }
            var workflowId = fiilWf.Id;

            var formTemplateId = formTemplateList[application.FillStage - 1].FormTemplateId;
            var localTime = _configurationService.GetCurrentWestAfricanDateTime();
            var eventLog = new EventLog()
            {
                ApplicationFormId = applicationForm.Id,
                Username = User.Identity.Name,
                WorkFlowId = workflowId,
                Action = "added his/her registration number to form: " + applicationForm.Name,
                Timestamp = localTime,
                FormTemplateId = formTemplateId,
                AppNum = application.AppNum
            };
            _eventLogRepo.SaveEvent(eventLog);


            // formTemplateList.RemoveAt(0);
            if (application.FillStage < formTemplateList.Count)
            {
                var nextTemplateInApp = formTemplateList[application.FillStage];
                var formTemplate = _appForm.GetFormTemplate(nextTemplateInApp.FormTemplateId);
                switch (formTemplate.Code)
                {
                    case "BD":
                        return RedirectToAction("SaveBiodata");
                    case "OLR":
                        return RedirectToAction("SaveOLevelResult");
                    case "ED":
                        return RedirectToAction("AddEducationalDetail", "EducationalDetails");
                    case "WE":
                        return RedirectToAction("AddWorkExperience", "WorkExperience");
                    case "REF":
                        return RedirectToAction("AddReference", "Reference");
                    case "PU":
                        return RedirectToAction("SavePassport");
                    case "CU":
                        return RedirectToAction("SaveCertificate");
                    case "PC":
                        return RedirectToAction("AddProgramCourse", "ApplicantsProgramCourse");
                    case "REG":
                        return RedirectToAction("SaveRegNum");
                    case "JR":
                        return RedirectToAction("AddJambScore");
                }
            }
            else
            {
                var app = _registrationService.GetApplicationDetails(applicationId);
               var currentWFStage = appFormWorkFlow.FindIndex(x => x.WorkFlowId == fiilWf.Id);
                app.WorkFlowStage = currentWFStage + 1;
                _registrationService.SaveApplication(app);
                //this means we have completed all the templates in the application so we remove the fill workflow from the list of workflow
                //and redirect to the next workflow;
                //appFormWorkFlow.RemoveAt(0);
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
        [HttpGet]
        public ActionResult SavePassport()
        {
            var applicationId = Convert.ToInt64(Session["AppId"]);
            var application = _registrationService.GetApplicationDetails(applicationId);

            var templatesInAppForm = _appForm.GeTemplatesInApp(application.AppFormId).ToList();

            var isRightState = _stateManager.ConfirmFillStage(application, templatesInAppForm, "PU", Session["WorkFlowList"] as List<ApplicationFormWorkFlow>, "Fill");
            if (!isRightState || application.UserName != User.Identity.GetUserName())
            {
                application.FillStage = 0;
                application.WorkFlowStage = 0;
                _registrationService.SaveApplication(application);
                ViewBag.message = "Please follow the right process, Do not copy and paste URL's";
                return View("ApplicationError");
                //return RedirectToAction("WorkFlowManager", "Application");
            }


            var studentPicture = _registrationService.GetPictureDetails(applicationId);
            var model = new PictureModel()
            {
                ApplicationId = applicationId,
                Name = studentPicture != null ? studentPicture.Name : null
            };
            return View(model);
        }
        [HttpPost]
        public ActionResult SavePassport(PictureModel model, HttpPostedFileBase picture)
        {
            if (picture.ContentLength > 0)
            {
                //check the type of file being uploaeded
                //check that format is in jpg
                string fileExtension = Path.GetExtension(picture.FileName);
                if (fileExtension != ".jpg")
                {
                    ModelState.AddModelError("", "Incorrect File format, Only jpg files are acceptable");

                    return View(model);
                }
                if (picture.ContentLength > 50 * 1024)
                {
                    ModelState.AddModelError("", "The picture you are uploading is larger than 50KB");

                    return View(model);
                }


                var pictureName = User.Identity.Name + "_" + picture.FileName;
                string filePath = System.Web.HttpContext.Current.Server.MapPath("~/images/StudentPassport/");
                picture.SaveAs(filePath + pictureName);

                try
                {
                    var reSizeImage = new Size(150, 150);
                    var srcImage = System.Drawing.Image.FromFile(filePath + pictureName);
                    var tnImage = srcImage.GetThumbnailImage(reSizeImage.Width, reSizeImage.Height, null, IntPtr.Zero);

                    var graphic = Graphics.FromImage(tnImage);
                    graphic.CompositingQuality = CompositingQuality.HighQuality;
                    graphic.SmoothingMode = SmoothingMode.HighQuality;
                    graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;

                    var rect = new System.Drawing.Rectangle(0, 0, reSizeImage.Width, reSizeImage.Height);
                    graphic.DrawImage(tnImage, rect);
                    string thumbnailPath = System.Web.HttpContext.Current.Server.MapPath("~/images/StudentPassport/Thumbnails/");
                    tnImage.Save(thumbnailPath + pictureName, ImageFormat.Jpeg);
                    srcImage.Dispose();
                    tnImage.Dispose();
                }
                catch(OutOfMemoryException ex)
                {
                    ModelState.AddModelError("", "The picture you are uploading is damaged");
                    return View(model);
                }
                catch(Exception Ex)
                {
                    ModelState.AddModelError("", "The picture you are uploading is damaged");
                    return View(model);
                }

                //var reSizeImage = new Size(150, 150);
                //var srcImage = System.Drawing.Image.FromFile(filePath + pictureName);
                //var tnImage = srcImage.GetThumbnailImage(reSizeImage.Width, reSizeImage.Height, null, IntPtr.Zero);

                //var graphic = Graphics.FromImage(tnImage);
                //graphic.CompositingQuality = CompositingQuality.HighQuality;
                //graphic.SmoothingMode = SmoothingMode.HighQuality;
                //graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;

                //var rect = new System.Drawing.Rectangle(0, 0, reSizeImage.Width, reSizeImage.Height);
                //graphic.DrawImage(tnImage, rect);
                //string thumbnailPath = System.Web.HttpContext.Current.Server.MapPath("~/images/StudentPassport/Thumbnails/");
                //tnImage.Save(thumbnailPath + pictureName, ImageFormat.Jpeg);
                //srcImage.Dispose();
                //tnImage.Dispose();


                var studentPicture = _registrationService.GetPictureDetails(model.ApplicationId) ?? new Picture();
                studentPicture.PictureUrl = filePath;
                studentPicture.Name = pictureName;
                studentPicture.ApplicationId = model.ApplicationId;
                _registrationService.SavePicture(studentPicture);

                var pictureModel = Mapper.Map<Picture, PictureModel>(studentPicture);

                return View(pictureModel);
            }

            ViewBag.message = "Something went wrong";
            return View("ApplicationError");
        }

        public ActionResult ContinueFromPassport()
        {
            var applicationId = Convert.ToInt64(Session["AppId"]);
            var studentPicture = _registrationService.GetPictureDetails(applicationId);
            var formTemplateList = Session["FormTemplates"] as List<TemplatesInAppForms>;
            if (studentPicture == null)
            {
                ModelState.AddModelError("", "You cannot continue application except you upload a picture");
                var model = new PictureModel()
                {
                    ApplicationId = applicationId,
                };
                return View("SavePassport", model);
            }


            var application = _registrationService.GetApplicationDetails(applicationId);
            var picTemplt = _configurationService.GetFormTemplateByCode("PU");
            if (picTemplt == null)
            {
                ViewBag.message = "An error occured, Please try again";
                return View("ApplicationError");
            }
            var currentFS = formTemplateList.FindIndex(x => x.FormTemplateId == picTemplt.Id);
            application.FillStage = currentFS + 1;
            _registrationService.SaveApplication(application);


            Session["AppId"] = applicationId;
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
                Action = "uploaded his/her Passport for Form" + applicationForm.Name,
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
                        return RedirectToAction("SaveBiodata");
                    case "OLR":
                        return RedirectToAction("SaveOLevelResult");
                    case "ED":
                        return RedirectToAction("AddEducationalDetail", "EducationalDetails");
                    case "WE":
                        return RedirectToAction("AddWorkExperience", "WorkExperience");
                    case "REF":
                        return RedirectToAction("AddReference", "Reference");
                    case "PU":
                        return RedirectToAction("SavePassport");
                    case "CU":
                        return RedirectToAction("SaveCertificate");
                    case "PC":
                        return RedirectToAction("AddProgramCourse", "ApplicantsProgramCourse");
                    case "REG":
                        return RedirectToAction("SaveRegNum");
                    case "JR":
                        return RedirectToAction("AddJambScore");
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
                // appFormWorkFlow.RemoveAt(0);
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
        //[HttpGet]
        //public ActionResult SaveProgram()
        //{
        //    var applicationId = Convert.ToInt64(Session["AppId"]);
        //    var application = _registrationService.GetApplicationDetails(applicationId);

        //    var templatesInAppForm = _appForm.GeTemplatesInApp(application.AppFormId).ToList();

        //    var isRightState = _stateManager.ConfirmFillStage(application, templatesInAppForm, "PC");
        //    if (!isRightState)
        //    {
        //        return RedirectToAction("WorkFlowManager", "Application");
        //    }


        //    var formTemplateList = Session["FormTemplates"] as List<TemplatesInAppForms> ?? new List<TemplatesInAppForms>();
        //    var programCourseTemplate = formTemplateList[application.FillStage];
        //    var templateSettings = _registrationService.GetFormTemplateSettings(programCourseTemplate.ApplicationFormId, programCourseTemplate.FormTemplateId);
        //    var maxEntry = templateSettings.MaxEntry;
        //    var applicantsProgramCourseList = _registrationService.GetApplicantsProgramCourses(applicationId).ToList();




        //    //Get all the programCourse for this form configured by Admin
        //    var appFormProgramCourse = _configurationService.GetAppFormProgramCourses(application.AppFormId).ToList();
        //    //Select the programCourseId
        //    var programCourseIdz = appFormProgramCourse.Select(x => x.ProgramCourseId).ToArray();
        //    //Get the programCourse entities themselves
        //    var programCourses = _configurationService.GetProgramCourses().Where(x => programCourseIdz.Contains(x.Id)).ToList();
        //    //Save the programCourses to a session
        //    Session["ApplicantsProgramCourse"] = programCourses;
        //    //Select the propgramIds
        //    var programIdsForThisForm = programCourses.Select(x => x.ProgramId).ToArray();
        //    //Select the List of Programs that have their Id in programIdsForThisForm
        //    var programsForThisAppForm = _configurationService.GetPrograms().Where(x => x.IsActive && programIdsForThisForm.Contains(x.Id)).ToList();

        //    var viewModel = new ApplicantProgramCourseCollection()
        //    {
        //        MaxEntry = maxEntry,
        //        ApplicantsProgramCourses = applicantsProgramCourseList
        //    };
        //    foreach (var item in applicantsProgramCourseList)
        //    {
        //        var programId = item.ProgramId;
        //        item.Programs = programsForThisAppForm;
        //        var programCoursesForCurrentForm = programCourses.Where(x => x.ProgramId == programId);
        //        var courseIdz = programCoursesForCurrentForm.Select(x => x.CourseId).ToArray();
        //        item.Courses = _configurationService.GetCourses().Where(x => courseIdz.Contains(x.Id) && x.IsActive).ToList();
        //    }

        //    if (maxEntry > applicantsProgramCourseList.Count)
        //    {
        //        for (int i = 0; i < maxEntry - applicantsProgramCourseList.Count; i++)
        //        {
        //            var applicantsProgramCoursee = new ApplicantsProgramCourse()
        //            {
        //                ApplicationId = applicationId,
        //                Programs = programsForThisAppForm,
        //                ProgramId = 0,
        //                CourseId = 0
        //            };

        //        }
        //    }
        //    return View(viewModel);
        //}
        //[HttpPost]
        //public ActionResult SaveProgram(ApplicantsProgramCourse appProgCourse)
        //{
        //    var applicationId = Convert.ToInt64(Session["AppId"]);
        //    var application = _registrationService.GetApplicationDetails(applicationId);
        //    application.ProgramId = appProgCourse.ProgramId;
        //    application.CourseOfStudyId = appProgCourse.CourseId;
        //    var course = _configurationService.GetCourse(appProgCourse.CourseId);
        //    application.DepartmentId = course.DepartmentId;
        //    application.FacultyId = course.Department.FacultyId;
        //    application.FillStage++;
        //    _registrationService.SaveApplication(application);

        //    //saving this back to session is really not necessary, i am just being paranoid
        //    Session["AppId"] = applicationId;

        //    var appFormWorkFlow = Session["WorkFlowList"] as List<ApplicationFormWorkFlow>;
        //    var applicationForm = _appForm.GetAppForms(appFormWorkFlow.FirstOrDefault().ApplicationFormId);


        //    //save event in the Event Log
        //    var workflowId = appFormWorkFlow[application.WorkFlowStage].WorkFlowId;
        //    var formTemplateList = Session["FormTemplates"] as List<TemplatesInAppForms>;
        //    var formTemplateId = formTemplateList[application.FillStage - 1].FormTemplateId;
        //    var eventLog = new EventLog()
        //    {
        //        ApplicationFormId = applicationForm.Id,
        //        Username = User.Identity.Name,
        //        WorkFlowId = workflowId,
        //        Action = "Added his/her Progam and Course of Study for application " + applicationForm.Name,
        //        Timestamp = DateTime.Now,
        //        FormTemplateId = formTemplateId,
        //        AppNum = application.AppNum
        //    };
        //    _eventLogRepo.SaveEvent(eventLog);

        //    // formTemplateList.RemoveAt(0);
        //    if (application.FillStage < formTemplateList.Count)
        //    {
        //        var nextTemplateInApp = formTemplateList[application.FillStage];
        //        var formTemplate = _appForm.GetFormTemplate(nextTemplateInApp.FormTemplateId);
        //        switch (formTemplate.Code)
        //        {
        //            case "BD":
        //                return RedirectToAction("SaveBiodata");
        //            case "OLR":
        //                return RedirectToAction("SaveOLevelResult");
        //            case "ED":
        //                return RedirectToAction("AddEducationalDetail", "EducationalDetails");
        //            case "WE":
        //                return RedirectToAction("AddWorkExperience", "WorkExperience");
        //            case "REF":
        //                return RedirectToAction("AddReference", "Reference");
        //            case "PU":
        //                return RedirectToAction("SavePassport");
        //            case "CU":
        //                return RedirectToAction("SaveCertificate");
        //            case "PC":
        //                return RedirectToAction("SaveProgram");
        //            case "REG":
        //                return RedirectToAction("SaveRegNum");
        //            case "JR":
        //                return RedirectToAction("AddJambScore");
        //        }
        //    }
        //    else
        //    {
        //        var app = _registrationService.GetApplicationDetails(applicationId);
        //        app.WorkFlowStage++;
        //        _registrationService.SaveApplication(app);
        //        //this means we have completed all the templates in the application so we remove the fill workflow from the list of workflow
        //        //and redirect to the next workflow;
        //        // appFormWorkFlow.RemoveAt(0);
        //        //then select nextworkflow and redirect appropriately
        //        if (application.WorkFlowStage < appFormWorkFlow.Count)
        //        {
        //            var nextWorkFlow = appFormWorkFlow[application.WorkFlowStage];
        //            var workFlowItem = _appForm.GetWorkFlowItem(nextWorkFlow.WorkFlowId);
        //            Session["WorkFlowList"] = appFormWorkFlow;
        //            switch (workFlowItem.Name)
        //            {
        //                case "Pay":
        //                    return RedirectToAction("ValidatePayment", "Payment");
        //                case "Fill":
        //                    return RedirectToAction("Fill", "Fill");
        //                case "Validate Jamb Score":
        //                    return RedirectToAction("ValidateJamb", "Validation");
        //                case "Validate Application Result":
        //                    return RedirectToAction("ValidateApplicationResult", "Validation");
        //                case "Validate Non-Application Result":
        //                    return RedirectToAction("ValidateNonApplicationResult", "Validation");
        //                case "Validate Admission Status":
        //                    return RedirectToAction("CheckAdmissionStatus", "Validation");
        //                case "Venue Assignment":
        //                    return RedirectToAction("VenueAssignment", "Validation");
        //            }
        //        }
        //        else
        //        {
        //            return RedirectToAction("ApplicationPreview", "Application");

        //        }
        //    }
        //    ViewBag.message = "Something went wrong";
        //    return View("ApplicationError");
        //}
        [HttpGet]
        public ActionResult SaveCertificate()
        {
            var applicationId = Convert.ToInt64(Session["AppId"]);
            var application = _registrationService.GetApplicationDetails(applicationId);
            var templatesInAppForm = _appForm.GeTemplatesInApp(application.AppFormId).ToList();

            var isRightState = _stateManager.ConfirmFillStage(application, templatesInAppForm, "CU", Session["WorkFlowList"] as List<ApplicationFormWorkFlow>, "Fill");
            if (!isRightState || application.UserName != User.Identity.GetUserName())
            {
                application.FillStage = 0;
                application.WorkFlowStage = 0;
                _registrationService.SaveApplication(application);
                ViewBag.message = "Please follow the right process, Do not copy and paste URL's";
                return View("ApplicationError");
                //return RedirectToAction("WorkFlowManager", "Application");
            }
            var certificates = _registrationService.GetCertificates(applicationId).ToList();
            Session["AppCerts"] = certificates;

            var formTemplateList = Session["FormTemplates"] as List<TemplatesInAppForms> ?? new List<TemplatesInAppForms>();
            var certTemplate = formTemplateList[application.FillStage];

            var templateSettings = _registrationService.GetFormTemplateSettings(certTemplate.ApplicationFormId, certTemplate.FormTemplateId);
            ViewBag.maxEntry = templateSettings.MaxEntry;
            return View(certificates);
        }
        [HttpPost]
        public ActionResult SaveCertificate(HttpPostedFileBase certificateUpload, string certificateType)
        {
            var applicationId = Convert.ToInt64(Session["AppId"]);
            var application = _registrationService.GetApplicationDetails(applicationId);
            //check that entry has not reached maximum entry.
            var formTemplateList = Session["FormTemplates"] as List<TemplatesInAppForms> ?? new List<TemplatesInAppForms>();
            var certTemplate = formTemplateList[application.FillStage];

            var templateSettings = _registrationService.GetFormTemplateSettings(certTemplate.ApplicationFormId,
                certTemplate.FormTemplateId);

            var certErrorModel = Session["AppCerts"] as List<Certificate> ?? new List<Certificate>();
            var numOfcertDetailsEntered = certErrorModel.Count;

            if (templateSettings.MaxEntry == numOfcertDetailsEntered)
            {
                ModelState.AddModelError("", "You have already entered the maximum number of Certificates allowed for this form, Please click continue to proceed with application");
                ViewBag.maxEntry = templateSettings.MaxEntry;
                return View(certErrorModel);
            }


            var appNum = Session["AppNum"].ToString();
            var applicantsCertificates = Session["AppCerts"] as List<Certificate> ?? new List<Certificate>();
            if (certificateUpload != null)
            {
                if (certificateUpload.ContentLength > 0)
                {
                    //check the type of file being uploaeded
                    //check that format is in csv
                    string fileExtension = Path.GetExtension(certificateUpload.FileName);
                    if (fileExtension != ".jpg")
                    {
                        ModelState.AddModelError("", "Incorrect File format, Only jpg files are acceptable");
                        ViewBag.maxEntry = templateSettings.MaxEntry;
                        return View(applicantsCertificates);
                    }

                    if (certificateUpload.ContentLength > 200 * 1024)
                    {
                        ViewBag.maxEntry = templateSettings.MaxEntry;
                        ModelState.AddModelError("", "File is too Large");
                        return View(applicantsCertificates);
                    }
                    string filePath = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/ApplicantCertificates/" + User.Identity.Name + "_" + certificateUpload.FileName);
                    certificateUpload.SaveAs(filePath);
                    var certificate = new Certificate()
                    {
                        CertificateName = certificateUpload.FileName,
                        ApplicationId = applicationId,
                        CertificateUrl = filePath,
                        CertificateType = certificateType
                    };
                    _registrationService.SaveCertificates(certificate);
                    applicantsCertificates.Add(certificate);
                    Session["AppCerts"] = applicantsCertificates;

                }
            }
            ViewBag.maxEntry = templateSettings.MaxEntry;
            return View(applicantsCertificates);
        }

        public ActionResult RemoveCertificate(long id)
        {
            try
            {
                var certificate = _registrationService.GetCertificate(id);
                string filePath = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/ApplicantCertificates/" + User.Identity.Name + "_" + certificate.CertificateName);
                System.IO.File.Delete(filePath);
                _registrationService.DeleteCertificate(certificate);
                return RedirectToAction("SaveCertificate");
            }
            catch (Exception)
            {
                ViewBag.message = "Something went wrong";
                return View("ApplicationError");
            }

        }
        public ActionResult FinishCertUpload()
        {
            var applicationId = Convert.ToInt64(Session["AppId"]);
            var application = _registrationService.GetApplicationDetails(applicationId);
            //check that entry is greater than min entry.
            var formTemplateList = Session["FormTemplates"] as List<TemplatesInAppForms> ?? new List<TemplatesInAppForms>();
            var certTemplate = formTemplateList[application.FillStage];

            var templateSettings = _registrationService.GetFormTemplateSettings(certTemplate.ApplicationFormId,
                certTemplate.FormTemplateId);

            var certErrorModel = Session["AppCerts"] as List<Certificate> ?? new List<Certificate>();
            var numOfcertDetailsEntered = certErrorModel.Count;

            if (templateSettings.MinEntry > numOfcertDetailsEntered)
            {
                ModelState.AddModelError("", "You have to upload at least " + templateSettings.MinEntry + " certificates to continue application");
                ViewBag.maxEntry = templateSettings.MaxEntry;
                return View("SaveCertificate", certErrorModel);
            }


            //var applicantsCertificates = Session["AppCerts"] as List<Certificate> ?? new List<Certificate>();
            //foreach (var appCert in applicantsCertificates)
            //{
            //    _registrationService.SaveCertificates(appCert);
            //}
            //var appId = Convert.ToInt64(Session["AppId"]);
            //var application = _registrationService.GetApplicationDetails(applicationId);
            var certTemplt = _configurationService.GetFormTemplateByCode("CU");
            if (certTemplt == null)
            {
                ViewBag.message = "An error occured, Please try again";
                return View("ApplicationError");
            }
            var currentFS = formTemplateList.FindIndex(x => x.FormTemplateId == certTemplt.Id);
            application.FillStage = currentFS + 1;
            _registrationService.SaveApplication(application);

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
                Action = "uploaded certificate(s) for application " + applicationForm.Name,
                Timestamp = localTime,
                FormTemplateId = formTemplateId,
                AppNum = application.AppNum
            };
            _eventLogRepo.SaveEvent(eventLog);

            //var formTemplateList = Session["FormTemplates"] as List<TemplatesInAppForms>;
            // formTemplateList.RemoveAt(0);
            if (application.FillStage < formTemplateList.Count)
            {
                var nextTemplateInApp = formTemplateList[application.FillStage];
                var formTemplate = _appForm.GetFormTemplate(nextTemplateInApp.FormTemplateId);
                switch (formTemplate.Code)
                {
                    case "BD":
                        return RedirectToAction("SaveBiodata");
                    case "OLR":
                        return RedirectToAction("SaveOLevelResult");
                    case "ED":
                        return RedirectToAction("AddEducationalDetail", "EducationalDetails");
                    case "WE":
                        return RedirectToAction("AddWorkExperience", "WorkExperience");
                    case "REF":
                        return RedirectToAction("AddReference", "Reference");
                    case "PU":
                        return RedirectToAction("SavePassport");
                    case "CU":
                        return RedirectToAction("SaveCertificate");
                    case "PC":
                        return RedirectToAction("AddProgramCourse", "ApplicantsProgramCourse");
                    case "REG":
                        return RedirectToAction("SaveRegNum");
                    case "JR":
                        return RedirectToAction("AddJambScore");
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
                //appFormWorkFlow.RemoveAt(0);
                //then select nextworkflow and redirect appropriately
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
            ViewBag.message("Something went wrong");
            return View("ApplicationError");
        }

        [HttpGet]
        public ActionResult SaveOLevelResult(bool? minimumDetailsFilled, int? minimumEntry)
        {
            var applicationId = Convert.ToInt32(Session["AppId"]);
            var application = _registrationService.GetApplicationDetails(applicationId);


            var templatesInAppForm = _appForm.GeTemplatesInApp(application.AppFormId).ToList();
            var isRightState = _stateManager.ConfirmFillStage(application, templatesInAppForm, "OLR", Session["WorkFlowList"] as List<ApplicationFormWorkFlow>, "Fill");
            if (!isRightState || application.UserName != User.Identity.GetUserName())
            {
                application.FillStage = 0;
                application.WorkFlowStage = 0;
                _registrationService.SaveApplication(application);
                ViewBag.message = "Please follow the right process, Do not copy and paste URL's";
                return View("ApplicationError");
                //return RedirectToAction("WorkFlowManager", "Application");
            }

            var grades = _registrationService.GetOLevelGrades().ToList();
            var subjects = _registrationService.GetOLevelSubjects().ToList();
            var examTypes = _registrationService.GeExamTypes().ToList();
            var yearList = new List<Years>();
            var currentYear = DateTime.Today.Year;
            for (int i = 1960; i <= currentYear; i++)
            {
                var newYear = new Years()
                {
                    Year = i
                };
                yearList.Add(newYear);
            }

            var formTemplateList = Session["FormTemplates"] as List<TemplatesInAppForms> ?? new List<TemplatesInAppForms>();
            var oLevelTemplate = formTemplateList[application.FillStage];
            var templateSettings = _registrationService.GetFormTemplateSettings(oLevelTemplate.ApplicationFormId, oLevelTemplate.FormTemplateId);

            var oLevelResultDetailsViewModel = new OLevelResultDetailsViewModel();
            oLevelResultDetailsViewModel.MaxEntry = templateSettings.MaxEntry;

            oLevelResultDetailsViewModel.ResultDetails = new List<OLevelResultDetails>();
            var listOfResultDetails = new List<OLevelResultDetails>();

            var oLevelDetailsForThisApp = _registrationService.GetOLevelDetails(applicationId).ToList();
            if (oLevelDetailsForThisApp.Any())
            {
                foreach (var detail in oLevelDetailsForThisApp)
                {
                    var oLevelResult = _registrationService.GetOLevelResults(detail.Id).ToList();
                    var oLevelResultDetails = new OLevelResultDetails()
                    {
                        DetailId = detail.Id,
                        CandidateName = detail.CandidateName,
                        //SchoolName = detail.SchoolName,
                        ExamNumber = detail.ExamNumber,
                        Year = detail.Year,
                        ExamType = detail.ExamType,
                        Subjects = subjects,
                        Grades = grades,
                        ExamTypes = examTypes,
                        Years = yearList
                    };
                    for (int i = 0; i < oLevelResult.Count; i++)
                    {
                        if (i == 0)
                        {
                            oLevelResultDetails.Subject1 = oLevelResult[i].SubjectId.ToString();
                            oLevelResultDetails.Grade1 = oLevelResult[i].GradeId.ToString();
                        }
                        if (i == 1)
                        {
                            oLevelResultDetails.Subject2 = oLevelResult[i].SubjectId.ToString();
                            oLevelResultDetails.Grade2 = oLevelResult[i].GradeId.ToString();
                        }
                        if (i == 2)
                        {
                            oLevelResultDetails.Subject3 = oLevelResult[i].SubjectId.ToString();
                            oLevelResultDetails.Grade3 = oLevelResult[i].GradeId.ToString();
                        }
                        if (i == 3)
                        {
                            oLevelResultDetails.Subject4 = oLevelResult[i].SubjectId.ToString();
                            oLevelResultDetails.Grade4 = oLevelResult[i].GradeId.ToString();
                        }
                        if (i == 4)
                        {
                            oLevelResultDetails.Subject5 = oLevelResult[i].SubjectId.ToString();
                            oLevelResultDetails.Grade5 = oLevelResult[i].GradeId.ToString();
                        }
                        if (i == 5)
                        {
                            oLevelResultDetails.Subject6 = oLevelResult[i].SubjectId.ToString();
                            oLevelResultDetails.Grade6 = oLevelResult[i].GradeId.ToString();
                        }
                        if (i == 6)
                        {
                            oLevelResultDetails.Subject7 = oLevelResult[i].SubjectId.ToString();
                            oLevelResultDetails.Grade7 = oLevelResult[i].GradeId.ToString();
                        }
                        if (i == 7)
                        {
                            oLevelResultDetails.Subject8 = oLevelResult[i].SubjectId.ToString();
                            oLevelResultDetails.Grade8 = oLevelResult[i].GradeId.ToString();
                        }
                        if (i == 8)
                        {
                            oLevelResultDetails.Subject9 = oLevelResult[i].SubjectId.ToString();
                            oLevelResultDetails.Grade9 = oLevelResult[i].GradeId.ToString();
                        }
                    }


                    listOfResultDetails.Add(oLevelResultDetails);
                }
            }
            else
            {
                var oLevelResultDetails = new OLevelResultDetails()
                {
                    Subjects = subjects,
                    Grades = grades,
                    ExamTypes = examTypes,
                    Years = yearList
                };
                listOfResultDetails.Add(oLevelResultDetails);
            }


            oLevelResultDetailsViewModel.ResultDetails = listOfResultDetails;
            Session["OLevelViewModel"] = oLevelResultDetailsViewModel;
            if (minimumDetailsFilled != null && minimumDetailsFilled == false)
            {
                ModelState.AddModelError("", "You have to enter at least " + minimumEntry + " O'Level Result(s) before you can proceed");
            }
            return View(oLevelResultDetailsViewModel);
            //var applicationId = Convert.ToInt32(Session["AppId"]);
            //var oLevelTemplateModel = new OLevelResultDetails();
            //var grades = _registrationService.GetOLevelGrades();
            //var subjects = _registrationService.GetOLevelSubjects();
            //oLevelTemplateModel.Grades = grades;
            //oLevelTemplateModel.Subjects = subjects;
            //oLevelTemplateModel.OLevelDetails = _registrationService.GetOLevelDetails(applicationId).ToList();
            //Session["oLevelDetails"] = oLevelTemplateModel.OLevelDetails;
            //if (minimumDetailsFilled != null && minimumDetailsFilled == false)
            //{
            //    ModelState.AddModelError("", "You have to enter at least " + minimumEntry + " O'Level Detail(s) before you can proceed");
            //}
            //return View(oLevelTemplateModel);
        }
        [HttpPost]
        public ActionResult SaveOLevelResult(OLevelResultDetails details)
        {
            try
            {
                var applicationId = Convert.ToInt64(Session["AppId"]);

                if (details.DetailId > 0)
                {
                    var resultDetails = _registrationService.GetOLevelDetail(details.DetailId);
                    _registrationService.DeleteOLevelDetai(resultDetails);
                }

                var oLevelDetail = new OLevelDetail()
                {
                    ApplicationId = applicationId,
                    CandidateName = details.CandidateName,
                    //SchoolName = details.SchoolName,
                    ExamNumber = details.ExamNumber,
                    Year = details.Year,
                    ExamType = details.ExamType
                };
                _registrationService.SaveOlevelDetail(oLevelDetail);

                //Save subject1
                var mathsSubject = _registrationService.GetOLevelSubjectByCode("MAT");
                var englishSubject = _registrationService.GetOLevelSubjectByCode("ENG");

                if (!string.IsNullOrEmpty(details.Subject1))
                {
                    var oLevelResult = new OLevelResult()
                    {
                        DetailId = oLevelDetail.Id,
                        SubjectId = Convert.ToInt32(details.Subject1),
                        GradeId = Convert.ToInt32(details.Grade1)
                    };
                    _registrationService.SaveOLevelResult(oLevelResult);
                }

                //Save subject2
                if (!string.IsNullOrEmpty(details.Subject2))
                {
                    var oLevelResult2 = new OLevelResult()
                       {
                           DetailId = oLevelDetail.Id,
                           SubjectId = Convert.ToInt32(details.Subject2),
                           GradeId = Convert.ToInt32(details.Grade2)
                       };
                    _registrationService.SaveOLevelResult(oLevelResult2);
                }


                //Save subject3
                if (!string.IsNullOrEmpty(details.Subject3))
                {
                    var oLevelResult3 = new OLevelResult()
                    {
                        DetailId = oLevelDetail.Id,
                        SubjectId = Convert.ToInt32(details.Subject3),
                        GradeId = Convert.ToInt32(details.Grade3)
                    };
                    _registrationService.SaveOLevelResult(oLevelResult3);
                }

                //Save subject4
                if (!string.IsNullOrEmpty(details.Subject4))
                {
                    var oLevelResult4 = new OLevelResult()
                    {
                        DetailId = oLevelDetail.Id,
                        SubjectId = Convert.ToInt32(details.Subject4),
                        GradeId = Convert.ToInt32(details.Grade4)
                    };
                    _registrationService.SaveOLevelResult(oLevelResult4);
                }

                //Save subject5
                if (!string.IsNullOrEmpty(details.Subject5))
                {
                    var oLevelResult5 = new OLevelResult()
                    {
                        DetailId = oLevelDetail.Id,
                        SubjectId = Convert.ToInt32(details.Subject5),
                        GradeId = Convert.ToInt32(details.Grade5)
                    };
                    _registrationService.SaveOLevelResult(oLevelResult5);
                }

                //Save subject6
                if (!string.IsNullOrEmpty(details.Subject6))
                {
                    var oLevelResult6 = new OLevelResult()
                    {
                        DetailId = oLevelDetail.Id,
                        SubjectId = Convert.ToInt32(details.Subject6),
                        GradeId = Convert.ToInt32(details.Grade6)
                    };
                    _registrationService.SaveOLevelResult(oLevelResult6);
                }

                //Save subject7
                if (!string.IsNullOrEmpty(details.Subject7))
                {
                    var oLevelResult7 = new OLevelResult()
                    {
                        DetailId = oLevelDetail.Id,
                        SubjectId = Convert.ToInt32(details.Subject7),
                        GradeId = Convert.ToInt32(details.Grade7)
                    };
                    _registrationService.SaveOLevelResult(oLevelResult7);
                }

                //Save subject8
                if (!string.IsNullOrEmpty(details.Subject8))
                {
                    var oLevelResult8 = new OLevelResult()
                    {
                        DetailId = oLevelDetail.Id,
                        SubjectId = Convert.ToInt32(details.Subject8),
                        GradeId = Convert.ToInt32(details.Grade8)
                    };
                    _registrationService.SaveOLevelResult(oLevelResult8);
                }

                //Save subject9
                if (!string.IsNullOrEmpty(details.Subject9))
                {
                    var oLevelResult9 = new OLevelResult()
                    {
                        DetailId = oLevelDetail.Id,
                        SubjectId = Convert.ToInt32(details.Subject9),
                        GradeId = Convert.ToInt32(details.Grade9)
                    };
                    _registrationService.SaveOLevelResult(oLevelResult9);
                }
            }
            catch (Exception)
            {
                ViewBag.message("Something went wrong");
                return View("ApplicationError");
            }
            TempData["OLevelSaved"] = "Success";

            return RedirectToAction("SaveOLevelResult");
        }

        public ActionResult AddMoreOLevel()
        {
            var grades = _registrationService.GetOLevelGrades().ToList();
            var subjects = _registrationService.GetOLevelSubjects().ToList().ToList(); ;
            var examTypes = _registrationService.GeExamTypes().ToList();
            var yearList = new List<Years>();
            var currentYear = DateTime.Today.Year;
            for (int i = 1960; i <= currentYear; i++)
            {
                var newYear = new Years()
                {
                    Year = i
                };
                yearList.Add(newYear);
            }
            var oLevelResultDetails = new OLevelResultDetails()
            {
                Subjects = subjects,
                Grades = grades,
                ExamTypes = examTypes,
                Years = yearList
            };
            var viewModel = Session["OLevelViewModel"] as OLevelResultDetailsViewModel ?? new OLevelResultDetailsViewModel();
            //if statement below is just used to make sure you dont multiple new empty result details when user refreshes the page for instance
            if (viewModel.ResultDetails.Count < viewModel.MaxEntry)
            {
                viewModel.ResultDetails.Add(oLevelResultDetails);
            }

            return View("SaveOLevelResult", viewModel);
        }
        public ActionResult ContinuFromOLevel()
        {
            var applicationId = Convert.ToInt64(Session["AppId"]);
            var application = _registrationService.GetApplicationDetails(applicationId);
            //check that entry has not reached maximum entry.
            var formTemplateList = Session["FormTemplates"] as List<TemplatesInAppForms> ?? new List<TemplatesInAppForms>();
            var oLevelTemplate = formTemplateList[application.FillStage];
            var templateSettings = _registrationService.GetFormTemplateSettings(oLevelTemplate.ApplicationFormId,
                oLevelTemplate.FormTemplateId);
            var viewModel = Session["OLevelViewModel"] as OLevelResultDetailsViewModel ?? new OLevelResultDetailsViewModel();
            var numOfOLevelDetailsEntered = viewModel.ResultDetails.Count;

            //with the way the GetMethod of SaveOLevelResult is configured it would always have at least 1 result detail that supplies Subject, Grade and ExamType
            //There for you should also check whether it is empty by just checking if the Id of OLevelResultDetails is > 0 when minEntry == 1 if not it would always
            //pass this validation
            if (templateSettings.MinEntry > numOfOLevelDetailsEntered)
            {
                return RedirectToAction("SaveOLevelResult", new { minimumDetailsFilled = false, minimumEntry = templateSettings.MinEntry });
            }

            if (templateSettings.MinEntry == 1 && numOfOLevelDetailsEntered == 1 && !(viewModel.ResultDetails.First().DetailId > 0))
            {
                return RedirectToAction("SaveOLevelResult", new { minimumDetailsFilled = false, minimumEntry = templateSettings.MinEntry });
            }
            var OLRTemplt = _configurationService.GetFormTemplateByCode("OLR");
            if (OLRTemplt == null)
            {
                ViewBag.message = "An error occured, Please try again";
                return View("ApplicationError");
            }
            var currentFS = formTemplateList.FindIndex(x => x.FormTemplateId == OLRTemplt.Id);
            application.FillStage = currentFS + 1;
            _registrationService.SaveApplication(application);

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
                Action = "added his/her OLevel Result(s) for " + applicationForm.Name,
                Timestamp = localTime,
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
                        return RedirectToAction("SaveBiodata");
                    case "OLR":
                        return RedirectToAction("SaveOLevelResult");
                    case "ED":
                        return RedirectToAction("AddEducationalDetail", "EducationalDetails");
                    case "WE":
                        return RedirectToAction("AddWorkExperience", "WorkExperience");
                    case "REF":
                        return RedirectToAction("AddReference", "Reference");
                    case "PU":
                        return RedirectToAction("SavePassport");
                    case "CU":
                        return RedirectToAction("SaveCertificate");
                    case "PC":
                        return RedirectToAction("AddProgramCourse", "ApplicantsProgramCourse");
                    case "REG":
                        return RedirectToAction("SaveRegNum");
                    case "JR":
                        return RedirectToAction("AddJambScore");
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
                //appFormWorkFlow.RemoveAt(0);
                //then select nextworkflow and redirect appropriately
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
            ViewBag.message("Something went wrong");
            return View("ApplicationError");
        }
        [HttpGet]
        public ActionResult AddJambScore()
        {
            var subjects = _registrationService.GetOLevelSubjectsForJamb().OrderBy(x => x.Name).ToList();
            var applicationId = Convert.ToInt32(Session["AppId"]);
            var application = _registrationService.GetApplicationDetails(applicationId);


            var templatesInAppForm = _appForm.GeTemplatesInApp(application.AppFormId).ToList();

            var isRightState = _stateManager.ConfirmFillStage(application, templatesInAppForm, "JR", Session["WorkFlowList"] as List<ApplicationFormWorkFlow>, "Fill");
            if (!isRightState || application.UserName != User.Identity.GetUserName())
            {
                application.FillStage = 0;
                application.WorkFlowStage = 0;
                _registrationService.SaveApplication(application);
                ViewBag.message = "Please follow the right process, Do not copy and paste URL's";
                return View("ApplicationError");
                // return RedirectToAction("WorkFlowManager", "Application");
            }
            var regNum = application.RegNum;
            if (!string.IsNullOrEmpty(regNum))
            {
                var jambBreakdowm = _configurationService.GetJambBreakDown(regNum);
                if (jambBreakdowm != null)
                {
                    return ContinueFromManualJambEntry();
                }
            }

            var applicantsJambBreakDown = regNum != null ? _registrationService.GetManualJambBreakDown(regNum) : new ManualJambBreakDown();
            var model = Mapper.Map<ManualJambBreakDown, ManualJambBreakDownModel>(applicantsJambBreakDown) ?? new ManualJambBreakDownModel();
            model.Subjects = subjects;
            model.RegNum = regNum;
            return View(model);
        }
        [HttpPost]
        public ActionResult AddJambScore(ManualJambBreakDown manualJambBreakDown)
        {
            try
            {
                var subjects = _registrationService.GetOLevelSubjectsForJamb().OrderBy(x => x.Name).ToList();
                var loggedOnUser = User.Identity.Name;
                var applicationsWithRegNumEntered = _configurationService.GetApplicationsByRegNum(manualJambBreakDown.RegNum).ToList();
                if (applicationsWithRegNumEntered.Any(x => x.UserName != loggedOnUser))
                {
                    ModelState.AddModelError("", "The registration number entered does not belong to you");
                    var model = Mapper.Map<ManualJambBreakDown, ManualJambBreakDownModel>(manualJambBreakDown);
                    model.Subjects = subjects;
                    return View(model);
                }


                manualJambBreakDown.TotalScore = manualJambBreakDown.EngScore + manualJambBreakDown.Subject2Score +
                                                 manualJambBreakDown.Subject3Score + manualJambBreakDown.Subject4Score;

                if (manualJambBreakDown.Id > 0)
                {
                    _registrationService.UpdateManualJambBreakDown(manualJambBreakDown);
                }
                else
                {
                    _registrationService.SaveManualJambBreakDown(manualJambBreakDown);
                }
                var applicationId = Convert.ToInt32(Session["AppId"]);
                var application = _registrationService.GetApplicationDetails(applicationId);
                application.RegNum = manualJambBreakDown.RegNum;
                _registrationService.SaveApplication(application);
                TempData["Created"] = Success;

                return RedirectToAction("AddJambScore");
            }
            catch (Exception)
            {

                ViewBag.message("Something went wrong");
                return View("ApplicationError");
            }

        }

        public ActionResult ContinueFromManualJambEntry()
        {
            var applicationId = Convert.ToInt64(Session["AppId"]);
            var application = _registrationService.GetApplicationDetails(applicationId);
            var formTemplateList = Session["FormTemplates"] as List<TemplatesInAppForms> ?? new List<TemplatesInAppForms>();


            var jrTemplt = _configurationService.GetFormTemplateByCode("JR");
            if (jrTemplt == null)
            {
                ViewBag.message = "An error occured, Please try again";
                return View("ApplicationError");
            }
            var currentFS = formTemplateList.FindIndex(x => x.FormTemplateId == jrTemplt.Id);
            application.FillStage = currentFS + 1;
            _registrationService.SaveApplication(application);

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
                Action = "added his/her Jamb Result for " + applicationForm.Name,
                Timestamp = localTime,
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
                        return RedirectToAction("SaveBiodata");
                    case "OLR":
                        return RedirectToAction("SaveOLevelResult");
                    case "ED":
                        return RedirectToAction("AddEducationalDetail", "EducationalDetails");
                    case "WE":
                        return RedirectToAction("AddWorkExperience", "WorkExperience");
                    case "REF":
                        return RedirectToAction("AddReference", "Reference");
                    case "PU":
                        return RedirectToAction("SavePassport");
                    case "CU":
                        return RedirectToAction("SaveCertificate");
                    case "PC":
                        return RedirectToAction("AddProgramCourse", "ApplicantsProgramCourse");
                    case "REG":
                        return RedirectToAction("SaveRegNum");
                    case "JR":
                        return RedirectToAction("AddJambScore");
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
                //appFormWorkFlow.RemoveAt(0);
                //then select nextworkflow and redirect appropriately
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
            ViewBag.message("Something went wrong");
            return View("ApplicationError");
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