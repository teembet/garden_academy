using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EduApply.Web.Models
{
    public class PaymentModel
    {
        public string ApplicationForm { get; set; }
        public string Session { get; set; }
        [Required]
        public decimal Amount { get; set; }
    }
}