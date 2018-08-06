using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduApply.Data.Entities
{
    public class MappedForm : BaseEntity<long>
    {
        public int FormId { get; set; }
        public int MappedFormId { get; set; }
    }
}
