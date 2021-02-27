using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using EduApply.Data.Entities;
using NPOI.SS.Formula.Functions;

namespace EduApply.Web.Models
{
    public class ManualJambBreakDownModel
    {
        public long Id { get; set; }
        [Required]
        [StringLength(10, ErrorMessage = "RegNum must be 10 characters long", MinimumLength = 10)]
        [Display(Name = "Registration Number")]
        public string RegNum { get; set; }
        //public string CourseOfStudy { get; set; }
        [Required]
        [Range(0, 100, ErrorMessage = "Score can only be between 0 and 100")]
        [Display(Name = "English Score")]
        public int? EngScore { get; set; }
        [Required]
        [Display(Name = "Subject 2")]
        public string Subject2 { get; set; }
        [Required]
        [Range(0, 100, ErrorMessage = "Score can only be between 0 and 100")]
        [Display(Name = "Score")]
        public int? Subject2Score { get; set; }
        [Required]
        [Display(Name = "Subject 3")]
        public string Subject3 { get; set; }
        [Required]
        [Range(0, 100, ErrorMessage = "Score can only be between 0 and 100")]
        [Display(Name = "Score")]
        public int? Subject3Score { get; set; }
        [Required]
        [Display(Name = "Subject 4")]
        public string Subject4 { get; set; }
        [Required]
        [Range(0, 100, ErrorMessage = "Score can only be between 0 and 100")]
        [Display(Name = "Score")]
        public int? Subject4Score { get; set; }
          [Display(Name = "Total Score")]
        public int? TotalScore { get; set; }
        public IEnumerable<OLevelSubject> Subjects { get; set; }
    }
}