using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduApply.Data.Entities
{
    public class ApplicationFormWorkFlow : BaseEntity<long>
    {
        public int ApplicationFormId { get; set; }
        public string WorkFlowOrder { get; set; }
        public int WorkFlowId { get; set; }
        public bool IsCompulsory { get; set; }
    }
}
