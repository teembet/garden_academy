using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EduApply.Web.Models
{
    public class ValidateAdmissionStatusModel
    {
        //[Display(Name = "Application Number")]
        //public string AppNum { get; set; }
        //[Display(Name = "Registration Number")]
        //public string RegNum { get; set; }

        [Required(ErrorMessage = "Enter your Registration number or Application number")]
        [Display(Name = "Application Number/Registration Number")]
        public string AppNumOrRegNum { get; set; }


        public string ProgramCode { get; set; }
        public string CourseName { get; set; }
    }
}