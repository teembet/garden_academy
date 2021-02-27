using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduApply.Data.Entities
{
    public class EventLog : BaseEntity<long>
    {
        public int ApplicationFormId { get; set; }
        public int WorkFlowId { get; set; }
        public string Username { get; set; }
        public string Action { get; set; }
        public DateTime Timestamp { get; set; }
        public string AppNum { get; set; }
        public int FormTemplateId { get; set; }

        public IEnumerable<ApplicationForm> AppForms { get; set; }
        public IEnumerable<WorkFlow> WorkFlows { get; set; }
    }
}
