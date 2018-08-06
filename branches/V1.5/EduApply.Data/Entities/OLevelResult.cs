using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduApply.Data.Entities
{
    public class OLevelResult : BaseEntity<long>
    {
        public long DetailId { get; set; }
        public int SubjectId { get; set; }
        public int GradeId { get; set; }
    }
}
