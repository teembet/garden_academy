using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EduApply.Data.Entities;
using EduApply.Logic.Interfaces;
using EduApply.Web.Models;

namespace EduApply.Web.Controllers
{
    [Authorize(Roles = "Admin, SchoolAdmin")]
    public class SearchController : Controller
    {
        private IConfigurationService _configurationService;
        private IApplicationFormRepository _appFormRepository;
        private ISearchRepository _searchRepository;
        private IRegistrationService _registrationService;
        public SearchController(IConfigurationService configurationService, IApplicationFormRepository appFormRepository, ISearchRepository searchRepository, IRegistrationService registrationService)
        {
            this._configurationService = configurationService;
            this._appFormRepository = appFormRepository;
            this._searchRepository = searchRepository;
            this._registrationService = registrationService;
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetApplicationFormsBySessionId(int? sessionId)
        {
            var sesId = sessionId == null ? -1 : Convert.ToInt32(sessionId);
           // var session = _configurationService.GetSession(sesId);
            var applicationForms = _appFormRepository.GetAppFormsBySessionId(sesId);
            var result = (from s in applicationForms
                          select new
                          {
                              id = s.Id,
                              name = s.Name
                          }).ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetFormTemplatesByFormId(int? formId)
        {
            var frmId = formId == null ? -1 : Convert.ToInt32(formId);
            var formTemplates = _appFormRepository.GetFormTemplatesByFormId(frmId);
            var result = (from s in formTemplates
                          select new
                          {
                              id = s.Id,
                              name = s.Name
                          }).ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetDepartmentsByFacultyId(int? facultyId)
        {
            var facId = facultyId == null ? -1 : Convert.ToInt32(facultyId);
            var departments = facId != -1 ? _configurationService.GetDepartments(facId).ToList() : _configurationService.GetDepartments();
            var result = (from s in departments
                          select new
                          {
                              id = s.Id,
                              name = s.Name
                          }).ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetCoursesByDepartmentId(int? departmentId)
        {
            var depId = departmentId == null ? -1 : Convert.ToInt32(departmentId);
            var courses = depId != -1 ? _configurationService.GetCoursesByDepId(depId) : _configurationService.GetCourses();
            var result = (from s in courses
                          select new
                          {
                              id = s.Id,
                              name = s.Name
                          }).ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetProgramsByCourseId(int? courseId)
        {
            var cosId = courseId == null ? -1 : Convert.ToInt32(courseId);
            var programs = cosId != -1 ? _configurationService.GetProgramsByCourseId(cosId) : _configurationService.GetPrograms();
            var result = (from s in programs
                          select new
                          {
                              id = s.Id,
                              name = s.Name,
                          }).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Index()
        {
            var searchModel = new SearchModel()
            {
                Sessions = _configurationService.GetSessions(),
                ApplicationForms = _appFormRepository.GetAppForms(),
                FormTemplates = _appFormRepository.GetFormTemplates(),
                Faculties = _configurationService.GetFaculties(),
                Departments = _configurationService.GetDepartments(),
                Courses = _configurationService.GetCourses(),
                Programs = _configurationService.GetPrograms(),
                Venues = _configurationService.GetVenues(),
                SearchResult = new List<SearchResult>()
            };
            return View(searchModel);
        }
        [HttpPost]
        public ActionResult Index(SearchResultQuery query)
        {
            var returnedResult = _searchRepository.GetSearchResult(query);
            var searchModel = new SearchModel()
            {
                SessionId = query.SessionId,
                Sessions = _configurationService.GetSessions(),
                FormId = query.FormId,
                ApplicationForms = _appFormRepository.GetAppForms(),
                FormTemplateId = query.FormTemplateId,
                FormTemplates = _appFormRepository.GetFormTemplates(),
                FacultyId = query.FacultyId,
                Faculties = _configurationService.GetFaculties(),
                DepartmentId = query.DepartmentId,
                Departments = _configurationService.GetDepartments(),
                CourseOfStudyId = query.CourseOfStudyId,
                Courses = _configurationService.GetCourses(),
                ProgramId = query.ProgramId,
                Programs = _configurationService.GetPrograms(),
                VenueId = query.VenueId,
                Venues = _configurationService.GetVenues(),
                IsPaid = query.IsPaid,
                IsAdmitted = query.IsAdmitted,
                IsSubmitted = query.IsSubmitted,
                Name = query.Name,
                StartDate = query.StartDate,
                EndDate = query.EndDate,
                DateType = query.DateType,
                SearchResult = returnedResult
            };
            Session["searchModel"] = searchModel;
            return View(searchModel);
        }
        public ActionResult ResetApplication(long appId)
        {
            var application = _registrationService.GetApplicationDetails(appId);
            application.IsSubmitted = false;
            _registrationService.SaveApplication(application);
            return RedirectToAction("SearchErrorHandler", new {errorMessage = "RESET SUCCESSFUL"});
        }
        public ActionResult SearchErrorHandler(string errorMessage)
        {
            var searchModel = Session["searchModel"] as SearchModel;
            TempData["ErrorMessage"] = errorMessage;
            return View("Index", searchModel);
        }
    }
}