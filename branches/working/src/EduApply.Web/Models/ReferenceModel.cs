using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EduApply.Web.Models
{
    public class ReferenceModel
    {
        public long ApplicationId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Occupation { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public virtual ApplicationModel Application { get; set; }
    }
}