using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EduApply.Web.Models
{
    public class ApplicantsProgramCourseModel
    {
        public long Id { get; set; }
        public long ApplicationId { get; set; }
        [Required]
        [Display(Name = "Program")]
        public int ProgramId { get; set; }
        [Required]
        [Display(Name = "Course of Study")]
        public int CourseId { get; set; }
        public IEnumerable<ProgramModel> Programs { get; set; }
        public IEnumerable<CourseModel> Courses { get; set; }
    }
}