using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduApply.Data.Entities
{
    public class ApplicantsProgramCourse : BaseEntity<long>
    {
        public long ApplicationId { get; set; }
        [Required(ErrorMessage = "Select your Program")]
        public int ProgramId { get; set; }
        [Required(ErrorMessage = "Select your Department")]
        public int DepartmentId { get; set; }
        [Required(ErrorMessage = "Select your Course")]
        public int CourseId { get; set; }
        //[Display(Name="Mode Of Entry")]
        public int? ModeOfStudyId { get; set; }


        public IEnumerable<Program> Programs { get; set; }
        public IEnumerable<Department> Departments { get; set; }
        public IEnumerable<Course> Courses { get; set; }
    }
}
