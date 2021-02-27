using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EduApply.Data.Entities;

namespace EduApply.Web.Models
{
    public class ApplicantProgramCourseCollection
    {
        public List<ApplicantsProgramCourse> ApplicantsProgramCourses { get; set; }
        public int MaxEntry { get; set; }
    }

    public class AppProgramCourseModificationModel
    {
        public int[] ProgramId { get; set; }
        public int[] CourseId { get; set; }
        public int[] DepartmentId { get; set; }
    }
}