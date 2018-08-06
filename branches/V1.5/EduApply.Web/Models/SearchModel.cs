using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using EduApply.Data.Entities;

namespace EduApply.Web.Models
{
    public class SearchModel
    {
        [Display(Name = "Session")]
        public int? SessionId { get; set; }

        [Display(Name = "Application Form Type")]
        public int? FormId { get; set; }

        [Display(Name = "Form Template")]
        public int? FormTemplateId { get; set; }

        [Display(Name = "Faculty")]
        public int? FacultyId { get; set; }

        [Display(Name = "Department")]
        public int? DepartmentId { get; set; }

        [Display(Name = "Course of study")]
        public int? CourseOfStudyId { get; set; }

        [Display(Name = "Program")]
        public int? ProgramId { get; set; }

        [Display(Name = "Venue")]
        public int? VenueId { get; set; }

        [Display(Name = "Payment Status")]
        public bool? IsPaid { get; set; }

        [Display(Name = "Application Status")]
        public bool? IsSubmitted { get; set; }

        [Display(Name = "Admission Status")]
        public bool? IsAdmitted { get; set; }

        [Display(Name = "Start Date")]
        public DateTime? StartDate { get; set; }

        [Display(Name = "End Date")]
        public DateTime? EndDate { get; set; }

        public string Name { get; set; }
        [Display(Name = "Application Number")]
        public string AppNo { get; set; }
        [Display(Name = "Registration Number")]
        public string RegNo { get; set; }
        [Display(Name = "Date Type")]
        public string DateType { get; set; }
        public IEnumerable<FormTemplate> FormTemplates { get; set; }
        public IEnumerable<Session> Sessions { get; set; }
        public IEnumerable<ApplicationForm> ApplicationForms { get; set; }
        public IEnumerable<Faculty> Faculties { get; set; }
        public IEnumerable<Department> Departments { get; set; }
        public IEnumerable<Course> Courses { get; set; }
        public IEnumerable<Program> Programs { get; set; }
        public IEnumerable<Venues> Venues { get; set; }
        public IEnumerable<SearchResult> SearchResult { get; set; }

    }
}