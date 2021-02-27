using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using EduApply.Data.Entities;

namespace EduApply.Web.Models
{
    public class UploadViewModel
    {
    }

    public class JambBiodataModel
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "Select Session")]
        [Display(Name = "Session")]
        public int SessionId { get; set; }
        public string RegNum { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }
        [Required]
        [Display(Name = "Course Code")]
        public string CourseCode { get; set; }
        [Display(Name = "Program Code")]
        public string ProgramCode { get; set; }
        public string Gender { get; set; }
        [Display(Name = "State of origin")]
        public string StateOfOrigin { get; set; }
        public string LGA { get; set; }
        public IEnumerable<SessionModel> Sessions { get; set; }
    }

    public class JambBreakDownModel
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "Select Session")]
        [Display(Name = "Session")]
        public int SessionId { get; set; }
        //[Required(ErrorMessage = "Enter a name for the file you are uploading")]
        //[Display(Name = "Name of Uploaded File")]
        //public string NameOfFile { get; set; }
        public string RegNum { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }
        public string Faculty { get; set; }
        [Required]
        [Display(Name = "Course Code")]
        public string CourseCode { get; set; }
        [Display(Name = "Program Code")]
        public string ProgramCode { get; set; }
        [Required]
        [Display(Name = "Course of Study")]
        public string CourseOfStudy { get; set; }
        public string Gender { get; set; }
        [Display(Name = "State of origin")]
        public string StateOfOrigin { get; set; }
        public string LGA { get; set; }
        [Required]
        [Range(0, 100, ErrorMessage = "Score can only be between 0 and 100")]
        public int EngScore { get; set; }
        [Required]
        public string Subject2 { get; set; }
        [Required]
        [Range(0, 100, ErrorMessage = "Score can only be between 0 and 100")]
        public int Subject2Score { get; set; }
        [Required]
        public string Subject3 { get; set; }
        [Required]
        [Range(0, 100, ErrorMessage = "Score can only be between 0 and 100")]
        public int Subject3Score { get; set; }
        [Required]
        public string Subject4 { get; set; }
        [Required]
        [Range(0, 100, ErrorMessage = "Score can only be between 0 and 100")]
        public int Subject4Score { get; set; }
        [Required]
        public int TotalScore { get; set; }

        public IEnumerable<SessionModel> Sessions { get; set; }
        public IEnumerable<CourseModel> Courses { get; set; }
    }
    public class FormResultModel
    {
        //[Required(ErrorMessage = "Select Application Form")]
        [Required]
        [Display(Name = "Application Form Type")]
        public int? ApplicationFormId { get; set; }
        [Required]
        [Display(Name = "Reg Num")]
        public string RegNum { get; set; }
        [Required]
        [Display(Name = "App Num")]
        public string AppNumOrRegNum { get; set; }
        [Required]
        [Display(Name = "Application Number")]
        public string AppNum { get; set; }
        [Display(Name = "English Score")]
        public decimal? EngScore { get; set; }
        [Display(Name = "Subject 2")]
        public string Subject2 { get; set; }
        [Display(Name = "Subject 2 score")]
        public decimal? Subject2Score { get; set; }
        [Display(Name = "Subject 3")]
        public string Subject3 { get; set; }
        [Display(Name = "Subject 3 Score")]
        public decimal? Subject3Score { get; set; }
        [Display(Name = "Subject 4")]
        public string Subject4 { get; set; }
        [Display(Name = "Subject 4 Score")]
        public decimal? Subject4Score { get; set; }
        [Display(Name = "Subject 5")]
        public string Subject5 { get; set; }
        [Display(Name = "Subject 5 Score")]
        public decimal? Subject5Score { get; set; }
        [Display(Name = "Total Score")]
        public decimal TotalScore { get; set; }
        public IEnumerable<ApplicationFormModel> ApplicationForms { get; set; }
        public IEnumerable<SessionModel> Sessions { get; set; }
    }
    public class SessionResultModel
    {
        [Required(ErrorMessage = "Select Session")]
        [Display(Name = "Session")]
        public int SessionId { get; set; }
        [Display(Name = "Registration Number")]
        public string RegNum { get; set; }
        [Display(Name = "English Score")]
        public decimal? EngScore { get; set; }
        [Display(Name = "Subject 2")]
        public string Subject2 { get; set; }
        [Display(Name = "Subject 2 Score")]
        public decimal? Subject2Score { get; set; }
        [Display(Name = "Subject 3")]
        public string Subject3 { get; set; }
        [Display(Name = "Subject 3 Score")]
        public decimal? Subject3Score { get; set; }
        [Display(Name = "Subject 4")]
        public string Subject4 { get; set; }
        [Display(Name = "Subject 4 Score")]
        public decimal? Subject4Score { get; set; }
        [Display(Name = "Total Score")]
        public decimal TotalScore { get; set; }
        public IEnumerable<SessionModel> Sessions { get; set; }
    }
    public class AdmittedListModel
    {
        [Required(ErrorMessage = "Select  Application Form Type")]
        [Display(Name = "Application Form Type")]
        public int ApplicationFormId { get; set; }
        [Required(ErrorMessage = "Select Program")]
        [Display(Name = "Program")]
        public int ProgramId { get; set; }

        [Required(ErrorMessage = "Select CourseOfStudy")]
        [Display(Name = "Course Of Study")]
        public int CourseOfStudyId { get; set; }
        //[Required(ErrorMessage = "Enter a name for the file you are uploading")]
        //[Display(Name = "Name of Admitted List")]
        public string RegNumm { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public IEnumerable<Program> Programs { get; set; }
        public IEnumerable<Course> Courses { get; set; }

        public IEnumerable<ApplicationFormModel> ApplicationForms { get; set; }
    }
    public class NonApplicantAdmittedListModel
    {
        [Required(ErrorMessage = "Select Session")]
        [Display(Name = "Session")]
        public int SessionId { get; set; }

        [Required(ErrorMessage = "Select Program")]
        [Display(Name = "Program")]
        public int ProgramId { get; set; }

        [Required(ErrorMessage = "Select CourseOfStudy")]
        [Display(Name = "Course Of Study")]
        public int CourseOfStudyId { get; set; }

        [Display(Name = "Application Form Type")]
        public int ApplicationFormId { get; set; }

        [Required(ErrorMessage = "Enter a name for the file you are uploading")]
        [Display(Name = "Name of Admitted List")]
        public string NameOfFile { get; set; }
        public IEnumerable<SessionModel> Sessions { get; set; }
        public IEnumerable<ProgramModel> Programs { get; set; }
        public IEnumerable<CourseModel> Courses { get; set; }
        public IEnumerable<ApplicationFormModel> AppForms { get; set; }
    }

    public class NonApplicatantUploadModel
    {
        public string AppNum { get; set; }
        public string RegNum { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public string ModeOfEntry { get; set; }
        public int LineNumber { get; set; }
    }
}