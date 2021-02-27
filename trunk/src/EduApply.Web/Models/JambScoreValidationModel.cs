using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EduApply.Web.Models
{
    public class JambScoreValidationModel
    {
        public string RegNum { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string CourseOfStudy { get; set; }
        public int EngScore { get; set; }
        [Required]
        public string Subject2 { get; set; }
        [Required]
        public int Subject2Score { get; set; }
        [Required]
        public string Subject3 { get; set; }
        [Required]
        public int Subject3Score { get; set; }
        [Required]
        public string Subject4 { get; set; }
        [Required]
        public int Subject4Score { get; set; }
        [Required]
        [Display(Name = "Total Score")]
        public int TotalScore { get; set; }
    }
}