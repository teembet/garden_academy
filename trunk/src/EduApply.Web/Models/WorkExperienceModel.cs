using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace EduApply.Web.Models
{
    public class WorkExperienceModel
    {
        [Required]
        public string Organization { get; set; }
        [Required]
        [Display(Name = "From Date")]
        public DateTime FromDate { get; set; }
        [Required]
        [Display(Name = "To Date")]
        public DateTime ToDate { get; set; }
        [Required]
        public string Position { get; set; }
        [Required]
        [Display(Name = "Description")]
        public string JobDescription { get; set; }
        public long ApplicationId { get; set; }
        public int  MaxEntry { get; set; }
        public virtual ApplicationModel Application { get; set; }
    }
}