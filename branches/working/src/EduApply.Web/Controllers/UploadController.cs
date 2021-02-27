using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using AutoMapper;
using CsvHelper;
using CsvHelper.TypeConversion;
using EduApply.Logic.Interfaces;
using EduApply.Logic.Service;
using EduApply.Logic.Utility;
using EduApply.Web.Models;
using EduApply.Data.Entities;
using iTextSharp.text;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace EduApply.Web.Controllers
{
    [Authorize(Roles = "Admin, SchoolAdmin")]
    public class UploadController : Controller
    {
        private IConfigurationService _configurationService;
        private IApplicationFormRepository _applicationFormRepository;
        private IRegistrationService _registrationService;
        private IEmailSender _emailSender;
        private IAuditTrailRepository _auditTrailRepository;
        public UploadController(IConfigurationService configurationService, IApplicationFormRepository applicationFormRepository, IRegistrationService registrationService, IEmailSender emailSender, IAuditTrailRepository auditTrailRepository)
        {
            this._configurationService = configurationService;
            this._applicationFormRepository = applicationFormRepository;
            this._registrationService = registrationService;
            this._emailSender = emailSender;
            this._auditTrailRepository = auditTrailRepository;
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult CheckForDuplicateName(string nameOfFile)
        {
            bool response = false;
            string path = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/AdminUploads/");
            string[] uploadedFiles = Directory.GetFiles(path, "*.csv");
            if (uploadedFiles.Contains(path + nameOfFile + ".csv"))
            {
                response = true;
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }
        //
        // GET: /Upload/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetAdmittedList(int? sessionId, int? programId, int? courseId, int? applicationFormId)
        {
            var applications = new List<Application>();
            var personalInfo = new List<PersonalInformation>();

            applications = _configurationService.GetApplicationsByParams(sessionId, programId, courseId, applicationFormId).Where(x => x.IsAdmitted).ToList();
            personalInfo = _configurationService.GetPersonalInformations().ToList();
            var result = (from app in applications
                          join perInfo in personalInfo on app.UserName equals perInfo.Email

                          select new
                          {
                              //id = s.Id,
                              appNum = app.AppNum,
                              regNum = app.RegNum,
                              lastName = perInfo.LastName,
                              firstName = perInfo.FirstName,
                              middleName = perInfo.MiddleName,
                              gender = perInfo.Gender,
                              program = _configurationService.GetProgram(app.ProgramIdAdmittedInto).Code,
                              course = _configurationService.GetCourse(app.CourseIdAdmittedInto).Name,
                              email = perInfo.Email,
                              phoneNumber = perInfo.PhoneNumber

                          }).ToList();
            var appNums = result.Select(x => x.appNum).ToList(); //result.Select(x => x.AppNum).ToList();
            Session["AdmittedAppNos"] = appNums;

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetNonApplicantAdmittedList(int? sessionId, int? programId, int? courseId, int? applicationFormId)
        {
            var nonApplicantAdmittedList = _registrationService.GetNonApplicantAdmittedLists(sessionId, programId, courseId, applicationFormId);
            var result = (from nonApp in nonApplicantAdmittedList

                          select new
                          {
                              regNum = nonApp.RegNum,
                              lastName = nonApp.LastName,
                              firstName = nonApp.FirstName,
                              middleName = nonApp.MiddleName,
                              program = nonApp.ProgramId > 0 ? _configurationService.GetProgram(nonApp.ProgramId).Code : "",
                              course = nonApp.CourseId > 0 ? _configurationService.GetCourse(nonApp.CourseId).Name : "",
                              email = nonApp.Email,
                              phoneNumber = nonApp.PhoneNumber

                          }).ToList();
            var regNums = result.Select(x => x.regNum).ToList(); //result.Select(x => x.AppNum).ToList();
            Session["AdmittedRegNums"] = regNums;

            return Json(result, JsonRequestBehavior.AllowGet);
        }


        //public ActionResult GetAdmittedListForForm(string applicationFormId)
        //{
        //    var applications = new List<Application>();
        //    var personalInfo = new List<PersonalInformation>();
        //    int outId = 0;
        //    if (string.IsNullOrEmpty(applicationFormId))
        //    {
        //        applicationFormId = "-1";
        //    }
        //    bool isValid = int.TryParse(applicationFormId, out outId);
        //    applications = _configurationService.GetApplications(int.Parse(applicationFormId)).Where(x => x.IsAdmitted).ToList();
        //    personalInfo = _configurationService.GetPersonalInformations().ToList();
        //    var result = (from app in applications
        //                  join perInfo in personalInfo on app.Id equals perInfo.ApplicationId

        //                  select new
        //                  {
        //                      //id = s.Id,
        //                      RegNum = app.RegNum,
        //                      LastName = perInfo.LastName,
        //                      FirstName = perInfo.FirstName,
        //                      MiddleName = perInfo.MiddleName,
        //                      gender = perInfo.Gender,
        //                      email = perInfo.Email,
        //                      phoneNumber = perInfo.PhoneNumber

        //                  }).ToList();

        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}

        public ActionResult GetJambListForSession(string sessionId, DataTableParamModel param)
        {
            if (string.IsNullOrEmpty(sessionId))
            {
                sessionId = "-1";
            }
            var jambPageBreakdown = _configurationService.GetJambBreakDown(int.Parse(sessionId), param.iDisplayStart, param.iDisplayLength, param.sSearch);
            var sEchoInt = Convert.ToInt32(param.sEcho);

            List<string[]> resultList = new List<string[]>();
            foreach (var item in jambPageBreakdown.JambList)
            {
                var entry = new string[]
                {
                    item.RegNum, item.LastName, item.FirstName, item.MiddleName, item.CourseCode, item.Gender,
                    item.EngScore.ToString(), item.Subject2, item.Subject2Score.ToString(), item.Subject3,
                    item.Subject3Score.ToString(), item.Subject4, item.Subject4Score.ToString(),
                    item.TotalScore.ToString()
                };

                resultList.Add(entry);
            }

            return Json(new
            {
                sEcho = sEchoInt,
                iTotalRecords = jambPageBreakdown.TotalCount,
                iTotalDisplayRecords = string.IsNullOrEmpty(param.sSearch) ? jambPageBreakdown.TotalCount : jambPageBreakdown.FilteredCount,
                aaData = resultList
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetJambBiodataForSession(string sessionId, DataTableParamModel param)
        {
            if (string.IsNullOrEmpty(sessionId))
            {
                sessionId = "-1";
            }
            var jambBiodataPageBreakdown = _configurationService.GetJambBiodata(int.Parse(sessionId), param.iDisplayStart, param.iDisplayLength, param.sSearch);
            var sEchoInt = Convert.ToInt32(param.sEcho);

            List<string[]> resultList = new List<string[]>();
            foreach (var item in jambBiodataPageBreakdown.JambList)
            {
                var entry = new string[]
                {
                    item.RegNum, item.LastName, item.FirstName, item.MiddleName,item.ProgramCode, item.CourseCode, item.Gender,
                    
                };

                resultList.Add(entry);
            }

            return Json(new
            {
                sEcho = sEchoInt,
                iTotalRecords = jambBiodataPageBreakdown.TotalCount,
                iTotalDisplayRecords = string.IsNullOrEmpty(param.sSearch) ? jambBiodataPageBreakdown.TotalCount : jambBiodataPageBreakdown.FilteredCount,
                aaData = resultList
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetResultForForm(string applicationFormId, DataTableParamModel param)
        {
            // int outId = 0;
            if (string.IsNullOrEmpty(applicationFormId))
            {
                applicationFormId = "-1";
            }
            // bool isValid = int.TryParse(applicationFormId, out outId);
            var formPageBrkdwn = _configurationService.GetFormResult(int.Parse(applicationFormId), param.iDisplayStart, param.iDisplayLength, param.sSearch);
            var sEchoInt = Convert.ToInt32(param.sEcho);

            List<string[]> resultList = new List<string[]>();
            foreach (var item in formPageBrkdwn.FormResultList)
            {
                var entry = new string[]
                {
                    item.RegNum, item.AppNum,item.EngScore.ToString(), item.Subject2,
                    item.Subject2Score.ToString(),item.Subject3,item.Subject3Score.ToString(),
                    item.Subject4, item.Subject4Score.ToString(),item.Subject5,item.Subject5Score.ToString(),item.TotalScore.ToString()
                };

                resultList.Add(entry);
            }

            return Json(new
            {
                sEcho = sEchoInt,
                iTotalRecords = formPageBrkdwn.TotalCount,
                iTotalDisplayRecords = string.IsNullOrEmpty(param.sSearch) ? formPageBrkdwn.TotalCount : formPageBrkdwn.FilteredCount,
                aaData = resultList
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetResultForSession(int? sessionId)
        {
            var sessionResults = new List<SessionResult>();


            sessionResults = _configurationService.GetSessionResult(sessionId).ToList();
            var result = (from s in sessionResults
                          select new
                          {
                              id = s.Id,
                              regNum = s.RegNum,
                              engScore = s.EngScore,
                              subject2 = s.Subject2,
                              subject2Score = s.Subject2Score,
                              subject3 = s.Subject3,
                              subject3Score = s.Subject3Score,
                              subject4 = s.Subject4,
                              subject4Score = s.Subject4Score,
                              totalScore = s.TotalScore

                          }).ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public void DeleteFormResult(long id)
        {
            try
            {
                var formResult = _configurationService.GetFormResult(id);
                _configurationService.DeleteFormResult(formResult);
            }
            catch (Exception)
            {

                throw;
            }

        }
        public void DeleteSessionResult(long id)
        {
            try
            {
                var sessionResult = _configurationService.GetSessionResult(id);
                _configurationService.DeleteSessionResult(sessionResult);
            }
            catch (Exception)
            {

                throw;
            }

        }
        [HttpGet]
        public ActionResult EditFormResult(long id)
        {
            var formResult = _configurationService.GetFormResult(id);
            formResult.ApplicationForms = _applicationFormRepository.GetAppForms();

            var model = Mapper.Map<FormResult, FormResultModel>(formResult);
            return View(model);
        }
        [HttpPost]
        public ActionResult EditSessionResult(SessionResultModel sessionResult)
        {
            decimal totalscore = 0;
            var savedSessionResult = _configurationService.GetSessionResult(sessionResult.RegNum);
            savedSessionResult.SessionId = sessionResult.SessionId;
            savedSessionResult.EngScore = sessionResult.EngScore;
            totalscore += Convert.ToDecimal(sessionResult.EngScore);
            savedSessionResult.Subject2 = sessionResult.Subject2;
            savedSessionResult.Subject2Score = sessionResult.Subject2Score;
            totalscore += Convert.ToDecimal(sessionResult.Subject2Score);
            savedSessionResult.Subject3 = sessionResult.Subject3;
            savedSessionResult.Subject3Score = sessionResult.Subject3Score;
            totalscore += Convert.ToDecimal(sessionResult.Subject3Score);
            savedSessionResult.Subject4 = sessionResult.Subject4;
            savedSessionResult.Subject4Score = sessionResult.Subject4Score;
            totalscore += Convert.ToDecimal(sessionResult.Subject4Score);
            savedSessionResult.TotalScore = totalscore > 0 ? totalscore : sessionResult.TotalScore;
            _configurationService.SaveSessionResult(savedSessionResult);
            TempData["Edited"] = true;
            return RedirectToAction("SessionResultUpload", new { sessionId = sessionResult.SessionId });
        }
        [HttpGet]
        public ActionResult EditSessionResult(long id)
        {
            var sessionResult = _configurationService.GetSessionResult(id);
            sessionResult.Sessions = _configurationService.GetSessions();

            var model = Mapper.Map<SessionResult, SessionResultModel>(sessionResult);
            return View(model);
        }
        [HttpPost]
        public ActionResult EditFormResult(FormResultModel formResult)
        {
            decimal totalScore = 0;
            var savedFormResult = _configurationService.GetFormResult(formResult.AppNum);
            savedFormResult.ApplicationFormId = formResult.ApplicationFormId;
            savedFormResult.EngScore = formResult.EngScore;
            totalScore += Convert.ToDecimal(formResult.EngScore);
            savedFormResult.Subject2 = formResult.Subject2;
            savedFormResult.Subject2Score = formResult.Subject2Score;
            totalScore += Convert.ToDecimal(formResult.Subject2Score);
            savedFormResult.Subject3 = formResult.Subject3;
            savedFormResult.Subject3Score = formResult.Subject3Score;
            totalScore += Convert.ToDecimal(formResult.Subject3Score);
            savedFormResult.Subject4 = formResult.Subject4;
            savedFormResult.Subject4Score = formResult.Subject4Score;
            totalScore += Convert.ToDecimal(formResult.Subject4Score);
            savedFormResult.TotalScore = totalScore > 0 ? totalScore : formResult.TotalScore;
            _configurationService.SaveFormResult(savedFormResult);
            TempData["Edited"] = true;
            return RedirectToAction("FormResultUpload", new { applicationFormId = formResult.ApplicationFormId });
        }
        public void DeleteJambResult(long id)
        {
            try
            {
                var jambResult = _configurationService.GetJambResult(id);
                _configurationService.DeleteJambBreakDown(jambResult);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet]
        public ActionResult EditJambBreakdown(long id)
        {
            var jambResult = _configurationService.GetJambResult(id);
            jambResult.Sessions = _configurationService.GetSessions();
            jambResult.Courses = _configurationService.GetCourses();
            var model = Mapper.Map<JambBreakDown, JambBreakDownModel>(jambResult);
            return View(model);
        }
        [HttpPost]
        public ActionResult EditJambBreakdown(JambBreakDown jmb)
        {
            jmb.TotalScore = jmb.Subject2Score + jmb.Subject3Score + jmb.Subject4Score + jmb.EngScore;
            var savedJmbBrkDwn = _configurationService.GetJambBreakDown(jmb.RegNum);
            savedJmbBrkDwn.SessionId = jmb.SessionId;
            savedJmbBrkDwn.LastName = jmb.LastName;
            savedJmbBrkDwn.MiddleName = jmb.MiddleName;
            savedJmbBrkDwn.FirstName = jmb.FirstName;
            savedJmbBrkDwn.ProgramCode = jmb.ProgramCode;
            savedJmbBrkDwn.CourseCode = jmb.CourseCode;
            savedJmbBrkDwn.CourseOfStudy = jmb.CourseOfStudy;
            savedJmbBrkDwn.Gender = jmb.Gender;
            savedJmbBrkDwn.StateOfOrigin = jmb.StateOfOrigin;
            savedJmbBrkDwn.LGA = jmb.LGA;
            savedJmbBrkDwn.EngScore = jmb.EngScore;
            savedJmbBrkDwn.Subject2 = jmb.Subject2;
            savedJmbBrkDwn.Subject2Score = jmb.Subject2Score;
            savedJmbBrkDwn.Subject3 = jmb.Subject3;
            savedJmbBrkDwn.Subject3Score = jmb.Subject3Score;
            savedJmbBrkDwn.Subject4 = jmb.Subject4;
            savedJmbBrkDwn.Subject4Score = jmb.Subject4Score;
            savedJmbBrkDwn.TotalScore = jmb.TotalScore;
            _configurationService.SaveJambBreakDown(savedJmbBrkDwn);
            return RedirectToAction("JambBreakDownUpload", new { sessionId = jmb.SessionId });
        }
        [HttpGet]
        public ActionResult JambBreakDownUpload(int? sessionId, int? sucessfulUploads, int? updatedUploads, int? faileduploads)
        {
            var localTime = _configurationService.GetCurrentWestAfricanDateTime();
            var jmbBrkDown = new JambBreakDown()
            {
                SessionId = sessionId != null ? Convert.ToInt32(sessionId) : new int(),
                Sessions = _configurationService.GetSessions().Where(x => x.EndDate > localTime).OrderByDescending(x => x.Name)
            };
            if (Session["JambBreakDownUpload"] != null)
            {
                var uploadError = Session["JambBreakDownUpload"] as List<string>;
                foreach (var error in uploadError ?? new List<string>())
                {
                    ModelState.AddModelError("", error);
                }
                Session["JambBreakDownUpload"] = null;
            }
            if (sucessfulUploads != null)
            {
                TempData["successfulUploads"] = sucessfulUploads;
            }
            if (updatedUploads != null)
            {
                TempData["updatedUploads"] = updatedUploads;
            }
            if (faileduploads != null)
            {
                TempData["failedUploads"] = faileduploads;
            }

            var model = Mapper.Map<JambBreakDown, JambBreakDownModel>(jmbBrkDown);
            return View(model);
        }
        [HttpPost]
        public ActionResult JambBreakDownUpload(JambBreakDown breakDown, HttpPostedFileBase jambBreakDown, string nameOfFile)
        {
            var duplicateRegNums = new List<string>();
            int lineReading = 2;
            var uploadErrors = new List<string>();
            var jambBreakDowns = new List<JambBreakDown>();
            bool uploaded = false;
            bool isUpdate = false;
            int successfulUploads = 0;
            int failedUploads = 0;
            int updatedUploads = 0;

            if (jambBreakDown != null)
            {
                if (jambBreakDown.ContentLength > 0)
                {
                    //check that format is in csv
                    string fileExtension = Path.GetExtension(jambBreakDown.FileName);
                    if (fileExtension != ".csv")
                    {
                        ModelState.AddModelError("", "Incorrect file format, Only CSV files are accepted");
                        var jmbBrkDown = new JambBreakDown()
                        {
                            Sessions = _configurationService.GetSessions()
                        };
                        var model = Mapper.Map<JambBreakDown, JambBreakDownModel>(jmbBrkDown);
                        return View(model);
                    }
                    string filePath = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/AdminUploads/" + jambBreakDown.FileName);
                    jambBreakDown.SaveAs(filePath);

                    //read file and save into db
                    var fileInfo = new FileInfo(filePath);
                    string filename = fileInfo.Name;

                    var filestream = new StreamReader(System.IO.File.OpenRead(filePath));
                    using (TextReader reader = filestream)
                    {
                        var csv = new CsvReader(reader);
                        while (csv.Read())
                        {
                            string regNum = "";
                            try
                            {
                                regNum = csv.GetField<string>("RegNum").Trim();
                                if (!string.IsNullOrEmpty(regNum))
                                {
                                    JambBreakDown jambResult = jambBreakDowns.FirstOrDefault(x => x.RegNum == regNum);
                                    if (jambResult != null)
                                    {
                                        duplicateRegNums.Add(regNum);
                                        uploadErrors.Add("more than one record was found with registration number :" +
                                                         regNum);
                                        failedUploads += 2;
                                        jambBreakDowns.Remove(jambResult);
                                        lineReading++;
                                        continue;
                                    }
                                    if (duplicateRegNums.Contains(regNum))
                                    {
                                        failedUploads++;
                                        lineReading++;
                                        continue;
                                    }
                                }
                                else
                                {
                                    uploadErrors.Add("Error in line " + lineReading + " Registration number is required");
                                    failedUploads++;
                                    lineReading++;
                                    continue;
                                }

                                #region fileUploadValidation

                                var lastname = csv.GetField<string>("LastName").Trim();
                                var firstname = csv.GetField<string>("FirstName").Trim();
                                var middleName = csv.GetField<string>("MiddleName").Trim();
                                var courseCode = csv.GetField<string>("CourseCode").Trim();
                                var programCode = csv.GetField<string>("ProgramCode").Trim();
                                var gender = csv.GetField<string>("Gender").Trim();
                                var stateOfOrigin = csv.GetField<string>("StateOfOrigin").Trim();
                                var lga = csv.GetField<string>("LGA").Trim();

                                var course = _configurationService.GetCoursesByCode(courseCode);
                                if (!course.Any())
                                {
                                    uploadErrors.Add("Error in line " + lineReading + " No course was found with code :" +
                                                     courseCode);
                                    failedUploads++;
                                    lineReading++;
                                    continue;
                                }
                                var program = _configurationService.GetProgramsByCode(programCode);
                                if (!program.Any())
                                {
                                    uploadErrors.Add("Error in line " + lineReading + " No program was found with code :" +
                                                     programCode);
                                    failedUploads++;
                                    lineReading++;
                                    continue;
                                }
                                if (string.IsNullOrEmpty(lastname))
                                {
                                    uploadErrors.Add("Error in line " + lineReading + " Last name is required");
                                    failedUploads++;
                                    lineReading++;
                                    continue;
                                }
                                if (string.IsNullOrEmpty(firstname))
                                {
                                    uploadErrors.Add("Error in line " + lineReading + " First name is required");
                                    failedUploads++;
                                    lineReading++;
                                    continue;
                                }

                                if (!string.IsNullOrEmpty(gender))
                                {
                                    if (gender != "F" && gender != "M")
                                    {
                                        uploadErrors.Add("Error in line " + lineReading +
                                                         " Gender: Use 'F' for female or 'M' for Male");
                                        failedUploads++;
                                        lineReading++;
                                        continue;
                                    }
                                }
                                var engScore = csv.GetField<int?>("EngScore");
                                if (engScore == null)
                                {
                                    uploadErrors.Add("Error in line " + lineReading +
                                                     " No score for English Language (column: EngScore)");
                                    failedUploads++;
                                    lineReading++;
                                    continue;
                                }
                                if (engScore > 100 || engScore < 0)
                                {
                                    uploadErrors.Add("Error in line " + lineReading +
                                                     " EngScore can only be between 0 and 100");
                                    failedUploads++;
                                    lineReading++;
                                    continue;
                                }
                                var subject2 = csv.GetField<string>("Subject2").Trim();
                                if (string.IsNullOrEmpty(subject2))
                                {
                                    uploadErrors.Add("Error in line " + lineReading + " Subject2 is empty");
                                    failedUploads++;
                                    lineReading++;
                                    continue;
                                }
                                var subject2Score = csv.GetField<int?>("Subject2Score");
                                if (subject2Score == null)
                                {

                                    uploadErrors.Add("Error in line " + lineReading +
                                                     " No score for Subject2  (column: Subject2Score)");
                                    failedUploads++;
                                    lineReading++;
                                    continue;
                                }
                                if (subject2Score > 100 || subject2Score < 0)
                                {
                                    uploadErrors.Add("Error in line " + lineReading +
                                                     " Score for Subject 2 can only be between 0 and 100");
                                    failedUploads++;
                                    lineReading++;
                                    continue;
                                }
                                var subject3 = csv.GetField<string>("Subject3").Trim();
                                if (string.IsNullOrEmpty(subject3))
                                {
                                    uploadErrors.Add("Error in line " + lineReading + "Subject3 is empty");
                                    failedUploads++;
                                    lineReading++;
                                    continue;
                                }
                                var subject3Score = csv.GetField<int?>("Subject3Score");
                                if (subject3Score == null)
                                {

                                    uploadErrors.Add("Error in line " + lineReading +
                                                     "No score for Subject3  (column: Subject3Score)");
                                    failedUploads++;
                                    lineReading++;
                                    continue;
                                }
                                if (subject3Score > 100 || subject2Score < 0)
                                {
                                    uploadErrors.Add("Error in line " + lineReading +
                                                     "Score for Subject 3 can only be between 0 and 100");
                                    failedUploads++;
                                    lineReading++;
                                    continue;
                                }
                                var subject4 = csv.GetField<string>("Subject4").Trim();
                                if (string.IsNullOrEmpty(subject4))
                                {
                                    uploadErrors.Add("Error in line " + lineReading + "Subject4 is empty");
                                    failedUploads++;
                                    lineReading++;
                                    continue;
                                }
                                var subject4Score = csv.GetField<int?>("Subject4Score");
                                if (subject4Score == null)
                                {

                                    uploadErrors.Add("Error in line " + lineReading +
                                                     "No score for Subject4  (column: Subject3Score)");
                                    failedUploads++;
                                    lineReading++;
                                    continue;
                                }

                                if (subject4Score > 100 || subject4Score < 0)
                                {
                                    uploadErrors.Add("Error in line " + lineReading +
                                                     "Score for Subject 4 can only be between 1 and 100");
                                    failedUploads++;
                                    lineReading++;
                                    continue;
                                }

                                #endregion


                                var jmb = new JambBreakDown();
                                jmb.SessionId = breakDown.SessionId;
                                jmb.RegNum = csv.GetField<string>("RegNum").Trim();
                                jmb.LastName = csv.GetField<string>("LastName").Trim();
                                jmb.FirstName = csv.GetField<string>("FirstName").Trim();
                                jmb.MiddleName = csv.GetField<string>("MiddleName").Trim();
                                //jmb.Faculty = csv.GetField<string>("Faculty").Trim();
                                jmb.CourseCode = courseCode;
                                jmb.ProgramCode = programCode;
                                // jmb.CourseOfStudy = csv.GetField<string>("CourseOfStudy").Trim();
                                jmb.Gender = csv.GetField<string>("Gender").Trim();
                                jmb.StateOfOrigin = csv.GetField<string>("StateOfOrigin").Trim();
                                jmb.LGA = csv.GetField<string>("LGA").Trim();
                                jmb.EngScore = csv.GetField<int>("EngScore");
                                jmb.Subject2 = csv.GetField<string>("Subject2").Trim();
                                jmb.Subject2Score = csv.GetField<int>("Subject2Score");
                                jmb.Subject3 = csv.GetField<string>("Subject3").Trim();
                                jmb.Subject3Score = csv.GetField<int>("Subject3Score");
                                jmb.Subject4 = csv.GetField<string>("Subject4").Trim();
                                jmb.Subject4Score = csv.GetField<int>("Subject4Score");
                                jmb.TotalScore = jmb.EngScore + jmb.Subject2Score + jmb.Subject3Score +
                                                 jmb.Subject4Score;
                                jambBreakDowns.Add(jmb);
                                lineReading++;


                            }
                            catch (CsvMissingFieldException ex)
                            {
                                uploadErrors.Add(ex.Message);
                                uploaded = false;
                                break;
                            }
                            catch (CsvTypeConverterException ex)
                            {
                                uploadErrors.Add("Error in Line " + lineReading + " " + ex.Message + " Only numbers are allowed for subject scores");
                                failedUploads++;
                                lineReading++;
                            }
                            catch (Exception ex)
                            {
                                uploadErrors.Add("Error in Line " + lineReading + " " + ex.Message);
                                failedUploads++;
                                lineReading++;
                            }

                        }
                    }

                    //At this point data has been read from file and all valid data has been saved into list, next we save them into the db.
                    if (jambBreakDowns.Count > 0)
                    {
                        foreach (var jmb in jambBreakDowns)
                        {
                            var savedJmb = _configurationService.GetJambBreakDown(jmb.RegNum) ?? new JambBreakDown();
                            isUpdate = savedJmb.Id > 0;
                            if (isUpdate)
                            {
                                savedJmb.SessionId = breakDown.SessionId;
                                savedJmb.LastName = jmb.LastName;
                                savedJmb.FirstName = jmb.FirstName;
                                savedJmb.MiddleName = jmb.MiddleName;
                                savedJmb.ProgramCode = jmb.ProgramCode;
                                savedJmb.CourseCode = jmb.CourseCode;
                                savedJmb.CourseOfStudy = jmb.CourseOfStudy;
                                savedJmb.Gender = jmb.Gender;
                                savedJmb.StateOfOrigin = jmb.StateOfOrigin;
                                savedJmb.LGA = jmb.LGA;
                                savedJmb.EngScore = jmb.EngScore;
                                savedJmb.Subject2 = jmb.Subject2;
                                savedJmb.Subject2Score = jmb.Subject2Score;
                                savedJmb.Subject3 = jmb.Subject3;
                                savedJmb.Subject3Score = jmb.Subject3Score;
                                savedJmb.Subject4 = jmb.Subject4;
                                savedJmb.Subject4Score = jmb.Subject4Score;
                                savedJmb.TotalScore = jmb.TotalScore;
                                _configurationService.SaveJambBreakDown(savedJmb);
                                updatedUploads++;
                                uploaded = true;
                            }
                            else
                            {
                                _configurationService.SaveJambBreakDown(jmb);
                                successfulUploads++;
                                uploaded = true;
                            }
                        }
                    }
                    if (uploaded)
                    {
                        var IUtilityService = EngineContext.Resolve<IUtilityService>();
                        var userRole = UserManager.GetRoles(User.Identity.GetUserId());
                        var localTime = _configurationService.GetCurrentWestAfricanDateTime();
                        var auditTrail = new AuditTrail()
                       {
                           UserId = User.Identity.GetUserId(),
                           Username = User.Identity.GetUserName(),
                           AuditActionId = Convert.ToInt32(AuditTrailActions.JambBreakdownUpload),
                           Details = "uploaded Jamb List " + "\'" + jambBreakDown.FileName + "\'",
                           TimeStamp = localTime,
                           UserRole = userRole.First(),
                           UserIp = IUtilityService.GetIp()
                       };
                        _auditTrailRepository.SaveAuditTrail(auditTrail);
                    }

                }
            }
            Session["JambBreakDownUpload"] = uploadErrors;
            return RedirectToAction("JambBreakDownUpload", new { sessionId = breakDown.SessionId, sucessfulUploads = successfulUploads, updatedUploads = updatedUploads, faileduploads = failedUploads });
        }
        [HttpGet]
        public ActionResult JambBiodataUpload(int? sessionId, int? sucessfulUploads, int? updatedUploads, int? faileduploads)
        {
            var localTime = _configurationService.GetCurrentWestAfricanDateTime();
            var jmbBiodata = new JambBiodata()
            {
                SessionId = sessionId != null ? Convert.ToInt32(sessionId) : new int(),
                Sessions = _configurationService.GetSessions().Where(x => x.EndDate > localTime).OrderByDescending(x => x.Name)
            };
            if (Session["JambBiodataUploadErrors"] != null)
            {
                var uploadError = Session["JambBiodataUploadErrors"] as List<string>;
                foreach (var error in uploadError ?? new List<string>())
                {
                    ModelState.AddModelError("", error);
                }
                Session["JambBiodataUploadErrors"] = null;
            }
            if (sucessfulUploads != null)
            {
                TempData["successfulUploads"] = sucessfulUploads;
            }
            if (updatedUploads != null)
            {
                TempData["updatedUploads"] = updatedUploads;
            }
            if (faileduploads != null)
            {
                TempData["failedUploads"] = faileduploads;
            }

            var model = Mapper.Map<JambBiodata, JambBiodataModel>(jmbBiodata);
            return View(model);
        }
        [HttpPost]
        public ActionResult JambBiodataUpload(JambBiodata breakDown, HttpPostedFileBase jambBiodata, string nameOfFile)
        {
            var duplicateRegNums = new List<string>();
            int lineReading = 2;
            var uploadErrors = new List<string>();
            var jambBiodataz = new List<JambBiodata>();
            bool uploaded = false;
            bool isUpdate = false;
            int successfulUploads = 0;
            int failedUploads = 0;
            int updatedUploads = 0;

            if (jambBiodata != null)
            {
                if (jambBiodata.ContentLength > 0)
                {
                    //check that format is in csv
                    string fileExtension = Path.GetExtension(jambBiodata.FileName);
                    if (fileExtension != ".csv")
                    {
                        ModelState.AddModelError("", "Incorrect file format, Only CSV files are accepted");
                        var jmbBiodata = new JambBiodata()
                        {
                            Sessions = _configurationService.GetSessions()
                        };
                        var model = Mapper.Map<JambBiodata, JambBiodataModel>(jmbBiodata);
                        return View(model);
                    }
                    string filePath = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/AdminUploads/" + jambBiodata.FileName);
                    jambBiodata.SaveAs(filePath);

                    //read file and save into db
                    var fileInfo = new FileInfo(filePath);
                    string filename = fileInfo.Name;

                    var filestream = new StreamReader(System.IO.File.OpenRead(filePath));
                    using (TextReader reader = filestream)
                    {
                        var csv = new CsvReader(reader);
                        while (csv.Read())
                        {
                            string regNum = "";
                            try
                            {
                                regNum = csv.GetField<string>("RegNum").Trim();
                                if (!string.IsNullOrEmpty(regNum))
                                {
                                    JambBiodata biodata = jambBiodataz.FirstOrDefault(x => x.RegNum == regNum);
                                    if (biodata != null)
                                    {
                                        duplicateRegNums.Add(regNum);
                                        uploadErrors.Add("more than one record was found with registration number :" +
                                                         regNum);
                                        failedUploads += 2;
                                        jambBiodataz.Remove(biodata);
                                        lineReading++;
                                        continue;
                                    }
                                    if (duplicateRegNums.Contains(regNum))
                                    {
                                        failedUploads++;
                                        lineReading++;
                                        continue;
                                    }
                                }
                                else
                                {
                                    uploadErrors.Add("Error in line " + lineReading + " Registration number is required");
                                    failedUploads++;
                                    lineReading++;
                                    continue;
                                }

                                #region fileUploadValidation

                                var lastname = csv.GetField<string>("LastName").Trim();
                                var firstname = csv.GetField<string>("FirstName").Trim();
                                var middleName = csv.GetField<string>("MiddleName").Trim();
                                var courseCode = csv.GetField<string>("CourseCode").Trim();
                                var programCode = csv.GetField<string>("ProgramCode").Trim();
                                var gender = csv.GetField<string>("Gender").Trim();
                                var stateOfOrigin = csv.GetField<string>("StateOfOrigin").Trim();
                                var lga = csv.GetField<string>("LGA").Trim();

                                var course = _configurationService.GetCoursesByCode(courseCode);
                                if (!course.Any())
                                {
                                    uploadErrors.Add("Error in line " + lineReading + " No course was found with code :" +
                                                     courseCode);
                                    failedUploads++;
                                    lineReading++;
                                    continue;
                                }
                                var program = _configurationService.GetProgramsByCode(programCode);
                                if (!program.Any())
                                {
                                    uploadErrors.Add("Error in line " + lineReading + " No program was found with code :" +
                                                     programCode);
                                    failedUploads++;
                                    lineReading++;
                                    continue;
                                }
                                if (string.IsNullOrEmpty(lastname))
                                {
                                    uploadErrors.Add("Error in line " + lineReading + " Last name is required");
                                    failedUploads++;
                                    lineReading++;
                                    continue;
                                }
                                if (string.IsNullOrEmpty(firstname))
                                {
                                    uploadErrors.Add("Error in line " + lineReading + " First name is required");
                                    failedUploads++;
                                    lineReading++;
                                    continue;
                                }

                                if (!string.IsNullOrEmpty(gender))
                                {
                                    if (gender != "F" && gender != "M")
                                    {
                                        uploadErrors.Add("Error in line " + lineReading +
                                                         " Gender: Use 'F' for female or 'M' for Male");
                                        failedUploads++;
                                        lineReading++;
                                        continue;
                                    }
                                }
                              

                                #endregion


                                var jmb = new JambBiodata();
                                jmb.SessionId = breakDown.SessionId;
                                jmb.RegNum = csv.GetField<string>("RegNum").Trim();
                                jmb.LastName = csv.GetField<string>("LastName").Trim();
                                jmb.FirstName = csv.GetField<string>("FirstName").Trim();
                                jmb.MiddleName = csv.GetField<string>("MiddleName").Trim();
                                jmb.CourseCode = courseCode;
                                jmb.ProgramCode = programCode;
                                jmb.Gender = csv.GetField<string>("Gender").Trim();
                                jmb.StateOfOrigin = csv.GetField<string>("StateOfOrigin").Trim();
                                jmb.LGA = csv.GetField<string>("LGA").Trim();
                                jambBiodataz.Add(jmb);
                                lineReading++;
                            }
                            catch (CsvMissingFieldException ex)
                            {
                                uploadErrors.Add(ex.Message);
                                uploaded = false;
                                break;
                            }
                            catch (CsvTypeConverterException ex)
                            {
                                uploadErrors.Add("Error in Line " + lineReading + " " + ex.Message + " Only numbers are allowed for subject scores");
                                failedUploads++;
                                lineReading++;
                            }
                            catch (Exception ex)
                            {
                                uploadErrors.Add("Error in Line " + lineReading + " " + ex.Message);
                                failedUploads++;
                                lineReading++;
                            }

                        }
                    }

                    //At this point data has been read from file and all valid data has been saved into list, next we save them into the db.
                    if (jambBiodataz.Count > 0)
                    {
                        foreach (var jmb in jambBiodataz)
                        {
                            var savedJmb = _configurationService.GetJambBiodata(jmb.RegNum) ?? new JambBiodata();
                            isUpdate = savedJmb.Id > 0;
                            if (isUpdate)
                            {
                                savedJmb.SessionId = breakDown.SessionId;
                                savedJmb.LastName = jmb.LastName;
                                savedJmb.FirstName = jmb.FirstName;
                                savedJmb.MiddleName = jmb.MiddleName;
                                savedJmb.ProgramCode = jmb.ProgramCode;
                                savedJmb.CourseCode = jmb.CourseCode;
                                savedJmb.Gender = jmb.Gender;
                                savedJmb.StateOfOrigin = jmb.StateOfOrigin;
                                savedJmb.LGA = jmb.LGA;
                                _configurationService.SaveJambBiodata(savedJmb);
                                updatedUploads++;
                                uploaded = true;
                            }
                            else
                            {
                                _configurationService.SaveJambBiodata(jmb);
                                successfulUploads++;
                                uploaded = true;
                            }
                        }
                    }
                    if (uploaded)
                    {
                        var IUtilityService = EngineContext.Resolve<IUtilityService>();
                        var userRole = UserManager.GetRoles(User.Identity.GetUserId());
                        var localTime = _configurationService.GetCurrentWestAfricanDateTime();
                        var auditTrail = new AuditTrail()
                        {
                            UserId = User.Identity.GetUserId(),
                            Username = User.Identity.GetUserName(),
                            AuditActionId = Convert.ToInt32(AuditTrailActions.JambBreakdownUpload),
                            Details = "uploaded Jamb Biodata " + "\'" + jambBiodata.FileName + "\'",
                            TimeStamp = localTime,
                            UserRole = userRole.First(),
                            UserIp = IUtilityService.GetIp()
                        };
                        _auditTrailRepository.SaveAuditTrail(auditTrail);
                    }

                }
            }
            Session["JambBiodataUploadErrors"] = uploadErrors;
            return RedirectToAction("JambBiodataUpload", new { sessionId = breakDown.SessionId, sucessfulUploads = successfulUploads, updatedUploads = updatedUploads, faileduploads = failedUploads });
        }
        [HttpGet]
        public ActionResult NonApplicantAdmittedList(int? sessionId, int? programId, int? courseOfStudyId, int? applicationFormId, int? sucessfulUploads, int? updatedUploads, int? faileduploads)
        {
            var model = new NonApplicantAdmittedListModel()
            {
                SessionId = sessionId != null ? Convert.ToInt32(sessionId) : new int(),
                ProgramId = programId != null ? Convert.ToInt32(programId) : new int(),
                CourseOfStudyId = courseOfStudyId != null ? Convert.ToInt32(courseOfStudyId) : new int(),
                ApplicationFormId = applicationFormId != null ? Convert.ToInt32(applicationFormId) : new int(),

                Sessions = Mapper.Map<IEnumerable<Session>, IEnumerable<SessionModel>>(_configurationService.GetSessions()),
                Programs = Mapper.Map<IEnumerable<Program>, IEnumerable<ProgramModel>>(_configurationService.GetPrograms()),
                Courses = programId == null ? new List<CourseModel>() : Mapper.Map<IEnumerable<Course>, IEnumerable<CourseModel>>(_configurationService.GetCourses(Convert.ToInt32(programId))),
                AppForms = Mapper.Map<IEnumerable<ApplicationForm>, IEnumerable<ApplicationFormModel>>(_applicationFormRepository.GetAppForms())
            };
            if (Session["UploadErrors"] != null)
            {
                var uploadError = Session["UploadErrors"] as List<string>;
                foreach (var error in uploadError)
                {
                    ModelState.AddModelError("", error);
                }
                Session["UploadErrors"] = null;
            }
            if (sucessfulUploads != null)
            {
                TempData["successfulUploads"] = sucessfulUploads;
            }
            if (faileduploads != null)
            {
                TempData["failedUploads"] = faileduploads;
            }
            if (updatedUploads != null)
            {
                TempData["updatedUploads"] = updatedUploads;
            }

            return View(model);
        }
        [HttpPost]
        public ActionResult NonApplicantAdmittedList(NonApplicantAdmittedListModel model, HttpPostedFileBase admittedList)
        {
            bool uploaded = false;
            int updatedUploads = 0;
            int successfulUploads = 0;
            int failedUploads = 0;
            int lineReading = 2;
            var duplicateRegNums = new List<string>();
            var uploadErrors = new List<string>();
            var nonApplicantAdmittedList = new List<NonApplicantAdmittedList>();
            if (admittedList != null)
            {
                if (admittedList.ContentLength > 0)
                {
                    #region confirmThatFormatOfUploadedFileIsCSV
                    string fileExtension = Path.GetExtension(admittedList.FileName);
                    if (fileExtension != ".csv")
                    {
                        ModelState.AddModelError("", "Incorrect File format, Only CSV files are acceptable");
                        var returnModel = new NonApplicantAdmittedListModel()
                        {
                            SessionId = model.SessionId,
                            ProgramId = model.ProgramId,
                            CourseOfStudyId = model.CourseOfStudyId,
                            ApplicationFormId = model.ApplicationFormId,
                            Sessions = Mapper.Map<IEnumerable<Session>, IEnumerable<SessionModel>>(_configurationService.GetSessions()),
                            Programs = Mapper.Map<IEnumerable<Program>, IEnumerable<ProgramModel>>(_configurationService.GetPrograms()),
                            Courses = new List<CourseModel>(),
                            AppForms = Mapper.Map<IEnumerable<ApplicationForm>, IEnumerable<ApplicationFormModel>>(_applicationFormRepository.GetAppForms())
                        };
                        return View(returnModel);
                    }
                    #endregion


                    string filePath = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/AdminUploads/" + admittedList.FileName);
                    admittedList.SaveAs(filePath);

                    //read file and save into db
                    //var fileInfo = new FileInfo(filePath);
                    //string filename = fileInfo.Name;

                    var filestream = new StreamReader(System.IO.File.OpenRead(filePath));
                    using (TextReader reader = filestream)
                    {
                        var csv = new CsvReader(reader);
                        while (csv.Read())
                        {
                            try
                            {
                                var regNum = csv.GetField<string>("RegNum").Trim();
                                var lastname = csv.GetField<string>("LastName").Trim();
                                var firstname = csv.GetField<string>("FirstName").Trim();
                                var middlename = csv.GetField<string>("MiddleName").Trim();
                                var email = csv.GetField<string>("Email").Trim();
                                var phoneNumber = csv.GetField<string>("PhoneNumber").Trim();
                                var modeOfEntry = csv.GetField<string>("ModeOfEntry").Trim();

                                if (!string.IsNullOrEmpty(regNum))
                                {
                                    var admApplicant = nonApplicantAdmittedList.FirstOrDefault(x => x.RegNum == regNum);
                                    if (admApplicant != null)
                                    {
                                        uploadErrors.Add("More than one record was found with registration number: " + regNum);
                                        nonApplicantAdmittedList.Remove(admApplicant);
                                        failedUploads += 2;
                                        lineReading++;
                                        duplicateRegNums.Add(regNum);
                                        continue;
                                    }
                                    if (duplicateRegNums.Contains(regNum))
                                    {
                                        failedUploads++;
                                        lineReading++;
                                        continue;
                                    }
                                    var admittedListEntry = _registrationService.GetAdmissionList(regNum, model.ApplicationFormId);
                                    if (admittedListEntry != null)
                                    {
                                        uploadErrors.Add("Record in line " + lineReading + " was not uploaded: Registration number: " + regNum + " is already present in admission List for applicants");
                                        failedUploads++;
                                        lineReading++;
                                        continue;
                                    }
                                }
                                else
                                {
                                    uploadErrors.Add("Error uploading data in line " + lineReading +
                                                     ": RegNum is a required field");
                                    failedUploads++;
                                    lineReading++;
                                    continue;
                                }

                                if (string.IsNullOrEmpty(lastname))
                                {
                                    uploadErrors.Add("Error uploading data in line " + lineReading +
                                                     ": Last Name is a required field");
                                    failedUploads++;
                                    lineReading++;
                                    continue;
                                }
                                if (string.IsNullOrEmpty(firstname))
                                {
                                    uploadErrors.Add("Error uploading data in line " + lineReading +
                                                     ": First Name is a required field");
                                    failedUploads++;
                                    lineReading++;
                                    continue;
                                }
                                //if (string.IsNullOrEmpty(email))
                                //{
                                //    uploadErrors.Add("Error uploading data in line " + lineReading +
                                //                     ": Email is a required field");
                                //    failedUploads++;
                                //    lineReading++;
                                //    continue;
                                //}
                                if (string.IsNullOrEmpty(modeOfEntry))
                                {
                                    uploadErrors.Add("Error uploading data in line " + lineReading +
                                                     ": ModeOfEntry is a required field");
                                    failedUploads++;
                                    lineReading++;
                                    continue;
                                }
                                //if (!string.IsNullOrEmpty(gender))
                                //{
                                //    if (gender != "F" && gender != "M")
                                //    {
                                //        uploadErrors.Add("Error in line " + lineReading + " Gender: Use 'F' for female or 'M' for Male");
                                //        failedUploads++;
                                //        lineReading++;
                                //        continue;
                                //    }
                                //}

                                var admittedApplicant = new NonApplicantAdmittedList()
                                {
                                    SessionId = model.SessionId,
                                    ProgramId = model.ProgramId,
                                    CourseId = model.CourseOfStudyId,
                                    FormId = model.ApplicationFormId,
                                    RegNum = regNum,
                                    LastName = lastname,
                                    FirstName = firstname,
                                    MiddleName = middlename,
                                    Email = email,
                                    PhoneNumber = phoneNumber,
                                    ModeOfEntry = modeOfEntry
                                };
                                nonApplicantAdmittedList.Add(admittedApplicant);
                                lineReading++;
                            }
                            catch (CsvMissingFieldException ex)
                            {
                                uploadErrors.Add(ex.Message);
                                uploaded = false;
                                break;
                            }
                            catch (Exception ex)
                            {
                                failedUploads++;
                                lineReading++;
                                uploadErrors.Add("Error uploading data in line " + lineReading +
                                                 ": One or more fields were not correctly filled");
                            }
                        }

                    }
                    foreach (var admApplicant in nonApplicantAdmittedList)
                    {
                        bool isUpdate = false;
                        var admittedEntry = _registrationService.GetNonApplicantAdmittedList(admApplicant.RegNum, admApplicant.FormId) ?? new NonApplicantAdmittedList();
                        isUpdate = admittedEntry.Id > 0;

                        if (isUpdate)
                        {
                            admittedEntry.RegNum = admApplicant.RegNum;
                            admittedEntry.SessionId = admApplicant.SessionId;
                            admittedEntry.ProgramId = admApplicant.ProgramId;
                            admittedEntry.CourseId = admApplicant.CourseId;
                            admittedEntry.FormId = model.ApplicationFormId;
                            admittedEntry.FirstName = admApplicant.FirstName;
                            admittedEntry.LastName = admApplicant.LastName;
                            admittedEntry.MiddleName = admApplicant.MiddleName;
                            admittedEntry.ModeOfEntry = admApplicant.ModeOfEntry;
                            admittedEntry.Email = admApplicant.Email;
                            admittedEntry.PhoneNumber = admApplicant.PhoneNumber;
                            _registrationService.SaveNonApplicantAdmittedList(admittedEntry);
                            uploaded = true;
                            updatedUploads++;
                        }
                        else
                        {
                            _registrationService.SaveNonApplicantAdmittedList(admApplicant);
                            uploaded = true;
                            successfulUploads++;
                        }
                    }

                }
            }
            if (uploaded)
            {
                var IUtilityService = EngineContext.Resolve<IUtilityService>();
                var userRole = UserManager.GetRoles(User.Identity.GetUserId());
                var localTime = _configurationService.GetCurrentWestAfricanDateTime();
                var auditTrail = new AuditTrail()
                   {
                       UserId = User.Identity.GetUserId(),
                       Username = User.Identity.GetUserName(),
                       AuditActionId = Convert.ToInt32(AuditTrailActions.AdmissionListUpload),
                       Details = "uploaded Admission List " + "\'" + admittedList.FileName + "\'",
                       TimeStamp = localTime,
                       UserRole = userRole.First(),
                       UserIp = IUtilityService.GetIp()
                   };
                _auditTrailRepository.SaveAuditTrail(auditTrail);
            }

            Session["UploadErrors"] = uploadErrors;
            return RedirectToAction("NonApplicantAdmittedList", new { sessionId = model.SessionId, programId = model.ProgramId, courseOfStudyId = model.CourseOfStudyId, applicationFormId = model.ApplicationFormId, sucessfulUploads = successfulUploads, updatedUploads = updatedUploads, faileduploads = failedUploads });
        }
        [HttpGet]
        public ActionResult ApplicantAdmittedList(int? applicationFormId, int? programId, int? courseId, int? sucessfulUploads, int? updatedUploads, int? faileduploads)
        {
            var model = new AdmittedListModel()
            {
                ApplicationFormId = applicationFormId != null ? Convert.ToInt32(applicationFormId) : new int(),
                ProgramId = programId != null ? Convert.ToInt32(programId) : new int(),
                CourseOfStudyId = courseId != null ? Convert.ToInt32(courseId) : new int(),
                ApplicationForms = Mapper.Map<IEnumerable<ApplicationForm>, IEnumerable<ApplicationFormModel>>(_applicationFormRepository.GetAppForms().OrderBy(x => x.Name)),
                Programs = _configurationService.GetPrograms(),
                Courses = _configurationService.GetCourses()
            };
            if (Session["ApplicantErrorList"] != null)
            {
                var uploadError = Session["ApplicantErrorList"] as List<string>;
                foreach (var error in uploadError)
                {
                    ModelState.AddModelError("", error);
                }
                Session["ApplicantErrorList"] = null;
            }
            if (sucessfulUploads != null)
            {
                TempData["successfulUploads"] = sucessfulUploads;
            }
            if (faileduploads != null)
            {
                TempData["failedUploads"] = faileduploads;
            }
            if (updatedUploads != null)
            {
                TempData["updatedUploads"] = updatedUploads;
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult ApplicantAdmittedList(AdmittedListModel mod, HttpPostedFileBase admittedList)
        {
            var duplicateRegNums = new List<string>();
            var duplicateAppNums = new List<string>();
            var admittedListCollection = new List<AdmissionList>();
            bool uploaded = false;
            var uploadErrors = new List<string>();
            int updatedUploads = 0;
            int successfulUploads = 0;
            int failedUploads = 0;
            var lineReading = 2;
            if (admittedList != null)
            {
                if (admittedList.ContentLength > 0)
                {
                    //check that format is in csv
                    string fileExtension = Path.GetExtension(admittedList.FileName);
                    if (fileExtension != ".csv")
                    {
                        ModelState.AddModelError("", "Incorrect File format, Only CSV files are acceptable");
                        var model = new AdmittedListModel()
                        {
                            ApplicationFormId = mod.ApplicationFormId,
                            ApplicationForms = Mapper.Map<IEnumerable<ApplicationForm>, IEnumerable<ApplicationFormModel>>(_applicationFormRepository.GetAppForms())
                        };
                        return View(model);
                    }
                    string filePath = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/AdminUploads/" + admittedList.FileName);
                    admittedList.SaveAs(filePath);

                    //read file and save into db
                    //var fileInfo = new FileInfo(filePath);
                    //string filename = fileInfo.Name;

                    var filestream = new StreamReader(System.IO.File.OpenRead(filePath));
                    using (TextReader reader = filestream)
                    {
                        var csv = new CsvReader(reader);
                        while (csv.Read())
                        {
                            try
                            {
                                var regNum = csv.GetField<string>("RegNum").Trim();
                                var appNum = csv.GetField<string>("AppNum").Trim();
                                var modeOfEntry = csv.GetField<string>("ModeOfEntry").Trim();
                                if (string.IsNullOrEmpty(regNum) && string.IsNullOrEmpty(appNum))
                                {
                                    lineReading++;
                                    continue;
                                }
                                var admittedListEntry = admittedListCollection.FirstOrDefault(x => !string.IsNullOrEmpty(x.RegNum) && x.RegNum == regNum);
                                if (admittedListEntry != null)
                                {
                                    admittedListCollection.Remove(admittedListEntry);
                                    duplicateRegNums.Add(regNum);
                                    failedUploads += 2;
                                    uploadErrors.Add("Duplicate Registration number on line " + lineReading);
                                    lineReading++;
                                    continue;
                                }
                                admittedListEntry = admittedListCollection.FirstOrDefault(x => !string.IsNullOrEmpty(x.AppNum) && x.AppNum == appNum);
                                if (admittedListEntry != null)
                                {
                                    admittedListCollection.Remove(admittedListEntry);
                                    duplicateAppNums.Add(appNum);
                                    failedUploads += 2;
                                    uploadErrors.Add("Duplicate Application number on line " + lineReading);
                                    lineReading++;
                                    continue;
                                }
                                if (duplicateRegNums.Contains(regNum))
                                {
                                    failedUploads++;
                                    lineReading++;
                                    continue;
                                }
                                if (duplicateAppNums.Contains(appNum))
                                {
                                    failedUploads++;
                                    lineReading++;
                                    continue;
                                }
                                //if (string.IsNullOrEmpty(regNum))
                                //{
                                //    uploadErrors.Add("Error uploading data in line " + lineReading + ", RegNum is a required field");
                                //    failedUploads++;
                                //    lineReading++;
                                //    continue;
                                //}
                                //if (string.IsNullOrEmpty(appNum))
                                //{
                                //    uploadErrors.Add("Error uploading data in line " + lineReading + ", AppNum is a required field");
                                //    failedUploads++;
                                //    lineReading++;
                                //    continue;
                                //}
                                if (!string.IsNullOrEmpty(regNum) && !string.IsNullOrEmpty(appNum))
                                {
                                    var application = _configurationService.GetApplicationByRegNumAndAppNum(regNum, appNum);
                                    if (application == null)
                                    {
                                        uploadErrors.Add("Invalid Registration number or Application number in line " + lineReading);
                                        lineReading++;
                                        failedUploads++;
                                        continue;
                                    }
                                    if (application.AppFormId != mod.ApplicationFormId)
                                    {
                                        uploadErrors.Add("Data in line " + lineReading + " was not uploaded, Applicant did not apply to this form");
                                        lineReading++;
                                        failedUploads++;
                                        continue;
                                    }
                                }
                                else
                                {
                                    if (!string.IsNullOrEmpty(regNum))
                                    {
                                        var applications = _configurationService.GetApplicationsByRegNum(regNum).ToList();
                                        if (!applications.Any())
                                        {
                                            uploadErrors.Add("No record of Application found with Registration Number: " + regNum + " in line " + lineReading);
                                            lineReading++;
                                            failedUploads++;
                                            continue;
                                        }
                                        if (!(applications.Any(x => x.AppFormId == mod.ApplicationFormId)))
                                        {
                                            uploadErrors.Add("Data in line " + lineReading + " was not uploaded, Applicant did not apply to this form");
                                            lineReading++;
                                            failedUploads++;
                                            continue;
                                        }
                                    }
                                    else
                                    {
                                        if (!string.IsNullOrEmpty(appNum))
                                        {
                                            var application = _registrationService.GetApplicationDetailsByAppNum(appNum);
                                            if (application == null)
                                            {
                                                uploadErrors.Add("No record of Application found with Application Number: " + appNum + " in line " + lineReading);
                                                lineReading++;
                                                failedUploads++;
                                                continue;
                                            }
                                            if (application.AppFormId != mod.ApplicationFormId)
                                            {
                                                uploadErrors.Add("Data in line " + lineReading + " was not uploaded, Applicant did not apply to this form");
                                                lineReading++;
                                                failedUploads++;
                                                continue;
                                            }
                                        }
                                    }
                                }

                                admittedListCollection.Add(new AdmissionList()
                                {
                                    RegNum = regNum,
                                    AppNum = appNum,
                                    FormId = mod.ApplicationFormId,
                                    ModeOfEntry = modeOfEntry
                                });

                            }
                            catch (CsvMissingFieldException ex)
                            {
                                uploadErrors.Add(ex.Message);
                                uploaded = false;
                                break;
                            }
                            catch (Exception ex)
                            {
                                uploadErrors.Add("Error: " + ex.Message);
                                failedUploads++;
                            }
                            lineReading++;
                        }
                    }

                    if (admittedListCollection.Any())
                    {
                        var localTime = _configurationService.GetCurrentWestAfricanDateTime();
                        uploaded = true;
                        foreach (var item in admittedListCollection)
                        {
                            if (!string.IsNullOrEmpty(item.AppNum))
                            {
                                var admittedEntry = _registrationService.GetAdmissionList(item.AppNum) ?? new AdmissionList();
                                var application = _registrationService.GetApplicationDetailsByAppNum(item.AppNum);
                                bool isUpdate = admittedEntry.Id > 0;
                                if (isUpdate)
                                {
                                    //check if item.RegNum is not null then add it and save, if it is null do nothing cos it is possible a record
                                    //has been previously uploaded with this appNum and also a valid regNum so it wont be wise to override with an empty regNum
                                    if (!string.IsNullOrEmpty(item.RegNum))
                                    {
                                        admittedEntry.RegNum = item.RegNum;
                                    }
                                    else
                                    {
                                        if (string.IsNullOrEmpty(admittedEntry.RegNum))
                                        {
                                            if (!string.IsNullOrEmpty(application.RegNum))
                                            {
                                                admittedEntry.RegNum = application.RegNum;
                                            }
                                        }

                                    }
                                    admittedEntry.ModeOfEntry = item.ModeOfEntry;
                                    admittedEntry.FormId = mod.ApplicationFormId;
                                    _registrationService.SaveAdmissionListItem(admittedEntry);
                                    updatedUploads++;
                                }
                                else
                                {
                                    if (string.IsNullOrEmpty(item.RegNum))
                                    {
                                        item.RegNum = application.RegNum == null ? "" : application.RegNum;
                                    }
                                    _registrationService.SaveAdmissionListItem(item);
                                    successfulUploads++;
                                }

                                application.ProgramIdAdmittedInto = mod.ProgramId;
                                application.CourseIdAdmittedInto = mod.CourseOfStudyId;
                                application.ModeOfEntry = item.ModeOfEntry;
                                application.IsAdmitted = true;
                                application.AdmittedDate = localTime;
                                _registrationService.SaveApplication(application);
                                var personalInformation = _registrationService.GetPersonalInformationByEmail(application.UserName);
                                var applicantsName = personalInformation.FirstName + " " + personalInformation.LastName;
                                string schoolName = _configurationService.GetSchoolName();
                                string sessionName = _configurationService.GetSession(application.SessionId).Name;
                                var programCode = _configurationService.GetProgram(mod.ProgramId).Code;
                                var courseName = _configurationService.GetCourse(mod.CourseOfStudyId).Name;
                                _emailSender.SendEmailAdmission(application.UserName, applicantsName, null, Convert.ToInt32(EmailType.AdmissionOffered), null, schoolName, sessionName, programCode, courseName);
                            }
                            else
                            {
                                var application = _registrationService.GetApplicationByRegNumAndFormId(item.RegNum, mod.ApplicationFormId);
                                var admittedEntry = _registrationService.GetAdmissionList(item.RegNum, item.FormId) ?? new AdmissionList();
                                bool isUpdate = admittedEntry.Id > 0;
                                if (isUpdate)
                                {
                                    admittedEntry.FormId = mod.ApplicationFormId;
                                    if (string.IsNullOrEmpty(admittedEntry.AppNum))
                                    {
                                        admittedEntry.AppNum = application.AppNum;
                                    }
                                    admittedEntry.ModeOfEntry = item.ModeOfEntry;
                                    _registrationService.SaveAdmissionListItem(admittedEntry);
                                    updatedUploads++;
                                }
                                else
                                {
                                    item.AppNum = application.AppNum;
                                    _registrationService.SaveAdmissionListItem(item);
                                    successfulUploads++;
                                }

                                application.ProgramIdAdmittedInto = mod.ProgramId;
                                application.CourseIdAdmittedInto = mod.CourseOfStudyId;
                                application.ModeOfEntry = item.ModeOfEntry;
                                application.IsAdmitted = true;
                                application.AdmittedDate = localTime;
                                _registrationService.SaveApplication(application);
                                var personalInformation = _registrationService.GetPersonalInformationByEmail(application.UserName);
                                var applicantsName = personalInformation.FirstName + " " + personalInformation.LastName;
                                string schoolName = _configurationService.GetSchoolName();
                                string sessionName = _configurationService.GetSession(application.SessionId).Name;
                                var programCode = _configurationService.GetProgram(mod.ProgramId).Code;
                                var courseName = _configurationService.GetCourse(mod.CourseOfStudyId).Name;
                                _emailSender.SendEmailAdmission(application.UserName, applicantsName, null, Convert.ToInt32(EmailType.AdmissionOffered), null, schoolName, sessionName, programCode, courseName);



                            }

                        }
                    }
                    if (uploaded)
                    {
                        var IUtilityService = EngineContext.Resolve<IUtilityService>();
                        var userRole = UserManager.GetRoles(User.Identity.GetUserId());
                        var localTime = _configurationService.GetCurrentWestAfricanDateTime();
                        var auditTrail = new AuditTrail()
                           {
                               UserId = User.Identity.GetUserId(),
                               Username = User.Identity.GetUserName(),
                               AuditActionId = Convert.ToInt32(AuditTrailActions.AdmissionListUpload),
                               Details = "uploaded Admission List " + "\'" + admittedList.FileName + "\'",
                               TimeStamp = localTime,
                               UserRole = userRole.First(),
                               UserIp = IUtilityService.GetIp()
                           };
                        _auditTrailRepository.SaveAuditTrail(auditTrail);
                    }

                }
            }
            Session["ApplicantErrorList"] = uploadErrors;
            return RedirectToAction("ApplicantAdmittedList", new { applicationFormId = mod.ApplicationFormId, programId = mod.ProgramId, courseId = mod.CourseOfStudyId, sucessfulUploads = successfulUploads, updatedUploads = updatedUploads, faileduploads = failedUploads });
        }
        [HttpGet]
        public ActionResult FormResultUpload(int? applicationFormId, int? sucessfulUploads, int? updatedUploads, int? faileduploads)
        {
            var formResult = new FormResult()
            {
                ApplicationFormId = applicationFormId != null ? Convert.ToInt32(applicationFormId) : new int(),
                ApplicationForms = _applicationFormRepository.GetAppForms(),
            };
            var model = Mapper.Map<FormResult, FormResultModel>(formResult);

            if (Session["UploadErrors"] != null)
            {
                var uploadError = Session["UploadErrors"] as List<string>;
                foreach (var error in uploadError)
                {
                    ModelState.AddModelError("", error);
                }
                Session["UploadErrors"] = null;
            }
            if (sucessfulUploads != null)
            {
                TempData["successfulUploads"] = sucessfulUploads;
            }
            if (faileduploads != null)
            {
                TempData["failedUploads"] = faileduploads;
            }
            if (updatedUploads != null)
            {
                TempData["updatedUploads"] = updatedUploads;
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult FormResultUpload(FormResult formResult, HttpPostedFileBase formResultUpload, string uploadFormat)
        {
            var duplicateAppNums = new List<string>();
            var duplicateRegNums = new List<string>();
            int lineReading = 2;
            var uploadErrors = new List<string>();
            var formResults = new List<FormResult>();
            bool isUploaded = false;
            bool isUpdate = false;
            int successfulUploads = 0;
            int failedUploads = 0;
            int updatedUploads = 0;
            var localTime = _configurationService.GetCurrentWestAfricanDateTime();
            if (formResultUpload != null)
            {
                if (formResultUpload.ContentLength > 0)
                {
                    //check that format is in csv
                    string fileExtension = Path.GetExtension(formResultUpload.FileName);
                    if (fileExtension != ".csv")
                    {
                        ModelState.AddModelError("", "Incorrect File format, Only CSV files are acceptable");
                        var result = new FormResult()
                        {
                            ApplicationFormId = formResult.ApplicationFormId,
                            ApplicationForms = _applicationFormRepository.GetAppForms().Where(x => x.EndDate < localTime)
                        };
                        var model = Mapper.Map<FormResult, FormResultModel>(result);
                        return View(model);
                    }
                    string filePath =
                              System.Web.HttpContext.Current.Server.MapPath("~/App_Data/AdminUploads/" +
                                                                            formResultUpload.FileName);
                    formResultUpload.SaveAs(filePath);
                    var filestream = new StreamReader(System.IO.File.OpenRead(filePath));
                    switch (uploadFormat)
                    {

                        case "1":

                            using (TextReader reader = filestream)
                            {
                                var csv = new CsvReader(reader);
                                while (csv.Read())
                                {
                                    try
                                    {
                                        var regNum = csv.GetField<string>("RegNum").Trim();
                                        var appNum = csv.GetField<string>("AppNum").Trim();
                                        var totalScore = csv.GetField<decimal?>("TotalScore");
                                        //check if the whole line is empty, if it is just move to the next line
                                        if (string.IsNullOrEmpty(regNum) && string.IsNullOrEmpty(appNum) && totalScore == null)
                                        {
                                            lineReading++;
                                            continue;
                                        }
                                        if (string.IsNullOrEmpty(regNum) && string.IsNullOrEmpty(appNum) && totalScore != null)
                                        {
                                            uploadErrors.Add("Error uploading data in line " + lineReading +
                                                            ", one of either Registration Number or Application Number is required");
                                            failedUploads++;
                                            lineReading++;
                                            continue;
                                        }
                                        if (totalScore == null)
                                        {
                                            uploadErrors.Add("Error uploading data in line " + lineReading +
                                                             ", Total score is required");
                                            failedUploads++;
                                            lineReading++;
                                            continue;
                                        }
                                        if (!string.IsNullOrEmpty(appNum))
                                        {
                                            var frm = formResults.FirstOrDefault(x => x.AppNum == appNum);
                                            if (frm != null)
                                            {
                                                formResults.Remove(frm);
                                                duplicateAppNums.Add(appNum);
                                                failedUploads += 2;
                                                uploadErrors.Add(
                                                    "more than one record was found with application number: " + appNum + " in your csv file");
                                                lineReading++;
                                                continue;
                                            }
                                            if (duplicateAppNums.Contains(appNum))
                                            {
                                                failedUploads++;
                                                lineReading++;
                                                continue;
                                            }
                                        }
                                        if (!string.IsNullOrEmpty(regNum))
                                        {
                                            var frm = formResults.FirstOrDefault(x => x.RegNum == regNum);
                                            if (frm != null)
                                            {
                                                formResults.Remove(frm);
                                                duplicateRegNums.Add(regNum);
                                                failedUploads += 2;
                                                uploadErrors.Add(
                                                    "more than one record was found with registration number: " + regNum + " in your csv file");
                                                lineReading++;
                                                continue;
                                            }
                                            if (duplicateRegNums.Contains(regNum))
                                            {
                                                failedUploads++;
                                                lineReading++;
                                                continue;
                                            }
                                        }

                                        if (!string.IsNullOrEmpty(regNum) && !string.IsNullOrEmpty(appNum))
                                        {
                                            var application = _configurationService.GetApplicationByRegNumAndAppNum(regNum, appNum);
                                            if (application == null)
                                            {
                                                uploadErrors.Add("Invalid Registration number or Application number in line " + lineReading);
                                                lineReading++;
                                                failedUploads++;
                                                continue;
                                            }
                                            if (application.AppFormId != formResult.ApplicationFormId)
                                            {
                                                uploadErrors.Add("Applicant did not apply to this form");
                                                lineReading++;
                                                failedUploads++;
                                                continue;
                                            }
                                        }
                                        else
                                        {
                                            if (!string.IsNullOrEmpty(regNum))
                                            {
                                                var applications = _configurationService.GetApplicationsByRegNum(regNum).ToList();
                                                if (!applications.Any())
                                                {
                                                    uploadErrors.Add("No record of Application found with Registration Number: " + regNum + " in line " + lineReading);
                                                    lineReading++;
                                                    failedUploads++;
                                                    continue;
                                                }
                                                if (!(applications.Any(x => x.AppFormId == formResult.ApplicationFormId)))
                                                {
                                                    uploadErrors.Add("Applicant did not apply to this form");
                                                    lineReading++;
                                                    failedUploads++;
                                                    continue;
                                                }
                                            }
                                            else
                                            {
                                                if (!string.IsNullOrEmpty(appNum))
                                                {
                                                    var application = _registrationService.GetApplicationDetailsByAppNum(appNum);
                                                    if (application == null)
                                                    {
                                                        uploadErrors.Add("No record of Application found with Application Number: " + appNum + " in line " + lineReading);
                                                        lineReading++;
                                                        failedUploads++;
                                                        continue;
                                                    }
                                                    if (application.AppFormId != formResult.ApplicationFormId)
                                                    {
                                                        uploadErrors.Add("Applicant did not apply to this form");
                                                        lineReading++;
                                                        failedUploads++;
                                                        continue;
                                                    }
                                                }
                                            }
                                        }

                                        //else
                                        //{
                                        //    uploadErrors.Add("Error uploading data in line " + lineReading +
                                        //                     ", Application Number is required");
                                        //    failedUploads++;
                                        //    lineReading++;
                                        //    continue;
                                        //}
                                        //if (string.IsNullOrEmpty(regNum))
                                        //{
                                        //    uploadErrors.Add("Error uploading data in line " + lineReading +
                                        //                    ", Registration number is required");
                                        //    failedUploads++;
                                        //    lineReading++;
                                        //    continue;
                                        //}


                                        //if compiler gets here t means all validations have been passed next we add it to list
                                        var frmRes = new FormResult()
                                        {
                                            ApplicationFormId = formResult.ApplicationFormId,
                                            RegNum = regNum,
                                            AppNum = appNum,
                                            TotalScore = Convert.ToDecimal(totalScore)
                                        };
                                        formResults.Add(frmRes);
                                        lineReading++;
                                    }
                                    catch (CsvMissingFieldException ex)
                                    {
                                        uploadErrors.Add(ex.Message);
                                        isUploaded = false;
                                        break;
                                    }
                                    catch (Exception ex)
                                    {
                                        uploadErrors.Add(ex.Message);
                                        failedUploads++;
                                        lineReading++;
                                    }
                                }
                            }
                            break;
                        case "2":

                            using (TextReader reader = filestream)
                            {
                                var csv = new CsvReader(reader);
                                while (csv.Read())
                                {
                                    try
                                    {
                                        var regNum = csv.GetField<string>("RegNum").Trim();
                                        var appNum = csv.GetField<string>("AppNum").Trim();
                                        var engScore = csv.GetField<decimal?>("EngScore");
                                        var subject2 = csv.GetField<string>("Subject2").Trim();
                                        var subject2Score = csv.GetField<decimal?>("Subject2Score");
                                        var subject3 = csv.GetField<string>("Subject3").Trim();
                                        var subject3Score = csv.GetField<decimal?>("Subject3Score");
                                        var subject4 = csv.GetField<string>("Subject4").Trim();
                                        var subject4Score = csv.GetField<decimal?>("Subject4Score");
                                        var subject5 = csv.GetField<string>("Subject5").Trim();
                                        var subject5Score = csv.GetField<decimal?>("Subject5Score");
                                        //var totalScore = csv.GetField<decimal?>("TotalScore");
                                        if (string.IsNullOrEmpty(regNum) && string.IsNullOrEmpty(appNum) && engScore == null && string.IsNullOrEmpty(subject2) && subject2Score == null && string.IsNullOrEmpty(subject3) && subject3Score == null && string.IsNullOrEmpty(subject4) && subject4Score == null)
                                        {
                                            lineReading++;
                                            continue;
                                        }
                                        //Check if RegNum is a duplicate by checking if a record in the list already has the RegNum, remove that entry if you find any
                                        //also add that regnum to the duplicateRegNums List<string>, this would help if duplicates are more than two
                                        if (!string.IsNullOrEmpty(appNum))
                                        {
                                            var frm = formResults.FirstOrDefault(x => x.AppNum == appNum);
                                            if (frm != null)
                                            {
                                                formResults.Remove(frm);
                                                duplicateAppNums.Add(appNum);
                                                failedUploads += 2;
                                                uploadErrors.Add(
                                                    "more than one record was found with application number: " + appNum + " in your csv file");
                                                lineReading++;
                                                continue;
                                            }
                                            if (duplicateAppNums.Contains(appNum))
                                            {
                                                failedUploads++;
                                                lineReading++;
                                                continue;
                                            }
                                        }
                                        if (!string.IsNullOrEmpty(regNum))
                                        {
                                            var frm = formResults.FirstOrDefault(x => x.RegNum == regNum);
                                            if (frm != null)
                                            {
                                                formResults.Remove(frm);
                                                duplicateRegNums.Add(regNum);
                                                failedUploads += 2;
                                                uploadErrors.Add(
                                                    "more than one record was found with registration number: " + regNum + " in your csv file");
                                                lineReading++;
                                                continue;
                                            }
                                            if (duplicateRegNums.Contains(regNum))
                                            {
                                                failedUploads++;
                                                lineReading++;
                                                continue;
                                            }
                                        }

                                        if (engScore == null)
                                        {
                                            uploadErrors.Add("Error uploading data in line " + lineReading +
                                                             ", Eng Score is required");
                                            failedUploads++;
                                            lineReading++;
                                            continue;
                                        }
                                        if (string.IsNullOrEmpty(subject2))
                                        {
                                            uploadErrors.Add("Error uploading data in line " + lineReading +
                                                           ", Subject2 is required");
                                            failedUploads++;
                                            lineReading++;
                                            continue;
                                        }
                                        if (subject2Score == null)
                                        {
                                            uploadErrors.Add("Error uploading data in line " + lineReading +
                                                             ", Subject2Score is required");
                                            failedUploads++;
                                            lineReading++;
                                            continue;
                                        }
                                        if (string.IsNullOrEmpty(subject3))
                                        {
                                            uploadErrors.Add("Error uploading data in line " + lineReading +
                                                           ", Subject3 is required");
                                            failedUploads++;
                                            lineReading++;
                                            continue;
                                        }
                                        if (subject3Score == null)
                                        {
                                            uploadErrors.Add("Error uploading data in line " + lineReading +
                                                             ", Subject3 Score is required");
                                            failedUploads++;
                                            lineReading++;
                                            continue;
                                        }
                                        if (string.IsNullOrEmpty(subject4))
                                        {
                                            uploadErrors.Add("Error uploading data in line " + lineReading +
                                                           ", Subject4 is required");
                                            failedUploads++;
                                            lineReading++;
                                            continue;
                                        }
                                        if (subject4Score == null)
                                        {
                                            uploadErrors.Add("Error uploading data in line " + lineReading +
                                                             ", Subject4 Score is required");
                                            failedUploads++;
                                            lineReading++;
                                            continue;
                                        }
                                        //check if the AppNum or RegNum is valid in the applications Table
                                        if (!string.IsNullOrEmpty(regNum) && !string.IsNullOrEmpty(appNum))
                                        {
                                            var application = _configurationService.GetApplicationByRegNumAndAppNum(regNum, appNum);
                                            if (application == null)
                                            {
                                                uploadErrors.Add("Invalid Registration number or Application number in line " + lineReading);
                                                lineReading++;
                                                failedUploads++;
                                                continue;
                                            }
                                            if (application.AppFormId != formResult.ApplicationFormId)
                                            {
                                                uploadErrors.Add("Applicant did not apply to this form");
                                                lineReading++;
                                                failedUploads++;
                                                continue;
                                            }
                                        }
                                        else
                                        {
                                            if (!string.IsNullOrEmpty(regNum))
                                            {
                                                //regNum is not unique this is why we are getting a collection and using it for validation here
                                                var applications = _configurationService.GetApplicationsByRegNum(regNum).ToList();
                                                if (!applications.Any())
                                                {
                                                    uploadErrors.Add("No record of Application found with Registration Number: " + regNum + " in line " + lineReading);
                                                    lineReading++;
                                                    failedUploads++;
                                                    continue;
                                                }
                                                if (!(applications.Any(x => x.AppFormId == formResult.ApplicationFormId)))
                                                {
                                                    uploadErrors.Add("Applicant did not apply to this form");
                                                    lineReading++;
                                                    failedUploads++;
                                                    continue;
                                                }
                                            }
                                            else
                                            {
                                                if (!string.IsNullOrEmpty(appNum))
                                                {
                                                    var application = _registrationService.GetApplicationDetailsByAppNum(appNum);
                                                    if (application == null)
                                                    {
                                                        uploadErrors.Add("No record of Application found with Application Number: " + appNum + " in line " + lineReading);
                                                        lineReading++;
                                                        failedUploads++;
                                                        continue;
                                                    }
                                                    if (application.AppFormId != formResult.ApplicationFormId)
                                                    {
                                                        uploadErrors.Add("Applicant did not apply to this form");
                                                        lineReading++;
                                                        failedUploads++;
                                                        continue;
                                                    }
                                                }
                                            }
                                        }
                                        //if compiler gets here t means all validations have been passed next we add it to list
                                        var frmRes = new FormResult()
                                        {
                                            ApplicationFormId = formResult.ApplicationFormId,
                                            RegNum = regNum,
                                            AppNum = appNum,
                                            EngScore = engScore,
                                            Subject2 = subject2,
                                            Subject2Score = subject2Score,
                                            Subject3 = subject3,
                                            Subject3Score = subject3Score,
                                            Subject4 = subject4,
                                            Subject4Score = subject4Score,
                                            Subject5 = subject5,
                                            Subject5Score = subject5Score,
                                            TotalScore = Convert.ToDecimal(engScore + subject2Score + subject3Score + subject4Score + subject5Score)
                                        };
                                        formResults.Add(frmRes);
                                        lineReading++;

                                    }
                                    catch (CsvMissingFieldException ex)
                                    {
                                        uploadErrors.Add(ex.Message);
                                        isUploaded = false;
                                        break;
                                    }
                                    catch (Exception ex)
                                    {
                                        uploadErrors.Add(ex.Message);
                                        failedUploads++;
                                        lineReading++;
                                    }
                                }
                            }
                            break;
                    }
                    //Now we have finished reading data in file, next we save into the database
                    if (formResults.Count > 0)
                    {
                        foreach (var frmRes in formResults)
                        {
                            try
                            {
                                if (!string.IsNullOrEmpty(frmRes.AppNum))
                                {
                                    var application = _registrationService.GetApplicationDetailsByAppNum(frmRes.AppNum);
                                    var savedFormResult = _configurationService.GetFormResult(frmRes.AppNum) ?? new FormResult();
                                    isUpdate = savedFormResult.Id > 0;
                                    if (isUpdate)
                                    {
                                        updatedUploads++;
                                        savedFormResult.ApplicationFormId = formResult.ApplicationFormId;
                                        if (!string.IsNullOrEmpty(frmRes.RegNum))
                                        {
                                            savedFormResult.RegNum = frmRes.RegNum;
                                        }
                                        else
                                        {
                                            if (string.IsNullOrEmpty(savedFormResult.RegNum))
                                            {
                                                if (!string.IsNullOrEmpty(application.RegNum))
                                                {
                                                    savedFormResult.RegNum = application.RegNum;
                                                }
                                            }

                                        }

                                        savedFormResult.AppNum = frmRes.AppNum;//not necessary
                                        savedFormResult.EngScore = frmRes.EngScore;
                                        savedFormResult.Subject2 = frmRes.Subject2;
                                        savedFormResult.Subject2Score = frmRes.Subject2Score;
                                        savedFormResult.Subject3 = frmRes.Subject3;
                                        savedFormResult.Subject3Score = frmRes.Subject3Score;
                                        savedFormResult.Subject4 = frmRes.Subject4;
                                        savedFormResult.Subject4Score = frmRes.Subject4Score;
                                        savedFormResult.Subject5 = frmRes.Subject5;
                                        savedFormResult.Subject5Score = frmRes.Subject5Score;
                                        savedFormResult.TotalScore = frmRes.TotalScore;
                                        _configurationService.SaveFormResult(savedFormResult);
                                        isUploaded = true;
                                    }
                                    else
                                    {
                                        if (string.IsNullOrEmpty(frmRes.RegNum))
                                        {
                                            frmRes.RegNum = application.RegNum;
                                        }
                                        _configurationService.SaveFormResult(frmRes);
                                        successfulUploads++;
                                        isUploaded = true;
                                    }
                                }
                                else
                                {
                                    var application = _registrationService.GetApplicationByRegNumAndFormId(frmRes.RegNum, Convert.ToInt32(formResult.ApplicationFormId));
                                    var savedFormResult = _configurationService.GetFormResult(Convert.ToInt32(formResult.ApplicationFormId), frmRes.RegNum) ?? new FormResult();
                                    isUpdate = savedFormResult.Id > 0;
                                    if (isUpdate)
                                    {
                                        updatedUploads++;
                                        savedFormResult.ApplicationFormId = formResult.ApplicationFormId;

                                        if (string.IsNullOrEmpty(savedFormResult.AppNum))
                                        {
                                            savedFormResult.AppNum = application.AppNum;
                                        }


                                        savedFormResult.EngScore = frmRes.EngScore;
                                        savedFormResult.Subject2 = frmRes.Subject2;
                                        savedFormResult.Subject2Score = frmRes.Subject2Score;
                                        savedFormResult.Subject3 = frmRes.Subject3;
                                        savedFormResult.Subject3Score = frmRes.Subject3Score;
                                        savedFormResult.Subject4 = frmRes.Subject4;
                                        savedFormResult.Subject4Score = frmRes.Subject4Score;
                                        savedFormResult.Subject5 = frmRes.Subject5;
                                        savedFormResult.Subject5Score = frmRes.Subject5Score;
                                        savedFormResult.TotalScore = frmRes.TotalScore;
                                        _configurationService.SaveFormResult(savedFormResult);
                                        isUploaded = true;
                                    }
                                    else
                                    {
                                        frmRes.AppNum = application.AppNum;
                                        _configurationService.SaveFormResult(frmRes);
                                        successfulUploads++;
                                        isUploaded = true;
                                    }
                                }


                            }
                            catch (Exception)
                            {

                                throw;
                            }
                        }
                    }


                    if (isUploaded)
                    {
                        var IUtilityService = EngineContext.Resolve<IUtilityService>();
                        var userRole = UserManager.GetRoles(User.Identity.GetUserId());

                        var auditTrail = new AuditTrail()
                        {
                            UserId = User.Identity.GetUserId(),
                            Username = User.Identity.GetUserName(),
                            AuditActionId = Convert.ToInt32(AuditTrailActions.ResultUpload),
                            Details = "uploaded Result " + "\'" + formResultUpload.FileName + "\'",
                            TimeStamp = localTime,
                            UserRole = userRole.First(),
                            UserIp = IUtilityService.GetIp()
                        };
                        _auditTrailRepository.SaveAuditTrail(auditTrail);
                    }
                }
            }
            Session["UploadErrors"] = uploadErrors;
            return RedirectToAction("FormResultUpload", new { applicationFormId = formResult.ApplicationFormId, sucessfulUploads = successfulUploads, UpdatedUploads = updatedUploads, faileduploads = failedUploads });
        }
        [HttpGet]
        public ActionResult SessionResultUpload(int? sessionId, int? sucessfulUploads, int? updatedUploads, int? faileduploads)
        {
            var sessionResult = new SessionResult()
            {
                SessionId = sessionId != null ? Convert.ToInt32(sessionId) : new int(),
                Sessions = _configurationService.GetSessions().OrderByDescending(x => x.Name)
            };
            var model = Mapper.Map<SessionResult, SessionResultModel>(sessionResult);

            if (Session["UploadErrors"] != null)
            {
                var uploadError = Session["UploadErrors"] as List<string>;
                foreach (var error in uploadError)
                {
                    ModelState.AddModelError("", error);
                }
                Session["UploadErrors"] = null;
            }
            if (sucessfulUploads != null)
            {
                TempData["successfulUploads"] = sucessfulUploads;
            }
            if (faileduploads != null)
            {
                TempData["failedUploads"] = faileduploads;
            }
            if (updatedUploads != null)
            {
                TempData["updatedUploads"] = updatedUploads;
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult SessionResultUpload(SessionResult sessionResult, HttpPostedFileBase sessionResultUpload, string uploadFormat)
        {
            var duplicateRegNums = new List<string>();
            int lineReading = 2;
            var uploadErrors = new List<string>();
            var sessionResults = new List<SessionResult>();
            bool isUploaded = false;
            bool isUpdate = false;
            int successfulUploads = 0;
            int failedUploads = 0;
            int updatedUploads = 0;
            if (sessionResultUpload != null)
            {
                if (sessionResultUpload.ContentLength > 0)
                {
                    //check that format is in csv
                    string fileExtension = Path.GetExtension(sessionResultUpload.FileName);
                    if (fileExtension != ".csv")
                    {
                        ModelState.AddModelError("", "Incorrect File format, Only CSV files are acceptable");
                        var result = new SessionResult()
                        {
                            Sessions = _configurationService.GetSessions()
                        };
                        var model = Mapper.Map<SessionResult, SessionResultModel>(result);
                        return View(model);
                    }
                    string filePath =
                              System.Web.HttpContext.Current.Server.MapPath("~/App_Data/AdminUploads/" +
                                                                            sessionResultUpload.FileName);
                    sessionResultUpload.SaveAs(filePath);
                    var filestream = new StreamReader(System.IO.File.OpenRead(filePath));
                    switch (uploadFormat)
                    {

                        case "1":

                            using (TextReader reader = filestream)
                            {
                                var csv = new CsvReader(reader);
                                while (csv.Read())
                                {
                                    try
                                    {
                                        var regNUm = csv.GetField<string>("RegNum").Trim();
                                        var totalScore = csv.GetField<decimal?>("TotalScore");
                                        if (string.IsNullOrEmpty(regNUm) && totalScore == null)
                                        {
                                            lineReading++;
                                            continue;
                                        }
                                        if (!string.IsNullOrEmpty(regNUm))
                                        {
                                            var ses = sessionResults.FirstOrDefault(x => x.RegNum == regNUm);
                                            if (ses != null)
                                            {
                                                sessionResults.Remove(ses);
                                                duplicateRegNums.Add(regNUm);
                                                failedUploads += 2;
                                                uploadErrors.Add(
                                                    "more than one record was found with registration number: " + regNUm);
                                                lineReading++;
                                                continue;
                                            }
                                            if (duplicateRegNums.Contains(regNUm))
                                            {
                                                failedUploads++;
                                                lineReading++;
                                                continue;
                                            }
                                        }
                                        else
                                        {
                                            uploadErrors.Add("Error uploading data in line " + lineReading +
                                                             " Registration Number is required");
                                            failedUploads++;
                                            lineReading++;
                                            continue;
                                        }
                                        if (totalScore == null)
                                        {
                                            uploadErrors.Add("Error uploading data in line " + lineReading +
                                                             " Total score is required");
                                            failedUploads++;
                                            lineReading++;
                                            continue;
                                        }
                                        //var application = _configurationService.GetApplicationsByRegNum(regNUm);
                                        //if (!application.Any())
                                        //{
                                        //    uploadErrors.Add("Error uploading data in line " + lineReading +
                                        //                     " No record was found for registration Number: " + regNUm);
                                        //    failedUploads++;
                                        //    lineReading++;
                                        //    continue;
                                        //}
                                        //if compiler gets here t means all validations have been passed next we add it to list
                                        var sesRes = new SessionResult()
                                        {
                                            SessionId = sessionResult.SessionId,
                                            RegNum = regNUm,
                                            TotalScore = Convert.ToDecimal(totalScore)
                                        };
                                        sessionResults.Add(sesRes);
                                        lineReading++;
                                    }
                                    catch (CsvMissingFieldException ex)
                                    {
                                        uploadErrors.Add(ex.Message);
                                        isUploaded = false;
                                        break;
                                    }
                                    catch (Exception ex)
                                    {
                                        uploadErrors.Add(ex.Message);
                                        failedUploads++;
                                        lineReading++;
                                    }
                                }
                            }
                            break;
                        case "2":

                            using (TextReader reader = filestream)
                            {
                                var csv = new CsvReader(reader);
                                while (csv.Read())
                                {
                                    try
                                    {
                                        var regNum = csv.GetField<string>("RegNum").Trim();
                                        var engScore = csv.GetField<decimal?>("EngScore");
                                        var subject2 = csv.GetField<string>("Subject2").Trim();
                                        var subject2Score = csv.GetField<decimal?>("Subject2Score");
                                        var subject3 = csv.GetField<string>("Subject3").Trim();
                                        var subject3Score = csv.GetField<decimal?>("Subject3Score");
                                        var subject4 = csv.GetField<string>("Subject4").Trim();
                                        var subject4Score = csv.GetField<decimal?>("Subject4Score");
                                        //var totalScore = csv.GetField<decimal?>("TotalScore");
                                        if (string.IsNullOrEmpty(regNum) && engScore == null && string.IsNullOrEmpty(subject2) && subject2Score == null && string.IsNullOrEmpty(subject3) && subject3Score == null && string.IsNullOrEmpty(subject4) && subject4Score == null)
                                        {
                                            lineReading++;
                                            continue;
                                        }
                                        //Check if RegNum is a duplicate by checking if a record in the list already has the RegNum, remove that entry if you find any
                                        //also add that regnum to the duplicateRegNums List<string>, this would help if duplicates are more than two
                                        if (!string.IsNullOrEmpty(regNum))
                                        {
                                            var ses = sessionResults.FirstOrDefault(x => x.RegNum == regNum);
                                            if (ses != null)
                                            {
                                                sessionResults.Remove(ses);
                                                duplicateRegNums.Add(regNum);
                                                failedUploads += 2;
                                                uploadErrors.Add(
                                                    "more than one record was found with registration number: " + regNum);
                                                lineReading++;
                                                continue;
                                            }
                                            if (duplicateRegNums.Contains(regNum))
                                            {
                                                failedUploads++;
                                                lineReading++;
                                                continue;
                                            }
                                        }
                                        else
                                        {
                                            uploadErrors.Add("Error uploading data in line " + lineReading +
                                                             " Registration Number is required");
                                            failedUploads++;
                                            lineReading++;
                                            continue;
                                        }

                                        if (engScore == null)
                                        {
                                            uploadErrors.Add("Error uploading data in line " + lineReading +
                                                             " Eng Score is required");
                                            failedUploads++;
                                            lineReading++;
                                            continue;
                                        }
                                        if (string.IsNullOrEmpty(subject2))
                                        {
                                            uploadErrors.Add("Error uploading data in line " + lineReading +
                                                           " Subject2 is required");
                                            failedUploads++;
                                            lineReading++;
                                            continue;
                                        }
                                        if (subject2Score == null)
                                        {
                                            uploadErrors.Add("Error uploading data in line " + lineReading +
                                                             " Subject2Score is required");
                                            failedUploads++;
                                            lineReading++;
                                            continue;
                                        }
                                        if (string.IsNullOrEmpty(subject3))
                                        {
                                            uploadErrors.Add("Error uploading data in line " + lineReading +
                                                           " Subject3 is required");
                                            failedUploads++;
                                            lineReading++;
                                            continue;
                                        }
                                        if (subject3Score == null)
                                        {
                                            uploadErrors.Add("Error uploading data in line " + lineReading +
                                                             " Subject3 Score is required");
                                            failedUploads++;
                                            lineReading++;
                                            continue;
                                        }
                                        if (string.IsNullOrEmpty(subject4))
                                        {
                                            uploadErrors.Add("Error uploading data in line " + lineReading +
                                                           " Subject4 is required");
                                            failedUploads++;
                                            lineReading++;
                                            continue;
                                        }
                                        if (subject4Score == null)
                                        {
                                            uploadErrors.Add("Error uploading data in line " + lineReading +
                                                             " Subject4 Score is required");
                                            failedUploads++;
                                            lineReading++;
                                            continue;
                                        }
                                        //check if the RegNum is valid in the applications Table
                                        //var application = _configurationService.GetApplicationsByRegNum(regNum);
                                        //if (!application.Any())
                                        //{
                                        //    uploadErrors.Add("Error uploading data in line " + lineReading +
                                        //                     " No record was found for registration Number: " + regNum);
                                        //    failedUploads++;
                                        //    lineReading++;
                                        //    continue;
                                        //}
                                        //if compiler gets here t means all validations have been passed next we add it to list
                                        var sesRes = new SessionResult()
                                        {
                                            SessionId = sessionResult.SessionId,
                                            RegNum = regNum,
                                            EngScore = engScore,
                                            Subject2 = subject2,
                                            Subject2Score = subject2Score,
                                            Subject3 = subject3,
                                            Subject3Score = subject3Score,
                                            Subject4 = subject4,
                                            Subject4Score = subject4Score,
                                            TotalScore = Convert.ToDecimal(engScore + subject2Score + subject3Score + subject4Score)
                                        };
                                        sessionResults.Add(sesRes);
                                        lineReading++;
                                    }
                                    catch (CsvMissingFieldException ex)
                                    {
                                        uploadErrors.Add(ex.Message);
                                        isUploaded = false;
                                        break;
                                    }
                                    catch (Exception ex)
                                    {
                                        uploadErrors.Add(ex.Message);
                                        failedUploads++;
                                        lineReading++;
                                    }
                                }
                            }
                            break;
                    }
                    //Now we have finished reading data in file, next we save into the database
                    if (sessionResults.Count > 0)
                    {
                        foreach (var sesRes in sessionResults)
                        {
                            try
                            {
                                var savedSessionRes = _configurationService.GetSessionResult(sesRes.RegNum) ?? new SessionResult();
                                isUpdate = savedSessionRes.Id > 0 ? true : false;
                                if (isUpdate)
                                {
                                    updatedUploads++;
                                    savedSessionRes.SessionId = sessionResult.SessionId;
                                    savedSessionRes.RegNum = sesRes.RegNum;
                                    savedSessionRes.EngScore = sesRes.EngScore;
                                    savedSessionRes.Subject2 = sesRes.Subject2;
                                    savedSessionRes.Subject2Score = sesRes.Subject2Score;
                                    savedSessionRes.Subject3 = sesRes.Subject3;
                                    savedSessionRes.Subject3Score = sesRes.Subject3Score;
                                    savedSessionRes.Subject4 = sesRes.Subject4;
                                    savedSessionRes.Subject4Score = sesRes.Subject4Score;
                                    savedSessionRes.TotalScore = sesRes.TotalScore;
                                    _configurationService.SaveSessionResult(savedSessionRes);
                                    isUploaded = true;
                                }
                                else
                                {
                                    _configurationService.SaveSessionResult(sesRes);
                                    successfulUploads++;
                                    isUploaded = true;
                                }

                            }
                            catch (Exception)
                            {

                                throw;
                            }
                        }
                    }


                    if (isUploaded)
                    {
                        var IUtilityService = EngineContext.Resolve<IUtilityService>();
                        var userRole = UserManager.GetRoles(User.Identity.GetUserId());
                        var localTime = _configurationService.GetCurrentWestAfricanDateTime();
                        var auditTrail = new AuditTrail()
                        {
                            UserId = User.Identity.GetUserId(),
                            Username = User.Identity.GetUserName(),
                            AuditActionId = Convert.ToInt32(AuditTrailActions.ResultUpload),
                            Details = "uploaded Result " + "\'" + sessionResultUpload.FileName + "\'",
                            TimeStamp = localTime,
                            UserRole = userRole.First(),
                            UserIp = IUtilityService.GetIp()
                        };
                        _auditTrailRepository.SaveAuditTrail(auditTrail);
                    }
                }
            }

            Session["UploadErrors"] = uploadErrors;
            return RedirectToAction("SessionResultUpload", new { sessionId = sessionResult.SessionId, sucessfulUploads = successfulUploads, UpdatedUploads = updatedUploads, faileduploads = failedUploads });
        }
        public ActionResult DownloadPreFormatedDoc(string fileName)
        {
            string filePath = "";
            try
            {
                filePath = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/PreFormattedFiles/" + fileName);

            }
            catch (FileNotFoundException)
            {
                throw;
            }
            catch (Exception)
            {

                throw;
            }

            return File(filePath, "application/vnd.ms-excel", fileName);
        }
        string GenerateAppNum(int appFormId)
        {
            var lastApplication = _registrationService.GetLastApplication();
            var applicationId = lastApplication == null ? 999 : lastApplication.Id;
            string todaysDate = DateTime.Today.ToString();
            todaysDate = todaysDate.Remove(10);
            string[] sep = todaysDate.Split('/');
            string year = sep[2].Remove(0, 2);
            string editedDate = sep[0] + sep[1] + year;
            string applicationNumber = (appFormId + "/" + (applicationId + 1) + "/" + editedDate).Trim();
            return applicationNumber;
        }
        private ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }
        private ApplicationRoleManager RoleManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationRoleManager>();
            }
        }
    }
}