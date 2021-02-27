using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduApply.Data.Entities
{
    public class ApiLog:BaseEntity<long>
    {
        public string Action { get; set; }
        public string Details { get; set; }
        public DateTime TimeStamp { get; set; }
        public string UserIp { get; set; }
    }


}
