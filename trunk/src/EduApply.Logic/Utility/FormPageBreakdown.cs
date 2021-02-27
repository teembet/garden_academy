using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduApply.Data.Entities;

namespace EduApply.Logic.Utility
{
    public class FormPageBreakdown
    {
        public int FilteredCount { get; set; }
        public int TotalCount { get; set; }
        public List<FormResult> FormResultList { get; set; }
    }
}
