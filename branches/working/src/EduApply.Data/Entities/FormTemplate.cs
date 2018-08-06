using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduApply.Data.Entities
{
    public class FormTemplate : BaseEntity<int>
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public int MinEntry { get; set; }
        public int MaxEntry { get; set; }
    }
}
