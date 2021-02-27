using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduApply.Data.Entities
{
    public class VenueMappings : BaseEntity<long>
    {
        public int FormId { get; set; }
        public int ProgramId { get; set; }
        public int CourseOfStudyId { get; set; }
        public bool IsActive { get; set; }
        public int ExamVenueId { get; set; }

    }
}
