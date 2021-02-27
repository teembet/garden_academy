using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EduApply.Data.Entities;
using EduApply.Logic.Interfaces;
using EduApply.Web.Models;
using iTextSharp.text;
using Microsoft.AspNet.Identity;
using EduApply.Logic.Utility;

namespace EduApply.Web.Controllers
{
    [Authorize]
    public class ApplicantsProgramCourseController : Controller
    {
        private IRegistrationService _registrationService;
        private IApplicationFormRepository _appForm;
        private IEventLogRepository _eventLogRepo;
        private IStateManager _stateManager;
        private IConfigurationService _configurationService;

        public ApplicantsProgramCourseController(IRegistrationService registrationService, IConfigurationService configurationService, IApplicationFormRepository appForm, IEventLogRepository eventLogRepo, IStateManager stateManager)
        {
            this._registrationService = registrationService;
            this._appForm = appForm;
            this._eventLogRepo = eventLogRepo;
            this._stateManager = stateManager;
            this._configurationService = configurationService;
        }
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        [AllowAnonymous]
        public ActionResult ValidateDeptSelection(List<int> deptIdz)
        {
            bool isValid = true;
            var firstId = deptIdz[0];
            int appFormId = Convert.ToInt32(Session["AppFormId"]);
            var applicationForm = _appForm.GetAppForms(appFormId);
            if (applicationForm.DontAllowDiffDeptSelection)
            {
                foreach (var id in deptIdz)
                {
                    if (id != firstId)
                    {
                        isValid = false;
                    }
                }
            }
            return Json(isValid, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        [AllowAnonymous]
        public ActionResult GetDepartmentByProgramId(int? programId)
        {
            if (programId == null)
                programId = -1;
            var courses = new List<Course>();
            var departments = new List<Department>();
            if (User.IsInRole("Student"))
            {
                var programCourses = Session["ApplicantsProgramCourse"] as List<ProgramCourse>;
                var programCoursesForCurrentForm = programCourses.Where(x => x.ProgramId == programId);
                var courseIdz = programCoursesForCurrentForm.Select(x => x.CourseId).ToArray();
                courses = _configurationService.GetCourses().Where(x => courseIdz.Contains(x.Id) && x.IsActive).ToList();
                Session["SelectCourses"] = courses;
                departments = courses.Select(x => x.Department).Distinct().ToList();
            }
            else
            {
                courses = _configurationService.GetCourses(Convert.ToInt32(programId)).ToList();
            }

            var result = (from s in departments
                          select new
                          {
                              id = s.Id,
                              name = s.Name
                          }).ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        [AllowAnonymous]
        public ActionResult GetCourseByDeptId(int? deptId)
        {
            if (deptId == null)
                deptId = -1;
            var courses = Session["SelectCourses"] as List<Course>;
            courses = courses.Where(x => x.DepartmentId == Convert.ToInt32(deptId)).ToList();

            var result = (from s in courses
                          select new
                          {
                              id = s.Id,
                              name = s.Name
                          }).ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddProgramCourse(int? minEntry)
        {

            var applicationId = Convert.ToInt64(Session["AppId"]);
            var application = _registrationService.GetApplicationDetails(applicationId);

            var templatesInAppForm = _appForm.GeTemplatesInApp(application.AppFormId).ToList();

            var isRightState = _stateManager.ConfirmFillStage(application, templatesInAppForm, "PC", Session["WorkFlowList"] as List<ApplicationFormWorkFlow>, "Fill");
            if (!isRightState || application.UserName != User.Identity.GetUserName())
            {
                application.FillStage = 0;
                application.WorkFlowStage = 0;
                _registrationService.SaveApplication(application);
                ViewBag.message = "Please follow the right process, Do not copy and paste URL's";
                return View("ApplicationError");
                //  return RedirectToAction("WorkFlowManager", "Application");
            }
            var applicationForm = _appForm.GetAppForms(application.AppFormId);
            if (applicationForm.UseProgramCourseFromJamb)
            {
                var applicantsJambResult = _configurationService.GetJambBreakDown(application.RegNum);
                if (applicantsJambResult != null)
                {
                    var course = _configurationService.GetCoursesByCode(applicantsJambResult.CourseCode).ToList().FirstOrDefault();
                    var program = _configurationService.GetProgramsByCode(applicantsJambResult.ProgramCode).ToList().FirstOrDefault();
                    if (program != null && course != null)
                    {
                        var savedProgramCourse = _registrationService.GetApplicantsProgramCourses(applicationId).FirstOrDefault() ?? new ApplicantsProgramCourse();
                        savedProgramCourse.ApplicationId = applicationId;
                        savedProgramCourse.ProgramId = program.Id;
                        savedProgramCourse.CourseId = course.Id;
                        savedProgramCourse.DepartmentId = course.DepartmentId;
                        _registrationService.SaveApplicantsProgramCourse(savedProgramCourse);


                        var department = _configurationService.GetDepartment(course.DepartmentId);
                        application.DepartmentId = department.Id;
                        application.FacultyId = department.FacultyId;
                        _registrationService.SaveApplication(application);


                        _registrationService.SaveApplicantsProgramCourse(savedProgramCourse);
                        return ContinueFromProgCourse();
                    }
                }

            }

            if (applicationForm.UseDetailsFromJambBiodata)
            {
                var applicantsJambBiodata = _configurationService.GetJambBiodata(application.RegNum);
                if (applicantsJambBiodata != null)
                {
                    var course = _configurationService.GetCoursesByCode(applicantsJambBiodata.CourseCode).ToList().FirstOrDefault();
                    var program = _configurationService.GetProgramsByCode(applicantsJambBiodata.ProgramCode).ToList().FirstOrDefault();
                    if (program != null && course != null)
                    {
                        var savedProgramCourse = _registrationService.GetApplicantsProgramCourses(applicationId).FirstOrDefault() ?? new ApplicantsProgramCourse();
                        savedProgramCourse.ApplicationId = applicationId;
                        savedProgramCourse.ProgramId = program.Id;
                        savedProgramCourse.CourseId = course.Id;
                        savedProgramCourse.DepartmentId = course.DepartmentId;
                        _registrationService.SaveApplicantsProgramCourse(savedProgramCourse);


                        var department = _configurationService.GetDepartment(course.DepartmentId);
                        application.DepartmentId = department.Id;
                        application.FacultyId = department.FacultyId;
                        _registrationService.SaveApplication(application);


                        _registrationService.SaveApplicantsProgramCourse(savedProgramCourse);
                        return ContinueFromProgCourse();
                    }
                }

            }


            var formTemplateList = Session["FormTemplates"] as List<TemplatesInAppForms> ?? new List<TemplatesInAppForms>();
            var programCourseTemplate = formTemplateList[application.FillStage];
            var templateSettings = _registrationService.GetFormTemplateSettings(programCourseTemplate.ApplicationFormId, programCourseTemplate.FormTemplateId);
            var maxEntry = templateSettings.MaxEntry;

            //Get the List of programCourse previously eneterd by applicant if any
            var applicantsProgramCourseList = _registrationService.GetApplicantsProgramCourses(applicationId).ToList();




            //Get all the programCourse for this form configured by Admin
            var appFormProgramCourse = _configurationService.GetAppFormProgramCourses(application.AppFormId).ToList();
            //Select the programCourseId
            var programCourseIdz = appFormProgramCourse.Select(x => x.ProgramCourseId).ToArray();
            //Get the programCourse entities themselves
            var programCourses = _configurationService.GetProgramCourses().Where(x => programCourseIdz.Contains(x.Id)).ToList();
            //Save the programCourses to a session
            Session["ApplicantsProgramCourse"] = programCourses;
            //Select the propgramIds
            var programIdsForThisForm = programCourses.Select(x => x.ProgramId).ToArray();
            //Select the List of Programs that have their Id in programIdsForThisForm
            var programsForThisAppForm = _configurationService.GetPrograms().Where(x => x.IsActive && programIdsForThisForm.Contains(x.Id)).OrderBy(x => x.Code).ToList();

            var viewModel = new ApplicantProgramCourseCollection()
            {
                MaxEntry = maxEntry,
                ApplicantsProgramCourses = applicantsProgramCourseList
            };

            //This loop will only be executed if applicant has previously entered 1 or more programcourses
            foreach (var item in applicantsProgramCourseList)
            {
                var selectedItem = item;
                var programId = selectedItem.ProgramId;
                var modeOfStudy = selectedItem.ModeOfStudyId;
                item.Programs = programsForThisAppForm;
                var programCoursesForSelectedProgram = programCourses.Where(x => x.ProgramId == programId);
                var courseIdz = programCoursesForSelectedProgram.Select(x => x.CourseId).ToArray();
                //item.Courses 
                var courses = _configurationService.GetCourses().Where(x => courseIdz.Contains(x.Id) && x.IsActive).OrderBy(x => x.Name).ToList();
                var departmentIdz = courses.Select(x => x.DepartmentId).Distinct();
                var departments = courses.Select(x => x.Department).ToList();
                item.Departments = departments;
                //next we filter courses and eliminate courses that dont belong to selected department.
                item.Courses = courses.Where(x => x.DepartmentId == selectedItem.DepartmentId);
            }

            //Populate other dropdowns that applicant dint select from
            if (maxEntry > applicantsProgramCourseList.Count)
            {
                var difference = maxEntry - applicantsProgramCourseList.Count;
                for (int i = 0; i < difference; i++)
                {
                    var applicantsProgramCoursee = new ApplicantsProgramCourse()
                    {
                        ApplicationId = applicationId,
                        Programs = programsForThisAppForm,
                        ProgramId = 0,
                        DepartmentId = 0,
                        CourseId = 0,
                        Departments = new List<Department>(),
                        Courses = new List<Course>()
                    };
                    viewModel.ApplicantsProgramCourses.Add(applicantsProgramCoursee);

                }
            }
            if (minEntry != null)
            {
                ModelState.AddModelError("", "You need to enter at least " + minEntry + " Program and Course Choice");
            }
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult AddProgramCourse(AppProgramCourseModificationModel model)
        {
            if (model.ProgramId.Any())
            {
                var applicationId = Convert.ToInt64(Session["AppId"]);
                var savedApplicantProgramCourses = _registrationService.GetApplicantsProgramCourses(applicationId).ToList();
                //First Delete all program course applicant has previously saved if any
                if (savedApplicantProgramCourses.Any())
                {
                    foreach (var item in savedApplicantProgramCourses)
                    {
                        _registrationService.DeleteApplicantsProgramCourse(item);
                    }
                }

                for (int i = 0; i < model.ProgramId.Length; i++)
                {
                    if (model.ProgramId[i] > 0)
                    {
                        //var modeofstudy = new ModeOfStudy()
                        //{
                        //    ModeOfStudyId = 
                        //};

                        var applicantsProgramCourse = new ApplicantsProgramCourse()
                        {
                            ApplicationId = applicationId,
                            ProgramId = model.ProgramId[i],
                            DepartmentId = model.DepartmentId[i],
                            CourseId = model.CourseId[i],
                            ModeOfStudyId = model.ModeOfStudyId
                        };
                        _registrationService.SaveApplicantsProgramCourse(applicantsProgramCourse);
                        //Select the first programCourse and place student is the department and faculty for that course
                        if (i == 0)
                        {
                            if (model.DepartmentId[i] > 0)
                            {
                                var department = _configurationService.GetDepartment(model.DepartmentId[i]);
                                var application = _registrationService.GetApplicationDetails(applicationId);
                                application.DepartmentId = department.Id;
                                application.FacultyId = department.FacultyId;
                                _registrationService.SaveApplication(application);
                            }
                        }
                    }

                }
            }
            TempData["AppCourseSaved"] = "Success";
            return RedirectToAction("AddProgramCourse");
        }

        public ActionResult ContinueFromProgCourse()
        {

            var applicationId = Convert.ToInt64(Session["AppId"]);
            var application = _registrationService.GetApplicationDetails(applicationId);
            //check that entry has not reached maximum entry.
            var formTemplateList = Session["FormTemplates"] as List<TemplatesInAppForms> ?? new List<TemplatesInAppForms>();
            var programCouresTemplate = formTemplateList[application.FillStage];
            var templateSettings = _registrationService.GetFormTemplateSettings(programCouresTemplate.ApplicationFormId, programCouresTemplate.FormTemplateId);
            var appProgCourse = _registrationService.GetApplicantsProgramCourses(applicationId).ToList();
            var numOfprogramCourseEntered = appProgCourse.Count;

            if (templateSettings.MinEntry > numOfprogramCourseEntered)
            {
                return RedirectToAction("AddProgramCourse", new { minEntry = templateSettings.MinEntry });
            }


            var PcsTemplt = _configurationService.GetFormTemplateByCode("PC");
            if (PcsTemplt == null)
            {
                ViewBag.message = "An error occured, Please try again";
                return View("ApplicationError");
            }
            var currentFS = formTemplateList.FindIndex(x => x.FormTemplateId == PcsTemplt.Id);
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
                Action = "added his/her program Course choice for form" + applicationForm.Name,
                Timestamp = localTime,
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
    }
}