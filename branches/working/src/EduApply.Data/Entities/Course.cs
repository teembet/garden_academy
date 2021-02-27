using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduApply.Data.Entities
{
    public class Course : BaseEntity<int>
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public string Code { get; set; }
        public IEnumerable<Program> ProgramsForThisCourse { get; set; }
        public IEnumerable<Program> ProgramsNotForThisCourse { get; set; }
        public int DepartmentId { get; set; }
        [ForeignKey("DepartmentId")]
        public virtual Department Department { get; set; }
    }
}
