using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EduApply.Data.Entities;
using EduApply.Logic.Interfaces;

namespace EduApply.Web.Controllers
{
    [Authorize(Roles = "Admin, SchoolAdmin")]
    public class EventLogController : Controller
    {
        private IEventLogRepository _eventLog;
        private IApplicationFormRepository _appForm;
        private IConfigurationService _configService;
        public EventLogController(IEventLogRepository eventLog, IApplicationFormRepository appForm, IConfigurationService configService)
        {
            this._eventLog = eventLog;
            this._appForm = appForm;
            this._configService = configService;
        }
        public ActionResult GetEventList(int? appFormId, int? workFlowId, DateTime? startDate, DateTime? endDate, string keyword)
        {
            // var auditSections = _auditTrailRepository.GetAuditSections();
            var eventLogs = _eventLog.GetEventLogs(appFormId, workFlowId,keyword, startDate, endDate).OrderByDescending(x => x.Timestamp).ToList();
            var result = from s in eventLogs
                         select new
                         {
                             //auditSection = "section",
                             logId = s.Id,
                             username = s.Username,
                             appLicationForm = _appForm.GetAppForms(s.ApplicationFormId).Name,
                             workFlow = _configService.GetWorkFlow(s.WorkFlowId).Name,
                             details = s.Action,
                             timestamp = s.Timestamp.ToString("dd-MMM-yyyy h:mm tt")
                         };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        //
        // GET: /EventLog/
        public ActionResult Index()
        {
            var eventLog = new EventLog();
            eventLog.AppForms = _appForm.GetAppForms();
            eventLog.WorkFlows = _configService.GetWorkFlow();
            return View(eventLog);
        }
    }
}