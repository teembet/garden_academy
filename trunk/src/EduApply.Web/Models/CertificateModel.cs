using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EduApply.Web.Models
{
    public class CertificateModel
    {
        [Display(Name = "Certificate Name")]
        public string CertificateName { get; set; }
        [Display(Name = "Certificate")]
        public string CertificateUrl { get; set; }
        public string CertificateType { get; set; }
        public long ApplicationId { get; set; }
    }
}