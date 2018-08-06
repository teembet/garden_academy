using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduApply.Data.Entities
{
    public class ApplicationNoFormat : BaseEntity<long>
    {
        public int ApplicationFormId { get; set; }
        public string Prefix { get; set; }
        public string Suffix { get; set; }
        public int StartNumber { get; set; }
        public int LastNumberAllocated { get; set; }
        public int Range { get; set; }
        [ForeignKey("ApplicationFormId")]
        public virtual ApplicationForm ApplicationForm { get; set; }
    }
}
