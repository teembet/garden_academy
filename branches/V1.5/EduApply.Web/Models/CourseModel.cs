using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Web;
using EduApply.Data.Entities;

namespace EduApply.Web.Models
{
    public class CourseModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
        public string Code { get; set; }
        public IEnumerable<ProgramModel> ProgramsForThisCourse { get; set; }
        public IEnumerable<ProgramModel> ProgramsNotForThisCourse { get; set; }
        [Required]
        [Display(Name = "Department")]
        public int DepartmentId { get; set; }
        public DepartmentModel Department;
        public IEnumerable<Department> Departments { get; set; }


    }

    public class CourseModelModification
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Code { get; set; }
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
        [Required]
        [Display(Name = "Department")]
        public int DepartmentId { get; set; }
        public int[] IdsToAdd { get; set; }
        public int[] IdsToDelete { get; set; }
    }
}