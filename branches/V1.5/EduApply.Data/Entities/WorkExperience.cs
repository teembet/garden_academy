using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduApply.Data.Entities
{
    public class WorkExperience : BaseEntity<long>
    {
        public string Organization { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string Position { get; set; }
        public string JobDescription { get; set; }
        public long ApplicationId { get; set; }
        public bool IsCurrentJob { get; set; }
    }
}
