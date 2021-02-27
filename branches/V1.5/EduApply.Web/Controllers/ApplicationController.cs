﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using EduApply.Data.Entities;
using EduApply.Logic.Interfaces;
using EduApply.Web.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace EduApply.Web.Controllers
{
    public class ApplicationController : Controller
    {
        private IApplicationFormRepository _appForm;
        private IEventLogRepository _eventLogRepo;
        private IRegistrationService _registrationService;
        private IConfigurationService _configurationService;
        private ILocationRepository _locationRepository;
        private IPrintService _printService;

        public ApplicationController(IApplicationFormRepository appForm, IEventLogRepository eventLogRepo, IConfigurationService configurationService, IRegistrationService registrationService, ILocationRepository locationRepository, IPrintService printService)
        {
            this._appForm = appForm;
            this._eventLogRepo = eventLogRepo;
            this._registrationService = registrationService;
            this._configurationService = configurationService;
            this._locationRepository = locationRepository;
            this._printService = printService;
        }
        // GET: Application
        public ActionResult Index()
        {
            var appForms = _appForm.GetAppForms().Where(x => x.EndDate >= DateTime.UtcNow);
            var model = Mapper.Map<IEnumerable<ApplicationForm>, IEnumerable<ApplicationFormModel>>(appForms);
            return View(model);
        }
        public ActionResult ResumeSubmitted(int appFormId, long applicationId, string name)
        {
            var localTime = _configurationService.GetCurrentWestAfricanDateTime();
            var appForm = _appForm.GetAppForms(appFormId);
            var application = _registrationService.GetApplicationDetails(applicationId);
            Session["AppId"] = application.Id;
            Session["AppFormId"] = appFormId;
            Session["AppNum"] = application.AppNum;
            Session["AppFormName"] = name;
            Session["ApplicationForm"] = appForm;
            if (appForm.EndDate > localTime && appForm.AllowApplicationEditAfterSubmission)
            {
                return RedirectToAction("Resume", new { appFormId = appFormId, applicationId = applicationId, name = name });
            }
            return RedirectToAction("ApplicationPreview");
        }
        public ActionResult Resume(int appFormId, long applicationId, string name)
        {
            var appForm = _appForm.GetAppForms(appFormId);
            var appFormWorkFlow = _appForm.GetApplicationFormWorkFlow2(appFormId).ToList();
            Session["WorkFlowList"] = appFormWorkFlow;

            var application = _registrationService.GetApplicationDetails(applicationId);
            Session["AppId"] = application.Id;
            Session["AppFormId"] = appFormId;
            Session["AppNum"] = application.AppNum;
            Session["AppFormName"] = name;
            Session["ApplicationForm"] = appForm;
            Session["FormTemplates"] = _appForm.GeTemplatesInApp(appFormId).ToList();

            application.WorkFlowStage = 0;
            application.FillStage = 0;
            _registrationService.SaveApplication(application);
            Session["Application"] = application;
            //get the first workflow item of the the list of workflow for this form to determine where to  redirect user to
            if (application.WorkFlowStage == appFormWorkFlow.Count)
            {
                return RedirectToAction("ApplicationPreview");
            }
            var workflowId = appFormWorkFlow[application.WorkFlowStage].WorkFlowId;
            var eventLog = new EventLog()
            {
                ApplicationFormId = appFormId,
                Username = User.Identity.Name,
                WorkFlowId = workflowId,
                Action = "resumed his or her application for " + name,
                Timestamp = DateTime.Now
            };
            _eventLogRepo.SaveEvent(eventLog);
            var workFlowItem = _appForm.GetWorkFlowItem(workflowId);
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
            //if we reach here then something wentt wrong
            ViewBag.message = "Something went wrong";
            return View("ApplicationError");
        }
        public ActionResult Apply(int appFormId, string name)
        {

            var appForm = _appForm.GetAppForms(appFormId);

            var anyApplication = _registrationService.GetOpenApplication(appFormId, User.Identity.Name).ToList();
            if (anyApplication.Any())
            {
                Session["AppNum"] = "";
                Session["AppFormName"] = "";
                if (anyApplication.Any(x => !x.IsSubmitted))
                {
                    ViewBag.message = "Sorry you cannot apply to this form because you have an open application for this form";
                    return View("ApplicationError");
                }

                if (!appForm.AllowMultipleApplications)
                {
                    ViewBag.message = "Sorry you cannot apply to this form more than once";
                    return View("ApplicationError");
                }
            }



            var appFormWorkFlow = _appForm.GetApplicationFormWorkFlow2(appFormId).ToList();
            Session["WorkFlowList"] = appFormWorkFlow;

            var passedWorkFlow = new List<ApplicationFormWorkFlow>();
            Session["PassedWorkFlow"] = passedWorkFlow;
            var localTime = _configurationService.GetCurrentWestAfricanDateTime();
            //start application for applicant
            // var session = _configurationService.GetSessionByAppForm(appForm);
            var application = new Application()
            {
                IsSubmitted = false,
                IsAdmitted = false,
                AppNum = GenerateAppNum(appFormId),
                ApplicationDate = localTime,
                WorkFlowStage = 0,
                FillStage = 0,
                UserName = User.Identity.Name,
                AppFormId = appFormId,
                SessionId = appForm.SessionId
            };
            _registrationService.SaveApplication(application);
            Session["AppId"] = application.Id;
            Session["AppFormId"] = appFormId;
            Session["AppNum"] = application.AppNum;
            Session["AppFormName"] = name;
            Session["ApplicationForm"] = appForm;

            //get the first workflow item of the the list of workflow for this form to determine where to  redirect user to
            var workflowId = appFormWorkFlow.FirstOrDefault().WorkFlowId;
            var eventLog = new EventLog()
            {
                ApplicationFormId = appFormId,
                Username = User.Identity.Name,
                WorkFlowId = workflowId,
                Action = "started an application for " + name,
                Timestamp = DateTime.Now
            };
            _eventLogRepo.SaveEvent(eventLog);
            var workFlowItem = _appForm.GetWorkFlowItem(workflowId);
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
            //if we reach here then something wentt wrong
            ViewBag.message = "Something went wrong";
            return View("ApplicationError");
        }
        public ActionResult WorkFlowManager()
        {
            var applicationId = Convert.ToInt32(Session["AppId"]);
            var application = _registrationService.GetApplicationDetails(applicationId);
            var appFormWorkFlow = Session["WorkFlowList"] as List<ApplicationFormWorkFlow>;
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
                application = _registrationService.GetApplicationDetails(applicationId);
                application.IsSubmitted = true;
                ViewBag.message = "Congratulations, you have successfully completed your Application";
                return View("ApplicationComplete");
            }

            ViewBag.message("Something went wrong");
            return View("ApplicationError");
        }

        public ActionResult BackApplication()
        {
            var applicationId = Convert.ToInt64(Session["AppId"]);
            var application = _registrationService.GetApplicationDetails(applicationId);
            if (application.WorkFlowStage > 0)
            {
                application.WorkFlowStage--;
                _registrationService.SaveApplication(application);
            }


            var appFormWorkFlow = Session["WorkFlowList"] as List<ApplicationFormWorkFlow>;
            var prevWorkFlow = appFormWorkFlow[application.WorkFlowStage];
            var workFlowItem = _appForm.GetWorkFlowItem(prevWorkFlow.WorkFlowId);
            // Session["WorkFlowList"] = appFormWorkFlow;
            switch (workFlowItem.Name)
            {
                case "Pay":
                    if (application.IsPaid && application.WorkFlowStage > 0)
                    {
                        return RedirectToAction("BackApplication");
                    }
                    return RedirectToAction("ValidatePayment", "Payment");
                case "Fill":
                    if (application.WorkFlowStage > 0)
                    {
                        return RedirectToAction("BackApplication", "Fill");
                    }
                    if (application.FillStage > 0)
                        application.FillStage--;
                    _registrationService.SaveApplication(application);
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
                    return RedirectToAction("BackApplication");

            }
            ViewBag.message = "Ooops!, Something went wrong";
            return View("ApplicationError");
        }

        string GenerateAppNum(int appFormId)
        {
            var lastApplication = _registrationService.GetLastApplication();
            var applicationId = lastApplication == null ? 999 : lastApplication.Id;
            string todaysDate = DateTime.Today.ToString();
            todaysDate = todaysDate.Remove(10);
            string[] sep = todaysDate.Split('/');
            string year = sep[2].Remove(0, 2);
            string editedDate = sep[0] + sep[1] + year;
            string applicationNumber = (appFormId + "/" + (applicationId + 1) + "/" + editedDate).Trim();
            return applicationNumber;
        }

        public ActionResult ApplicationPreview()
        {
            try
            {
                var appFormId = Convert.ToInt32(Session["AppFormId"]);
                var applicationForm = _appForm.GetAppForms(appFormId);
                var appFormWorkFlow = _appForm.GetApplicationFormWorkFlow2(appFormId).ToList();
                var workFlowIdz = appFormWorkFlow.Select(x => x.WorkFlowId).ToList();
                var applicationId = Convert.ToInt64(Session["AppId"]);
                var application = _registrationService.GetApplicationDetails(applicationId);
                var applicationPreviewModel = new ApplicationPreviewPageModel();
                applicationPreviewModel.ApplicationId = applicationId;
                if (application.ExamVenueId > 0)
                {
                    var examVenue = _configurationService.GetExamVenue(application.ExamVenueId);
                    applicationPreviewModel.Venue = examVenue.Venue.Name;
                    applicationPreviewModel.SeatNo = application.SeatNo;
                    applicationPreviewModel.ExamDate = examVenue.ExamDate;
                }
                var oLevelResultDetails = new List<OLevelResultDetailsPreview>();


                var oLevelDetails = _registrationService.GetOLevelDetails(applicationId).ToList();
                if (oLevelDetails.Any())
                {

                    var grades = _registrationService.GetOLevelGrades().ToList();
                    var subjects = _registrationService.GetOLevelSubjects().ToList();
                    foreach (var detail in oLevelDetails)
                    {
                        var oLevelResultDetailsPreview = new OLevelResultDetailsPreview()
                        {
                            CandidateName = detail.CandidateName,
                            //SchoolName = detail.SchoolName,
                            ExamNumber = detail.ExamNumber,
                            ExamType = detail.ExamType,
                            Year = detail.Year
                        };
                        var subjectResults = _registrationService.GetOLevelResults(detail.Id).ToList();
                        for (int i = 0; i < subjectResults.Count; i++)
                        {
                            if (i == 0)
                            {
                                oLevelResultDetailsPreview.Subject1 = subjects.FirstOrDefault(x => x.Id == subjectResults[i].SubjectId).Name;
                                oLevelResultDetailsPreview.Grade1 = grades.FirstOrDefault(x => x.Id == subjectResults[i].GradeId).Name;
                            }
                            if (i == 1)
                            {
                                oLevelResultDetailsPreview.Subject2 = subjects.FirstOrDefault(x => x.Id == subjectResults[i].SubjectId).Name;
                                oLevelResultDetailsPreview.Grade2 = grades.FirstOrDefault(x => x.Id == subjectResults[i].GradeId).Name;
                            }
                            if (i == 2)
                            {
                                oLevelResultDetailsPreview.Subject3 = subjects.FirstOrDefault(x => x.Id == subjectResults[i].SubjectId).Name;
                                oLevelResultDetailsPreview.Grade3 = grades.FirstOrDefault(x => x.Id == subjectResults[i].GradeId).Name;
                            }
                            if (i == 3)
                            {
                                oLevelResultDetailsPreview.Subject4 = subjects.FirstOrDefault(x => x.Id == subjectResults[i].SubjectId).Name;
                                oLevelResultDetailsPreview.Grade4 = grades.FirstOrDefault(x => x.Id == subjectResults[i].GradeId).Name;
                            }
                            if (i == 4)
                            {
                                oLevelResultDetailsPreview.Subject5 = subjects.FirstOrDefault(x => x.Id == subjectResults[i].SubjectId).Name;
                                oLevelResultDetailsPreview.Grade5 = grades.FirstOrDefault(x => x.Id == subjectResults[i].GradeId).Name;
                            }
                            if (i == 5)
                            {
                                oLevelResultDetailsPreview.Subject6 = subjects.FirstOrDefault(x => x.Id == subjectResults[i].SubjectId).Name;
                                oLevelResultDetailsPreview.Grade6 = grades.FirstOrDefault(x => x.Id == subjectResults[i].GradeId).Name;
                            }
                            if (i == 6)
                            {
                                oLevelResultDetailsPreview.Subject7 = subjects.FirstOrDefault(x => x.Id == subjectResults[i].SubjectId).Name;
                                oLevelResultDetailsPreview.Grade7 = grades.FirstOrDefault(x => x.Id == subjectResults[i].GradeId).Name;
                            }
                            if (i == 7)
                            {
                                oLevelResultDetailsPreview.Subject8 = subjects.FirstOrDefault(x => x.Id == subjectResults[i].SubjectId).Name;
                                oLevelResultDetailsPreview.Grade8 = grades.FirstOrDefault(x => x.Id == subjectResults[i].GradeId).Name;
                            }
                            if (i == 8)
                            {
                                oLevelResultDetailsPreview.Subject9 = subjects.FirstOrDefault(x => x.Id == subjectResults[i].SubjectId).Name;
                                oLevelResultDetailsPreview.Grade9 = grades.FirstOrDefault(x => x.Id == subjectResults[i].GradeId).Name;
                            }
                        }
                        oLevelResultDetails.Add(oLevelResultDetailsPreview);
                    }
                }
                applicationPreviewModel.OLevelResult = oLevelResultDetails;
                var formResult = _configurationService.GetFormResult(application.MappedToAppNum);//?? _configurationService.GetFormResult(applicationForm.MappedToFormId, application.RegNum)
                applicationPreviewModel.FormResultModel = Mapper.Map<FormResult, FormResultModel>(formResult);

                if (workFlowIdz.Contains(7))
                {
                    //7 is for non application result in the DB, if this changes at any time please remember to change it here
                    //or better yet figure out a better way to do this.
                    var sessionResult = _configurationService.GetSessionResult(application.RegNum);
                    applicationPreviewModel.SessionResultModel = Mapper.Map<SessionResult, SessionResultModel>(sessionResult);
                }

                if (workFlowIdz.Contains(3))
                {
                    //3 is for jamb break down in the DB, if this changes at any time please remember to change it here
                    var applicantsJambBreakDown = _configurationService.GetJambBreakDown(application.RegNum);
                    var manualResult = _registrationService.GetManualJambBreakDown(application.RegNum);
                    if (applicantsJambBreakDown != null)
                    {
                        applicationPreviewModel.JambScoreValidationModel = new JambScoreValidationModel()
                        {
                            RegNum = applicantsJambBreakDown.RegNum,
                            LastName = applicantsJambBreakDown.LastName,
                            FirstName = applicantsJambBreakDown.FirstName,
                            MiddleName = applicantsJambBreakDown.MiddleName,
                            CourseOfStudy = applicantsJambBreakDown.CourseOfStudy,
                            EngScore = applicantsJambBreakDown.EngScore,
                            Subject2 = applicantsJambBreakDown.Subject2,
                            Subject2Score = applicantsJambBreakDown.Subject2Score,
                            Subject3 = applicantsJambBreakDown.Subject3,
                            Subject3Score = applicantsJambBreakDown.Subject3Score,
                            Subject4 = applicantsJambBreakDown.Subject4,
                            Subject4Score = applicantsJambBreakDown.Subject4Score,
                            TotalScore = applicantsJambBreakDown.TotalScore

                        };
                    }
                    else
                    {
                        if (manualResult != null)
                        {
                            applicationPreviewModel.JambScoreValidationModel = new JambScoreValidationModel()
                                               {
                                                   RegNum = manualResult.RegNum,
                                                   //LastName = applicantsJambBreakDown.LastName,
                                                   //FirstName = applicantsJambBreakDown.FirstName,
                                                   //MiddleName = applicantsJambBreakDown.MiddleName,
                                                   //CourseOfStudy = applicantsJambBreakDown.CourseOfStudy,
                                                   EngScore = Convert.ToInt32(manualResult.EngScore),
                                                   Subject2 = manualResult.Subject2,
                                                   Subject2Score = Convert.ToInt32(manualResult.Subject2Score),
                                                   Subject3 = manualResult.Subject3,
                                                   Subject3Score = Convert.ToInt32(manualResult.Subject3Score),
                                                   Subject4 = manualResult.Subject4,
                                                   Subject4Score = Convert.ToInt32(manualResult.Subject4Score),
                                                   TotalScore = Convert.ToInt32(manualResult.TotalScore)

                                               };
                        }


                    }
                }


                var personalInformation = _registrationService.GetPersonalInformation(User.Identity.GetUserId()) ?? new PersonalInformation();
                var personalInfoModel = Mapper.Map<PersonalInformation, PersonalInfoPreviewModel>(personalInformation);
                var country = _locationRepository.GetCountry(personalInformation.Nationality);
                var state = _locationRepository.GetState(personalInformation.StateOfOrigin);
                var lga = _locationRepository.GetLocalGovernmentArea(personalInformation.LocalGovernment);
                personalInfoModel.Nationality = country.Name;
                personalInfoModel.StateOfOrigin = state.Name;
                personalInfoModel.LocalGovernment = lga.Name;
                applicationPreviewModel.PersonalInformationModel = personalInfoModel;

                //get certificates
                var certificates = _registrationService.GetCertificates(applicationId).ToList();
                applicationPreviewModel.Certificates = certificates;


                var educationalDetails = _registrationService.GetEducationalDetails(applicationId).ToList();

                applicationPreviewModel.EducationalDetailsModel =
                    Mapper.Map<List<EducationalDetails>, List<EducationalDetailsModel>>(educationalDetails);

                applicationPreviewModel.PictureDetails = _registrationService.GetPictureDetails(applicationId);

                applicationPreviewModel.References = _registrationService.GetReferences(applicationId);
                applicationPreviewModel.WorkExperiences = _registrationService.GetWorkExperience(applicationId);
                applicationPreviewModel.AdmissionStatus = application.IsAdmitted;

                var programId = application.ProgramId;
                if (programId > 0)
                {
                    var program = _configurationService.GetProgram(programId);
                    var course = _configurationService.GetCourse(application.CourseOfStudyId);

                    applicationPreviewModel.ProgramCoursePreviewModel = new ProgramCoursePreviewModel()
                    {
                        Program = program.Name,
                        Course = course.Name
                    };
                }
                if (application.IsSubmitted)
                {
                    var localTime = _configurationService.GetCurrentWestAfricanDateTime();
                    if (applicationForm.EndDate < localTime || !applicationForm.AllowApplicationEditAfterSubmission)
                    {
                        //This means form has closed
                        return View("SubmittedApplicationPreview", applicationPreviewModel);
                    }
                }


                return View(applicationPreviewModel);
            }
            catch (Exception)
            {

                ViewBag.message = "Something went wrong";
                return View("ApplicationError");
            }



        }
        public ActionResult SubmitApplication()
        {
            var applicationId = Convert.ToInt64(Session["AppId"]);
            var application = _registrationService.GetApplicationDetails(applicationId);
            if (!application.IsSubmitted)
            {
                var localTime = _configurationService.GetCurrentWestAfricanDateTime();
                application.IsSubmitted = true;
                application.SubmissionDate = localTime;
            }
            _registrationService.SaveApplication(application);

            ViewBag.message = "Congratulations, you have successfully completed your Application";
            return View("ApplicationComplete");
        }
        public ActionResult PrintAppForm(long applicationId)
        {
            string appFormDetails = "";
            var localTime = _configurationService.GetCurrentWestAfricanDateTime();
            var url = _configurationService.GetCurrentUrl();
            var schoolName = _configurationService.GetSchoolName();
            var application = _registrationService.GetApplicationDetails(applicationId);
            var appForm = _appForm.GetAppForms(application.AppFormId);
            if (appForm.FormCategoryId != 1)
            {
                //this means form is not an application Form
                return RedirectToAction("SearchErrorHandler", "Search", new { errorMessage = "NOT APPLICATION FORM" });
            }
            var personalInfo = _registrationService.GetPersonalInformationByEmail(application.UserName);
            appFormDetails += _printService.GetHeaderAndPassport(applicationId);
            appFormDetails = appFormDetails.Replace("#Url", url);
            appFormDetails = appFormDetails.Replace("#Date", localTime.ToString("dd-MMM-yyyy h:mm tt"));
            appFormDetails = appFormDetails.Replace("#SchoolName#", schoolName);
            appFormDetails += _printService.GetPersonalInformation(personalInfo);
            var workflowIdz = _appForm.GetApplicationFormWorkFlow2(application.AppFormId).Select(x => x.WorkFlowId).ToArray();
            if (workflowIdz.Any())
            {
                //check if jambBreakdown is part of workflow
                if (workflowIdz.Contains(3))
                {
                    appFormDetails += _printService.GetJambBreakDown(application.RegNum);
                }
                //check if nonapplicationResult is part of workflow
                if (workflowIdz.Contains(7))
                {
                    appFormDetails += _printService.GetNonApplicationResult(application.RegNum);
                }
                //NOTE: i am not doing the above for the remaining workflow or fprmtemplate because they are reliant on applicationId or application number
                // which are always unique per application therefore there is no need as this can take care of itself but the others might have entries for
                //other applications

            }

            appFormDetails += _printService.GetApplicationResult(application.AppNum, application.RegNum);

            appFormDetails += _printService.GetApplicantsVenue(application.AppFormId, application.ProgramId, application.CourseOfStudyId, application.ExamVenueId, application.SeatNo);
            appFormDetails += _printService.GetOLevelResult(application.Id);
            appFormDetails += _printService.GetEducationalDetails(application.Id);
            appFormDetails += _printService.GetWorkExperience(application.Id);
            appFormDetails += _printService.GetReferees(application.Id);
            appFormDetails += _printService.GetCertificate(application.Id);
            appFormDetails += _printService.GetProgramCourse(application.ProgramId, application.CourseOfStudyId);

            string path = Server.MapPath("~/App_Data/PdfFiles");
            string imgPath = Server.MapPath("~/images/jamb-logo-150x150.jpg");
            string filename = path + "/" + personalInfo.Email + "_ApplicatoinForm.pdf";

            var document = new Document(PageSize.A4, 20f, 20f, 20f, 20f);
            try
            {
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(filename, FileMode.Create));

                string htmlText = appFormDetails;
                // htmlText = htmlText.Replace("#ImageLogoUrl#", imgPath);

                var html = new StringReader(htmlText);

                document.Open();
                XMLWorkerHelper.GetInstance().ParseXHtml(writer, document, html);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                document.Close();
            }
            string fullPath = Path.Combine(Server.MapPath("~/App_Data/PdfFiles"), filename);
            return File(fullPath, "application/pdf", "ApplicationForm.pdf");
        }

        public ActionResult PrintPhotoCard(long applicationId)
        {
            var application = _registrationService.GetApplicationDetails(applicationId);
            var appForm = _appForm.GetAppForms(application.AppFormId);
            if (appForm.FormCategoryId != 1)
            {
                //this means form is not an application Form
                return RedirectToAction("SearchErrorHandler", "Search",
                new { errorMessage = "NOT APPLICATION FORM" });
            }
            if (!application.IsSubmitted)
            {
                return RedirectToAction("SearchErrorHandler", "Search",
                new { errorMessage = "NOT SUBMITTED" });
            }

            var examVenue = application.ExamVenueId > 0 ? _configurationService.GetExamVenue(application.ExamVenueId) : new ExamVenue();
            var personalInfo = _registrationService.GetPersonalInformationByEmail(application.UserName);
            string url = _configurationService.GetCurrentUrl();
            var jambBreakDown = _configurationService.GetJambBreakDown(application.RegNum);
            var schoolName = _configurationService.GetSchoolName();
            var localTime = _configurationService.GetCurrentWestAfricanDateTime();

            var studentPicture = _registrationService.GetPictureDetails(application.Id);
            string imgPath = Server.MapPath("~/images/SchoolLogo.jpg");
            string passportPath = studentPicture != null ? Server.MapPath("~/images/StudentPassport/" + studentPicture.Name) : "";
            var faculty = application.FacultyId > 0 ? _configurationService.GetFaculty(application.FacultyId) : new Faculty();
            var department = application.DepartmentId > 0 ? _configurationService.GetDepartment(application.DepartmentId)
                : new Department();
            var htmlTemplate = Server.MapPath("~/PrintTemplates/PhotoCard.html");
            string htmlText = System.IO.File.ReadAllText(htmlTemplate);
            htmlText = htmlText.Replace("#ImageLogoUrl#", imgPath);
            htmlText = htmlText.Replace("#SchoolName#", schoolName);
            htmlText = htmlText.Replace("#ImageStudentUrl#", passportPath);
            htmlText = htmlText.Replace("#FormName#", appForm.Name);
            htmlText = htmlText.Replace("#FullName#", personalInfo.LastName + " " + personalInfo.FirstName + " " + personalInfo.MiddleName);
            htmlText = htmlText.Replace("#RegNo#", application.RegNum);
            htmlText = htmlText.Replace("#Gender#", personalInfo.Gender);
            htmlText = htmlText.Replace("#Lga#", _locationRepository.GetLocalGovernmentArea(personalInfo.LocalGovernment).Name);
            htmlText = htmlText.Replace("#State#", _locationRepository.GetState(personalInfo.StateOfOrigin).Name);
            htmlText = htmlText.Replace("#Nationality#", _locationRepository.GetCountry(personalInfo.Nationality).Name);
            htmlText = htmlText.Replace("#DateofBirth#", Convert.ToDateTime(personalInfo.DateOfBirth).ToString("dd-MMM-yyyy"));
            htmlText = htmlText.Replace("#Faculty#", faculty.Name);
            htmlText = htmlText.Replace("#Department#", department.Name);
            htmlText = htmlText.Replace("#ApplicationNumber#", application.AppNum);

            if (examVenue.Venue != null)
            {
                htmlText = htmlText.Replace("#ExamDate#", examVenue.ExamDate.ToString("dd-MMM-yyyy h:mm tt"));
                htmlText = htmlText.Replace("#ExamVenue#", examVenue.Venue.Name);
                htmlText = htmlText.Replace("#SeatNumber#", application.SeatNo.ToString());
            }
            else
            {
                htmlText = htmlText.Replace("#ExamDate#", "");
                htmlText = htmlText.Replace("#ExamVenue#", "");
                htmlText = htmlText.Replace("#SeatNumber#", "");
                htmlText = htmlText.Replace("EXAM DATE:", "");
                htmlText = htmlText.Replace("EXAM VENUE:", "");
                htmlText = htmlText.Replace("SEAT NUMBER:", "");
            }

            htmlText = htmlText.Replace("#Url#", url);
            htmlText = htmlText.Replace("#printDate", localTime.ToString("dd/MMM/yyyy h:mm tt"));
            if (jambBreakDown != null)
            {
                htmlText = htmlText.Replace("#UtmeSubjects#", "English, " + jambBreakDown.Subject2 + ", " + jambBreakDown.Subject3 + ", " + jambBreakDown.Subject4);
                htmlText = htmlText.Replace("#UtmeScore#", jambBreakDown.TotalScore.ToString());
            }
            else
            {

                htmlText = htmlText.Replace("#UtmeSubjects#", "");
                htmlText = htmlText.Replace("#UtmeScore#", "");
                htmlText = htmlText.Replace("UTME SCORE:", "");
                htmlText = htmlText.Replace("UTME SUBJECTS:", "");
            }


            //htmlText = htmlText.Replace("#Date#", DateTime.Now.ToString("dd-MMM-yyyy"));
            var html = new StringReader(htmlText);

            string path = Server.MapPath("~/App_Data/PdfFiles/");

            string filename = path + "PhotoCard.pdf";

            var document = new Document(PageSize.A4, 20f, 20f, 20f, 20f);
            try
            {
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(filename, FileMode.Create));
                document.Open();
                XMLWorkerHelper.GetInstance().ParseXHtml(writer, document, html);
            }

            catch (Exception)
            {
                throw;
            }
            finally
            {
                document.Close();
                //ShowPdf(filename);
            }
            string fullPath = Path.Combine(Server.MapPath("~/App_Data/PdfFiles"), filename);
            return File(fullPath, "application/pdf", application.AppNum + " PhotoCard.pdf");
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