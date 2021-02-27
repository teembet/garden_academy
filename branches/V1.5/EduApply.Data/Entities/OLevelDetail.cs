using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduApply.Data.Entities
{
    public class OLevelDetail : BaseEntity<long>
    {
        public long ApplicationId { get; set; }
        public string CandidateName { get; set; }
        public string ExamNumber { get; set; }
        //year below is nullable int because if not it wld show '0' in the textbox
        public int? Year { get; set; }
        public string ExamType { get; set; }
    }
}
