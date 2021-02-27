using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduApply.Data.Entities
{
    public class Certificate : BaseEntity<long>
    {
        public string CertificateName { get; set; }
        public string CertificateUrl { get; set; }
        public string CertificateType { get; set; }
        public long ApplicationId { get; set; }
    }

}
