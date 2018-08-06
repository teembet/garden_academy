using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using EduApply.Data.Entities;
using EduApply.Logic.Interfaces;
using EduApply.Logic.Service;
using EduApply.Web.Models;

namespace EduApply.Web.Controllers
{
    public class ApplicantsController : ApiController
    {
        IRegistrationService registrationService = EngineContext.Resolve<IRegistrationService>();
        IConfigurationService configurationService = EngineContext.Resolve<IConfigurationService>();
        IApiLogRepository apiService = EngineContext.Resolve<IApiLogRepository>();
        public IEnumerable<FeeRequestPayment> Get()
        {
            var IUtilityService = EngineContext.Resolve<IUtilityService>();
            var apiLog = new ApiLog()
            {
                Action = "GET",
                Details = "Retrieved All data for payment",
                TimeStamp = configurationService.GetCurrentWestAfricanDateTime(),
                UserIp = IUtilityService.GetIp()
            };
            apiService.LogApiEvent(apiLog);

            var payments = registrationService.GetFeeRequests();

            return payments;
        }
        public AttemptedPaymentModel GetSingleFeeRequest(long TransactionReference)
        {
            var IUtilityService = EngineContext.Resolve<IUtilityService>();

            var attemptedPayment = apiService.GetAttemptedPayment(TransactionReference);
            var application = registrationService.GetApplicationDetails(attemptedPayment.ApplicationId);
            var splits = configurationService.GetSplits(application.AppFormId).ToList();
            //General Log
            if (attemptedPayment != null)
            {
                var apiLog = new ApiLog()
                {
                    Action = "GET",
                    Details = "Retrieved payment data for applicant with transaction Reference: " + TransactionReference,
                    TimeStamp = configurationService.GetCurrentWestAfricanDateTime(),
                    UserIp = IUtilityService.GetIp()
                };
                apiService.LogApiEvent(apiLog);
            }
            var AttemptedPayment = Mapper.Map<AttemptedPayment, AttemptedPaymentModel>(attemptedPayment);
            AttemptedPayment.Splits = splits;
            return AttemptedPayment;

        }

        public HttpResponseMessage PostSingle(string appNum)
        {
            var IUtilityService = EngineContext.Resolve<IUtilityService>();
            var applicationDetails = registrationService.GetApplicationDetailsByAppNum(appNum);
            if (applicationDetails != null)
            {
                //General Log
                var apiLog = new ApiLog()
                {
                    Action = "POST",
                    Details = "Confirmed payment for applicant with Application Number: " + appNum,
                    TimeStamp = configurationService.GetCurrentWestAfricanDateTime(),
                    UserIp = IUtilityService.GetIp()
                };
                apiService.LogApiEvent(apiLog);
                applicationDetails.IsPaid = true;
                registrationService.SaveApplication(applicationDetails);
                return Request.CreateResponse(HttpStatusCode.NoContent);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }

        public HttpResponseMessage Post(IEnumerable<FeeRequestPayment> payments)
        {
            var IUtilityService = EngineContext.Resolve<IUtilityService>();

            foreach (var payment in payments)
            {
                var applicationDetails = registrationService.GetApplicationDetailsByAppNum(payment.ApplicationNumber);
                if (applicationDetails != null)
                {                    //General Log
                    var apiLog = new ApiLog()
                    {
                        Action = "POST",
                        Details = "Confirmed payment for applicant with Application Number: " + payment.ApplicationNumber,
                        TimeStamp = configurationService.GetCurrentWestAfricanDateTime(),
                        UserIp = IUtilityService.GetIp()
                    };
                    apiService.LogApiEvent(apiLog);
                    applicationDetails.IsPaid = true;
                    registrationService.SaveApplication(applicationDetails);
                    return Request.CreateResponse(HttpStatusCode.NoContent);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }




            }

            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

    }
}
