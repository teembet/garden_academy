using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EduApply.Data.Entities;
using EduApply.Logic.Interfaces;
using EduApply.Logic.Service;

namespace EduApply.Web.Controllers
{
    public class ApplicantsController : ApiController
    {
        IRegistrationService registrationService = EngineContext.Resolve<IRegistrationService>();
        IApiLogRepository apiService = EngineContext.Resolve<IApiLogRepository>();
        public IEnumerable<FeeRequest> Get()
        {
            var apiLog = new ApiLog()
            {
                Action = "GET",
                Details = "Retrieved All data for payment",
                TimeStamp = DateTime.Now,
                UserIp = UtilityService.GetIp(System.Web.HttpContext.Current)
            };
            apiService.LogApiEvent(apiLog);

            var payments = registrationService.GetFeeRequests();

            return payments;
        }
        public FeeRequest GetSingleFeeRequest(long PAYEE_ID, string PAYMENT_TYPE)
        {
            var feeRequest = registrationService.GetFeeRequest(PAYEE_ID, PAYMENT_TYPE);
            //General Log
            if (feeRequest != null)
            {
                var apiLog = new ApiLog()
                {
                    Action = "GET",
                    Details = "Retrieved payment data for applicant with PAYEE_ID: " + PAYEE_ID,
                    TimeStamp = DateTime.Now,
                    UserIp = UtilityService.GetIp(System.Web.HttpContext.Current)
                };
                apiService.LogApiEvent(apiLog);

                var savedAttemptedPayment = apiService.GetAttemptedPayment(feeRequest.ApplicationNumber, feeRequest.PayeeID);
                if (savedAttemptedPayment == null)
                {
                    var attemptedPayment = new AttemptedPayment()
                    {
                        ApplicationNumber = feeRequest.ApplicationNumber,
                        TerminalId = "0000223344",
                        PayeeId = feeRequest.PayeeID
                    };
                    apiService.LogAttemptedPayment(attemptedPayment);
                }

            }
            return feeRequest;

        }

        public HttpResponseMessage PostSingle(string appNum)
        {
            var applicationDetails = registrationService.GetApplicationDetailsByAppNum(appNum);
            if (applicationDetails != null)
            {
                //General Log
                var apiLog = new ApiLog()
                {
                    Action = "POST",
                    Details = "Confirmed payment for applicant with Application Number: " + appNum,
                    TimeStamp = DateTime.Now,
                    UserIp = UtilityService.GetIp(System.Web.HttpContext.Current)
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

        public HttpResponseMessage Post(IEnumerable<FeeRequest> payments)
        {

            foreach (var payment in payments)
            {
                var applicationDetails = registrationService.GetApplicationDetailsByAppNum(payment.ApplicationNumber);
                if (applicationDetails != null)
                {                    //General Log
                    var apiLog = new ApiLog()
                    {
                        Action = "POST",
                        Details = "Confirmed payment for applicant with Application Number: " + payment.ApplicationNumber,
                        TimeStamp = DateTime.Now,
                        UserIp = UtilityService.GetIp(System.Web.HttpContext.Current)
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
