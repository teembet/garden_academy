using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduApply.Data.Entities
{
    public class AuditTrail : BaseEntity<long>
    {
        public string UserId { get; set; }
        public string Username { get; set; } 
        public int AuditActionId { get; set; }
        public string Details { get; set; }
        public DateTime TimeStamp { get; set; }
        public string UserIp { get; set; }
        public string UserRole { get; set; }
    }
}
