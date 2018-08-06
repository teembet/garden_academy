using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EduApply.Web.Models
{
    public class ApplicationModel
    {
        public string RegNo { get; set; }
        public bool IsSubmitted { get; set; }
        public bool IsAdmitted { get; set; }
        public DateTime? ApplicationDate { get; set; }
        public DateTime? AdmittedDate { get; set; }
        public DateTime? SubmissionDate { get; set; }
        public DateTime? PaymentDate { get; set; }
        public int WorkFlowStage { get; set; }
        public int FillStage { get; set; }
        public string UserName { get; set; }
        public int AppFormId { get; set; }
        public int SessionId { get; set; }
        public int ProgramId { get; set; }
        public int CourseOfStudyId { get; set; }
        public int DepartmenyId { get; set; }
        public int FacultyId { get; set; }
        public ICollection<PersonalInformationModel> PersonalInformations { get; set; }
        public ICollection<ReferenceModel> References { get; set; }
        public ICollection<WorkExperienceModel> WorkExperiences { get; set; }
    }

    public class ApplicationViewModel
    {
        public long ApplicationId { get; set; }
        public int ApplicationFormId { get; set; }
        public DateTime ApplicationClosingDate { get; set; }
        public string AppFormName { get; set; }
    }
    public class SubmittedApplicationViewModel
    {
        public long ApplicationId { get; set; }
        public int ApplicationFormId { get; set; }
        public string AppFormName { get; set; }
    }
}