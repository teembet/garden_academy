using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduApply.Data.Entities
{
    public class NonApplicantAdmittedList : BaseEntity<long>
    {
        public string RegNum { get; set; }
        public int SessionId { get; set; }
        public int ProgramId { get; set; }
        public int CourseId { get; set; }
        public int FormId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string ModeOfEntry { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
