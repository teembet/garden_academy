using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using EduApply.Data.Entities;

namespace EduApply.Web.Models
{
    public class VenueMappingsModel
    {
        [Required(ErrorMessage = "Select Application Form Type")]
        [Display(Name = "Application Form Type")]
        public int FormId { get; set; }

        [Required(ErrorMessage = "Select Faculty")]
        [Display(Name = "Faculty")]
        public int FacultyId { get; set; }

        [Required(ErrorMessage = "Select Department")]
        [Display(Name = "Department")]
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "Select Course of Study")]
        [Display(Name = "Course of study")]
        public int CourseOfStudyId { get; set; }

        [Required(ErrorMessage = "Select Program")]
        [Display(Name = "Program")]
        public int ProgramId { get; set; }
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
        [Display(Name = "Exam Date")]
        [Required]
        public int[] ExamVenueIdz { get; set; }

        public IEnumerable<ApplicationForm> ApplicationForms { get; set; }
        public List<Program> Programs { get; set; }
        public List<Course> Courses { get; set; }
        public IEnumerable<Faculty> Faculties { get; set; }
        public IEnumerable<Department> Departments { get; set; }
        public IEnumerable<ExamVenue> ExamVenues { get; set; }
    }

    public class VenueMappingsModificationModel
    {
        public int FormId { get; set; }
        public int FacultyId { get; set; }
        public int DepartmentId { get; set; }
        public int CourseOfStudyId { get; set; }
        public int ProgramId { get; set; }
        public bool IsActive { get; set; }
        public int[] MappedExamVenue { get; set; }
    }
}