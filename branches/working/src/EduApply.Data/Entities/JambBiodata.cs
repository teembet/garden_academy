using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduApply.Data.Entities
{
    public class JambBiodata : BaseEntity<long>
    {
        public int SessionId { get; set; }
        public string RegNum { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string ProgramCode { get; set; }
        public string CourseCode { get; set; }
        public string Gender { get; set; }
        public string StateOfOrigin { get; set; }
        public string LGA { get; set; }
        public IEnumerable<Session> Sessions { get; set; }
    }
}
