using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Serialization;
using EduApply.Data.Entities;
using EduApply.Logic.Interfaces;
using EduApply.Logic.Service;
using EduApply.Logic.Utility;
using EduApply.Web.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;

namespace EduApply.Web.Controllers
{
    [Authorize]
    public class PaymentController : Controller
    {
        private IApplicationFormRepository _appForm;
        private IRegistrationService _registrationService;
        private IEventLogRepository _eventLogRepo;
        private IStateManager _stateManager;
        private IConfigurationService _configurationService;
        private IApiLogRepository _apiLogRepository;

        public PaymentController(IApplicationFormRepository appForm, IApiLogRepository apiLogRepository, IRegistrationService registrationService, IEventLogRepository eventLogRepo, IStateManager stateManager, IConfigurationService configurationService)
        {
            this._appForm = appForm;
            this._registrationService = registrationService;
            this._eventLogRepo = eventLogRepo;
            this._stateManager = stateManager;
            this._configurationService = configurationService;
            this._apiLogRepository = apiLogRepository;
        }
        // GET: Payment
        public ActionResult Pay()
        {
            var paymentModel = new PaymentModel();
            if (Session["Payment"] != null)
            {
                paymentModel.Amount = Convert.ToDecimal(Session["Payment"]);
            }
            return View(paymentModel);
        }

        [HttpGet]
        public ActionResult ValidatePayment()
        {
            var applicationId = Convert.ToInt32(Session["AppId"]);
            var application = _registrationService.GetApplicationDetails(applicationId);
            var applicationForm = _appForm.GetAppForms(application.AppFormId);
            var applicationFormWorkFlow = Session["WorkFlowList"] as List<ApplicationFormWorkFlow>;

            var isRightState = _stateManager.ConfirmWorkFlowStage(application, applicationFormWorkFlow, "Pay");
            if (!isRightState || application.UserName != User.Identity.GetUserName())
            {
                application.FillStage = 0;
                application.WorkFlowStage = 0;
                _registrationService.SaveApplication(application);
                ViewBag.message = "Please follow the right process, Do not copy and paste URL's";
                return View("ApplicationError");
                //  return RedirectToAction("WorkFlowManager", "Application");
            }
            if (application.IsPaid)
            {
                var paymentWorkFlow = _configurationService.GetWorkFlowbyActionName("ValidatePayment");
                if (paymentWorkFlow == null)
                {
                    ViewBag.message = "An Error, please try again...";
                    return View("ApplicationError");
                }
                var workFlowStage = applicationFormWorkFlow.FindIndex(x => x.WorkFlowId == paymentWorkFlow.Id);
                application.WorkFlowStage = workFlowStage + 1;
                _registrationService.SaveApplication(application);
                return RedirectToAction("WorkFlowManager", "Application");
            }
            var agencyCode = !string.IsNullOrEmpty(applicationForm.AgencyCode) ? applicationForm.AgencyCode : EngineContext.Resolve<Tenancy>().Code;
            var attemptedPayments = _configurationService.GetAttemptedPayments(applicationId).Where(x => !x.Successful);
            foreach (var item in attemptedPayments)
            {
                var webPayment = ProcessPayment(item.TransactionReference, agencyCode, "");
                if (webPayment.Successful && applicationForm.Fee <= webPayment.AmountBeingPaid)
                {
                    var localTime = _configurationService.GetCurrentWestAfricanDateTime();
                    application.IsPaid = true;
                    application.PaymentDate = localTime;
                    _registrationService.SaveApplication(application);

                    var intendingPayment = _apiLogRepository.GetAttemptedPayment(item.TransactionReference);
                    intendingPayment.Successful = true;
                    intendingPayment.FeeStatus = "Paid";
                    _apiLogRepository.LogAttemptedPayment(intendingPayment);

                    application.WorkFlowStage++;
                    _registrationService.SaveApplication(application);
                    return RedirectToAction("WorkFlowManager", "Application");
                }
            }

            if (applicationForm != null)
            {
                var paymentModel = new PaymentModel()
                {
                    ApplicationForm = applicationForm.Name,
                    Amount = Convert.ToDecimal(applicationForm.Fee),
                    Session = _configurationService.GetSession(applicationForm.SessionId).Name
                };
                return View(paymentModel);
            }

            return View();


        }

        public ActionResult ProceedToPayment()
        {
            var applicationId = Convert.ToInt64(Session["AppId"]);
            var applicationForm = Session["ApplicationForm"] as ApplicationForm;
            var currentDate = _configurationService.GetCurrentWestAfricanDateTime();
            var userName = User.Identity.Name;
            var personalInformation = _registrationService.GetPersonalInformationByEmail(userName);

            var attemptedPayment = new AttemptedPayment()
            {
                TransactionDate = currentDate,
                Email = userName,
                Customer = personalInformation.LastName + " " + personalInformation.FirstName + " " + personalInformation.MiddleName,
                Currency = "566",
                AmountDue = Convert.ToDecimal(applicationForm.Fee),
                TotalAmount = Convert.ToDecimal(applicationForm.Fee),
                Amount = Convert.ToDecimal(applicationForm.Fee),
                ApplicationId = applicationId,
                Narration = applicationForm.Name,
                PaymentType = applicationForm.Name,
                FeeStatus = "Fee has not yet been paid",
                AgencyCode = !string.IsNullOrEmpty(applicationForm.AgencyCode) ? applicationForm.AgencyCode : EngineContext.Resolve<Tenancy>().Code
            };
            _apiLogRepository.LogAttemptedPayment(attemptedPayment);
            Session["attemptedPayment"] = attemptedPayment;
            return this.PaymentContainer();
        }
        //
        public ActionResult PaymentContainer()
        {
            var applicationId = Convert.ToInt64(Session["AppId"]);
            var application = _registrationService.GetApplicationDetails(applicationId);
            var applicationForm = _appForm.GetAppForms(application.AppFormId);
            var model = Session["attemptedPayment"] as AttemptedPayment;// _configurationService.GetAttemptedPayment(applicationId);
            var agencyCode = !string.IsNullOrEmpty(applicationForm.AgencyCode) ? applicationForm.AgencyCode : EngineContext.Resolve<Tenancy>().Code;
            var url = "http://payments.silveredgeprojects.com/instantiate/gateways?transactionReference=" + model.TransactionReference + "&agencycode=" + agencyCode + "&gatewayagencycode=";
            // var url = "http://payments.silveredgeprojects.com/instantiate/gateways";

            //JavaScript(@"<script>window.top.location = 'https://apps.facebook.com/yourappnamespace/';</script>")
            return Redirect(url);
        }

        public ActionResult PaymentStatus(string transactionReference, string agencycode, string gatewayagencycode)
        {
            try
            {

                string queryUrl = string.Format("http://payments.silveredgeprojects.com/api/payment/status?transactionReference={0}&agencycode={1}&gatewayagencycode={2}",
                 transactionReference, agencycode, gatewayagencycode);
                WebRequest request = HttpWebRequest.Create(queryUrl);

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream dataStream = response.GetResponseStream();
                string returnString = new StreamReader(dataStream).ReadToEnd();

                JsonSerializerSettings _settings = new JsonSerializerSettings();
                _settings.DateParseHandling = DateParseHandling.DateTime | DateParseHandling.DateTimeOffset;
                _settings.DateFormatString = "dd/MM/yyyy";

                var localTime = _configurationService.GetCurrentWestAfricanDateTime();
                var intendingPayment = _apiLogRepository.GetAttemptedPayment(Convert.ToInt64(transactionReference));
                var webPayment = JsonConvert.DeserializeObject<WebPayment>(returnString, _settings);
                var application = _registrationService.GetApplicationDetails(intendingPayment.ApplicationId);
                var applicationForm = _appForm.GetAppForms(application.AppFormId);
                if (webPayment.Successful && applicationForm.Fee <= webPayment.AmountBeingPaid)
                {

                    application.IsPaid = true;
                    application.PaymentDate = localTime;
                    _registrationService.SaveApplication(application);

                    intendingPayment.Successful = true;
                    intendingPayment.FeeStatus = "Paid";
                    _apiLogRepository.LogAttemptedPayment(intendingPayment);
                }
                else
                {
                    return RedirectToAction("ValidatePayment");
                }

                var appFormWorkFlow = Session["WorkFlowList"] as List<ApplicationFormWorkFlow>;


                // application.AppNum = Session["AppNum"].ToString();
                var paymentWorkFlow = _configurationService.GetWorkFlowbyActionName("ValidatePayment");
                if (paymentWorkFlow == null)
                {
                    ViewBag.message = "An Error, please try again...";
                    return View("ApplicationError");
                }
                var workFlowStage = appFormWorkFlow.FindIndex(x => x.WorkFlowId == paymentWorkFlow.Id);
                application.WorkFlowStage = workFlowStage + 1;
                _registrationService.SaveApplication(application);
                //get the current work flow stage by getting the first work flow item in the list
                var workflowId = appFormWorkFlow[application.WorkFlowStage].WorkFlowId;
                var eventLog = new EventLog()
                {
                    ApplicationFormId = applicationForm.Id,
                    Username = User.Identity.Name,
                    WorkFlowId = workflowId,
                    Action = "validated his/her Application result for" + applicationForm.Name,
                    Timestamp = localTime
                };
                _eventLogRepo.SaveEvent(eventLog);

                if (application.WorkFlowStage < appFormWorkFlow.Count)
                {
                    //Session["WorkFlowList"] = appFormWorkFlow;
                    //get the next workflow
                    var nextWorkFlow = appFormWorkFlow[application.WorkFlowStage];
                    var workFlowItem = _appForm.GetWorkFlowItem(nextWorkFlow.WorkFlowId);
                    switch (workFlowItem.Name)
                    {
                        case "Pay":
                            return RedirectToAction("ValidatePayment", "Payment");
                        case "Fill":
                            return RedirectToAction("Fill", "Fill");
                        case "Validate Jamb Score":
                            return RedirectToAction("ValidateJamb", "Validation");
                        case "Validate Non-Application Result":
                            return RedirectToAction("ValidateNonApplicationResult", "Validation");
                        case "Validate Admission Status":
                            return RedirectToAction("CheckAdmissionStatus", "Validation");
                        case "Venue Assignment":
                            return RedirectToAction("VenueAssignment", "Validation");

                    }
                }
                else
                {
                    return RedirectToAction("ApplicationPreview", "Application");


                }
                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {

                return RedirectToAction("ValidatePayment");
            }

        }

        public WebPayment ProcessPayment(long transactionReference, string agencycode, string gatewayagencycode)
        {
            try
            {
                string queryUrl =
                    string.Format(
                        "http://payments.silveredgeprojects.com/api/payment/status?transactionReference={0}&agencycode={1}&gatewayagencycode={2}",
                        transactionReference, agencycode, gatewayagencycode);
                WebRequest request = HttpWebRequest.Create(queryUrl);

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream dataStream = response.GetResponseStream();
                string returnString = new StreamReader(dataStream).ReadToEnd();

                JsonSerializerSettings _settings = new JsonSerializerSettings();
                _settings.DateParseHandling = DateParseHandling.DateTime | DateParseHandling.DateTimeOffset;
                _settings.DateFormatString = "dd/MM/yyyy";

                var webPayment = JsonConvert.DeserializeObject<WebPayment>(returnString, _settings);
                return webPayment;
            }
            catch
            {
                return new WebPayment();
            }
        }

        public ActionResult PrintInvoice()
        {
            var applicationId = Convert.ToInt64(Session["AppId"]);
            var applicationFormId = Convert.ToInt32(Session["AppFormId"]);
            var application = _registrationService.GetApplicationDetails(applicationId);
            var applicationForm = _appForm.GetAppForms(applicationFormId);

            var attemptedPayment = _apiLogRepository.GetAttemptedPaymentByAppId(applicationId);
            var personalInfo = _registrationService.GetPersonalInformationByEmail(User.Identity.GetUserName());
            var schoolName = _configurationService.GetSchoolName();
            var localTime = _configurationService.GetCurrentWestAfricanDateTime();
            var url = _configurationService.GetCurrentUrl();
            var localDate = _configurationService.GetCurrentWestAfricanDateTime();


            if (attemptedPayment == null)
            {
                attemptedPayment = new AttemptedPayment()
                {
                    PaymentType = applicationForm.Name,
                    Narration = applicationForm.Name,
                    Amount = Convert.ToDecimal(applicationForm.Fee),
                    TotalAmount = Convert.ToDecimal(applicationForm.Fee),
                    AmountDue = Convert.ToDecimal(applicationForm.Fee),
                    PayeeName = personalInfo.LastName + " " + personalInfo.FirstName + " " + personalInfo.MiddleName,
                    PhoneNumber = personalInfo.PhoneNumber,
                    Email = personalInfo.Email,
                    FeeStatus = "Fee has not yet been paid",
                    TransactionDate = localDate,
                    ApplicationId = applicationId,
                    Currency = "566",
                    Customer = personalInfo.LastName + " " + personalInfo.FirstName + " " + personalInfo.MiddleName,
                    AgencyCode = !string.IsNullOrEmpty(applicationForm.AgencyCode) ? applicationForm.AgencyCode : EngineContext.Resolve<Tenancy>().Code
                };
                _apiLogRepository.LogAttemptedPayment(attemptedPayment);
            }




            string path = Server.MapPath("~/App_Data/PdfFiles");
            var theme = EngineContext.Resolve<Theme>();
            string imgPath = Server.MapPath("~/images/" + theme.Logo);
            string filename = path + "/" + User.Identity.Name + "_ApplicationInvoice.pdf";

            var document = new Document(PageSize.A4, 20f, 20f, 20f, 20f);
            try
            {
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(filename, FileMode.Create));

                var htmlTemplate = Server.MapPath("~/PrintTemplates/FormInvoice.html");
                string htmlText = System.IO.File.ReadAllText(htmlTemplate);
                htmlText = htmlText.Replace("#ImageLogoUrl#", imgPath);
                htmlText = htmlText.Replace("#SchoolName", schoolName);
                htmlText = htmlText.Replace("#Url#", url);
                htmlText = htmlText.Replace("#AppNum#", application.AppNum);
                htmlText = htmlText.Replace("#FormName#", applicationForm.Name);
                htmlText = htmlText.Replace("#ApplicantName#", personalInfo.LastName + " " + personalInfo.FirstName + " " + personalInfo.MiddleName);
                htmlText = htmlText.Replace("#FormFee#", applicationForm.Fee.ToString());
                htmlText = htmlText.Replace("#printDate", localTime.ToString("dd-MMM-yyyy h:mm tt"));
                htmlText = htmlText.Replace("#PaymentID#", attemptedPayment.TransactionReference.ToString());
                htmlText = htmlText.Replace("#PaymentType#", attemptedPayment.PaymentType);
                var html = new StringReader(htmlText);


                document.Open();
                XMLWorkerHelper.GetInstance().ParseXHtml(writer, document, html);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                document.Close();
            }
            string fullPath = Path.Combine(Server.MapPath("~/App_Data/PdfFiles"), filename);
            return File(fullPath, "application/pdf", application.AppNum + "_FormInvoice.pdf");
        }

        //public ActionResult Skip()
        //{
        //    var appFormWorkFlow = Session["WorkFlowList"] as List<ApplicationFormWorkFlow>;
        //    var currentWorkFlowId = appFormWorkFlow.FirstOrDefault().WorkFlowId;
        //    var currentAppFromId = appFormWorkFlow.FirstOrDefault().ApplicationFormId;

        //    var applicationFormWorkFlow = _appForm.GetApplicationFormWorkFlow(currentAppFromId, currentWorkFlowId);
        //    if (!applicationFormWorkFlow.IsCompulsory)
        //    {
        //        var applicationForm = _appForm.GetAppForms(appFormWorkFlow.FirstOrDefault().ApplicationFormId);
        //        var workflowId = appFormWorkFlow.FirstOrDefault().WorkFlowId;
        //        var eventLog = new EventLog()
        //        {
        //            ApplicationFormId = applicationForm.Id,
        //            Username = User.Identity.Name,
        //            WorkFlowId = workflowId,
        //            Action = "made payment for " + applicationForm.Name,
        //            Timestamp = DateTime.Now
        //        };
        //        _eventLogRepo.SaveEvent(eventLog);

        //        var applicationId = Convert.ToInt64(Session["AppId"]);
        //        var application = _registrationService.GetApplicationDetails(applicationId);
        //        application.WorkFlowStage++;
        //        _registrationService.SaveApplication(application);

        //        appFormWorkFlow.RemoveAt(0);
        //        if (appFormWorkFlow.Any())
        //        {
        //            var nextWorkFlow = appFormWorkFlow.FirstOrDefault();
        //            var workFlowItem = _appForm.GetWorkFlowItem(nextWorkFlow.WorkFlowId);
        //            Session["WorkFlowList"] = appFormWorkFlow;
        //            switch (workFlowItem.Name)
        //            {
        //                case "Fill":
        //                    return RedirectToAction("Fill", "Fill");
        //                case "Validate Jamb Score":
        //                    return RedirectToAction("ValidateJamb", "Validation");
        //                case "Validate Application Result":
        //                    return RedirectToAction("ValidateApplicationResult", "Validation");
        //                case "Validate Non-Application Result":
        //                    return RedirectToAction("ValidateNonApplicationResult", "Validation");
        //                case "Validate Admission Status":
        //                    return RedirectToAction("ValidateAdmissionStatus", "Validation");

        //            }
        //        }
        //        else
        //        {
        //            ViewBag.message = "Congratulations, you have successfully completed your Application";
        //            return View("ApplicationComplete");
        //        }

        //    }
        //    else
        //    {
        //        ModelState.AddModelError("","Sorry, This stage is compulsory and cannot be skipped");
        //    }

        //    ViewBag.message("Something went wrong");
        //    return View("ApplicationError");
        //}

        //public ActionResult ValidatePayment(decimal amount)
        //{
        //    if (amount < 2000)
        //    {
        //        TempData["InvalidAmount"] = true;
        //        return RedirectToAction("Pay");
        //    }
        //    var eachWorkFlow = Session["WorkFlowOrder"] as string[];
        //    var application = Session["Application"] as Application;
        //    int workflowStage = application.WorkFlowStage;
        //    workflowStage++;
        //    application = _registrationService.GetApplicationDetails(application.Id);
        //    application.WorkFlowStage = workflowStage;
        //    _registrationService.UpdateDb();
        //    Session["Application"] = application;
        //    switch (eachWorkFlow[workflowStage].Trim())
        //    {
        //        case "Fill":
        //            //var formTemplates = _appForm.GetFormTemplates();
        //            //int fillStage = application.FillStage;
        //            //var tempInApp = _appForm.GeTemplatesInApp(id).ToList();
        //            //var targetTempInApp = tempInApp[fillStage];
        //            //var fillPageToReturn = formTemplates.FirstOrDefault(x => x.Id == targetTempInApp.FormTemplateId);
        //            //return View(fillPageToReturn != null ? fillPageToReturn.Name : "Biodata");
        //            return RedirectToAction("Index", "Fill");
        //        case "Validate Jamb Score":
        //            return RedirectToAction("ValidateJambScore", "Validation");
        //            //return View("ValidateJambScore");
        //        case "Validate Result":
        //            return RedirectToAction("ValidateResult", "Validation");
        //            //return View("ValidateResult");
        //    }
        //    return Content("Success");
        //}


        //[HttpGet]
        //public ActionResult ValidatePayment()
        //{
        //    var applicationId = Convert.ToInt32(Session["AppId"]);
        //    var application = _registrationService.GetApplicationDetails(applicationId);
        //    var applicationFormWorkFlow = Session["WorkFlowList"] as List<ApplicationFormWorkFlow>;

        //    var isRightState = _stateManager.ConfirmWorkFlowStage(application, applicationFormWorkFlow, "Pay");
        //    if (!isRightState)
        //    {
        //        return RedirectToAction("WorkFlowManager", "Application");
        //    }
        //    if (application.IsPaid)
        //    {
        //        application.WorkFlowStage++;
        //        _registrationService.SaveApplication(application);
        //        return RedirectToAction("WorkFlowManager", "Application");
        //    }
        //    var payment = _registrationService.GetFeeRequest(application.AppNum);
        //    if (payment == null)
        //    {
        //        //this means user has not yet printed invoice, so return the view
        //        return View();
        //    }
        //    // var payeeId = payment.Id;
        //    //instead of the return view below it should be the implementation of etranzact api but for now lets just return the view
        //    return View();

        //    #region payment

        //    var tenancy = EngineContext.Resolve<Tenancy>();
        //    string queryUrl = string.Format("http://demo.etranzact.com/WebConnectPlus/queryReference.jsp?TERMINAL_ID={0}&PAYEE_ID={1}",
        //     tenancy.EtranzactTerminalId, payment.PayeeID);
        //    WebRequest request = HttpWebRequest.Create(queryUrl);
        //    // WebRequest request = HttpWebRequest.Create("http://demo.etranzact.com/WebConnectPlus/query.jsp?TERMINAL_ID=5001102824&CONFIRMATION_NO=500856021403605531760");
        //    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        //    Stream dataStream = response.GetResponseStream();
        //    string returnString = new StreamReader(dataStream).ReadToEnd();
        //    returnString = returnString.Trim("</html>".ToCharArray());
        //    returnString = returnString.Trim();

        //    if (returnString.Trim() == "-1" || string.IsNullOrEmpty(returnString.Trim()))
        //    {
        //        //this means payment has not been made or at least has not bn reflected

        //        return View();
        //    }
        //    else
        //    {
        //        string[] splits = returnString.Split(new char[] { '&' });
        //        string trans_amount = splits.SingleOrDefault(x => x.StartsWith("TRANS_AMOUNT"));
        //        //check if trans amount is empty and assign 0 to it else spilt using '=' and get the value after the = sign
        //        trans_amount = string.IsNullOrWhiteSpace(trans_amount) ? "0" : trans_amount.Split(new char[] { '=' })[1];
        //        string receipt_No = splits.SingleOrDefault(x => x.Contains("RECEIPT_NO"));
        //        receipt_No = string.IsNullOrWhiteSpace(receipt_No) ? "" : receipt_No.Split(new char[] { '=' })[1];
        //        string confirm_No = splits.SingleOrDefault(x => x.Contains("PAYMENT_CODE"));
        //        confirm_No = string.IsNullOrWhiteSpace(confirm_No) ? "" : confirm_No.Split(new char[] { '=' })[1];


        //        var paidAmount = Convert.ToDecimal(trans_amount);
        //        var applicationForm = Session["ApplicationForm"] as ApplicationForm;
        //        if (applicationForm.Fee <= paidAmount)
        //        {
        //            application.IsPaid = true;
        //            application.PaymentDate = DateTime.Now;
        //            application.WorkFlowStage++;
        //            _registrationService.SaveApplication(application);

        //            var appFormWorkFlow = Session["WorkFlowList"] as List<ApplicationFormWorkFlow>;
        //            var workflowId = appFormWorkFlow[application.WorkFlowStage].WorkFlowId;
        //            var eventLog = new EventLog()
        //            {
        //                ApplicationFormId = applicationForm.Id,
        //                Username = User.Identity.Name,
        //                WorkFlowId = workflowId,
        //                Action = "made payment for " + applicationForm.Name,
        //                Timestamp = DateTime.Now
        //            };
        //            _eventLogRepo.SaveEvent(eventLog);

        //            return RedirectToAction("WorkFlowManager", "Application");
        //        }

        //    }
        //    #endregion



        //    //if we reach here then something went wrong
        //    ViewBag.message = "Something went wrong";
        //    return View("ApplicationError");
        //}

        //[HttpPost]
        //public ActionResult ValidatePayment(decimal amount)
        //{
        //    var applicationId = Convert.ToInt64(Session["AppId"]);
        //    var application = _registrationService.GetApplicationDetails(applicationId);
        //    var appFormWorkFlow = Session["WorkFlowList"] as List<ApplicationFormWorkFlow>;
        //    var applicationForm = _appForm.GetAppForms(appFormWorkFlow.FirstOrDefault().ApplicationFormId);
        //    if (amount >= applicationForm.Fee)
        //    {
        //        var workflowId = appFormWorkFlow[application.WorkFlowStage].WorkFlowId;
        //        var eventLog = new EventLog()
        //        {
        //            ApplicationFormId = applicationForm.Id,
        //            Username = User.Identity.Name,
        //            WorkFlowId = workflowId,
        //            Action = "made payment for " + applicationForm.Name,
        //            Timestamp = DateTime.Now
        //        };
        //        _eventLogRepo.SaveEvent(eventLog);


        //        application.IsPaid = true;
        //        application.WorkFlowStage++;
        //        _registrationService.SaveApplication(application);
        //        Session["Payment"] = amount;
        //    }
        //    else
        //    {
        //        ModelState.AddModelError("", "Amount entered is less than fee for this form");
        //        return View();
        //    }
        //    //var passedWorkFlow = Session["PassedWorkFlow"] as List<ApplicationFormWorkFlow> ?? new List<ApplicationFormWorkFlow>();
        //    //passedWorkFlow.Insert(0, appFormWorkFlow.FirstOrDefault());
        //    //Session["PassedWorkFlow"] = passedWorkFlow;
        //    //appFormWorkFlow.RemoveAt(0);
        //    if (application.WorkFlowStage < appFormWorkFlow.Count)
        //    {
        //        var nextWorkFlow = appFormWorkFlow[application.WorkFlowStage];
        //        var workFlowItem = _appForm.GetWorkFlowItem(nextWorkFlow.WorkFlowId);
        //        Session["WorkFlowList"] = appFormWorkFlow;
        //        switch (workFlowItem.Name)
        //        {
        //            case "Fill":
        //                return RedirectToAction("Fill", "Fill");
        //            case "Validate Jamb Score":
        //                return RedirectToAction("ValidateJamb", "Validation");
        //            case "Validate Application Result":
        //                return RedirectToAction("ValidateApplicationResult", "Validation");
        //            case "Validate Non-Application Result":
        //                return RedirectToAction("ValidateNonApplicationResult", "Validation");
        //            case "Validate Admission Status":
        //                return RedirectToAction("CheckAdmissionStatus", "Validation");

        //        }
        //    }
        //    else
        //    {
        //        return RedirectToAction("ApplicationPreview", "Application");

        //        //var applicationId = Convert.ToInt64(Session["AppId"]);

        //    }

        //    ViewBag.message("Something went wrong");
        //    return View("ApplicationError");
        //}
    }
}