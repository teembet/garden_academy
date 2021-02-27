using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduApply.Data.Entities;

namespace EduApply.Logic.Interfaces
{
    public interface IEventLogRepository
    {
        void SaveEvent(EventLog eventLog);
        IEnumerable<EventLog> GetEventLogs(int? applicationFormId, int? workFlowId, string keyword, DateTime? startDate, DateTime? endDate);
    }
}
