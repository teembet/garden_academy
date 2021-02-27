using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EduApply.Data.Entities;
using EduApply.Logic.Interfaces;
using EduApply.Web.Models;
using iTextSharp.text;
using Microsoft.AspNet.Identity.Owin;

namespace EduApply.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class VenueMappingController : Controller
    {
        public const string Success = "Success";
        private IApplicationFormRepository _applicationFormRepository;
        private IConfigurationService _configurationService;
        private IAuditTrailRepository _auditTrailRepository;
        private IVenueAssignmentService _venueService;
        public VenueMappingController(IApplicationFormRepository applicationFormRepository,
             IVenueAssignmentService venueService, 
            IConfigurationService configurationService, IAuditTrailRepository auditTrailRepository)
        {
            this._applicationFormRepository = applicationFormRepository;
            this._configurationService = configurationService;
            this._venueService = venueService;

            this._auditTrailRepository = auditTrailRepository;
        }


        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetDepartmentsByFacultyId(int? facultyId)
        {
            var facId = facultyId == null ? -1 : Convert.ToInt32(facultyId);
            var departments = _configurationService.GetDepartments(facId).OrderBy(x => x.Name).ToList();
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
            var courses = _configurationService.GetCoursesByDepId(depId).OrderBy(x => x.Name);
            var result = (from s in courses
                          select new
                          {
                              id = s.Id,
                              name = s.Name
                          }).ToList();
            result.Insert(0, new { id = -1, name = "All" });
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetProgramsByCourseId(int? courseId)
        {
            var cosId = courseId == null ? -1 : Convert.ToInt32(courseId);
            var programs = _configurationService.GetProgramsByCourseId(cosId).OrderBy(x => x.Code);
            var result = (from s in programs
                          select new
                          {
                              id = s.Id,
                              name = s.Code,
                          }).ToList();
            result.Insert(0, new { id = -1, name = "All" });
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LoadVenues(int formId, int facultyId, int departmentId, int programId, int courseId)
        {
            var venueMapping = _venueService.GetVenueMappings(formId, courseId, programId).ToList();
            var venueIdsForSelection = venueMapping.Select(x => x.ExamVenueId).ToArray();
            var model = new VenueMappingsModel()
            {
                ApplicationForms = _applicationFormRepository.GetAppForms().OrderBy(x => x.Name),
                Faculties = _configurationService.GetFaculties(),
                Departments = _configurationService.GetDepartments(facultyId),
                Courses = _configurationService.GetCoursesByDepId(departmentId).ToList(),
                Programs = _configurationService.GetProgramsByCourseId(courseId).ToList(),
                ExamVenues = _venueService.GetExamVenues().OrderBy(x => x.ExamDate),
                FacultyId = facultyId,
                DepartmentId = departmentId,
                ProgramId = programId,
                CourseOfStudyId = courseId,
                FormId = formId,
                IsActive = venueMapping.Count > 0 ? venueMapping.FirstOrDefault().IsActive : false,
                ExamVenueIdz = venueIdsForSelection
            };

            model.Courses.Insert(0, new Course() { Id = -1, Name = "All" });
            model.Programs.Insert(0, new Program() { Id = -1, Name = "All", Code = "All" });

            return View("Mapper", model);
        }
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Mapper()
        {
            var model = new VenueMappingsModel()
            {
                ApplicationForms = _applicationFormRepository.GetAppForms().OrderBy(x => x.Name),
                Faculties = _configurationService.GetFaculties().OrderBy(x => x.Name),
                Departments = new List<Department>(), //_configurationService.GetDepartments(),
                Courses = new List<Course>(), //_configurationService.GetCourses(),
                Programs = new List<Program>(), //_configurationService.GetPrograms(),
                ExamVenues = _venueService.GetExamVenues().Where(x => x.IsActive).OrderBy(x=>x.ExamDate),
                // Venues = _configurationService.GetVenues(),
                // Venues = new List<Venues>()
            };
            return View(model);
        }
        [HttpPost]
        public ActionResult Mapper(VenueMappingsModificationModel model)
        {
            if (model != null)
            {
                #region allProgramsAndCourses
                if (model.CourseOfStudyId == -1 && model.ProgramId == -1)
                {
                    var courses = _configurationService.GetCourses();
                    foreach (var course in courses)
                    {
                        //get all the program for course
                        var programsForCourse = _configurationService.GetProgramsByCourseId(course.Id);
                        foreach (var program in programsForCourse)
                        {
                            var savedVenueMapping = _venueService.GetVenueMappings(model.FormId, course.Id, program.Id).ToList();
                            var examVenueIds = savedVenueMapping.Select(x => x.ExamVenueId).ToList();
                            foreach (var item in model.MappedExamVenue)
                            {
                                if (examVenueIds.Contains(item))
                                {
                                    //then this is an update do not add a new entry to db
                                    var venueMapping = _venueService.GetVenueMappings(model.FormId, course.Id, program.Id, item);
                                    venueMapping.IsActive = model.IsActive;
                                    _venueService.SaveVenueMapping(venueMapping);
                                }
                                else
                                {
                                    var venueMapping = new VenueMappings()
                                    {
                                        ExamVenueId = item,
                                        FormId = model.FormId,
                                        CourseOfStudyId = course.Id,
                                        ProgramId = program.Id,
                                        IsActive = model.IsActive,
                                    };
                                    _venueService.SaveVenueMapping(venueMapping);
                                }
                            }
                            //delete unchecked venues
                            var idzToDelete = examVenueIds.Except(model.MappedExamVenue);
                            foreach (var item in idzToDelete)
                            {
                                var venueMapping = _venueService.GetVenueMappings(model.FormId, course.Id, program.Id, item);
                                _venueService.DeleteVenueMapping(venueMapping);
                            }
                            TempData["Created"] = Success;
                        }
                    }
                }
                #endregion

                #region ACourseAndAllTheProgramsItBelongsTo

                if (model.CourseOfStudyId > 0 && model.ProgramId == -1)
                {
                    //get all the programs for the selected course
                    var programsForSelectedCourse = _configurationService.GetProgramsByCourseId(model.CourseOfStudyId);
                    foreach (var program in programsForSelectedCourse)
                    {
                        var savedVenueMapping = _venueService.GetVenueMappings(model.FormId, model.CourseOfStudyId, program.Id).ToList();
                        var examVenueIds = savedVenueMapping.Select(x => x.ExamVenueId).ToList();
                        foreach (var item in model.MappedExamVenue)
                        {
                            if (examVenueIds.Contains(item))
                            {
                                //then this is an update do not add a new entry to db
                                var venueMapping = _venueService.GetVenueMappings(model.FormId, model.CourseOfStudyId, program.Id, item);
                                venueMapping.IsActive = model.IsActive;
                                _venueService.SaveVenueMapping(venueMapping);
                            }
                            else
                            {
                                var venueMapping = new VenueMappings()
                                {
                                    ExamVenueId = item,
                                    FormId = model.FormId,
                                    CourseOfStudyId = model.CourseOfStudyId,
                                    ProgramId = program.Id,
                                    IsActive = model.IsActive,
                                };
                                _venueService.SaveVenueMapping(venueMapping);
                            }
                        }
                        //delete unchecked venues
                        var idzToDelete = examVenueIds.Except(model.MappedExamVenue);
                        foreach (var item in idzToDelete)
                        {
                            var venueMapping = _venueService.GetVenueMappings(model.FormId, model.CourseOfStudyId, program.Id, item);
                            _venueService.DeleteVenueMapping(venueMapping);
                        }
                        TempData["Created"] = Success;
                    }

                }
                #endregion

                #region OneCourseAndOneProgram
                if (model.CourseOfStudyId > 0 && model.ProgramId > 0)
                {
                    var savedVenueMapping = _venueService.GetVenueMappings(model.FormId, model.CourseOfStudyId, model.ProgramId).ToList();
                    var examVenueIds = savedVenueMapping.Select(x => x.ExamVenueId).ToList();
                    foreach (var item in model.MappedExamVenue)
                    {
                        if (examVenueIds.Contains(item))
                        {
                            //then this is an update do not add a new entry to db
                            var venueMapping = _venueService.GetVenueMappings(model.FormId, model.CourseOfStudyId, model.ProgramId, item);
                            venueMapping.IsActive = model.IsActive;
                            _venueService.SaveVenueMapping(venueMapping);
                        }
                        else
                        {
                            var venueMapping = new VenueMappings()
                            {
                                ExamVenueId = item,
                                FormId = model.FormId,
                                CourseOfStudyId = model.CourseOfStudyId,
                                ProgramId = model.ProgramId,
                                IsActive = model.IsActive,
                            };
                            _venueService.SaveVenueMapping(venueMapping);
                        }
                    }
                    //delete unchecked venues
                    var idzToDelete = examVenueIds.Except(model.MappedExamVenue);
                    foreach (var item in idzToDelete)
                    {
                        var venueMapping = _venueService.GetVenueMappings(model.FormId, model.CourseOfStudyId, model.ProgramId, item);
                        _venueService.DeleteVenueMapping(venueMapping);
                    }
                    TempData["Created"] = Success;
                }
                #endregion

            }
            return RedirectToAction("LoadVenues", new { formId = model.FormId, facultyId = model.FacultyId, departmentId = model.DepartmentId, programId = model.ProgramId, courseId = model.CourseOfStudyId });
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