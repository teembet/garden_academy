using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using EduApply.Data.Entities;

namespace EduApply.Web.Models
{
    public class ApplicationFormModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Form Name is required")]
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        [Required(ErrorMessage = "Select Form Category")]
        [Display(Name = "Form Category")]
        public int? FormCategoryId { get; set; }
        [Required(ErrorMessage = "Select Session")]
        [Display(Name = "Session")]
        public int? SessionId { get; set; }
        [Display(Name = "Admission Letter Format")]
        public int? AdmissionLetterFormatId { get; set; }
        //[Display(Name = "Map to Form")]
        //public int? MappedToFormId { get; set; }
        public string AgencyCode { get; set; }
        [Required]
        [Range(0, Double.MaxValue, ErrorMessage = "Fee cannot be less than Zero")]
        public decimal? Fee { get; set; }
        [Display(Name = "Allow Multiple Applications")]
        public bool AllowMultipleApplications { get; set; }
        [Display(Name = "Allow Jamb Result Printing")]
        public bool AllowJambResultPrinting { get; set; }
        [Display(Name = "Allow Application Result Printing")]
        public bool AllowAppResultPrinting { get; set; }
        [Display(Name = "Allow Non-Application Result Printing")]
        public bool AllowNonAppResultPrinting { get; set; }
        [Display(Name = "Allow Admission Letter Printing")]
        public bool AllowAdmissionLetterPriniting { get; set; }
        [Display(Name = "Allow Appplication Form Printing")]
        public bool AllowApplicationFormPrinting { get; set; }
        [Display(Name = "Allow Photo Card Printing")]
        public bool AllowPhotoCardPrinting { get; set; }
        [Display(Name = "Allow Application Editing After Submission")]
        public bool AllowApplicationEditAfterSubmission { get; set; }
        [Display(Name = "Dont Allow Different Department Selection")]
        public bool DontAllowDiffDeptSelection { get; set; }
        [Display(Name = "Use Program Course From Jamb")]
        public bool UseProgramCourseFromJamb { get; set; }
        [Display(Name = "Allow Bank Payment")]
        public bool AllowBankPayment { get; set; }
        [Display(Name = "Allow Online Payment")]
        public bool AllowOnlinePayment { get; set; }
        [Display(Name = "Dont Use Default Seat Numbering")]
        public bool DontUseDefaultSeatNumbering { get; set; }
         [Display(Name = "Use Direct Entry Upload Details")]
        public bool UseDetailsFromJambBiodata { get; set; }
        public IEnumerable<FormCategory> FormCategories { get; set; }
        public IEnumerable<Session> Sessions { get; set; }
        public IEnumerable<AdmissionLetterFormat> AdmissionLetterFormats { get; set; }
        public IEnumerable<ApplicationForm> ApplicationForms { get; set; }
        public IEnumerable<Gateway> Gateways { get; set; }
        public IEnumerable<ClassOfDegree> ClassOfDegrees { get; set; }
        public IEnumerable<WorkFlowModel> WorkFlowList { get; set; }
        public IEnumerable<FormTemplateModel> FormTemplates { get; set; }
        public IEnumerable<ProgramCourse> ProgramCourses { get; set; }
        public int[] ProgramCourseIdsForThisForm { get; set; }
        public int[] MappedFormIdsForThisForm { get; set; }
        public int[] FormTempletIdz { get; set; }
        public int[] ClassOfDegreeIdzForThisForm { get; set; }
        public long[] GatewayIdzForThisForm { get; set; }
        //  public IEnumerable<AppFormProgramCourse> AppFormProgramCourses { get; set; }

    }

    public class ApplicationFormModificationModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Form Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Enter a start Date")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "Enter End Date")]
        public DateTime EndDate { get; set; }
        public int? FormCategoryId { get; set; }
        public int? SessionId { get; set; }
        public int? AdmissionLetterFormatId { get; set; }
        //public int? MappedToFormId { get; set; }
        public string AgencyCode { get; set; }
        public decimal Fee { get; set; }
        public bool AllowMultipleApplications { get; set; }
        public IEnumerable<FormTemplateModel> FormTemplatesAbsent { get; set; }
        public int[] workFlowItems { get; set; }
        public int[] IsCompulsoryItems { get; set; }
        public int[] IdsToAdd { get; set; }
        public int BD_MinEntry { get; set; }
        public int BD_MaxEntry { get; set; }
        public int OLR_MinEntry { get; set; }
        public int OLR_MaxEntry { get; set; }
        public int ED_MinEntry { get; set; }
        public int ED_MaxEntry { get; set; }
        public int WE_MinEntry { get; set; }
        public int WE_MaxEntry { get; set; }
        public int REF_MinEntry { get; set; }
        public int REF_MaxEntry { get; set; }
        public int PU_MinEntry { get; set; }
        public int PU_MaxEntry { get; set; }
        public int CU_MinEntry { get; set; }
        public int CU_MaxEntry { get; set; }
        public int PC_MinEntry { get; set; }
        public int PC_MaxEntry { get; set; }

    }

    public class AdvancedSettingsModel
    {
        public int Id { get; set; }
        public bool AllowMultipleApplications { get; set; }
        public bool AllowJambResultPrinting { get; set; }
        public bool AllowAppResultPrinting { get; set; }
        public bool AllowNonAppResultPrinting { get; set; }
        public bool AllowAdmissionLetterPriniting { get; set; }
        public bool AllowApplicationFormPrinting { get; set; }
        public bool AllowPhotoCardPrinting { get; set; }
        public bool AllowApplicationEditAfterSubmission { get; set; }
        public bool DontAllowDiffDeptSelection { get; set; }
        public bool UseProgramCourseFromJamb { get; set; }
        public bool AllowBankPayment { get; set; }
        public bool AllowOnlinePayment { get; set; }
        public bool DontUseDefaultSeatNumbering { get; set; }
        public bool UseDetailsFromJambBiodata { get; set; }
        public int[] ProgramCourseIds { get; set; }
        public int[] MappedFormIds { get; set; }
        public long[] GatewayIdz { get; set; }
        public int[] ClassOfDegreeIdz { get; set; }
    }
}