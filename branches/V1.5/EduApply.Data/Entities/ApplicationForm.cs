using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduApply.Data.Entities
{
    public class ApplicationForm : BaseEntity<int>
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int FormCategoryId { get; set; }
        public int SessionId { get; set; }
       // public int MappedToFormId { get; set; }
        public decimal? Fee { get; set; }
        public bool AllowMultipleApplications { get; set; }
        public bool AllowJambResultPrinting { get; set; }
        public bool AllowAppResultPrinting { get; set; }
        public bool AllowNonAppResultPrinting { get; set; }
        public bool AllowAdmissionLetterPriniting { get; set; }
        public bool AllowApplicationFormPrinting { get; set; }
        public bool AllowPhotoCardPrinting { get; set; }
        public bool AllowApplicationEditAfterSubmission { get; set; }
        public IEnumerable<WorkFlow> WorkFlowList { get; set; }
        public IEnumerable<FormTemplate> FormTemplates { get; set; }
        public IEnumerable<ProgramCourse> ProgramCourses { get; set; }
    }
}
