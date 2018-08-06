using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduApply.Data.Entities
{
    public class JambBreakDown : BaseEntity<long>
    {
        public int SessionId { get; set; }
        public string RegNum { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        //public string Faculty { get; set; }
        public string CourseCode { get; set; }
        public string CourseOfStudy { get; set; }
        public string Gender { get; set; }
        public string StateOfOrigin { get; set; }
        public string LGA { get; set; }
        public int EngScore { get; set; }
        public string Subject2 { get; set; }
        public int Subject2Score { get; set; }
        public string Subject3 { get; set; }
        public int Subject3Score { get; set; }
        public string Subject4 { get; set; }
        public int Subject4Score { get; set; }
        public int TotalScore { get; set; }
        public IEnumerable<Session> Sessions { get; set; }
        public IEnumerable<Course> Courses { get; set; }
    }
}
