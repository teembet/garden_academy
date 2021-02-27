using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EduApply.Web.Models
{
    public class ProgramModel
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Program Name")]
        public string Name { get; set; }
         [Display(Name = "Program Code")]
        public string Code { get; set; }
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        public IEnumerable<CourseModel> CoursesInProgram { get; set; }
        public IEnumerable<CourseModel> CoursesNotInProgram { get; set; }
    }

    public class ProgramModelModification
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Code { get; set; }
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
        [Display(Name = "Is Post Degree")]
        public int[] IdsToAdd { get; set; }
        public int[] IdsToDelete { get; set; }
    }
}