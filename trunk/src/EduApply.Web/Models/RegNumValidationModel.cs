using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EduApply.Web.Models
{
    public class RegNumValidationModel
    {
        //public long Id { get; set; }
        //public int ApplicationFormId { get; set; }
        [Required]
        [Display(Name = "Registration Number")]
        public string RegNum { get; set; }
    }
}