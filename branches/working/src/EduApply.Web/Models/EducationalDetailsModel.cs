using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using EduApply.Data.Entities;

namespace EduApply.Web.Models
{
    public class EducationalDetailsModel
    {
        public long Id { get; set; }
        [Required]
        [Display(Name = "School Name")]
        public string SchoolName { get; set; }
        [Required]
        public string Qualification { get; set; }
        [Required]
        [Display(Name = "Class of Degree")]
        public string ClassOfDegree { get; set; }
        [Required]
        [Display(Name = "CGPA")]
        [Range(0.000,5.000, ErrorMessage="Accepted range is between 0.00 and 5.00)")]
        public string CGPA { get; set; }
        [Required]
        [Range(1960, 2015, ErrorMessage = "Accepted range is between 1960 and 2015")]
        [Display(Name = "Entry Year")]
        public int? EntryYear { get; set; }
        [Display(Name = "Graduation Month")]
        public string GraduationMonth { get; set; }
        [Required]
        [Range(1960, 2015, ErrorMessage = "Accepted range is between 1960 and 2015")]
        [Display(Name = "Graduation Year")]
        public int? GraduationYear { get; set; }

        public IEnumerable<ClassOfDegree> Degrees; 
        public long ApplicationId { get; set; }
    }
}