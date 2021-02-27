using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduApply.Data.Entities;
using EduApply.Logic.Interfaces;

namespace EduApply.Logic.Repository
{
    public class AuditTrailRepository : SqlRepository, IAuditTrailRepository
    {
        public AuditTrailRepository(IDbContext context)
            : base(context)
        {

        }

        public void SaveAuditTrail(AuditTrail auditTrail)
        {
            this.Insert<AuditTrail>(auditTrail);
            this.SaveChanges();
        }

        public IEnumerable<AuditTrail> GetAuditTrails(int? auditSectionId, int? auditActionId, DateTime? startDate, DateTime? endDate, string userRole, string keyword)
        {
            var isAllParametersNull = true;
            var auditTrails = this.GetAll<AuditTrail>();
            if (auditSectionId != null)
            {
                var auditActionIds = this.GetAuditActions().Where(x => x.SectionId == auditSectionId).Select(x => x.Id).ToList();
                auditTrails = auditTrails.Where(x => auditActionIds.Contains(x.AuditActionId));
                isAllParametersNull = false;
            }
            if (auditActionId != null)
            {
                auditTrails = auditTrails.Where(x => x.AuditActionId == auditActionId);
                isAllParametersNull = false;
            }
            if (!string.IsNullOrEmpty(userRole))
            {
                auditTrails = auditTrails.Where(x => x.UserRole == userRole);
                isAllParametersNull = false;
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                auditTrails = auditTrails.Where(x => x.Details.Contains(keyword));
                isAllParametersNull = false;
            }
            if ((startDate != null && endDate != null) && endDate >= startDate)
            {
                auditTrails = auditTrails.Where(x => (x.TimeStamp >= startDate && x.TimeStamp <= endDate));
                isAllParametersNull = false;
            }
            if (isAllParametersNull)
            {
                auditTrails = auditTrails.Where(x => x.Id == -1);
                isAllParametersNull = false;
            }
            return auditTrails.ToList();
        }

        public IEnumerable<AuditAction> GetAuditActions()
        {
            var auditActions = this.GetAll<AuditAction>();
            return auditActions.ToList();
        }

        public IEnumerable<AuditSection> GetAuditSections()
        {
            var auditSections = this.GetAll<AuditSection>();
            return auditSections.ToList();
        }


        public IEnumerable<AuditAction> GetAuditActions(int? auditSectionId)
        {
            if (auditSectionId == null)
                return this.GetAll<AuditAction>().ToList();

            var auditActions = this.GetAll<AuditAction>().Where(x => x.SectionId == auditSectionId);
            return auditActions.ToList();
        }

        public AuditAction GetAuditAction(int auditActionId)
        {
            var auditAuction = this.GetAll<AuditAction>().FirstOrDefault(x => x.Id == auditActionId);
            return auditAuction;
        }

        public AuditSection GetAuditSection(int auditSectionId)
        {
            var auditSection = this.GetAll<AuditSection>().FirstOrDefault(x => x.Id == auditSectionId);
            return auditSection;
        }


        public AuditTrail GetAuditTrail(int id)
        {
            var auditTrail = this.GetAll<AuditTrail>().First(x => x.Id == id);
            return auditTrail;
        }
    }
}
