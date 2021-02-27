using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduApply.Data.Entities
{
    public class AppFormProgramCourse : BaseEntity<long>
    {
        public int AppFormId { get; set; }
        public int ProgramCourseId { get; set; }
    }
}
