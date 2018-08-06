using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EduApply.Data.Entities;
using EduApply.Logic.Interfaces;
using EduApply.Logic.Service;
using EduApply.Logic.Utility;
using EduApply.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace EduApply.Web.Controllers
{
    public class CorrectionController : Controller
    {
        private ISearchRepository _searchRepository;
        private IRegistrationService _registrationService;
        private IConfigurationService _configurationService;
        private IApplicationFormRepository _appForm;
        private IAuditTrailRepository _auditTrailRepository;
        public CorrectionController(ISearchRepository searchRepository, IRegistrationService registrationService, IConfigurationService configurationService, IApplicationFormRepository appForm, IAuditTrailRepository auditTrailRepository)
        {
            this._searchRepository = searchRepository;
            this._registrationService = registrationService;
            this._configurationService = configurationService;
            this._appForm = appForm;
            this._auditTrailRepository = auditTrailRepository;
        }
        //
        // GET: /Correction/
        public ActionResult Index()
        {
            var correctionModel = new CorrectionModel()
            {
                SearchResults = new List<SearchResult>()
            };
            return View(correctionModel);
        }

        [HttpPost]
        public ActionResult Index(string regNum, string appNum)
        {
            var result = _searchRepository.GetSearchResult(regNum, appNum).ToList();
            var correctionModel = new CorrectionModel()
            {
                RegNum = regNum,
                AppNum = appNum,
                SearchResults = result
            };
            return View(correctionModel);
        }

        public ActionResult Update(long appId, string regNum, string appNum)
        {
            var application = _registrationService.GetApplicationDetails(appId);
            string oldRegNum = application.RegNum;
            string oldAppNum = application.AppNum;

            application.RegNum = regNum;
            application.AppNum = appNum;
            _registrationService.SaveApplication(application);

            //Update Name if Jamb Result is present
            if (!string.IsNullOrEmpty(application.RegNum))
            {
                var applicantsJambResult = _configurationService.GetJambBreakDown(application.RegNum);
                if (applicantsJambResult != null)
                {
                    var personalInformation = _registrationService.GetPersonalInformationByEmail(application.UserName);
                    TempData["JambResultPresent"] = true;
                    personalInformation.LastName = applicantsJambResult.LastName;
                    personalInformation.FirstName = applicantsJambResult.FirstName;
                    personalInformation.MiddleName = applicantsJambResult.MiddleName;
                    personalInformation.RegNum = application.RegNum;

                    _registrationService.UpdatePersonalInformation(personalInformation);
                }

                //Update Program Course if Applicants Application Form is configured to do so
                var applicationForm = _appForm.GetAppForms(application.AppFormId);
                if (applicationForm.UseProgramCourseFromJamb)
                {
                    if (applicantsJambResult != null)
                    {
                        var course = _configurationService.GetCoursesByCode(applicantsJambResult.CourseCode).ToList().FirstOrDefault();
                        var program = _configurationService.GetProgramsByCode(applicantsJambResult.ProgramCode).ToList().FirstOrDefault();
                        if (program != null && course != null)
                        {
                            var savedProgramCourse = _registrationService.GetApplicantsProgramCourses(appId).FirstOrDefault() ?? new ApplicantsProgramCourse();
                            savedProgramCourse.ApplicationId = appId;
                            savedProgramCourse.ProgramId = program.Id;
                            savedProgramCourse.CourseId = course.Id;
                            _registrationService.SaveApplicantsProgramCourse(savedProgramCourse);


                            var department = _configurationService.GetDepartment(course.DepartmentId);
                            application.DepartmentId = department.Id;
                            application.FacultyId = department.FacultyId;
                            _registrationService.SaveApplication(application);

                            _registrationService.SaveApplicantsProgramCourse(savedProgramCourse);
                        }
                    }

                }

            }

            var IUtilityService = EngineContext.Resolve<IUtilityService>();
            var userRole = UserManager.GetRoles(User.Identity.GetUserId());
            var localTime = _configurationService.GetCurrentWestAfricanDateTime();
            var auditTrail = new AuditTrail()
            {
                UserId = User.Identity.GetUserId(),
                Username = User.Identity.GetUserName(),
                AuditActionId = Convert.ToInt32(AuditTrailActions.ApplicantRegistration),
                Details = "Changed Applicants RegNum and AppNum from " + oldRegNum + ", " + oldAppNum + " to " + regNum + ", " + appNum,
                TimeStamp = localTime,
                UserRole = userRole.First(),
                UserIp = IUtilityService.GetIp()
            };
            _auditTrailRepository.SaveAuditTrail(auditTrail);

            TempData["AppUpdated"] = "Success";
            return RedirectToAction("Index");
        }

        public ActionResult Delete(long appId)
        {
            var application = _registrationService.GetApplicationDetails(appId);
            var regNum = application.RegNum;
            var userName = application.UserName;
            _registrationService.DeleteApplication(application);

            var IUtilityService = EngineContext.Resolve<IUtilityService>();
            var userRole = UserManager.GetRoles(User.Identity.GetUserId());
            var localTime = _configurationService.GetCurrentWestAfricanDateTime();
            var auditTrail = new AuditTrail()
            {
                UserId = User.Identity.GetUserId(),
                Username = User.Identity.GetUserName(),
                AuditActionId = Convert.ToInt32(AuditTrailActions.ApplicantRegistration),
                Details = "Deleted Application with RegNum and UserName " + regNum + ", "+userName,
                TimeStamp = localTime,
                UserRole = userRole.First(),
                UserIp = IUtilityService.GetIp()
            };
            _auditTrailRepository.SaveAuditTrail(auditTrail);
            TempData["AppDeleted"] = "Success";
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