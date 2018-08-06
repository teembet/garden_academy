using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EduApply.Logic.Interfaces;
using EduApply.Logic.Service;
using EduApply.Logic.Utility;
using Ionic.Zip;
using Newtonsoft.Json;

namespace EduApply.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ConfigurationController : Controller
    {
        private IRegistrationService _registrationService;
        private IConfigurationService _configurationService;
        private IApplicationFormRepository _appForm;
        private IApiLogRepository _apiLogRepository;
        public ConfigurationController(IRegistrationService registrationService, IConfigurationService configurationService, IApplicationFormRepository appForm, IApiLogRepository apiLogRepository)
        {
            this._registrationService = registrationService;
            this._configurationService = configurationService;
            this._appForm = appForm;
            this._apiLogRepository = apiLogRepository;
        }
        // GET: Configuration
        public ActionResult Index()
        {

           


            //var applicantsProgramCourse = _registrationService.GetApplicantsProgramCourses();
            //foreach (var appProgCourse in applicantsProgramCourse)
            //{
            //    var course = _configurationService.GetCourse(appProgCourse.CourseId);
            //    if (course != null)
            //    {
            //        appProgCourse.DepartmentId = course.DepartmentId;
            //        _registrationService.SaveApplicantsProgramCourse(appProgCourse);
            //    }

            //}



            //var applicantsProgramCourse = _registrationService.GetApplicantsProgramCourses();
            //foreach (var appProgCourse in applicantsProgramCourse)
            //{
            //    var course = _configurationService.GetCourse(appProgCourse.CourseId);
            //    var department = _configurationService.GetDepartment(course.DepartmentId);
            //    var application = _registrationService.GetApplicationDetails(appProgCourse.ApplicationId);
            //    if (application != null)
            //    {
            //        if (application.DepartmentId > 0)
            //        {
            //            continue;
            //        }
            //        application.DepartmentId = department.Id;
            //        application.FacultyId = department.FacultyId;
            //        _registrationService.SaveApplication(application);
            //    }

            //}


            //var applications = _registrationService.GetApplications().Where(x => x.IsSubmitted && !x.IsPaid);
            //foreach (var application in applications)
            //{
            //    var applicationForm = _appForm.GetAppForms(application.AppFormId);
            //    var agencyCode = !string.IsNullOrEmpty(applicationForm.AgencyCode) ? applicationForm.AgencyCode : EngineContext.Resolve<Tenancy>().Code;
            //    var attemPayment = _configurationService.GetAttemptedPayments(application.Id).ToList();
            //    foreach (var item in attemPayment)
            //    {
            //        try
            //        {
            //            string queryUrl =
            //             string.Format("http://payments.silveredgeprojects.com/api/payment/Revalidate?transactionReference={0}&agencycode={1}&gatewayagencycode={2}",
            //                 item.TransactionReference, agencyCode, item.GatewayAgencyCode);
            //            WebRequest request = HttpWebRequest.Create(queryUrl);

            //            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            //            Stream dataStream = response.GetResponseStream();
            //            string returnString = new StreamReader(dataStream).ReadToEnd();

            //            JsonSerializerSettings _settings = new JsonSerializerSettings();
            //            _settings.DateParseHandling = DateParseHandling.DateTime | DateParseHandling.DateTimeOffset;
            //            _settings.DateFormatString = "dd/MM/yyyy";

            //            var isSuccessful = JsonConvert.DeserializeObject<Boolean>(returnString, _settings);
            //            if (isSuccessful)
            //            {
            //                var localTime = _configurationService.GetCurrentWestAfricanDateTime();
            //                application.IsPaid = true;
            //                application.PaymentDate = localTime;
            //                _registrationService.SaveApplication(application);

            //                var attemptedPayment = _apiLogRepository.GetAttemptedPayment(item.TransactionReference);
            //                attemptedPayment.Successful = true;
            //                attemptedPayment.FeeStatus = "Paid";
            //                _apiLogRepository.LogAttemptedPayment(attemptedPayment);
            //            }

            //        }
            //        catch (Exception)
            //        {
            //            continue;
            //        }
            //    }
            //}
            return RedirectToAction("Index", "Session");
        }
    }
}