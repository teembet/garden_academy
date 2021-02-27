using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EduApply.Data.Entities;
using EduApply.Logic.Interfaces;
using NPOI.HSSF.UserModel;

namespace EduApply.Web.Controllers
{
    public class DownloadController : Controller
    {
        private IConfigurationService _configurationService;
        private IApplicationFormRepository _applicationFormRepository;
        private IRegistrationService _registrationService;
        private IAuditTrailRepository _auditTrailRepository;
        public DownloadController(IConfigurationService configurationService, IApplicationFormRepository applicationFormRepository, IRegistrationService registrationService, IAuditTrailRepository auditTrailRepository)
        {
            this._configurationService = configurationService;
            this._registrationService = registrationService;
            this._applicationFormRepository = applicationFormRepository;
            this._auditTrailRepository = auditTrailRepository;
        }
        //
        // GET: /Download/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateExcelForAuditTrail(IEnumerable<int> trailIdz)
        {

            var workbook = new HSSFWorkbook();
            var sheet = workbook.CreateSheet("Audit Trail");
            var rowIndex = 0;
            var row = sheet.CreateRow(rowIndex);
            row.CreateCell(0).SetCellValue("Audit Action");
            row.CreateCell(1).SetCellValue("Details");
            row.CreateCell(2).SetCellValue("Role");
            row.CreateCell(3).SetCellValue("Timestamp");
            foreach (var trailId in trailIdz)
            {
                rowIndex++;
                row = sheet.CreateRow(rowIndex);
                var auditTrail = _auditTrailRepository.GetAuditTrail(trailId) ?? new AuditTrail();
                var auditAction = _auditTrailRepository.GetAuditAction(auditTrail.AuditActionId);
                row.CreateCell(0).SetCellValue(auditAction.Name);
                row.CreateCell(1).SetCellValue(auditTrail.Details);
                row.CreateCell(2).SetCellValue(auditTrail.UserRole);
                row.CreateCell(3).SetCellValue(auditTrail.TimeStamp.ToString("dd-MMM-yyyy h:mm tt"));
            }
            var exportData = new MemoryStream();
            workbook.Write(exportData);

            string filePath = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/Downloads/AuditTrail.xls");
            //var file = File(exportData.ToArray(), "application/vnd.ms-excel",
            //                "AuditTrail_" + String.Format("MembershipExport-{0:dd-MMM-yyyy hh:mm:ss}.xls", DateTime.Now));

            using (var file = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite))
            {
                workbook.Write(file);
            }
            var fileInfo = new FileInfo(filePath);
            string filename = fileInfo.Name;

            return Json(filename, JsonRequestBehavior.AllowGet);

            //return File(exportData.ToArray(), "application/vnd.ms-excel",
            //    "AuditTrail_" + String.Format("MembershipExport-{0:dd-MMM-yyyy hh:mm:ss}.xls", DateTime.Now));
        }

        public ActionResult CreateExcel(IEnumerable<string> appNums)
        {

            var workbook = new HSSFWorkbook();
            var sheet = workbook.CreateSheet("Result");
            var rowIndex = 0;
            var row = sheet.CreateRow(rowIndex);
            row.CreateCell(0).SetCellValue("Application Number");
            row.CreateCell(1).SetCellValue("Registration Number");
            row.CreateCell(2).SetCellValue("English Score");
            row.CreateCell(3).SetCellValue("Subject 2");
            row.CreateCell(4).SetCellValue("Score");
            row.CreateCell(5).SetCellValue("Subject 3");
            row.CreateCell(6).SetCellValue("Score");
            row.CreateCell(7).SetCellValue("Subject 4");
            row.CreateCell(8).SetCellValue("Score");
            row.CreateCell(9).SetCellValue("Total Score");
            foreach (var appNum in appNums)
            {
                rowIndex++;
                row = sheet.CreateRow(rowIndex);
                var formResult = _configurationService.GetFormResult(appNum) ?? new FormResult();
                row.CreateCell(0).SetCellValue(formResult.AppNum);
                row.CreateCell(1).SetCellValue(formResult.RegNum);
                row.CreateCell(2).SetCellValue(formResult.EngScore.ToString());
                row.CreateCell(3).SetCellValue(formResult.Subject2);
                row.CreateCell(4).SetCellValue(formResult.Subject2Score.ToString());
                row.CreateCell(5).SetCellValue(formResult.Subject3);
                row.CreateCell(6).SetCellValue(formResult.Subject3Score.ToString());
                row.CreateCell(7).SetCellValue(formResult.Subject4);
                row.CreateCell(8).SetCellValue(formResult.Subject4Score.ToString());
                row.CreateCell(9).SetCellValue(formResult.TotalScore.ToString());
            }
            var exportData = new MemoryStream();
            workbook.Write(exportData);

            string filePath = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/Downloads/Result.xls");
            //var file = File(exportData.ToArray(), "application/vnd.ms-excel",
            //                "AuditTrail_" + String.Format("MembershipExport-{0:dd-MMM-yyyy hh:mm:ss}.xls", DateTime.Now));

            using (var file = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite))
            {
                workbook.Write(file);
            }
            var fileInfo = new FileInfo(filePath);
            string filename = fileInfo.Name;

            return Json(filename, JsonRequestBehavior.AllowGet);

            //return File(exportData.ToArray(), "application/vnd.ms-excel",
            //    "AuditTrail_" + String.Format("MembershipExport-{0:dd-MMM-yyyy hh:mm:ss}.xls", DateTime.Now));
        }
        public ActionResult CreateExcelForSessionResult(IEnumerable<string> regNums)
        {

            var workbook = new HSSFWorkbook();
            var sheet = workbook.CreateSheet("Result");
            var rowIndex = 0;
            var row = sheet.CreateRow(rowIndex);
            row.CreateCell(0).SetCellValue("RegNum");
            row.CreateCell(1).SetCellValue("EngScore");
            row.CreateCell(2).SetCellValue("Subject2");
            row.CreateCell(3).SetCellValue("Subject2Score");
            row.CreateCell(4).SetCellValue("Subject3");
            row.CreateCell(5).SetCellValue("Subject3Score");
            row.CreateCell(6).SetCellValue("Subject4");
            row.CreateCell(7).SetCellValue("Subject4Score");
            row.CreateCell(8).SetCellValue("Total Score");
            foreach (var regNum in regNums)
            {
                rowIndex++;
                row = sheet.CreateRow(rowIndex);
                var sessionResult = _configurationService.GetSessionResult(regNum) ?? new SessionResult();
                row.CreateCell(0).SetCellValue(sessionResult.RegNum);
                row.CreateCell(1).SetCellValue(sessionResult.EngScore.ToString());
                row.CreateCell(2).SetCellValue(sessionResult.Subject2);
                row.CreateCell(3).SetCellValue(sessionResult.Subject2Score.ToString());
                row.CreateCell(4).SetCellValue(sessionResult.Subject3);
                row.CreateCell(5).SetCellValue(sessionResult.Subject3Score.ToString());
                row.CreateCell(6).SetCellValue(sessionResult.Subject4);
                row.CreateCell(7).SetCellValue(sessionResult.Subject4Score.ToString());
                row.CreateCell(8).SetCellValue(sessionResult.TotalScore.ToString());
            }
            var exportData = new MemoryStream();
            workbook.Write(exportData);

            string filePath = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/Downloads/Result.xls");
            //var file = File(exportData.ToArray(), "application/vnd.ms-excel",
            //                "AuditTrail_" + String.Format("MembershipExport-{0:dd-MMM-yyyy hh:mm:ss}.xls", DateTime.Now));

            using (var file = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite))
            {
                workbook.Write(file);
            }
            var fileInfo = new FileInfo(filePath);
            string filename = fileInfo.Name;

            return Json(filename, JsonRequestBehavior.AllowGet);

            //return File(exportData.ToArray(), "application/vnd.ms-excel",
            //    "AuditTrail_" + String.Format("MembershipExport-{0:dd-MMM-yyyy hh:mm:ss}.xls", DateTime.Now));
        }
        public ActionResult CreateExcelForJambBreakDown(IEnumerable<string> regNums)
        {

            var workbook = new HSSFWorkbook();
            var sheet = workbook.CreateSheet("Jamb Breakdown");
            var rowIndex = 0;
            var row = sheet.CreateRow(rowIndex);
            row.CreateCell(0).SetCellValue("RegNum");
            row.CreateCell(1).SetCellValue("Last Name");
            row.CreateCell(2).SetCellValue("First Name");
            row.CreateCell(3).SetCellValue("Middle Name");
            // row.CreateCell(4).SetCellValue("Course of study");
            row.CreateCell(4).SetCellValue("Course Code");
            row.CreateCell(5).SetCellValue("Gender");
            row.CreateCell(6).SetCellValue("State of origin");
            row.CreateCell(7).SetCellValue("LGA");
            row.CreateCell(8).SetCellValue("English Score");
            row.CreateCell(9).SetCellValue("Subject 2");
            row.CreateCell(10).SetCellValue("Subject 2 score");
            row.CreateCell(11).SetCellValue("Subject 3");
            row.CreateCell(12).SetCellValue("Subject 3 score");
            row.CreateCell(13).SetCellValue("Subject 4");
            row.CreateCell(14).SetCellValue("Subject 4 score");
            row.CreateCell(15).SetCellValue("Total score");
            foreach (var regNum in regNums)
            {
                rowIndex++;
                row = sheet.CreateRow(rowIndex);
                var jambResult = _configurationService.GetJambBreakDown(regNum) ?? new JambBreakDown();
                row.CreateCell(0).SetCellValue(jambResult.RegNum);
                row.CreateCell(1).SetCellValue(jambResult.LastName);
                row.CreateCell(2).SetCellValue(jambResult.FirstName);
                row.CreateCell(3).SetCellValue(jambResult.MiddleName);
                //row.CreateCell(4).SetCellValue(jambResult.CourseOfStudy);
                row.CreateCell(4).SetCellValue(jambResult.CourseCode);
                row.CreateCell(5).SetCellValue(jambResult.Gender);
                row.CreateCell(6).SetCellValue(jambResult.StateOfOrigin);
                row.CreateCell(7).SetCellValue(jambResult.LGA);
                row.CreateCell(8).SetCellValue(jambResult.EngScore);
                row.CreateCell(9).SetCellValue(jambResult.Subject2);
                row.CreateCell(10).SetCellValue(jambResult.Subject2Score);
                row.CreateCell(11).SetCellValue(jambResult.Subject3);
                row.CreateCell(12).SetCellValue(jambResult.Subject3Score);
                row.CreateCell(13).SetCellValue(jambResult.Subject4);
                row.CreateCell(14).SetCellValue(jambResult.Subject4Score);
                row.CreateCell(15).SetCellValue(jambResult.TotalScore);
            }
            var exportData = new MemoryStream();
            workbook.Write(exportData);

            string filePath = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/Downloads/JambResult.xls");


            using (var file = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite))
            {
                workbook.Write(file);
            }
            var fileInfo = new FileInfo(filePath);
            string filename = fileInfo.Name;

            return Json(filename, JsonRequestBehavior.AllowGet);

        }
        public ActionResult CreateExcelForSearch(IEnumerable<string> appNums)
        {
            var applications = _registrationService.GetApplications().Where(x => appNums.Contains(x.AppNum)).ToList();
            var usernames = applications.Select(x => x.UserName).ToList();
            var personalInformations = _configurationService.GetPersonalInformations().Where(x => usernames.Contains(x.Email)).ToList();

            var result = (from app in applications
                          join personalInformation in personalInformations on app.UserName equals personalInformation.Email
                          select new
                          {
                              lastName = personalInformation.LastName,
                              firstName = personalInformation.FirstName,
                              middleName = personalInformation.MiddleName,
                              phoneNumber = personalInformation.PhoneNumber,
                              emailAddress = personalInformation.Email,
                              seatNumber = app.SeatNo,
                              regNo = app.RegNum,
                              appNo = app.AppNum,
                              Session = _configurationService.GetSession(app.SessionId).Name,
                              applicationForm = _applicationFormRepository.GetAppForms(app.AppFormId).Name,
                              faculty = app.FacultyId > 0 ? _configurationService.GetFaculty(app.FacultyId).Name : "",
                              department = app.DepartmentId > 0 ? _configurationService.GetDepartment(app.DepartmentId).Name : "",
                              course = app.CourseOfStudyId > 0 ? _configurationService.GetCourse(app.CourseOfStudyId).Name : "",
                              program = app.ProgramId > 0 ? _configurationService.GetProgram(app.ProgramId).Code : "",
                              venue = app.ExamVenueId > 0 ? _configurationService.GetVenue(app.ExamVenueId).Name : "",
                              seatNo = app.SeatNo,
                              paymentStatus = app.IsPaid ? "Paid" : "NOT PAID",
                              applicationStatus = app.IsSubmitted ? "SUBMITTED" : "NOT SUBMITTED",
                              admissionStatus = app.IsAdmitted ? "ADMITTED" : "NOT ADMITTTED",
                              applicationDate = Convert.ToDateTime(app.ApplicationDate).ToString("dd-MMM-yyyy h:mm tt"),
                              paymentDate = app.PaymentDate != null ? Convert.ToDateTime(app.PaymentDate).ToString("dd-MMM-yyyy h:mm tt") : "NOT PAID",
                              submissionDate = app.SubmissionDate != null ? Convert.ToDateTime(app.SubmissionDate).ToString("dd-MMM-yyyy h:mm tt") : "NOT SUBMITTED",
                              ExamDate = app.ExamVenueId > 0 ? _configurationService.GetExamVenue(app.ExamVenueId).ExamDate.ToString("dd-MMM-yyyy h:mm tt") : ""

                          }
                ).ToList();


            var workbook = new HSSFWorkbook();
            var sheet = workbook.CreateSheet("Search Result");
            var rowIndex = 0;
            var row = sheet.CreateRow(rowIndex);
            row.CreateCell(0).SetCellValue("Last Name");
            row.CreateCell(1).SetCellValue("First Name");
            row.CreateCell(2).SetCellValue("Middle Name");
            row.CreateCell(3).SetCellValue("Phone");
            row.CreateCell(4).SetCellValue("Email");
            row.CreateCell(5).SetCellValue("Seat No");
            row.CreateCell(6).SetCellValue("RegN0");
            row.CreateCell(7).SetCellValue("AppNo");
            row.CreateCell(8).SetCellValue("Faculty");
            row.CreateCell(9).SetCellValue("Department");
            row.CreateCell(10).SetCellValue("Course of study");
            row.CreateCell(11).SetCellValue("Program");
            row.CreateCell(12).SetCellValue("Session");
            row.CreateCell(13).SetCellValue("Application Form");
            row.CreateCell(14).SetCellValue("Venue");
            row.CreateCell(15).SetCellValue("Seat No");
            row.CreateCell(16).SetCellValue("Payment Date");
            row.CreateCell(17).SetCellValue("Application Status");
            row.CreateCell(18).SetCellValue("Admission Status");
            row.CreateCell(19).SetCellValue("Application Date");
            row.CreateCell(20).SetCellValue("Submission Date");
            row.CreateCell(21).SetCellValue("Exam Date");
            foreach (var res in result)
            {
                rowIndex++;
                row = sheet.CreateRow(rowIndex);
                row.CreateCell(0).SetCellValue(res.lastName);
                row.CreateCell(1).SetCellValue(res.firstName);
                row.CreateCell(2).SetCellValue(res.middleName);
                row.CreateCell(3).SetCellValue(res.phoneNumber);
                row.CreateCell(4).SetCellValue(res.emailAddress);
                row.CreateCell(5).SetCellValue(res.seatNo);
                row.CreateCell(6).SetCellValue(res.regNo);
                row.CreateCell(7).SetCellValue(res.appNo);
                row.CreateCell(8).SetCellValue(res.faculty);
                row.CreateCell(9).SetCellValue(res.department);
                row.CreateCell(10).SetCellValue(res.course);
                row.CreateCell(11).SetCellValue(res.program);
                row.CreateCell(12).SetCellValue(res.Session);
                row.CreateCell(13).SetCellValue(res.applicationForm);
                row.CreateCell(14).SetCellValue(res.venue);
                row.CreateCell(15).SetCellValue(res.seatNo);
                row.CreateCell(16).SetCellValue(res.paymentDate);
                row.CreateCell(17).SetCellValue(res.applicationStatus);
                row.CreateCell(18).SetCellValue(res.admissionStatus);
                row.CreateCell(19).SetCellValue(res.applicationDate);
                row.CreateCell(20).SetCellValue(res.submissionDate);
                row.CreateCell(21).SetCellValue(res.ExamDate);
            }
            var exportData = new MemoryStream();
            workbook.Write(exportData);

            string filePath = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/Downloads/SearchResult.xls");


            using (var file = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite))
            {
                workbook.Write(file);
            }
            var fileInfo = new FileInfo(filePath);
            string filename = fileInfo.Name;

            return Json(filename, JsonRequestBehavior.AllowGet);

        }
        public ActionResult CreateExcelForAdmissionList(IEnumerable<string> appNums)
        {

            var application = new Application();
            var personalInfo = new PersonalInformation();

            var workbook = new HSSFWorkbook();
            var sheet = workbook.CreateSheet("Admission List");
            var rowIndex = 0;
            var row = sheet.CreateRow(rowIndex);
            row.CreateCell(0).SetCellValue("AppNum");
            row.CreateCell(1).SetCellValue("RegNum");
            row.CreateCell(2).SetCellValue("Last Name");
            row.CreateCell(3).SetCellValue("First Name");
            row.CreateCell(4).SetCellValue("Middle Name");
            row.CreateCell(5).SetCellValue("Program");
            row.CreateCell(6).SetCellValue("Course");
            row.CreateCell(7).SetCellValue("Gender");
            row.CreateCell(8).SetCellValue("Phone Number");
            foreach (var appNum in appNums)
            {
                rowIndex++;
                row = sheet.CreateRow(rowIndex);
                application = _configurationService.GetApplications(appNum) ?? new Application();
                personalInfo = _registrationService.GetPersonalInformationByEmail(application.UserName) ?? new PersonalInformation();

                row.CreateCell(0).SetCellValue(appNum);
                row.CreateCell(1).SetCellValue(application.RegNum);
                row.CreateCell(2).SetCellValue(personalInfo.LastName);
                row.CreateCell(3).SetCellValue(personalInfo.FirstName);
                row.CreateCell(4).SetCellValue(personalInfo.MiddleName);
                row.CreateCell(5).SetCellValue(_configurationService.GetProgram(application.ProgramIdAdmittedInto).Code);
                row.CreateCell(6).SetCellValue(_configurationService.GetCourse(application.CourseIdAdmittedInto).Name);
                row.CreateCell(7).SetCellValue(personalInfo.Gender);
                row.CreateCell(8).SetCellValue(personalInfo.PhoneNumber);
            }
            var exportData = new MemoryStream();
            workbook.Write(exportData);

            string filePath = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/Downloads/AdmittedList.xls");


            using (var file = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite))
            {
                workbook.Write(file);
            }
            var fileInfo = new FileInfo(filePath);
            string filename = fileInfo.Name;

            return Json(filename, JsonRequestBehavior.AllowGet);

        }
        public ActionResult CreateExcelForNonAdmissionList(IEnumerable<string> regNums)
        {

            var workbook = new HSSFWorkbook();
            var sheet = workbook.CreateSheet("Admission List");
            var rowIndex = 0;
            var row = sheet.CreateRow(rowIndex);
            row.CreateCell(0).SetCellValue("RegNum");
            row.CreateCell(1).SetCellValue("Last Name");
            row.CreateCell(2).SetCellValue("First Name");
            row.CreateCell(3).SetCellValue("Middle Name");
            row.CreateCell(4).SetCellValue("Program");
            row.CreateCell(5).SetCellValue("Course");
            row.CreateCell(6).SetCellValue("Phone Number");
            row.CreateCell(7).SetCellValue("Email");
            row.CreateCell(8).SetCellValue("Mode of Entry");
            foreach (var regNum in regNums)
            {
                rowIndex++;
                row = sheet.CreateRow(rowIndex);
                var nonApplicant = _registrationService.GetNonApplicantAdmittedList(regNum);

                row.CreateCell(0).SetCellValue(regNum);
                row.CreateCell(1).SetCellValue(nonApplicant.LastName);
                row.CreateCell(2).SetCellValue(nonApplicant.FirstName);
                row.CreateCell(3).SetCellValue(nonApplicant.MiddleName);
                row.CreateCell(4).SetCellValue(_configurationService.GetProgram(nonApplicant.ProgramId).Code);
                row.CreateCell(5).SetCellValue(_configurationService.GetCourse(nonApplicant.CourseId).Name);
                row.CreateCell(6).SetCellValue(nonApplicant.PhoneNumber);
                row.CreateCell(7).SetCellValue(nonApplicant.Email);
                row.CreateCell(8).SetCellValue(nonApplicant.ModeOfEntry);
            }
            var exportData = new MemoryStream();
            workbook.Write(exportData);

            string filePath = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/Downloads/NonApplicantAdmittedList.xls");


            using (var file = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite))
            {
                workbook.Write(file);
            }
            var fileInfo = new FileInfo(filePath);
            string filename = fileInfo.Name;

            return Json(filename, JsonRequestBehavior.AllowGet);

        }
        public ActionResult DownloadExcel(string file)
        {
            // string filePath = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/Downloads/"+file);
            string fullPath = Path.Combine(Server.MapPath("~/App_Data/Downloads"), file);
            return File(fullPath, "application/vnd.ms-excel", file);
        }
    }
}