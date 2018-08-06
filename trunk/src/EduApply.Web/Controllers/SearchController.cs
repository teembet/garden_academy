using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EduApply.Data.Entities;
using EduApply.Logic.Interfaces;
using EduApply.Logic.Service;
using EduApply.Logic.Utility;
using EduApply.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json;

namespace EduApply.Web.Controllers
{
    [Authorize(Roles = "Admin, SchoolAdmin")]
    public class SearchController : Controller
    {
        private IConfigurationService _configurationService;
        private IApplicationFormRepository _appFormRepository;
        private ISearchRepository _searchRepository;
        private IRegistrationService _registrationService;
        private IVenueAssignmentService _venueService;
        private IApiLogRepository _apiLogRepository;
        private IAuditTrailRepository _auditTrailRepository;
        public SearchController(IConfigurationService configurationService, IApiLogRepository apiLogRepository,IAuditTrailRepository auditTrailRepository, IVenueAssignmentService venueService, IApplicationFormRepository appFormRepository, ISearchRepository searchRepository, IRegistrationService registrationService)
        {
            this._configurationService = configurationService;
            this._appFormRepository = appFormRepository;
            this._searchRepository = searchRepository;
            this._registrationService = registrationService;
            this._venueService = venueService;
            this._auditTrailRepository = auditTrailRepository;
            this._apiLogRepository = apiLogRepository;
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetApplicationFormsBySessionId(int? sessionId)
        {
            var sesId = sessionId == null ? -1 : Convert.ToInt32(sessionId);
            // var session = _configurationService.GetSession(sesId);
            var applicationForms = _appFormRepository.GetAppFormsBySessionId(sesId);
            var result = (from s in applicationForms
                          select new
                          {
                              id = s.Id,
                              name = s.Name
                          }).ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetFormTemplatesByFormId(int? formId)
        {
            var frmId = formId == null ? -1 : Convert.ToInt32(formId);
            var formTemplates = _appFormRepository.GetFormTemplatesByFormId(frmId);
            var result = (from s in formTemplates
                          select new
                          {
                              id = s.Id,
                              name = s.Name
                          }).ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetDepartmentsByFacultyId(int? facultyId)
        {
            var facId = facultyId == null ? -1 : Convert.ToInt32(facultyId);
            var departments = facId != -1 ? _configurationService.GetDepartments(facId).ToList() : _configurationService.GetDepartments();
            var result = (from s in departments
                          select new
                          {
                              id = s.Id,
                              name = s.Name
                          }).ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetCoursesByDepartmentId(int? departmentId)
        {
            var depId = departmentId == null ? -1 : Convert.ToInt32(departmentId);
            var courses = depId != -1 ? _configurationService.GetCoursesByDepId(depId) : _configurationService.GetCourses();
            var result = (from s in courses
                          select new
                          {
                              id = s.Id,
                              name = s.Name
                          }).ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetProgramsByCourseId(int? courseId)
        {
            var cosId = courseId == null ? -1 : Convert.ToInt32(courseId);
            var programs = cosId != -1 ? _configurationService.GetProgramsByCourseId(cosId) : _configurationService.GetPrograms();
            var result = (from s in programs
                          select new
                          {
                              id = s.Id,
                              name = s.Name,
                          }).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Index()
        {
            var searchModel = new SearchModel()
            {
                Sessions = _configurationService.GetSessions(),
                ApplicationForms = _appFormRepository.GetAppForms().OrderBy(x => x.Name),
                FormTemplates = _appFormRepository.GetFormTemplates().OrderBy(x => x.Name),
                Faculties = _configurationService.GetFaculties().OrderBy(x => x.Name),
                Departments = _configurationService.GetDepartments().OrderBy(x => x.Name),
                Courses = _configurationService.GetCourses().OrderBy(x => x.Name),
                Programs = _configurationService.GetPrograms().OrderBy(x => x.Code),
                Venues = _venueService.GetVenues().OrderBy(x => x.Name),
                SearchResult = new List<SearchResult>()
            };
            return View(searchModel);
        }
        [HttpPost]
        public ActionResult Index(SearchResultQuery query)
        {
            var returnedResult = _searchRepository.GetSearchResult(query).ToList();
            var searchModel = new SearchModel()
            {
                SessionId = query.SessionId,
                Sessions = _configurationService.GetSessions().OrderBy(x => x.Name),
                FormId = query.FormId,
                ApplicationForms = _appFormRepository.GetAppForms().OrderBy(x => x.Name),
                FormTemplateId = query.FormTemplateId,
                FormTemplates = _appFormRepository.GetFormTemplates().OrderBy(x => x.Name),
                FacultyId = query.FacultyId,
                Faculties = _configurationService.GetFaculties().OrderBy(x => x.Name),
                DepartmentId = query.DepartmentId,
                Departments = _configurationService.GetDepartments().OrderBy(x => x.Name),
                CourseOfStudyId = query.CourseOfStudyId,
                Courses = _configurationService.GetCourses().OrderBy(x => x.Name),
                ProgramId = query.ProgramId,
                Programs = _configurationService.GetPrograms().OrderBy(x => x.Code),
                VenueId = query.VenueId,
                Venues = _venueService.GetVenues().OrderBy(x => x.Name),
                IsPaid = query.IsPaid,
                IsAdmitted = query.IsAdmitted,
                IsSubmitted = query.IsSubmitted,
                Name = query.Name,
                StartDate = query.StartDate,
                EndDate = query.EndDate,
                DateType = query.DateType,
                SearchResult = returnedResult
            };
            var appNums = returnedResult.Select(x => x.AppNum).ToList();
            Session["SearchAppNos"] = appNums;
            Session["searchModel"] = searchModel;
            return View(searchModel);
        }
        public ActionResult ResetApplication(long appId)
        {
            var application = _registrationService.GetApplicationDetails(appId);
            application.IsSubmitted = false;
            _registrationService.SaveApplication(application);
            return RedirectToAction("SearchErrorHandler", new { errorMessage = "RESET SUCCESSFUL" });
        }

        public ActionResult ReversePayment(long appId)
        {
            var application = _registrationService.GetApplicationDetails(appId);
            var applicationForm = _appFormRepository.GetAppForms(application.AppFormId);
            var agencyCode = !string.IsNullOrEmpty(applicationForm.AgencyCode) ? applicationForm.AgencyCode : EngineContext.Resolve<Tenancy>().Code;
            var attemPayment = _configurationService.GetAttemptedPayments(appId).Where(x => x.Successful).ToList();
            if (application.IsPaid)
            {
                foreach (var item in attemPayment)
                {
                    try
                    {
                        string queryUrl = string.Format("http://payments.silveredgeprojects.com/api/payment/Reversal?transactionReference={0}&agencycode={1}&gatewayagencycode={2}", item.TransactionReference, agencyCode, item.GatewayAgencyCode);
                        WebRequest request = HttpWebRequest.Create(queryUrl);

                        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                        Stream dataStream = response.GetResponseStream();
                        string returnString = new StreamReader(dataStream).ReadToEnd();

                        JsonSerializerSettings _settings = new JsonSerializerSettings();
                        _settings.DateParseHandling = DateParseHandling.DateTime | DateParseHandling.DateTimeOffset;
                        _settings.DateFormatString = "dd/MM/yyyy";

                        var isSuccessful = JsonConvert.DeserializeObject<Boolean>(returnString, _settings);
                        if (isSuccessful)
                        {

                            application.IsPaid = false;
                            _registrationService.SaveApplication(application);

                            var attemptedPaymentList = _apiLogRepository.GetAttemptedPaymentsForApplicant(application.Id);
                            foreach (var paymentItem in attemptedPaymentList)
                            {
                                paymentItem.FeeStatus = "Fee Reversed";
                                paymentItem.Successful = false;
                                _apiLogRepository.LogAttemptedPayment(paymentItem);
                            }
                            var IUtilityService = EngineContext.Resolve<IUtilityService>();
                            var loggedInUserRole = UserManager.GetRoles(User.Identity.GetUserId());
                            var localTime = _configurationService.GetCurrentWestAfricanDateTime();
                            var auditTrail = new AuditTrail()
                            {
                                UserId = User.Identity.GetUserId(),
                                Username = User.Identity.GetUserName(),
                                AuditActionId = Convert.ToInt32(AuditTrailActions.UpdateUserRecord),
                                Details = User.Identity.GetUserName()+" Deactivated Payment For"+application.UserName,
                                TimeStamp = localTime,
                                UserRole = loggedInUserRole.First(),
                                UserIp = IUtilityService.GetIp()
                            };
                            _auditTrailRepository.SaveAuditTrail(auditTrail);
                            return RedirectToAction("SearchErrorHandler", new { errorMessage = "REVERSAL SUCCESSFUL" });
                        }
                    }
                    catch (Exception)
                    {

                        continue;
                    }
                }


            }
            else
            {
                return RedirectToAction("SearchErrorHandler", new { errorMessage = "REVERSAL UNNECESSARY" });
            }


            return RedirectToAction("SearchErrorHandler", new { errorMessage = "REVERSAL FAILED" });
        }

        public ActionResult RevalidatePayment(long appId)
        {
            var application = _registrationService.GetApplicationDetails(appId);
            var applicationForm = _appFormRepository.GetAppForms(application.AppFormId);
            var agencyCode = !string.IsNullOrEmpty(applicationForm.AgencyCode) ? applicationForm.AgencyCode : EngineContext.Resolve<Tenancy>().Code;
            var attemPayment = _configurationService.GetAttemptedPayments(appId).Where(x => !x.Successful).ToList();
            if (!application.IsPaid)
            {
                foreach (var item in attemPayment)
                {
                    try
                    {
                        string queryUrl =
                         string.Format("http://payments.silveredgeprojects.com/api/payment/Revalidate?transactionReference={0}&agencycode={1}&gatewayagencycode={2}",
                             item.TransactionReference, agencyCode, item.GatewayAgencyCode);
                        WebRequest request = HttpWebRequest.Create(queryUrl);

                        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                        Stream dataStream = response.GetResponseStream();
                        string returnString = new StreamReader(dataStream).ReadToEnd();

                        JsonSerializerSettings _settings = new JsonSerializerSettings();
                        _settings.DateParseHandling = DateParseHandling.DateTime | DateParseHandling.DateTimeOffset;
                        _settings.DateFormatString = "dd/MM/yyyy";

                        var isSuccessful = JsonConvert.DeserializeObject<Boolean>(returnString, _settings);
                        if (isSuccessful)
                        {
                            var localTime = _configurationService.GetCurrentWestAfricanDateTime();
                            application.IsPaid = true;
                            application.PaymentDate = localTime;
                            _registrationService.SaveApplication(application);

                            var attemptedPayment = _apiLogRepository.GetAttemptedPayment(item.TransactionReference);
                            attemptedPayment.Successful = true;
                            attemptedPayment.FeeStatus = "Paid";
                            _apiLogRepository.LogAttemptedPayment(attemptedPayment);
                            return RedirectToAction("SearchErrorHandler", new { errorMessage = "REVALIDATE SUCCESSFUL" });
                        }
                        return RedirectToAction("SearchErrorHandler", new { errorMessage = "PAYMENT RECORD NOT FOUND" });

                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }

            }
            else
            {
                return RedirectToAction("SearchErrorHandler", new { errorMessage = "REVALIDATE UNNECESSARY" });
            }
            return RedirectToAction("SearchErrorHandler", new { errorMessage = "REVALIDATE FAILED" });
        }

        public ActionResult SearchErrorHandler(string errorMessage)
        {
            var searchModel = Session["searchModel"] as SearchModel;
            TempData["ErrorMessage"] = errorMessage;
            return View("Index", searchModel);
        }

        private ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

    }
}