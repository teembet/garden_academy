using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EduApply.Web.Models
{
    public class PaymentModel
    {
        [Required]
        public decimal Amount { get; set; }
    }
}