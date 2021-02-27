using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EduApply.Data.Entities;

namespace EduApply.Web.Models
{
    public class ApplicationPreviewPageModel
    {
        public int FormCategoryId { get; set; }
        public long ApplicationId { get; set; }
        public string Venue { get; set; }
        public int SeatNo { get; set; }
        public DateTime ExamDate { get; set; }
        public PaymentModel PaymentModel { get; set; }
        public bool AdmissionStatus { get; set; }
        public FormResultModel FormResultModel { get; set; }
        public SessionResultModel SessionResultModel { get; set; }
        public JambScoreValidationModel JambScoreValidationModel { get; set; }
        public PersonalInfoPreviewModel PersonalInformationModel { get; set; }
        public IEnumerable<Certificate> Certificates { get; set; }
        public IEnumerable<EducationalDetailsModel> EducationalDetailsModel { get; set; }
        public Picture PictureDetails { get; set; }
        public RegNumValidationModel RegNumValidationModel { get; set; }
        public OLevelResultDetails OLevelResultDetails { get; set; }
        public IEnumerable<WorkExperience> WorkExperiences { get; set; }
        public IEnumerable<Reference> References { get; set; }
        public IEnumerable<OLevelResultDetailsPreview> OLevelResult { get; set; }
        public List<ProgramCoursePreviewModel>  ProgramCoursePreviewModel { get; set; }
    }
}