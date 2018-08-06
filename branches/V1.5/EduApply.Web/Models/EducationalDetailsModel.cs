﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using EduApply.Data.Entities;

namespace EduApply.Web.Models
{
    public class EducationalDetailsModel
    {
        [Required]
        [Display(Name = "School Name")]
        public string SchoolName { get; set; }
        [Required]
        public string Qualification { get; set; }
        [Required]
        [Display(Name = "Class of Degree")]
        public string ClassOfDegree { get; set; }
        //public decimal CGPA { get; set; }
        [Required]
        [Range(1960, 2015, ErrorMessage = "Accepted range is between 1960 and 2015")]
        [Display(Name = "Entry Year")]
        public int? EntryYear { get; set; }
        [Required]
        [Range(1960, 2015, ErrorMessage = "Accepted range is between 1960 and 2015")]
        [Display(Name = "Graduation Year")]
        public int? GraduationYear { get; set; }

        public IEnumerable<ClassOfDegree> Degrees; 
        public long ApplicationId { get; set; }
    }
}