using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduApply.Data.Entities
{
    public class Payment:BaseEntity<long>
    {
        public long ApplicationId { get; set; }
        public string AppNum { get; set; }
        public string FormName { get; set; }
        public decimal FormFee { get; set; }
    }
}
