using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EduApply.Web.Models
{
    public class StateModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int CountryId { get; set; }
    }
}