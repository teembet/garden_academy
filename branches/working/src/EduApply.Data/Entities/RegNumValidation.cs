using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduApply.Data.Entities
{
    public class RegNumValidation: BaseEntity<long>
    {
        public int ApplicationFormId { get; set; }
        public string RegNum { get; set; }
    }
}
