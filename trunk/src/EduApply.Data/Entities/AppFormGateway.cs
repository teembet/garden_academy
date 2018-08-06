using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduApply.Data.Entities
{
    public class AppFormGateway : BaseEntity<long>
    {
        public int AppFormId { get; set; }
        public long GatewayId { get; set; }
    }
}
