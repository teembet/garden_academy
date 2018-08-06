using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduApply.Data.Entities
{
    public class Department : BaseEntity<int>
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public int FacultyId { get; set; }
        [ForeignKey("FacultyId")]
        public virtual Faculty Faculty { get; set; }
        public virtual ICollection<Course> Departments { get; set; }
    }
}
