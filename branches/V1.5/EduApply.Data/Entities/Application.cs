using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduApply.Data.Entities
{
    public class Application : BaseEntity<long>
    {
        public string RegNum { get; set; }
        public string AppNum { get; set; }
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
        public int DepartmentId { get; set; }
        public int FacultyId { get; set; }
        public bool IsPaid { get; set; }
        public int ExamVenueId { get; set; }
        public int SeatNo { get; set; }
        public bool IsJambPassed { get; set; }
        public int ProgramIdAdmittedInto { get; set; }
        public int CourseIdAdmittedInto { get; set; }
        public string MappedToAppNum { get; set; }
        public string ModeOfEntry { get; set; }

    }
}
