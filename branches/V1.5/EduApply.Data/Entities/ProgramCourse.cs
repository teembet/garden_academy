using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduApply.Data.Entities
{
    public class ProgramCourse : BaseEntity<int>
    {
        public int ProgramId { get; set; }
        public int CourseId { get; set; }
        [ForeignKey("ProgramId")]
        public virtual Program Program { get; set; }
        [ForeignKey("CourseId")]
        public virtual Course Course { get; set; }
    }
}
