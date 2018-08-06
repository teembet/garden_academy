using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduApply.Data.Entities
{
    public class Faculty : BaseEntity<int>
    {
        public string Name { get; set; }
        public virtual ICollection<Department> Departments { get; set; }
    }


}
