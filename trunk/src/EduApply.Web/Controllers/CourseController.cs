using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using EduApply.Data.Entities;
using EduApply.Logic.Interfaces;
using EduApply.Logic.Service;
using EduApply.Logic.Utility;
using EduApply.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace EduApply.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CourseController : Controller
    {
        public const string Success = "Success";
        private IConfigurationService _config;
        private IAuditTrailRepository _auditTrailRepository;
        public CourseController(IConfigurationService config, IAuditTrailRepository auditTrailRepository)
        {
            this._config = config;
            this._auditTrailRepository = auditTrailRepository;
        }
        // GET: Course
        public ActionResult Index()
        {
            var courses = _config.GetCourses().OrderBy(x => x.Name);
            var model = Mapper.Map<IEnumerable<Course>, IEnumerable<CourseModel>>(courses);
            //model.Program = Mapper.Map<IEnumerable<Program>, IEnumerable<ProgramModel>>(programs);
            return View(model);
        }

        public ActionResult Create()
        {
            var course = new Course();
            course.ProgramsNotForThisCourse = _config.GetPrograms();
            var model = Mapper.Map<Course, CourseModel>(course);
            model.Departments = _config.GetDepartments();
            return View(model);
        }
        [HttpPost]
        public ActionResult Create(CourseModelModification _course)
        {
            try
            {
                var courses = _config.GetCourses(_course.Name);
                if (courses.Any())
                {
                    AddModelError("A Course with the name entered already exist");
                    var cours = new Course();
                    cours.ProgramsNotForThisCourse = _config.GetPrograms().OrderBy(x=>x.Name);
                    var model = Mapper.Map<Course, CourseModel>(cours);
                    model.Departments = _config.GetDepartments().OrderBy(x=>x.Name);
                    return View(model);
                }

                var coursesByCode = _config.GetCoursesByCode(_course.Code);
                if (coursesByCode.Any())
                {
                    AddModelError("A Course with the code entered already exist");
                    var cours = new Course();
                    cours.ProgramsNotForThisCourse = _config.GetPrograms().OrderBy(x=>x.Name);
                    var model = Mapper.Map<Course, CourseModel>(cours);
                    model.Departments = _config.GetDepartments().OrderBy(x=>x.Name);
                    return View(model);
                }
                var course = new Course()
                      {
                          Name = _course.Name,
                          IsActive = _course.IsActive,
                          Code = _course.Code,
                          DepartmentId = _course.DepartmentId,
                          //DepartmentName = _config.GetDepartment(_course.DepartmentId).Name
                      };
                _config.SaveCourse(course);
                var IUtilityService = EngineContext.Resolve<IUtilityService>();
                var userRole = UserManager.GetRoles(User.Identity.GetUserId());
                var localTime = _config.GetCurrentWestAfricanDateTime();
                var auditTrail = new AuditTrail()
                {
                    UserId = User.Identity.GetUserId(),
                    Username = User.Identity.GetUserName(),
                    AuditActionId = Convert.ToInt32(AuditTrailActions.AddCourse),
                    Details = "created Course \'" + course.Name + "\'",
                    TimeStamp = localTime,
                    UserRole = userRole.First(),
                    UserIp = IUtilityService.GetIp()
                };
                _auditTrailRepository.SaveAuditTrail(auditTrail);


                foreach (var id in _course.IdsToAdd ?? new int[] { })
                {
                    var programcourse = new ProgramCourse()
                    {
                        ProgramId = id,
                        CourseId = course.Id
                    };
                    _config.SaveProgramCourse(programcourse);

                }
                TempData["Created"] = Success;
                return RedirectToAction("Index");



            }
            catch (Exception)
            {
                TempData["Created"] = "Failed";
                throw;
            }

        }
        [HttpGet]
        public ActionResult Edit(int courseId)
        {
            var course = _config.GetCourse(courseId);
            var idsOfProgramsForThisCourse = _config.GetProgramCoursesByCourseId(course.Id).Select(x => x.ProgramId).ToList();
            var programsForThisCourse = _config.GetPrograms().Where(p => idsOfProgramsForThisCourse.Contains(p.Id)).OrderBy(x=>x.Name).ToList();
            var programsNotForThisCourse = _config.GetPrograms().Except(programsForThisCourse).OrderBy(x=>x.Name).ToList();
            course.ProgramsForThisCourse = programsForThisCourse;
            course.ProgramsNotForThisCourse = programsNotForThisCourse;

            var model = Mapper.Map<Course, CourseModel>(course);
            model.Departments = _config.GetDepartments();
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(CourseModelModification _course)
        {
            try
            {
                var courses = _config.GetCourses(_course.Name).Where(x => x.Id != _course.Id).ToList();
                if (courses.Any())
                {
                    AddModelError("name entered for this course has already been taken by another course");
                    //the rest of the code below within this if, is to return the form to the way it was before the edit
                    var courseMoodel = _config.GetCourse(_course.Id);
                    var idsOfProgramsForThisCourse = _config.GetProgramCoursesByCourseId(courseMoodel.Id).Select(x => x.ProgramId).ToList();
                    var programsForThisCourse = _config.GetPrograms().Where(p => idsOfProgramsForThisCourse.Contains(p.Id)).OrderBy(x=>x.Name).ToList();
                    var programsNotForThisCourse = _config.GetPrograms().Except(programsForThisCourse).OrderBy(x=>x.Name).ToList();
                    courseMoodel.ProgramsForThisCourse = programsForThisCourse;
                    courseMoodel.ProgramsNotForThisCourse = programsNotForThisCourse;

                    var model = Mapper.Map<Course, CourseModel>(courseMoodel);
                    model.Departments = _config.GetDepartments().OrderBy(x=>x.Name);
                    return View(model);
                }
                //check that course code does not belong to aother course
                var coursesByCode = _config.GetCoursesByCode(_course.Code).Where(x => x.Id != _course.Id).ToList();
                if (coursesByCode.Any())
                {
                    AddModelError("code entered for this course has already been taken by another course");
                    //the rest of the code below within this if, is to return the form to the way it was before the edit
                    var courseMoodel = _config.GetCourse(_course.Id);
                    var idsOfProgramsForThisCourse = _config.GetProgramCoursesByCourseId(courseMoodel.Id).Select(x => x.ProgramId).ToList();
                    var programsForThisCourse = _config.GetPrograms().Where(p => idsOfProgramsForThisCourse.Contains(p.Id)).OrderBy(x=>x.Name).ToList();
                    var programsNotForThisCourse = _config.GetPrograms().Except(programsForThisCourse).OrderBy(x=>x.Name).ToList();
                    courseMoodel.ProgramsForThisCourse = programsForThisCourse;
                    courseMoodel.ProgramsNotForThisCourse = programsNotForThisCourse;

                    var model = Mapper.Map<Course, CourseModel>(courseMoodel);
                    model.Departments = _config.GetDepartments().OrderBy(x=>x.Name);
                    return View(model);
                }


                var course = _config.GetCourse(_course.Id);
                course.Name = _course.Name;
                course.IsActive = _course.IsActive;
                course.Code = _course.Code;
                course.DepartmentId = _course.DepartmentId;
                //  course.DepartmentName = _config.GetDepartment(_course.DepartmentId).Name;
                _config.SaveCourse(course);

                foreach (var id in _course.IdsToAdd ?? new int[] { })
                {
                    var programCourse = new ProgramCourse()
                    {
                        ProgramId = id,
                        CourseId = course.Id
                    };
                    _config.SaveProgramCourse(programCourse);
                }
                foreach (var id in _course.IdsToDelete ?? new int[] { })
                {
                    var programCourse = _config.GetProgramCourseByCourseIdAndProgramId(id, course.Id);
                    _config.DeleteProgramCourse(programCourse);
                }
                var IUtilityService = EngineContext.Resolve<IUtilityService>();
                var userRole = UserManager.GetRoles(User.Identity.GetUserId());
                var localTime = _config.GetCurrentWestAfricanDateTime();
                var auditTrail = new AuditTrail()
                {
                    UserId = User.Identity.GetUserId(),
                    Username = User.Identity.GetUserName(),
                    AuditActionId = Convert.ToInt32(AuditTrailActions.AddCourse),
                    Details = "edited Course \'" + course.Name + "\'",
                    TimeStamp = localTime,
                    UserRole = userRole.First(),
                    UserIp = IUtilityService.GetIp()
                };
                _auditTrailRepository.SaveAuditTrail(auditTrail);

                TempData["Edited"] = Success;
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                TempData["Edited"] = "Failed";
                throw;
            }

        }

        public void AddModelError(string error)
        {
            ModelState.AddModelError("", error);
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