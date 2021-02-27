using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduApply.Data.Entities
{
    public class Program:BaseEntity<int>
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public bool IsActive { get; set; }
        public IEnumerable<Course> CoursesInProgram { get; set; }
        public IEnumerable<Course> CoursesNotInProgram { get; set; }
    }
}
