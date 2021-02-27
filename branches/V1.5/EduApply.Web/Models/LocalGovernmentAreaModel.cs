using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EduApply.Web.Models
{
    public class LocalGovernmentAreaModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public long StateId { get; set; }
    }
}