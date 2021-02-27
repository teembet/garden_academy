using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using EduApply.Data.Entities;

namespace EduApply.Web.Models
{
    public class ApplicationNoFormatModel
    {
        public long Id { get; set; }
        [Required]
        [Display(Name = "Application Form")]
        public int ApplicationFormId { get; set; }
        public string Prefix { get; set; }
        public string Suffix { get; set; }
        [Required]
        [Display(Name = "Start Number")]
        public int? StartNumber { get; set; }
        public int LastNumberAllocated { get; set; }
        [Required]
        public int? Range { get; set; }
        public IEnumerable<ApplicationForm> ApplicationForms { get; set; }
    }
}