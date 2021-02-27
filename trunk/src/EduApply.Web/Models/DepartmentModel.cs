using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using EduApply.Data.Entities;

namespace EduApply.Web.Models
{
    public class DepartmentModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        [Display(Name = "Faculty")]
        public int FacultyId { get; set; }
        public virtual FacultyModel Faculty { get; set; }
        public IEnumerable<Faculty> Faculties { get; set; }
    }
}