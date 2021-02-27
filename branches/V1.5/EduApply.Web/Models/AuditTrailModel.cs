using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EduApply.Data.Entities;

namespace EduApply.Web.Models
{
    public class AuditTrailModel
    {
        public string UserId { get; set; }
        public int AuditActionId { get; set; }
        public int AuditSectionId { get; set; }
        public string Details { get; set; }
        public DateTime TimeStamp { get; set; }
        public string UserIp { get; set; }
        public string UserRole { get; set; }

        public IEnumerable<AuditSection> AuditSections { get; set; }
        public IEnumerable<AuditAction> AuditActions { get; set; }
        public IEnumerable<ApplicationRole> Roles { get; set; }
    }
}