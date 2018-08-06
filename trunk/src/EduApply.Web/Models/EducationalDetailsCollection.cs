using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EduApply.Data.Entities;

namespace EduApply.Web.Models
{
    public class EducationalDetailsCollection
    {
        public int MaxEntry { get; set; }
        public List<EducationalDetails> EducationalDetails { get; set; }
        public IEnumerable<ClassOfDegree> ClassOfDegrees { get; set; }
    }
}