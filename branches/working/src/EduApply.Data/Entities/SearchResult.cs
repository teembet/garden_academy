using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduApply.Data.Entities
{
    public class SearchResult
    {
        public long ApplicationId { get; set; }
        public string RegNum { get; set; }
        public string AppNum { get; set; }
        public string Lastname { get; set; }
        public string Firsname { get; set; }
        public string Middlename { get; set; }
        public string ProgramCourse { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? ApplicationDate { get; set; }
        public DateTime? SubmissionDate { get; set; }
        public DateTime? PaymentDate { get; set; }
        public DateTime? AdmittedDate { get; set; } 
    }
}
