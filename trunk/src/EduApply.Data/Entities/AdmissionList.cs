using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduApply.Data.Entities
{
    public class AdmissionList : BaseEntity<long>
    {
        public string RegNum { get; set; }
        public string AppNum { get; set; }
        public int FormId { get; set; }
        public string ModeOfEntry { get; set; }
    }
}
