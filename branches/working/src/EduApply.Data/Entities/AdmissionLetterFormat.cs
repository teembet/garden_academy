using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduApply.Data.Entities
{
    public class AdmissionLetterFormat: BaseEntity<int>
    {
        public string Name { get; set; }
    }
}
