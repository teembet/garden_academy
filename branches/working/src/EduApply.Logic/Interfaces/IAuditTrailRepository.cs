using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduApply.Data.Entities;

namespace EduApply.Logic.Interfaces
{
    public interface IAuditTrailRepository
    {
        void SaveAuditTrail(AuditTrail auditTrail);
        IEnumerable<AuditTrail> GetAuditTrails(int? auditSectionId, int? auditActionId, DateTime? startDate, DateTime? endDate, string userRole, string keyword);
        IEnumerable<AuditAction> GetAuditActions();
        IEnumerable<AuditAction> GetAuditActions(int? auditSectionId);
        IEnumerable<AuditSection> GetAuditSections();
        AuditAction GetAuditAction(int auditActionId);
        AuditSection GetAuditSection(int auditSectionId);
        AuditTrail GetAuditTrail(int id);
    }
}
