using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EduApply.Web.Models
{
    public class SearchResultModel
    {
        public string RegNum { get; set; }
        public string AppNum { get; set; }
        public string Lastname { get; set; }
        public string Firsname { get; set; }
        public string Middlename { get; set; }
        public DateTime ApplicationDate { get; set; }
        public DateTime SubmissionDate { get; set; }
        public DateTime PaymentDate { get; set; }
        public DateTime AdmittedDate { get; set; }
    }
}