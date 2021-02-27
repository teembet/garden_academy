using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduApply.Data.Entities
{
    public class TemplatesInAppForms : BaseEntity<int>
    {
        public int ApplicationFormId { get; set; }
        public int FormTemplateId { get; set; }
    }
}
