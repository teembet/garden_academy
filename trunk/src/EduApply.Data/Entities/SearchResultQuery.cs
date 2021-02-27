
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduApply.Data.Entities
{
    public class SearchResultQuery
    {
        public int? SessionId { get; set; }
        public int? FormId { get; set; }
        public int? FormTemplateId { get; set; }
        public int? FacultyId { get; set; }
        public int? DepartmentId { get; set; }
        public int? CourseOfStudyId { get; set; }
        public int? ProgramId { get; set; }
        public int? VenueId { get; set; }
        public bool? IsPaid { get; set; }
        public bool? IsSubmitted { get; set; }
        public bool? IsAdmitted { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Name { get; set; }
        public string AppNo { get; set; }
        public string RegNo { get; set; }
        public string DateType { get; set; }
    }
}
