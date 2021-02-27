using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduApply.Data.Entities;
using EduApply.Logic.Interfaces;

namespace EduApply.Logic.Repository
{
    public class EventLogRepository : SqlRepository, IEventLogRepository
    {
        public EventLogRepository(IDbContext context)
            : base(context)
        {

        }

        public void SaveEvent(EventLog eventLog)
        {
            this.Insert<EventLog>(eventLog);
            this.SaveChanges();
        }


        public IEnumerable<EventLog> GetEventLogs(int? applicationFormId, int? workFlowId, string keyword, DateTime? startDate, DateTime? endDate)
        {
            var isAllParametersNull = true;
            var eventLogs = this.GetAll<EventLog>().ToList();
            if (applicationFormId != null)
            {
                eventLogs = eventLogs.Where(x => x.ApplicationFormId == applicationFormId).ToList();
                isAllParametersNull = false;
            }
            if (workFlowId != null)
            {
                eventLogs = eventLogs.Where(x => x.WorkFlowId == workFlowId).ToList();
                isAllParametersNull = false;
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                eventLogs = eventLogs.Where(x => x.Action.Contains(keyword)).ToList();
                isAllParametersNull = false;
            }
            if ((startDate != null && endDate != null) && endDate >= startDate)
            {
                eventLogs = eventLogs.Where(x => (x.Timestamp >= startDate && x.Timestamp <= endDate)).ToList();
                isAllParametersNull = false;
            }
            if (isAllParametersNull)
            {
                eventLogs = eventLogs.Where(x => x.Id == -1).ToList();
            }
            return eventLogs.ToList();
        }
    }
}
