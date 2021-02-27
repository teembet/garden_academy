using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EduApply.Web.Models
{
    public class FacultyModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter Faculty name")]
        public string Name { get; set; }
    }
}