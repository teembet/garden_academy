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
    public class DepartmentController : Controller
    {
        public const string Success = "Success";
        private IConfigurationService _config;
        private IAuditTrailRepository _auditTrailRepository;

        public DepartmentController(IConfigurationService config, IAuditTrailRepository auditTrailRepository)
        {
            this._config = config;
            this._auditTrailRepository = auditTrailRepository;
        }
        //
        // GET: /Department/
        public ActionResult Index()
        {
            var departments = _config.GetDepartments().OrderBy(x => x.Name);
            var departmentModel = Mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentModel>>(departments);
            return View(departmentModel);
        }
        [HttpGet]
        public ActionResult Create()
        {
            var model = new DepartmentModel();
            model.Faculties = _config.GetFaculties();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(Department department)
        {
            var departments = _config.GetDepartments(department.Name);
            if (departments.Any())
            {
                ModelState.AddModelError("", "A department with the name entered already exist");
                var model = new DepartmentModel();
                model.Faculties = _config.GetFaculties();
                return View(model);
            }
            var departmentsByCode = _config.GetDepartmentsByCode(department.Code);
            if (departmentsByCode.Any())
            {
                ModelState.AddModelError("", "A department with the code entered already exist");
                var model = new DepartmentModel();
                model.Faculties = _config.GetFaculties();
                return View(model);
            }
            // department.FacultyName = _config.GetFaculty(department.FacultyId).Name;
            _config.SaveDepartment(department);
            TempData["Created"] = Success;
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int departmentId)
        {
            var department = _config.GetDepartment(departmentId);
            var model = Mapper.Map<Department, DepartmentModel>(department);
            model.Faculties = _config.GetFaculties();
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(Department department)
        {
            var departments = _config.GetDepartments(department.Name).Where(x => x.Id != department.Id).ToList();
            if (departments.Any())
            {
                ModelState.AddModelError("", "A Department with the name entered already exist");
                var model = Mapper.Map<Department, DepartmentModel>(department);
                model.Faculties = _config.GetFaculties();
                return View(model);
            }
            var departmentsByCode = _config.GetDepartmentsByCode(department.Code).Where(x => x.Id != department.Id).ToList();
            if (departmentsByCode.Any())
            {
                ModelState.AddModelError("", "A Department with the code entered already exist");
                var model = Mapper.Map<Department, DepartmentModel>(department);
                model.Faculties = _config.GetFaculties();
                return View(model);
            }
            var departmentToUpdate = _config.GetDepartment(department.Id);
            departmentToUpdate.Name = department.Name;
            departmentToUpdate.Code = department.Code;
            departmentToUpdate.FacultyId = department.FacultyId;
            //departmentToUpdate.FacultyName = _config.GetFaculty(department.FacultyId).Name;
            _config.SaveDepartment(departmentToUpdate);
            TempData["Edited"] = Success;
            return RedirectToAction("Index");
        }
    }
}