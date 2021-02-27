using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using EduApply.Data.Entities;

namespace EduApply.Web.Models
{
    public class OLevelDetailsModel
    {
        public long ApplicationId { get; set; }
        public string CandidateName { get; set; }
        //public string SchoolName { get; set; }
        public string ExamNumber { get; set; }
        public int Year { get; set; }
        public string ExamType { get; set; }
        public virtual ICollection<OLevelResultModel> OLevelResults { get; set; }

    }

    public class OLevelResultDetailsViewModel
    {
        public List<OLevelResultDetails> ResultDetails { get; set; }
        public int MaxEntry { get; set; }
    }

    public class Years
    {
        public int  Year { get; set; } 
    }
    public class OLevelResultDetails
    {
        public long DetailId { get; set; }
        [Required]
        [Display(Name = "Candidate Name")]
        public string CandidateName { get; set; }
        //[Required]
        //[Display(Name = "School Name")]
        //public string SchoolName { get; set; }
        [Required]
        [Display(Name = "Exam Number")]
        public string ExamNumber { get; set; }
        [Required]
        public int? Year { get; set; }
        [Required]
        [Display(Name = "Exam Type")]
        public string ExamType { get; set; }
        public IEnumerable<Years> Years { get; set; }
        public IEnumerable<OLevelSubject> Subjects { get; set; }
        public IEnumerable<OLevelGrade> Grades { get; set; }
        public IEnumerable<ExamType> ExamTypes { get; set; }
        public List<OLevelDetail> OLevelDetails { get; set; }

        [Display(Name = "English")]
        public string Subject1 { get; set; }
        public string Grade1 { get; set; }
        [Display(Name = "Mathematics")]
        public string Subject2 { get; set; }
        public string Grade2 { get; set; }
        public string Subject3 { get; set; }

        public string Grade3 { get; set; }
       
        public string Subject4 { get; set; }

        public string Grade4 { get; set; }
 
        public string Subject5 { get; set; }

        public string Grade5 { get; set; }
     
        public string Subject6 { get; set; }

        public string Grade6 { get; set; }
   
        public string Subject7 { get; set; }

        public string Grade7 { get; set; }

        public string Subject8 { get; set; }

        public string Grade8 { get; set; }
       
        public string Subject9 { get; set; }

        public string Grade9 { get; set; }
    }

    public class OLevelResultDetailsPreview
    {
        public string CandidateName { get; set; }
        public string SchoolName { get; set; }
        public string ExamNumber { get; set; }
        public int? Year { get; set; }
        public string ExamType { get; set; }



        public string Subject1 { get; set; }
        public string Grade1 { get; set; }
        public string Subject2 { get; set; }
        public string Grade2 { get; set; }
        public string Subject3 { get; set; }
        public string Grade3 { get; set; }
        public string Subject4 { get; set; }
        public string Grade4 { get; set; }
        public string Subject5 { get; set; }
        public string Grade5 { get; set; }
        public string Subject6 { get; set; }
        public string Grade6 { get; set; }
        public string Subject7 { get; set; }
        public string Grade7 { get; set; }
        public string Subject8 { get; set; }
        public string Grade8 { get; set; }
        public string Subject9 { get; set; }
        public string Grade9 { get; set; }
    }
}