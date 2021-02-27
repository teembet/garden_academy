using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using EduApply.Data.Entities;

namespace EduApply.Web.Models
{
    public class ExamVenueModel
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Venue")]
        public int VenueId { get; set; }
        [Required]
        [Display(Name = "Exam Date")]
        public DateTime ExamDate { get; set; }
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
        public IEnumerable<Venues> Venues { get; set; }
    }
}