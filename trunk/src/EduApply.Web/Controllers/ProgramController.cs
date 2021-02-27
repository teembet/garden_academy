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
    public class ProgramController : Controller
    {
        public const string Success = "Success";
        private IConfigurationService _config;
        private IAuditTrailRepository _auditTrailRepository;
        public ProgramController(IConfigurationService config, IAuditTrailRepository auditTrailRepository)
        {
            this._config = config;
            this._auditTrailRepository = auditTrailRepository;
        }
        // GET: Program
        public ActionResult Index()
        {
            var programs = _config.GetPrograms().OrderBy(x=>x.Name);
            var model = Mapper.Map<IEnumerable<Program>, IEnumerable<ProgramModel>>(programs);
            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            var program = new Program();
            program.CoursesNotInProgram = _config.GetCourses().OrderBy(x=>x.Name);
            var model = Mapper.Map<Program, ProgramModel>(program);
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(ProgramModelModification _program)
        {
            try
            {
                var programs = _config.GetPrograms(_program.Name);
                if (programs.Any())
                {
                    AddModelError("Program name entered has already been used for another program");
                    var programm = new Program();
                    programm.CoursesNotInProgram = _config.GetCourses().OrderBy(x=>x.Name);
                    var model = Mapper.Map<Program, ProgramModel>(programm);
                    return View(model);
                }
                if (!string.IsNullOrEmpty(_program.Code))
                {
                    var programsByCode = _config.GetProgramsByCode(_program.Code);
                    if (programsByCode.Any())
                    {
                        AddModelError("A Program with the code entered already exist");
                        var programm = new Program();
                        programm.CoursesNotInProgram = _config.GetCourses().OrderBy(x=>x.Name);
                        var model = Mapper.Map<Program, ProgramModel>(programm);
                        return View(model);
                    }
                }

                var program = new Program()
                                          {
                                              Name = _program.Name,
                                              Code = _program.Code,
                                              IsActive = _program.IsActive,
                                          };
                var IUtilityService = EngineContext.Resolve<IUtilityService>();
                var userRole = UserManager.GetRoles(User.Identity.GetUserId());
                _config.SaveProgram(program);
                var localTime = _config.GetCurrentWestAfricanDateTime();
                var auditTrail = new AuditTrail()
                {
                    UserId = User.Identity.GetUserId(),
                    Username = User.Identity.GetUserName(),
                    AuditActionId = Convert.ToInt32(AuditTrailActions.AddProgram),
                    Details = "created Program \'" + program.Name + "\'",
                    TimeStamp = localTime,
                    UserRole = userRole.First(),
                    UserIp = IUtilityService.GetIp()
                };
                _auditTrailRepository.SaveAuditTrail(auditTrail);
                foreach (var id in _program.IdsToAdd ?? new int[] { })
                {
                    var pc = new ProgramCourse()
                    {
                        ProgramId = program.Id,
                        CourseId = id
                    };
                    _config.SaveProgramCourse(pc);

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
        public ActionResult Edit(int programId)
        {
            var program = _config.GetProgram(programId);
            var idzOfcoursesForProgram = _config.GetProgramCoursesByProgramId(program.Id).Select(x => x.CourseId).ToList();
            var coursesForThisProgram = _config.GetCourses().Where(x => idzOfcoursesForProgram.Contains(x.Id)).OrderBy(x=>x.Name).ToList();
            var coursesNotForThisProgram = _config.GetCourses().Except(coursesForThisProgram).OrderBy(x=>x.Name).ToList();

            program.CoursesInProgram = coursesForThisProgram;
            program.CoursesNotInProgram = coursesNotForThisProgram;

            var model = Mapper.Map<Program, ProgramModel>(program);
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(ProgramModelModification _program)
        {
            try
            {
                //check if user is changing program name to name of an existing program
                var programs = _config.GetPrograms(_program.Name).Where(x => x.Id != _program.Id).ToList();
                if (programs.Any())
                {
                    //get current configurations for program to pass back to view
                    AddModelError("Program name entered has already been used for another program");
                    var programModel = _config.GetProgram(_program.Id);
                    var idzOfcoursesForProgram = _config.GetProgramCoursesByProgramId(programModel.Id).Select(x => x.CourseId).ToList();
                    var coursesForThisProgram = _config.GetCourses().Where(x => idzOfcoursesForProgram.Contains(x.Id)).OrderBy(x=>x.Name).ToList();
                    var coursesNotForThisProgram = _config.GetCourses().Except(coursesForThisProgram).OrderBy(x=>x.Name).ToList();

                    programModel.CoursesInProgram = coursesForThisProgram;
                    programModel.CoursesNotInProgram = coursesNotForThisProgram;

                    var model = Mapper.Map<Program, ProgramModel>(programModel);
                    return View(model);
                }

                //check if user is changing program name to name of an existing program
                var programsByCode = _config.GetProgramsByCode(_program.Code).Where(x => x.Id != _program.Id).ToList();
                if (programsByCode.Any())
                {
                    //get current configurations for program to pass back to view
                    AddModelError("Code entered for this Program has already been taken by another Program");
                    var programModel = _config.GetProgram(_program.Id);
                    var idzOfcoursesForProgram = _config.GetProgramCoursesByProgramId(programModel.Id).Select(x => x.CourseId).ToList();
                    var coursesForThisProgram = _config.GetCourses().Where(x => idzOfcoursesForProgram.Contains(x.Id)).OrderBy(x=>x.Name).ToList();
                    var coursesNotForThisProgram = _config.GetCourses().Except(coursesForThisProgram).OrderBy(x => x.Name).ToList();

                    programModel.CoursesInProgram = coursesForThisProgram;
                    programModel.CoursesNotInProgram = coursesNotForThisProgram;

                    var model = Mapper.Map<Program, ProgramModel>(programModel);
                    return View(model);
                }

                var program = _config.GetProgram(_program.Id);
                program.Name = _program.Name;
                program.Code = _program.Code;
                program.IsActive = _program.IsActive;
                _config.SaveProgram(program);


                foreach (var id in _program.IdsToAdd ?? new int[] { })
                {
                    var pc = new ProgramCourse()
                    {
                        ProgramId = program.Id,
                        CourseId = id
                    };
                    _config.SaveProgramCourse(pc);
                }


                foreach (var id in _program.IdsToDelete ?? new int[] { })
                {
                    var pc = _config.GetProgramCourseByCourseIdAndProgramId(program.Id, id);

                    _config.DeleteProgramCourse(pc);
                }

                var IUtilityService = EngineContext.Resolve<IUtilityService>();
                TempData["Edited"] = Success;
                var userRole = UserManager.GetRoles(User.Identity.GetUserId());
                var localTime = _config.GetCurrentWestAfricanDateTime();
                var auditTrail = new AuditTrail()
                {
                    UserId = User.Identity.GetUserId(),
                    Username = User.Identity.GetUserName(),
                    AuditActionId = Convert.ToInt32(AuditTrailActions.AddProgram),
                    Details = "Edited Program \'" + program.Name + "\'",
                    TimeStamp = localTime,
                    UserRole = userRole.First(),
                    UserIp = IUtilityService.GetIp()
                };
                _auditTrailRepository.SaveAuditTrail(auditTrail);
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

