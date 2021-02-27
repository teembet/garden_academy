using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduApply.Data.Entities
{
    public class EducationalDetails : BaseEntity<long>
    {
        public string SchoolName { get; set; }
        public string Qualification { get; set; }
        public long ApplicationId { get; set; }
        public string ClassOfDegree { get; set; }
        //public decimal CGPA { get; set; }
        public int? EntryYear { get; set; }
        public int? GraduationYear { get; set; }
    }
}
