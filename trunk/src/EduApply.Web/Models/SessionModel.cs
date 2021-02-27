using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EduApply.Web.Models
{
    public class SessionModel
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Session")]
        public string Name { get; set; }
        [Required(ErrorMessage = ("Select a start Date"))]
        [DataType(DataType.DateTime)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = ("Select an End Date"))]
        [DataType(DataType.DateTime)]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }
    }
}