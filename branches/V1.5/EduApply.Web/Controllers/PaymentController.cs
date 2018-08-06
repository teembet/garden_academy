using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EduApply.Data.Entities;
using EduApply.Logic.Interfaces;
using EduApply.Web.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using Microsoft.AspNet.Identity;

namespace EduApply.Web.Controllers
{
    public class PaymentController : Controller
    {
        private IApplicationFormRepository _appForm;
        private IRegistrationService _registrationService;
        private IEventLogRepository _eventLogRepo;
        private IStateManager _stateManager;
        private IConfigurationService _configurationService;

        public PaymentController(IApplicationFormRepository appForm, IRegistrationService registrationService, IEventLogRepository eventLogRepo, IStateManager stateManager, IConfigurationService configurationService)
        {
            this._appForm = appForm;
            this._registrationService = registrationService;
            this._eventLogRepo = eventLogRepo;
            this._stateManager = stateManager;
            this._configurationService = configurationService;
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
            var applicationFormWorkFlow = Session["WorkFlowList"] as List<ApplicationFormWorkFlow>;

            var isRightState = _stateManager.ConfirmWorkFlowStage(application, applicationFormWorkFlow, "Pay");
            if (!isRightState)
            {
                return RedirectToAction("WorkFlowManager", "Application");
            }
            if (application.IsPaid)
            {
                application.WorkFlowStage++;
                _registrationService.SaveApplication(application);
                return RedirectToAction("WorkFlowManager", "Application");
            }
            var payment = _registrationService.GetFeeRequest(application.AppNum);
            if (payment == null)
            {
                //this means user has not yet printed invoice, so return the view
                return View();
            }
           // var payeeId = payment.Id;
            //instead of the return view below it should be the implementation of etranzact api but for now lets just return the view
            return View();



            //if we reach here then something went wrong
            ViewBag.message = "Something went wrong";
            return View("ApplicationError");
        }
        [HttpPost]
        public ActionResult ValidatePayment(decimal amount)
        {
            var applicationId = Convert.ToInt64(Session["AppId"]);
            var application = _registrationService.GetApplicationDetails(applicationId);
            var appFormWorkFlow = Session["WorkFlowList"] as List<ApplicationFormWorkFlow>;
            var applicationForm = _appForm.GetAppForms(appFormWorkFlow.FirstOrDefault().ApplicationFormId);
            if (amount >= applicationForm.Fee)
            {
                var workflowId = appFormWorkFlow[application.WorkFlowStage].WorkFlowId;
                var eventLog = new EventLog()
                {
                    ApplicationFormId = applicationForm.Id,
                    Username = User.Identity.Name,
                    WorkFlowId = workflowId,
                    Action = "made payment for " + applicationForm.Name,
                    Timestamp = DateTime.Now
                };
                _eventLogRepo.SaveEvent(eventLog);


                application.IsPaid = true;
                application.WorkFlowStage++;
                _registrationService.SaveApplication(application);
                Session["Payment"] = amount;
            }
            else
            {
                ModelState.AddModelError("", "Amount entered is less than fee for this form");
                return View();
            }
            //var passedWorkFlow = Session["PassedWorkFlow"] as List<ApplicationFormWorkFlow> ?? new List<ApplicationFormWorkFlow>();
            //passedWorkFlow.Insert(0, appFormWorkFlow.FirstOrDefault());
            //Session["PassedWorkFlow"] = passedWorkFlow;
            //appFormWorkFlow.RemoveAt(0);
            if (application.WorkFlowStage < appFormWorkFlow.Count)
            {
                var nextWorkFlow = appFormWorkFlow[application.WorkFlowStage];
                var workFlowItem = _appForm.GetWorkFlowItem(nextWorkFlow.WorkFlowId);
                Session["WorkFlowList"] = appFormWorkFlow;
                switch (workFlowItem.Name)
                {
                    case "Fill":
                        return RedirectToAction("Fill", "Fill");
                    case "Validate Jamb Score":
                        return RedirectToAction("ValidateJamb", "Validation");
                    case "Validate Application Result":
                        return RedirectToAction("ValidateApplicationResult", "Validation");
                    case "Validate Non-Application Result":
                        return RedirectToAction("ValidateNonApplicationResult", "Validation");
                    case "Validate Admission Status":
                        return RedirectToAction("CheckAdmissionStatus", "Validation");

                }
            }
            else
            {
                return RedirectToAction("ApplicationPreview", "Application");

                //var applicationId = Convert.ToInt64(Session["AppId"]);

            }

            ViewBag.message("Something went wrong");
            return View("ApplicationError");
        }

        public ActionResult PrintInvoice()
        {
            var applicationId = Convert.ToInt64(Session["AppId"]);
            var applicationFormId = Convert.ToInt32(Session["AppFormId"]);
            var application = _registrationService.GetApplicationDetails(applicationId);
            var applicationForm = _appForm.GetAppForms(applicationFormId);
            var feeRequest = _registrationService.GetFeeRequest(application.AppNum);
            var personalInfo = _registrationService.GetPersonalInformationByEmail(User.Identity.GetUserName());
            var schoolName = _configurationService.GetSchoolName();
            var localTime = _configurationService.GetCurrentWestAfricanDateTime();
            var url = _configurationService.GetCurrentUrl();
            if (feeRequest == null)
            {
                feeRequest = new FeeRequest()
                {
                    ApplicationNumber = application.AppNum,
                    PaymentType = applicationForm.Name,
                    Amount = Convert.ToDecimal(applicationForm.Fee),
                    PayeeName = personalInfo.LastName + " " + personalInfo.FirstName + " " + personalInfo.MiddleName,
                    PhoneNumber = personalInfo.PhoneNumber,
                    Email = personalInfo.Email,
                    FeeStatus = "Fee has not yet been paid"
                };
                _registrationService.SaveFeeRequest(feeRequest);
            }


            string path = Server.MapPath("~/App_Data/PdfFiles");
            string imgPath = Server.MapPath("~/images/SchoolLogo.jpg");
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
                htmlText = htmlText.Replace("#PaymentID#", DateTime.Now.ToString(feeRequest.PayeeID.ToString()));
                htmlText = htmlText.Replace("#PaymentType#", feeRequest.PaymentType);
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
    }
}