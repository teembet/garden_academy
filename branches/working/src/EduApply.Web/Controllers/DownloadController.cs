using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.EnterpriseServices.Internal;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using EduApply.Data.Entities;
using EduApply.Logic.Interfaces;
using EduApply.Logic.Service;
using EduApply.Logic.Utility;
using EduApply.Web.Models;
using Ionic.Zip;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.Util;
using NPOI.XSSF.UserModel;
using EduApply.Logic.Repository;

namespace EduApply.Web.Controllers
{
    [Authorize]
    public class DownloadController : Controller
    {
        private IConfigurationService _configurationService;
        private IApplicationFormRepository _applicationFormRepository;
        private IRegistrationService _registrationService;
        private IAuditTrailRepository _auditTrailRepository;
        private IVenueAssignmentService _venueService;
        private ILocationRepository _locationRepository;
        public DownloadController(IConfigurationService configurationService, IVenueAssignmentService venueService, IApplicationFormRepository applicationFormRepository, IRegistrationService registrationService, IAuditTrailRepository auditTrailRepository, ILocationRepository locationRepository)
        {
            this._configurationService = configurationService;
            this._registrationService = registrationService;
            this._applicationFormRepository = applicationFormRepository;
            this._auditTrailRepository = auditTrailRepository;
            this._venueService = venueService;
            this._locationRepository = locationRepository;
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
            //using(MemoryStream ms = new MemoryStream())
            using (var file = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite))//
            {
                workbook.Write(file);//
                //byte[] bytes = new byte[ms.Length];
                //ms.Read(bytes, 0, (int)file.Length);
                //ms.Write(bytes, 0, (int)file.Length);
                //ms.Close();
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
            row.CreateCell(4).SetCellValue("Program Code");
            row.CreateCell(5).SetCellValue("Course Code");
            row.CreateCell(6).SetCellValue("Gender");
            row.CreateCell(7).SetCellValue("State of origin");
            row.CreateCell(8).SetCellValue("LGA");
            row.CreateCell(9).SetCellValue("English Score");
            row.CreateCell(10).SetCellValue("Subject 2");
            row.CreateCell(11).SetCellValue("Subject 2 score");
            row.CreateCell(12).SetCellValue("Subject 3");
            row.CreateCell(13).SetCellValue("Subject 3 score");
            row.CreateCell(14).SetCellValue("Subject 4");
            row.CreateCell(15).SetCellValue("Subject 4 score");
            row.CreateCell(16).SetCellValue("Total score");
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
                row.CreateCell(4).SetCellValue(jambResult.ProgramCode);
                row.CreateCell(5).SetCellValue(jambResult.CourseCode);
                row.CreateCell(6).SetCellValue(jambResult.Gender);
                row.CreateCell(7).SetCellValue(jambResult.StateOfOrigin);
                row.CreateCell(8).SetCellValue(jambResult.LGA);
                row.CreateCell(9).SetCellValue(jambResult.EngScore);
                row.CreateCell(10).SetCellValue(jambResult.Subject2);
                row.CreateCell(11).SetCellValue(jambResult.Subject2Score);
                row.CreateCell(12).SetCellValue(jambResult.Subject3);
                row.CreateCell(13).SetCellValue(jambResult.Subject3Score);
                row.CreateCell(14).SetCellValue(jambResult.Subject4);
                row.CreateCell(15).SetCellValue(jambResult.Subject4Score);
                row.CreateCell(16).SetCellValue(jambResult.TotalScore);
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
            var tenancy = EngineContext.Resolve<Tenancy>();
            //I am overriding the appNums passed to this method as a quick fix because it only contains appNums currently showing
            //on the grid, since we are using pagination, it doesnt contain appNums of other pages, however Session["SearchAppNos"]
            //contains all AppNums in the search result, i could also remove the appNums parameter totally but for now i am leaving it
            appNums = Session["SearchAppNos"] as List<string>;
            //  int formId = ((SearchModel)Session["searchModel"]).FormId.Value;
            var applications = _registrationService.GetApplications().Where(x => appNums.Contains(x.AppNum)).ToList();
            var usernames = applications.Select(x => x.UserName).ToList();
            var personalInformations = _configurationService.GetPersonalInformations().Where(x => usernames.Contains(x.Email)).ToList();
            // var educationalDetails = _registrationService.GetEducationalDetails().Where(x=>)

            var result = (from app in applications
                          join personalInformation in personalInformations on app.UserName equals personalInformation.Email
                          select new
                          {
                              formId = app.AppFormId,
                              formCategoryId = _applicationFormRepository.GetAppForms(app.AppFormId).FormCategoryId,
                              applicationId = app.Id,
                              lastName = personalInformation.LastName,
                              firstName = personalInformation.FirstName,
                              middleName = personalInformation.MiddleName,
                              phoneNumber = personalInformation.PhoneNumber,
                              address = personalInformation.HomeAddress,
                              gender = personalInformation.Gender,
                              DOB = Convert.ToDateTime(personalInformation.DateOfBirth).ToString("dd-MMM-yy"),
                              emailAddress = personalInformation.Email,
                              country = personalInformation.Nationality,
                              state = personalInformation.StateOfOrigin,
                              LG = personalInformation.LocalGovernment,
                              seatNumber = app.SeatNo,
                              regNo = app.RegNum,
                              appNo = app.AppNum,
                              Session = _configurationService.GetSession(app.SessionId).Name,
                              applicationForm = _applicationFormRepository.GetAppForms(app.AppFormId).Name,
                              JambScore = _registrationService.GetTotalJambScore(app.RegNum),
                              // cgpa=_registrationService.GetEducationalDetails(app.AppFormId),///WORK BY OMO
                              //faculty = app.FacultyId > 0 ? _configurationService.GetFaculty(app.FacultyId).Name : "",
                              //department = app.DepartmentId > 0 ? _configurationService.GetDepartment(app.DepartmentId).Name : "",
                              //course = app.CourseOfStudyId > 0 ? _configurationService.GetCourse(app.CourseOfStudyId).Name : "",
                              //program = app.ProgramId > 0 ? _configurationService.GetProgram(app.ProgramId).Code : "",
                              venue = app.ExamVenueId > 0 ? _venueService.GetExamVenue(app.ExamVenueId).Venue.Name : "",
                              seatNo = app.SeatNo,
                              paymentStatus = app.IsPaid ? "Paid" : "NOT PAID",
                              applicationStatus = app.IsSubmitted ? "SUBMITTED" : "NOT SUBMITTED",
                              admissionStatus = app.IsAdmitted ? "ADMITTED" : "NOT ADMITTTED",
                              applicationDate = Convert.ToDateTime(app.ApplicationDate).ToString("dd-MMM-yyyy h:mm tt"),
                              paymentDate = app.PaymentDate != null ? Convert.ToDateTime(app.PaymentDate).ToString("dd-MMM-yyyy h:mm tt") : "NOT PAID",
                              submissionDate = app.SubmissionDate != null ? Convert.ToDateTime(app.SubmissionDate).ToString("dd-MMM-yyyy h:mm tt") : "NOT SUBMITTED",
                              ExamDate = app.ExamVenueId > 0 ? _venueService.GetExamVenue(app.ExamVenueId).ExamDate.ToString("dd-MMM-yyyy h:mm tt") : ""

                          }
                ).ToList();


            var workbook = new HSSFWorkbook();

            var sheet = workbook.CreateSheet("Search Result");
            var rowIndex = 0;
            var row = sheet.CreateRow(rowIndex);

            if (tenancy.Code == "FUTO")
            {
                row.CreateCell(0).SetCellValue("RegNo");
                row.CreateCell(1).SetCellValue("AppNo");
                row.CreateCell(2).SetCellValue("Last Name");
                row.CreateCell(3).SetCellValue("First Name");
                row.CreateCell(4).SetCellValue("Middle Name");
                row.CreateCell(5).SetCellValue("Address");
                row.CreateCell(6).SetCellValue("Date of Birth");
                row.CreateCell(7).SetCellValue("Gender");
                row.CreateCell(8).SetCellValue("Phone");
                row.CreateCell(9).SetCellValue("Email");
                row.CreateCell(10).SetCellValue("Country");
                row.CreateCell(11).SetCellValue("State");
                row.CreateCell(12).SetCellValue("LGA");
                row.CreateCell(13).SetCellValue("Application Form");
                row.CreateCell(14).SetCellValue("Session");
                row.CreateCell(15).SetCellValue("Faculty");
                row.CreateCell(16).SetCellValue("Department");///WORK TO GET DONE
                row.CreateCell(17).SetCellValue("Program");
                row.CreateCell(18).SetCellValue("Course");
                row.CreateCell(19).SetCellValue("O'Level Result");
                row.CreateCell(20).SetCellValue("Educational Details");
                row.CreateCell(21).SetCellValue("CGPA");
                row.CreateCell(22).SetCellValue("Work Experience");
                row.CreateCell(23).SetCellValue("Jamb Score");
                row.CreateCell(24).SetCellValue("Payment Date");
                row.CreateCell(25).SetCellValue("Application Date");
                row.CreateCell(26).SetCellValue("Application Status");
                row.CreateCell(27).SetCellValue("Exam Date");
                row.CreateCell(28).SetCellValue("Exam Venue");
                row.CreateCell(29).SetCellValue("Seat No");
                row.CreateCell(30).SetCellValue("Admission Status");
                row.CreateCell(31).SetCellValue("Submission Date");
                row.CreateCell(32).SetCellValue("Mode Of Study");
                row.CreateCell(33).SetCellValue("Passport");
            }
            else
            {
                row.CreateCell(0).SetCellValue("RegNo");
                row.CreateCell(1).SetCellValue("AppNo");
                row.CreateCell(2).SetCellValue("Last Name");
                row.CreateCell(3).SetCellValue("First Name");
                row.CreateCell(4).SetCellValue("Middle Name");
                row.CreateCell(5).SetCellValue("Address");
                row.CreateCell(6).SetCellValue("Date of Birth");
                row.CreateCell(7).SetCellValue("Gender");
                row.CreateCell(8).SetCellValue("Phone");
                row.CreateCell(9).SetCellValue("Email");
                row.CreateCell(10).SetCellValue("Country");
                row.CreateCell(11).SetCellValue("State");
                row.CreateCell(12).SetCellValue("LGA");
                row.CreateCell(13).SetCellValue("Application Form");
                row.CreateCell(14).SetCellValue("Session");
                row.CreateCell(15).SetCellValue("Faculty");
                row.CreateCell(16).SetCellValue("Department");
                row.CreateCell(17).SetCellValue("Program");
                row.CreateCell(18).SetCellValue("Course");
                row.CreateCell(19).SetCellValue("O'Level Result");
                row.CreateCell(20).SetCellValue("Educational Details");
                //row.CreateCell(21).SetCellValue("CGPA");
                row.CreateCell(21).SetCellValue("Work Experience");
                row.CreateCell(22).SetCellValue("Jamb Score");
                row.CreateCell(23).SetCellValue("Payment Date");
                row.CreateCell(24).SetCellValue("Application Date");
                row.CreateCell(25).SetCellValue("Application Status");
                row.CreateCell(26).SetCellValue("Exam Date");
                row.CreateCell(27).SetCellValue("Exam Venue");
                row.CreateCell(28).SetCellValue("Seat No");
                row.CreateCell(29).SetCellValue("Admission Status");
                row.CreateCell(30).SetCellValue("Submission Date");
                row.CreateCell(31).SetCellValue("Passport");
            }

            int y1 = 1;
            int y2 = 1;
            foreach (var res in result)
            {

                string progCourse = "";
                string facultyName = "";
                string departmentName = "";
                string appnum = res.appNo;
                string regnum = "";
                string CourseTitle = "";
                string cgpaValue = "";
                string modeOfStudy = "";
                //string Address = "";
                var selectedItem = res;


                string applicantsOLevelResult;
                string educationalDetails;
                string workEperience;
                Country country;
                State state;
                LocalGovernmentArea lga;
                Picture studentPicture;
                if (res.formCategoryId == 1)
                {
                    var applicantsProgramCourse = _registrationService.GetApplicantsProgramCourses(selectedItem.applicationId).ToList();
                    if (applicantsProgramCourse.Any())
                    {
                        var firstProgramCourse = applicantsProgramCourse.FirstOrDefault();
                        if (firstProgramCourse != null)
                        {
                            var department = _configurationService.GetCourse(firstProgramCourse.CourseId).Department;
                            var faculty = department.Faculty;
                            var educationalDetail = _registrationService.GetEducationalDetails(res.applicationId).FirstOrDefault();
                            facultyName = faculty.Name;
                            departmentName = department.Name;
                            if (educationalDetail == null)
                            {
                                applicantsOLevelResult = GetOLevelResultDetails(selectedItem.applicationId);
                                educationalDetails = GetEducationalDetails(selectedItem.applicationId);
                                workEperience = GetWorkExperience(selectedItem.applicationId);
                                country = _locationRepository.GetCountry(selectedItem.country);
                                state = _locationRepository.GetState(selectedItem.state);
                                lga = _locationRepository.GetLocalGovernmentArea(selectedItem.LG);
                                rowIndex++;
                                row = sheet.CreateRow(rowIndex);

                                if (tenancy.Code == "FUTO")
                                {
                                    row.CreateCell(0).SetCellValue(res.regNo);
                                    row.CreateCell(1).SetCellValue(appnum);
                                    row.CreateCell(2).SetCellValue(res.lastName);
                                    row.CreateCell(3).SetCellValue(res.firstName);
                                    row.CreateCell(4).SetCellValue(res.middleName);
                                    row.CreateCell(5).SetCellValue(res.address);
                                    row.CreateCell(6).SetCellValue(res.DOB.ToString());
                                    row.CreateCell(7).SetCellValue(res.gender);
                                    row.CreateCell(8).SetCellValue(res.phoneNumber);
                                    row.CreateCell(9).SetCellValue(res.emailAddress);
                                    row.CreateCell(10).SetCellValue(country != null ? country.Name : "N/A");
                                    row.CreateCell(11).SetCellValue(state != null ? state.Name : "N/A");
                                    row.CreateCell(12).SetCellValue(lga != null ? lga.Name : "N/A");
                                    row.CreateCell(13).SetCellValue(res.applicationForm);
                                    row.CreateCell(14).SetCellValue(res.Session);
                                    row.CreateCell(15).SetCellValue(facultyName);
                                    row.CreateCell(16).SetCellValue(departmentName);
                                    row.CreateCell(17).SetCellValue(progCourse);
                                    row.CreateCell(18).SetCellValue(CourseTitle);
                                    row.CreateCell(19).SetCellValue(applicantsOLevelResult);
                                    row.CreateCell(20).SetCellValue(educationalDetails);
                                    row.CreateCell(21).SetCellValue(cgpaValue);
                                    row.CreateCell(22).SetCellValue(workEperience);
                                    row.CreateCell(23).SetCellValue(res.JambScore);
                                    row.CreateCell(24).SetCellValue(res.paymentDate);
                                    row.CreateCell(25).SetCellValue(res.applicationDate);
                                    row.CreateCell(26).SetCellValue(res.applicationStatus);
                                    row.CreateCell(27).SetCellValue(res.ExamDate);
                                    row.CreateCell(28).SetCellValue(res.venue);
                                    row.CreateCell(29).SetCellValue(res.seatNo);
                                    row.CreateCell(30).SetCellValue(res.admissionStatus);
                                    row.CreateCell(31).SetCellValue(res.submissionDate);
                                    row.CreateCell(32).SetCellValue(modeOfStudy.Replace('_', ' '));
                                    row.CreateCell(33).Row.Height = 1600;
                                }
                                else
                                {
                                    row.CreateCell(0).SetCellValue(res.regNo);
                                    row.CreateCell(1).SetCellValue(appnum);
                                    row.CreateCell(2).SetCellValue(res.lastName);
                                    row.CreateCell(3).SetCellValue(res.firstName);
                                    row.CreateCell(4).SetCellValue(res.middleName);
                                    row.CreateCell(5).SetCellValue(res.address);
                                    row.CreateCell(6).SetCellValue(res.DOB.ToString());
                                    row.CreateCell(7).SetCellValue(res.gender);
                                    row.CreateCell(8).SetCellValue(res.phoneNumber);
                                    row.CreateCell(9).SetCellValue(res.emailAddress);
                                    row.CreateCell(10).SetCellValue(country != null ? country.Name : "N/A");
                                    row.CreateCell(11).SetCellValue(state != null ? state.Name : "N/A");
                                    row.CreateCell(12).SetCellValue(lga != null ? lga.Name : "N/A");
                                    row.CreateCell(13).SetCellValue(res.applicationForm);
                                    row.CreateCell(14).SetCellValue(res.Session);
                                    row.CreateCell(15).SetCellValue(facultyName);
                                    row.CreateCell(16).SetCellValue(departmentName);
                                    row.CreateCell(17).SetCellValue(progCourse);
                                    row.CreateCell(18).SetCellValue(CourseTitle);
                                    row.CreateCell(19).SetCellValue(applicantsOLevelResult);
                                    row.CreateCell(20).SetCellValue(educationalDetails);
                                    //row.CreateCell(21).SetCellValue(cgpaValue);
                                    row.CreateCell(21).SetCellValue(workEperience);
                                    row.CreateCell(22).SetCellValue(res.JambScore);
                                    row.CreateCell(23).SetCellValue(res.paymentDate);
                                    row.CreateCell(24).SetCellValue(res.applicationDate);
                                    row.CreateCell(25).SetCellValue(res.applicationStatus);
                                    row.CreateCell(26).SetCellValue(res.ExamDate);
                                    row.CreateCell(27).SetCellValue(res.venue);
                                    row.CreateCell(28).SetCellValue(res.seatNo);
                                    row.CreateCell(29).SetCellValue(res.admissionStatus);
                                    row.CreateCell(30).SetCellValue(res.submissionDate);
                                    row.CreateCell(31).Row.Height = 1600;
                                }

                                sheet.SetColumnWidth(12, 7000);
                                sheet.SetColumnWidth(21, 4500);

                                //row.RowStyle = workbook.CreateCellStyle();
                                ////row.RowStyle.Alignment = HorizontalAlignment.Center;
                                //row.RowStyle.VerticalAlignment = VerticalAlignment.Top;
                                //row.RowStyle.WrapText = true;



                                studentPicture = _registrationService.GetPictureDetails(selectedItem.applicationId);
                                if (studentPicture != null)
                                {
                                    try
                                    {
                                        /* Create a Workbook and Worksheet */
                                        string folderPath = System.IO.Path.Combine(Server.MapPath("~"), "images/StudentPassport/Thumbnails");
                                        //grab the image file
                                        string imagesPath = System.IO.Path.Combine(folderPath, studentPicture.Name);

                                        //create an image from the path
                                        System.Drawing.Image image = System.Drawing.Image.FromFile(imagesPath);
                                        MemoryStream ms = new MemoryStream();
                                        //pull the memory stream from the image (I need this for the byte array later)
                                        image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                                        //the drawing patriarch will hold the anchor and the master information
                                        var patriarch = sheet.CreateDrawingPatriarch();
                                        //store the coordinates of which cell and where in the cell the image goes

                                        int intx1;
                                        int inty1;
                                        int intx2;
                                        int inty2;
                                        if (tenancy.Code == "FUTO")
                                        {
                                            intx1 = 33;
                                            inty1 = y1;
                                            intx2 = 34;
                                            inty2 = y2;
                                        }
                                        else
                                        {
                                            intx1 = 31;
                                            inty1 = y1;
                                            intx2 = 32;
                                            inty2 = y2;

                                        }


                                        HSSFClientAnchor anchor = new HSSFClientAnchor(20, 0, 40, 20, intx1, inty1, intx2, inty2);
                                        //types are 0, 2, and 3. 0 resizes within the cell, 2 doesn’t
                                        anchor.AnchorType = 2;
                                        //add the byte array and encode it for the excel file
                                        int index = workbook.AddPicture(ms.ToArray(), NPOI.SS.UserModel.PictureType.JPEG);
                                        var signaturePicture = patriarch.CreatePicture(anchor, index);
                                        /* Call resize method, which resizes the image */
                                        signaturePicture.Resize(0.7);

                                        var style = workbook.CreateCellStyle();
                                        style.WrapText = true;
                                        style.VerticalAlignment = VerticalAlignment.Top;
                                        sheet.SetDefaultColumnStyle(12, style);
                                    }
                                    catch (Exception)
                                    {


                                    }
                                }

                                y1++;
                                y2++;
                                continue;
                            }
                            else
                            {
                                cgpaValue = educationalDetail.CGPA;
                            }
                            if (firstProgramCourse.ModeOfStudyId == null)
                            {
                                modeOfStudy = Enum.GetName(typeof(ModeOfStudy), 0);
                            }
                            else
                            {
                                modeOfStudy = Enum.GetName(typeof(ModeOfStudy), firstProgramCourse.ModeOfStudyId);
                            }



                            //regnum = _configurationService.GetPersonalInformations().Select(x=>x.RegNum).Where(x => usernames.Contains(x.Email)).ToString();
                            //regnum = result.Where(x => x.regNo.Contains(x.emailAddress)).ToString();
                        }
                        foreach (var appPc in applicantsProgramCourse)
                        {
                            var programCode = _configurationService.GetProgram(appPc.ProgramId).Code;
                            var courseName = _configurationService.GetCourse(appPc.CourseId).Name;
                            var educationalDetail = _registrationService.GetEducationalDetails(res.applicationId).FirstOrDefault();

                            progCourse = programCode;
                            CourseTitle = courseName;
                            cgpaValue = educationalDetail.CGPA;
                            if (firstProgramCourse.ModeOfStudyId == null)
                            {
                                modeOfStudy = Enum.GetName(typeof(ModeOfStudy), 0);
                            }
                            else
                            {
                                modeOfStudy = Enum.GetName(typeof(ModeOfStudy), firstProgramCourse.ModeOfStudyId);
                            }
                            //progCourse += "(" + programCode + ") " + courseName + ",";
                        }

                        //if (!string.IsNullOrEmpty(progCourse))
                        //{
                        //    var lastIndexOfComma = progCourse.LastIndexOf(',');
                        //    progCourse = progCourse.Remove(lastIndexOfComma);
                        //}

                    }
                    else
                    {
                        var nonApplicantProgramCourse = _registrationService.GetNonApplicantAdmittedList(selectedItem.regNo);
                        if (nonApplicantProgramCourse != null)
                        {
                            var department = _configurationService.GetCourse(nonApplicantProgramCourse.CourseId).Department;
                            var faculty = department.Faculty;
                            var educationalDetail = _registrationService.GetEducationalDetails(res.applicationId).FirstOrDefault();

                            facultyName = faculty.Name;
                            departmentName = department.Name;
                            // regnum = result.Where(x => x.regNo.Contains(x.emailAddress)).ToString();

                            var programCode = _configurationService.GetProgram(nonApplicantProgramCourse.ProgramId).Code;
                            var courseName = _configurationService.GetCourse(nonApplicantProgramCourse.CourseId).Name;
                            progCourse = programCode;
                            CourseTitle = courseName;
                            cgpaValue = educationalDetail.CGPA;
                            //Address = res.address;
                            //progCourse += "(" + programCode + ") " + courseName;
                        }
                    }
                }
                else
                {

                    var program = new Program();
                    var courseOfStudy = new Course();
                    var faculty = new Faculty();
                    var application = new Application();
                    var nonApplicantAdmittedEntry = new NonApplicantAdmittedList();

                    var mappedToFormIdz = _configurationService.GetMappedForms(res.formId).Select(x => x.MappedFormId).ToArray();
                    //   var mappedFormId = currentAapplicationForm.MappedToFormId;
                    if (mappedToFormIdz.Any())
                    {
                        foreach (var mappedFormId in mappedToFormIdz)
                        {
                            //first we check for applicant
                            //first we assume appNumOrRegNum is application number 
                            if (string.IsNullOrEmpty(res.regNo))
                            {
                                application = _configurationService.GetApplicationByEmailAndFormId(res.emailAddress, mappedFormId);
                                if (application != null)
                                {
                                    if (application.ProgramIdAdmittedInto > 0 && application.CourseIdAdmittedInto > 0)
                                    {
                                        appnum = application.AppNum;
                                        program = _configurationService.GetProgram(application.ProgramIdAdmittedInto);
                                        courseOfStudy = _configurationService.GetCourse(application.CourseIdAdmittedInto);
                                        faculty = courseOfStudy.Department.Faculty;
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                //next we assume appNumOrRegNum is a registration number
                                if (!string.IsNullOrEmpty(res.regNo))
                                {
                                    application = _registrationService.GetAdmittedApplicationByRegNumAndFormId(res.regNo, mappedFormId);
                                    if (application != null)
                                    {

                                        if (application.ProgramIdAdmittedInto > 0)
                                        {
                                            appnum = application.AppNum;
                                            program = _configurationService.GetProgram(application.ProgramIdAdmittedInto);
                                            courseOfStudy = _configurationService.GetCourse(application.CourseIdAdmittedInto);
                                            faculty = courseOfStudy.Department.Faculty;
                                        }
                                        else
                                        {
                                            nonApplicantAdmittedEntry = _registrationService.GetNonApplicantAdmittedList(res.regNo, mappedFormId);
                                            if (nonApplicantAdmittedEntry != null)
                                            {
                                                program = _configurationService.GetProgram(nonApplicantAdmittedEntry.ProgramId);
                                                courseOfStudy = _configurationService.GetCourse(nonApplicantAdmittedEntry.CourseId);
                                                faculty = courseOfStudy.Department.Faculty;
                                            }
                                        }
                                        break;
                                    }
                                    //finally we check if application is null, if it is then no application record was found while we assumed it appNumOrRegNum was for ann applicant
                                    //therefore we would now check non applicantAdmittedList
                                    nonApplicantAdmittedEntry = _registrationService.GetNonApplicantAdmittedList(res.regNo, mappedFormId);
                                    if (nonApplicantAdmittedEntry != null)
                                    {
                                        application = new Application();
                                        var programIdAdmittedInto = nonApplicantAdmittedEntry.ProgramId;
                                        var courseIdAdmittedInto = nonApplicantAdmittedEntry.CourseId;
                                        program = _configurationService.GetProgram(programIdAdmittedInto);
                                        courseOfStudy = _configurationService.GetCourse(courseIdAdmittedInto);
                                        faculty = courseOfStudy.Department.Faculty;
                                        application.ModeOfEntry = nonApplicantAdmittedEntry.ModeOfEntry;
                                        break;
                                    }
                                }
                            }

                        }

                        progCourse += program != null && courseOfStudy != null ? program.Code : "";
                        //progCourse += program != null && courseOfStudy != null ? "(" + program.Code + ") " + courseOfStudy.Name : "";
                        facultyName = faculty != null ? faculty.Name : "";
                        departmentName = courseOfStudy.Department != null ? courseOfStudy.Department.Name : "";
                        CourseTitle = courseOfStudy.Name;
                        //Address = res.address;
                    }
                    else
                    {
                        ViewBag.message = "An Error occured, please try again later";
                        return View("ApplicationError");
                    }
                }

                //if (string.IsNullOrWhiteSpace(facultyName) && string.IsNullOrWhiteSpace(departmentName) && formId != -1)
                //{
                //    _applicationFormRepository.GetProgramFacultyCourse(selectedItem.regNo, formId, out progCourse, out facultyName, out departmentName);
                //}

                
                applicantsOLevelResult = GetOLevelResultDetails(selectedItem.applicationId);
                educationalDetails = GetEducationalDetails(selectedItem.applicationId);
                workEperience = GetWorkExperience(selectedItem.applicationId);
                country = _locationRepository.GetCountry(selectedItem.country);
                state = _locationRepository.GetState(selectedItem.state);
                lga = _locationRepository.GetLocalGovernmentArea(selectedItem.LG);
                rowIndex++;
                row = sheet.CreateRow(rowIndex);

                if (tenancy.Code == "FUTO")
                {
                    row.CreateCell(0).SetCellValue(res.regNo);
                    row.CreateCell(1).SetCellValue(appnum);
                    row.CreateCell(2).SetCellValue(res.lastName);
                    row.CreateCell(3).SetCellValue(res.firstName);
                    row.CreateCell(4).SetCellValue(res.middleName);
                    row.CreateCell(5).SetCellValue(res.address);
                    row.CreateCell(6).SetCellValue(res.DOB.ToString());
                    row.CreateCell(7).SetCellValue(res.gender);
                    row.CreateCell(8).SetCellValue(res.phoneNumber);
                    row.CreateCell(9).SetCellValue(res.emailAddress);
                    row.CreateCell(10).SetCellValue(country != null ? country.Name : "N/A");
                    row.CreateCell(11).SetCellValue(state != null ? state.Name : "N/A");
                    row.CreateCell(12).SetCellValue(lga != null ? lga.Name : "N/A");
                    row.CreateCell(13).SetCellValue(res.applicationForm);
                    row.CreateCell(14).SetCellValue(res.Session);
                    row.CreateCell(15).SetCellValue(facultyName);
                    row.CreateCell(16).SetCellValue(departmentName);
                    row.CreateCell(17).SetCellValue(progCourse);
                    row.CreateCell(18).SetCellValue(CourseTitle);
                    row.CreateCell(19).SetCellValue(applicantsOLevelResult);
                    row.CreateCell(20).SetCellValue(educationalDetails);
                    row.CreateCell(21).SetCellValue(cgpaValue);
                    row.CreateCell(22).SetCellValue(workEperience);
                    row.CreateCell(23).SetCellValue(res.JambScore);
                    row.CreateCell(24).SetCellValue(res.paymentDate);
                    row.CreateCell(25).SetCellValue(res.applicationDate);
                    row.CreateCell(26).SetCellValue(res.applicationStatus);
                    row.CreateCell(27).SetCellValue(res.ExamDate);
                    row.CreateCell(28).SetCellValue(res.venue);
                    row.CreateCell(29).SetCellValue(res.seatNo);
                    row.CreateCell(30).SetCellValue(res.admissionStatus);
                    row.CreateCell(31).SetCellValue(res.submissionDate);
                    row.CreateCell(32).SetCellValue(modeOfStudy.Replace('_', ' '));
                    row.CreateCell(33).Row.Height = 1600;
                }
                else
                {
                    row.CreateCell(0).SetCellValue(res.regNo);
                    row.CreateCell(1).SetCellValue(appnum);
                    row.CreateCell(2).SetCellValue(res.lastName);
                    row.CreateCell(3).SetCellValue(res.firstName);
                    row.CreateCell(4).SetCellValue(res.middleName);
                    row.CreateCell(5).SetCellValue(res.address);
                    row.CreateCell(6).SetCellValue(res.DOB.ToString());
                    row.CreateCell(7).SetCellValue(res.gender);
                    row.CreateCell(8).SetCellValue(res.phoneNumber);
                    row.CreateCell(9).SetCellValue(res.emailAddress);
                    row.CreateCell(10).SetCellValue(country != null ? country.Name : "N/A");
                    row.CreateCell(11).SetCellValue(state != null ? state.Name : "N/A");
                    row.CreateCell(12).SetCellValue(lga != null ? lga.Name : "N/A");
                    row.CreateCell(13).SetCellValue(res.applicationForm);
                    row.CreateCell(14).SetCellValue(res.Session);
                    row.CreateCell(15).SetCellValue(facultyName);
                    row.CreateCell(16).SetCellValue(departmentName);
                    row.CreateCell(17).SetCellValue(progCourse);
                    row.CreateCell(18).SetCellValue(CourseTitle);
                    row.CreateCell(19).SetCellValue(applicantsOLevelResult);
                    row.CreateCell(20).SetCellValue(educationalDetails);
                    //row.CreateCell(21).SetCellValue(cgpaValue);
                    row.CreateCell(21).SetCellValue(workEperience);
                    row.CreateCell(22).SetCellValue(res.JambScore);
                    row.CreateCell(23).SetCellValue(res.paymentDate);
                    row.CreateCell(24).SetCellValue(res.applicationDate);
                    row.CreateCell(25).SetCellValue(res.applicationStatus);
                    row.CreateCell(26).SetCellValue(res.ExamDate);
                    row.CreateCell(27).SetCellValue(res.venue);
                    row.CreateCell(28).SetCellValue(res.seatNo);
                    row.CreateCell(29).SetCellValue(res.admissionStatus);
                    row.CreateCell(30).SetCellValue(res.submissionDate);
                    row.CreateCell(31).Row.Height = 1600;
                }

                sheet.SetColumnWidth(12, 7000);
                sheet.SetColumnWidth(21, 4500);

                //row.RowStyle = workbook.CreateCellStyle();
                ////row.RowStyle.Alignment = HorizontalAlignment.Center;
                //row.RowStyle.VerticalAlignment = VerticalAlignment.Top;
                //row.RowStyle.WrapText = true;



                studentPicture = _registrationService.GetPictureDetails(selectedItem.applicationId);
                if (studentPicture != null)
                {
                    try
                    {
                        /* Create a Workbook and Worksheet */
                        string folderPath = System.IO.Path.Combine(Server.MapPath("~"), "images/StudentPassport/Thumbnails");
                        //grab the image file
                        string imagesPath = System.IO.Path.Combine(folderPath, studentPicture.Name);

                        //create an image from the path
                        System.Drawing.Image image = System.Drawing.Image.FromFile(imagesPath);
                        MemoryStream ms = new MemoryStream();
                        //pull the memory stream from the image (I need this for the byte array later)
                        image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                        //the drawing patriarch will hold the anchor and the master information
                        var patriarch = sheet.CreateDrawingPatriarch();
                        //store the coordinates of which cell and where in the cell the image goes

                        int intx1;
                        int inty1;
                        int intx2;
                        int inty2;
                        if (tenancy.Code == "FUTO")
                        {
                            intx1 = 33;
                            inty1 = y1;
                            intx2 = 34;
                            inty2 = y2;
                        }
                        else
                        {
                            intx1 = 31;
                            inty1 = y1;
                            intx2 = 32;
                            inty2 = y2;

                        }


                        HSSFClientAnchor anchor = new HSSFClientAnchor(20, 0, 40, 20, intx1, inty1, intx2, inty2);
                        //types are 0, 2, and 3. 0 resizes within the cell, 2 doesn’t
                        anchor.AnchorType = 2;
                        //add the byte array and encode it for the excel file
                        int index = workbook.AddPicture(ms.ToArray(), NPOI.SS.UserModel.PictureType.JPEG);
                        var signaturePicture = patriarch.CreatePicture(anchor, index);
                        /* Call resize method, which resizes the image */
                        signaturePicture.Resize(0.7);

                        var style = workbook.CreateCellStyle();
                        style.WrapText = true;
                        style.VerticalAlignment = VerticalAlignment.Top;
                        sheet.SetDefaultColumnStyle(12, style);
                    }
                    catch (Exception)
                    {


                    }
                }

                y1++;
                y2++;

            }
            var exportData = new MemoryStream();
            workbook.Write(exportData);
            var excelName = tenancy.Code + "-SearchResult.xls";
            string filePath = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/Downloads/" + excelName);

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

            //I am overriding the appNums passed to this method as a quick fix because it only contains appNums currently showing
            //on the grid, since we are using pagination, it doesnt contain appNums of other pages, however Session["SearchAppNos"]
            //contains all AppNums in the search result, i could also remove the appNums parameter totally but for now i am leaving it
            appNums = Session["AdmittedAppNos"] as List<string>;
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
            //I am overriding the appNums passed to this method as a quick fix because it only contains appNums currently showing
            //on the grid, since we are using pagination, it doesnt contain appNums of other pages, however Session["SearchAppNos"]
            //contains all AppNums in the search result, i could also remove the appNums parameter totally but for now i am leaving it
            regNums = Session["AdmittedRegNums"] as List<string>;
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
            string fullPath = Path.Combine(Server.MapPath("~/App_Data/Downloads"), file);
            return File(fullPath, "application/vnd.ms-excel", file);
        }
        public ActionResult DownloadZip(string fileName)
        {
            string fullPath = Path.Combine(Server.MapPath("~/App_Data/ZipFiles"), fileName);
            return File(fullPath, "application/zip", fileName);
        }
        public string GetEducationalDetails(long applicationId)
        {
            var eduStringBuilder = new StringBuilder();
            var educationaDetails = _registrationService.GetEducationalDetails(applicationId).ToList();
            if (educationaDetails.Any())
            {
                int length = educationaDetails.Count;
                for (int i = 0; i < length; i++)
                {
                    if (length > 1)
                    {
                        switch (i + 1)
                        {
                            case 1:
                                eduStringBuilder.AppendLine("FIRST DETAIL:");
                                break;
                            case 2:
                                eduStringBuilder.AppendLine("SECOND DETAIL:");
                                break;
                            case 3:
                                eduStringBuilder.AppendLine("THIRD DETAIL:");
                                break;
                        }
                    }
                    var detail = educationaDetails[i];
                    eduStringBuilder.Append(detail.SchoolName + ", " + detail.Qualification + "(" +
                                            detail.ClassOfDegree + "). " + detail.GraduationMonth + "-" + detail.GraduationYear);
                }
            }
            return eduStringBuilder.ToString();
        }

        public string GetWorkExperience(long applicationId)
        {
            var weStringBuilder = new StringBuilder();
            var workExperience = _registrationService.GetWorkExperience(applicationId).ToList();
            if (workExperience.Any())
            {
                int length = workExperience.Count;
                for (int i = 0; i < length; i++)
                {
                    if (length > 1)
                    {
                        switch (i + 1)
                        {
                            case 1:
                                weStringBuilder.AppendLine("FIRST DETAIL:");
                                break;
                            case 2:
                                weStringBuilder.AppendLine(" SECOND DETAIL:");
                                break;
                            case 3:
                                weStringBuilder.AppendLine("THIRD DETAIL:");
                                break;
                        }
                    }
                    var detail = workExperience[i];
                    var toDate = detail.ToDate != null ? Convert.ToDateTime(detail.ToDate).ToString("dd-MMM-yy") : "TILL DATE";
                    weStringBuilder.Append(detail.Organization + ", From: " + Convert.ToDateTime(detail.FromDate).ToString("dd-MMM-yy") + " To: " +
                        toDate + ". " + detail.Position);
                }
            }
            return weStringBuilder.ToString();
        }
        public string GetOLevelResultDetails(long applicationId)
        {
            var oLevelResultDetails = new List<OLevelResultDetailsPreview>();
            var myStringBuilder = new StringBuilder();

            var oLevelDetails = _registrationService.GetOLevelDetails(applicationId).ToList();
            if (oLevelDetails.Any())
            {
                int counter = 1;

                var grades = _registrationService.GetOLevelGrades().ToList();
                var subjects = _registrationService.GetOLevelSubjects().ToList();
                foreach (var detail in oLevelDetails)
                {
                    if (oLevelDetails.Count > 1)
                    {
                        if (counter == 1)
                        {
                            myStringBuilder.Append("FIRST SITTING:");
                        }
                        if (counter == 2)
                        {
                            myStringBuilder.Append("SECOND SITTING:");
                        }

                    }
                    myStringBuilder.Append(" Candidate Name: " + detail.CandidateName);
                    myStringBuilder.Append(" Exam No: " + detail.ExamNumber);
                    myStringBuilder.Append(" Exam Type: " + detail.ExamType);
                    myStringBuilder.Append(" Year: " + detail.Year);
                    myStringBuilder.Append(" Result: ");
                    var oLevelResultDetailsPreview = new OLevelResultDetailsPreview()
                    {
                        CandidateName = detail.CandidateName,
                        //SchoolName = detail.SchoolName,
                        ExamNumber = detail.ExamNumber,
                        ExamType = detail.ExamType,
                        Year = detail.Year
                    };
                    var subjectResults = _registrationService.GetOLevelResults(detail.Id).ToList();
                    for (int i = 0; i < subjectResults.Count; i++)
                    {
                        if (i == 0)
                        {
                            oLevelResultDetailsPreview.Subject1 = subjects.FirstOrDefault(x => x.Id == subjectResults[i].SubjectId).Name;
                            oLevelResultDetailsPreview.Grade1 = grades.FirstOrDefault(x => x.Id == subjectResults[i].GradeId).Name;
                            myStringBuilder.Append(" " + oLevelResultDetailsPreview.Subject1);
                            myStringBuilder.Append("(" + oLevelResultDetailsPreview.Grade1 + ") ");
                        }
                        if (i == 1)
                        {
                            oLevelResultDetailsPreview.Subject2 = subjects.FirstOrDefault(x => x.Id == subjectResults[i].SubjectId).Name;
                            oLevelResultDetailsPreview.Grade2 = grades.FirstOrDefault(x => x.Id == subjectResults[i].GradeId).Name;
                            myStringBuilder.Append(" " + oLevelResultDetailsPreview.Subject2);
                            myStringBuilder.Append("(" + oLevelResultDetailsPreview.Grade2 + ")");
                        }
                        if (i == 2)
                        {
                            oLevelResultDetailsPreview.Subject3 = subjects.FirstOrDefault(x => x.Id == subjectResults[i].SubjectId).Name;
                            oLevelResultDetailsPreview.Grade3 = grades.FirstOrDefault(x => x.Id == subjectResults[i].GradeId).Name;
                            myStringBuilder.Append(" " + oLevelResultDetailsPreview.Subject3);
                            myStringBuilder.Append("(" + oLevelResultDetailsPreview.Grade3 + ")");
                        }
                        if (i == 3)
                        {
                            oLevelResultDetailsPreview.Subject4 = subjects.FirstOrDefault(x => x.Id == subjectResults[i].SubjectId).Name;
                            oLevelResultDetailsPreview.Grade4 = grades.FirstOrDefault(x => x.Id == subjectResults[i].GradeId).Name;
                            myStringBuilder.Append(" " + oLevelResultDetailsPreview.Subject4);
                            myStringBuilder.Append("(" + oLevelResultDetailsPreview.Grade4 + ")");
                        }
                        if (i == 4)
                        {
                            oLevelResultDetailsPreview.Subject5 = subjects.FirstOrDefault(x => x.Id == subjectResults[i].SubjectId).Name;
                            oLevelResultDetailsPreview.Grade5 = grades.FirstOrDefault(x => x.Id == subjectResults[i].GradeId).Name;
                            myStringBuilder.Append(" " + oLevelResultDetailsPreview.Subject5);
                            myStringBuilder.Append("(" + oLevelResultDetailsPreview.Grade5 + ")");
                        }
                        if (i == 5)
                        {
                            oLevelResultDetailsPreview.Subject6 = subjects.FirstOrDefault(x => x.Id == subjectResults[i].SubjectId).Name;
                            oLevelResultDetailsPreview.Grade6 = grades.FirstOrDefault(x => x.Id == subjectResults[i].GradeId).Name;
                            myStringBuilder.Append(" " + oLevelResultDetailsPreview.Subject6);
                            myStringBuilder.Append("(" + oLevelResultDetailsPreview.Grade6 + ")");
                        }
                        if (i == 6)
                        {
                            oLevelResultDetailsPreview.Subject7 = subjects.FirstOrDefault(x => x.Id == subjectResults[i].SubjectId).Name;
                            oLevelResultDetailsPreview.Grade7 = grades.FirstOrDefault(x => x.Id == subjectResults[i].GradeId).Name;
                            myStringBuilder.Append(" " + oLevelResultDetailsPreview.Subject7);
                            myStringBuilder.Append("(" + oLevelResultDetailsPreview.Grade7 + ")");
                        }
                        if (i == 7)
                        {
                            oLevelResultDetailsPreview.Subject8 = subjects.FirstOrDefault(x => x.Id == subjectResults[i].SubjectId).Name;
                            oLevelResultDetailsPreview.Grade8 = grades.FirstOrDefault(x => x.Id == subjectResults[i].GradeId).Name;
                            myStringBuilder.Append(" " + oLevelResultDetailsPreview.Subject8);
                            myStringBuilder.Append("(" + oLevelResultDetailsPreview.Grade8 + ")");
                        }
                        if (i == 8)
                        {
                            oLevelResultDetailsPreview.Subject9 = subjects.FirstOrDefault(x => x.Id == subjectResults[i].SubjectId).Name;
                            oLevelResultDetailsPreview.Grade9 = grades.FirstOrDefault(x => x.Id == subjectResults[i].GradeId).Name;
                            myStringBuilder.Append(" " + oLevelResultDetailsPreview.Subject9);
                            myStringBuilder.Append("(" + oLevelResultDetailsPreview.Grade9 + ")");
                        }
                    }
                    oLevelResultDetails.Add(oLevelResultDetailsPreview);
                    counter++;
                }
            }
            return myStringBuilder.ToString();
        }

        public ActionResult DownloadUploads(long applicationId)
        {
            var fileName = "StudentsUpload.zip";
            var picture = _registrationService.GetPictureDetails(applicationId);
            var certificates = _registrationService.GetCertificates(applicationId).ToList();
            if (picture == null && !certificates.Any())
            {
                return RedirectToAction("SearchErrorHandler", "Search", new { errorMessage = "UPLOAD EMPTY" });
            }
            using (ZipFile zip = new ZipFile())
            {
                if (picture != null)
                {
                    var passportPath = picture.PictureUrl + picture.Name;
                    zip.AddFile(passportPath, "Passport");
                }

                foreach (var cert in certificates)
                {
                    string certificatePath = cert.CertificateUrl;
                    zip.AddFile(certificatePath, "Certificates");
                }
                var zipfileFolderPath = Server.MapPath("/App_Data/ZipFiles");
                zip.Save(Path.Combine(zipfileFolderPath, "StudentsUpload.zip"));
            }
            string fullPath = Path.Combine(Server.MapPath("~/App_Data/ZipFiles"), fileName);
            var fileInfo = new FileInfo(fullPath);
            string filename = fileInfo.Name;

            //string fullPath = Path.Combine(Server.MapPath("~/App_Data/Downloads"), file);
            return File(fullPath, "application/vnd.ms-excel", filename);

            //  return Json(filename, JsonRequestBehavior.AllowGet);
        }
    }
}