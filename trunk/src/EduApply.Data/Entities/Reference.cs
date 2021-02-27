using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduApply.Data.Entities
{
   public class Reference:BaseEntity<long>
    {
       public long ApplicationId { get; set; }
       public string Name { get; set; }
       public string Occupation { get; set; }
       public string Address { get; set; }
       public string PhoneNumber { get; set; }
       public string Email { get; set; }
    }
}
