using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using EduApply.Data.Entities;
using EduApply.Logic.Repository;

namespace EduApply.Web.Models
{
    public class SplitConfigModel
    {
        [Display(Name = "Application Form")]
        public int ApplicationFormId { get; set; }
        public IEnumerable<ApplicationForm> ApplicationForms { get; set; }
        public int BankId { get; set; }
        public IEnumerable<Bank> Banks { get; set; }
        [Required]
        public string AccountNumber { get; set; }
        [Required]
        public decimal Amount { get; set; }
        public string Narration { get; set; }
        [Required]
        public string Name { get; set; }
        public List<Split> Splits { get; set; }
    }
}