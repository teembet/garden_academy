using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduApply.Data.Entities
{
    public class FormTemplateSettings : BaseEntity<long>
    {
        public int ApplicationFormId{ get; set; }
        public int FormTemplateId { get; set; }
        public int MinEntry { get; set; }
        public int MaxEntry { get; set; }
    }
}
