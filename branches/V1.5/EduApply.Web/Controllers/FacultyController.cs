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
     [Authorize(Roles = "Admin")]
    public class FacultyController : Controller
    {
        public const string Success = "Success";
        private IConfigurationService _config;
        private IAuditTrailRepository _auditTrailRepository;
        public FacultyController(IConfigurationService config, IAuditTrailRepository auditTrailRepository)
        {
            this._config = config;
            this._auditTrailRepository = auditTrailRepository;
        }
        //
        // GET: /Faculty/
        public ActionResult Index()
        {
            var faculties = _config.GetFaculties().OrderBy(x=>x.Name);
            return View(faculties);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Faculty faculty)
        {
            var faculties = _config.GetFaculties(faculty.Name);
            if (faculties.Any())
            {
                ModelState.AddModelError("", "Faculty already exists");
                return View();
            }
            _config.SaveFaculty(faculty);
            TempData["Created"] = Success;
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int facultyId)
        {
            var faculty = _config.GetFaculty(facultyId);
            var model = Mapper.Map<Faculty, FacultyModel>(faculty);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(Faculty faculty)
        {
            var faculties = _config.GetFaculties(faculty.Name).Where(x => x.Id != faculty.Id).ToList();
            if (faculties.Any())
            {
                ModelState.AddModelError("", "Faculty already exists");
                var model = Mapper.Map<Faculty, FacultyModel>(faculty);
                return View(model);
            }
            var facultyToUpdate = _config.GetFaculty(faculty.Id);
            facultyToUpdate.Name = faculty.Name;
            _config.SaveFaculty(facultyToUpdate);
            TempData["Edited"] = Success;
            return RedirectToAction("Index");
        }
    }
}