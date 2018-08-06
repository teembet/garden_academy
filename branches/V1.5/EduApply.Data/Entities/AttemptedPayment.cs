using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduApply.Data.Entities
{
    public class AttemptedPayment : BaseEntity<long>
    {
        public string ApplicationNumber { get; set; }
        public long PayeeId { get; set; }
        public string TerminalId { get; set; }
    }
}
