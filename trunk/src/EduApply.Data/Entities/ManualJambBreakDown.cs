using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduApply.Data.Entities
{
   public class ManualJambBreakDown:BaseEntity<long>
   {
       public string RegNum { get; set; }
        //public string CourseOfStudy { get; set; }
        public int? EngScore { get; set; }
        public string Subject2 { get; set; }
        public int? Subject2Score { get; set; }
        public string Subject3 { get; set; }
        public int? Subject3Score { get; set; }
        public string Subject4 { get; set; }
        public int? Subject4Score { get; set; }
        public int? TotalScore { get; set; }
    }
}
