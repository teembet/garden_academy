using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EduApply.Data.Entities;
using EduApply.Logic.Interfaces;
using EduApply.Web.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;

namespace EduApply.Web.Controllers
{
    public class ValidationController : Controller
    {
        private IApplicationFormRepository _appForm;
        private IRegistrationService _registrationService;
        private IConfigurationService _configurationService;
        private IEventLogRepository _eventLogRepo;
        private IStateManager _stateManager;

        public ValidationController(IApplicationFormRepository appForm, IRegistrationService registrationService, IConfigurationService configurationService, IEventLogRepository eventLogRepo, IStateManager stateManager)
        {
            this._appForm = appForm;
            this._registrationService = registrationService;
            this._configurationService = configurationService;
            this._eventLogRepo = eventLogRepo;
            this._stateManager = stateManager;
        }

        [HttpGet]
        public ActionResult ValidateApplicationResult(int? canProceed)
        {
            if (canProceed == 0)
            {
                ModelState.AddModelError("", "Sorry, you cannot proceed with this application without a valid result");
            }
            var formResultModel = new FormResultModel();
            var applicationId = Convert.ToInt64(Session["AppId"]);
            var application = _registrationService.GetApplicationDetails(applicationId);


            var applicationFormWorkFlow = Session["WorkFlowList"] as List<ApplicationFormWorkFlow>;

            var isRightState = _stateManager.ConfirmWorkFlowStage(application, applicationFormWorkFlow, "Validate Application Result");
            if (!isRightState)
            {
                return RedirectToAction("WorkFlowManager", "Application");
            }


            //i am using the MappedToAppNumto get result automatically if MappedToAppNum is not null, MappedToAppNum wont be null if user has checked result at least once
            if (!string.IsNullOrEmpty(application.MappedToAppNum))
            {
                var formResult = _configurationService.GetFormResult(application.MappedToAppNum);
                if (formResult != null)
                {
                    formResultModel.RegNum = formResult.RegNum;
                    formResultModel.AppNum = formResult.AppNum;
                    formResultModel.EngScore = formResult.EngScore;
                    formResultModel.Subject2 = formResult.Subject2;
                    formResultModel.Subject2Score = formResult.Subject2Score;
                    formResultModel.Subject3 = formResult.Subject3;
                    formResultModel.Subject3Score = formResult.Subject3Score;
                    formResultModel.Subject4 = formResult.Subject4;
                    formResultModel.Subject4Score = formResult.Subject4Score;
                    formResultModel.TotalScore = formResult.TotalScore;

                    Session["AppResultIsPresent"] = true;
                }

            }
            return View(formResultModel);
        }
        [HttpPost]
        public ActionResult ValidateApplicationResult(FormResultModel result)
        {
            var application = new Application();
            var detailBelongsToUser = false;
            var currentApplication = _registrationService.GetApplicationDetails(Convert.ToInt32(Session["AppId"]));
            var loggedOnUser = User.Identity.Name;

            if (string.IsNullOrEmpty(result.RegNum) && string.IsNullOrEmpty(result.AppNum))
            {
                ModelState.AddModelError("", "Please enter a valid application number or registration");
                return View(result);
            }
            if (!string.IsNullOrEmpty(result.RegNum) && !string.IsNullOrEmpty(result.AppNum))
            {
                application = _configurationService.GetApplicationByRegNumAndAppNum(result.RegNum, result.AppNum);
                if (application == null)
                {
                    ModelState.AddModelError("", "Invalid Registration number or application number");
                    return View(result);
                }

                if (application.UserName != loggedOnUser)
                {
                    ModelState.AddModelError("", "The details entered does not belong to you");
                    return View(result);
                }
                detailBelongsToUser = true;
            }

            //check using only application number
            if (!string.IsNullOrEmpty(result.AppNum))
            {
                if (!detailBelongsToUser)
                {
                    application = _registrationService.GetApplicationDetailsByAppNum(result.AppNum);
                    if (application == null)
                    {
                        ModelState.AddModelError("", "Invalid Application number");
                        return View(result);
                    }
                    if (application.UserName != loggedOnUser)
                    {
                        ModelState.AddModelError("", "The application number you entered does not belong to you");
                        return View(result);
                    }
                }


                var formResult = _configurationService.GetFormResult(result.AppNum);
                if (formResult != null)
                {
                    currentApplication.MappedToAppNum = result.AppNum;
                    if (!string.IsNullOrEmpty(formResult.RegNum))
                    {
                        currentApplication.RegNum = formResult.RegNum;
                        _registrationService.SaveApplication(currentApplication);
                    }

                    var formResultModel = new FormResultModel()
                    {
                        RegNum = formResult.RegNum,
                        AppNum = formResult.AppNum,
                        EngScore = formResult.EngScore,
                        Subject2 = formResult.Subject2,
                        Subject2Score = formResult.Subject2Score,
                        Subject3 = formResult.Subject3,
                        Subject3Score = formResult.Subject3Score,
                        Subject4 = formResult.Subject4,
                        Subject4Score = formResult.Subject4Score,
                        TotalScore = formResult.TotalScore
                    };
                    Session["AppResultIsPresent"] = true;
                    return View(formResultModel);
                }
                if (string.IsNullOrEmpty(result.RegNum) && !string.IsNullOrEmpty(application.RegNum))
                {
                    result.RegNum = application.RegNum;
                }
                //ModelState.AddModelError("", "There is no result for the Application Number/Registration Number Supplied ");
                //Session["AppResultIsPresent"] = false;
                //return View(result);
            }

            if (!string.IsNullOrEmpty(result.RegNum))
            {
                var appFormId = Convert.ToInt32(Session["AppFormId"]);
                var mappedToFormIdz = _configurationService.GetMappedForms(appFormId).Select(x => x.MappedFormId).ToArray();
                //var mappedToFormId = applicationForm.MappedToFormId;
                if (!detailBelongsToUser)
                {
                    var applications = _configurationService.GetApplicationsByRegNum(result.RegNum).ToList();
                    if (!applications.Any())
                    {
                        ModelState.AddModelError("", "Invalid Registration number");
                        return View(result);
                    }
                    if (applications.Any(x => x.UserName != loggedOnUser))
                    {
                        ModelState.AddModelError("", "The registration number you entered does not belong to you");
                        return View(result);
                    }
                }

                if (mappedToFormIdz.Any())
                {
                    foreach (var mappedToFormId in mappedToFormIdz)
                    {
                        var formResult = _configurationService.GetFormResult(mappedToFormId, result.RegNum);
                        if (formResult != null)
                        {
                            //save registration number to current application so it can be used to automatically get result incase user comes back
                            //or user goes to preview page
                            currentApplication.RegNum = formResult.RegNum;
                            if (!string.IsNullOrEmpty(formResult.AppNum))
                            {
                                currentApplication.MappedToAppNum = formResult.AppNum;
                            }
                            _registrationService.SaveApplication(currentApplication);
                            //Session["AppNum"] = formResult.AppNum;
                            var formResultModel = new FormResultModel()
                            {
                                RegNum = formResult.RegNum,
                                AppNum = formResult.AppNum,
                                EngScore = formResult.EngScore,
                                Subject2 = formResult.Subject2,
                                Subject2Score = formResult.Subject2Score,
                                Subject3 = formResult.Subject3,
                                Subject3Score = formResult.Subject3Score,
                                Subject4 = formResult.Subject4,
                                Subject4Score = formResult.Subject4Score,
                                TotalScore = formResult.TotalScore
                            };
                            Session["AppResultIsPresent"] = true;
                            return View(formResultModel);
                        }
                    }

                    ModelState.AddModelError("", "There is no result for the Application Number/Registration Number Supplied ");
                    Session["AppResultIsPresent"] = false;
                    return View(result);
                }
            }
            //if we get here it means admin has not yet mapped current application form to a valid application form
            ModelState.AddModelError("", "Ooops An error occured while trying to verify your result, please notify Admin");
            return View(result);
        }
        public ActionResult PrintAppResult(string appNum)
        {
            var formResult = _configurationService.GetFormResult(appNum);
            string path = Server.MapPath("~/App_Data/PdfFiles");
            string imgPath = Server.MapPath("~/images/SchoolLogo.jpg");
            string filename = path + "/" + User.Identity.Name + "_ApplicationResult.pdf";
            var schoolName = _configurationService.GetSchoolName();
            var application = _configurationService.GetApplications(appNum);
            var applicationForm = _appForm.GetAppForms(application.AppFormId);
            var localDate = _configurationService.GetCurrentWestAfricanDateTime();

            var document = new Document(PageSize.A4, 20f, 20f, 20f, 20f);
            try
            {
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(filename, FileMode.Create));
                //create object for image
                //iTextSharp.text.Image gif = iTextSharp.text.Image.GetInstance(imgPath + "/jamb-logo-150x150.jpg");
                var htmlTemplate = Server.MapPath("~/PrintTemplates/ApplicationResult.html");
                string htmlText = System.IO.File.ReadAllText(htmlTemplate);
                htmlText = htmlText.Replace("#ImageLogoUrl#", imgPath);
                htmlText = htmlText.Replace("#SchoolName#", schoolName);
                htmlText = htmlText.Replace("#RegNum#", formResult.RegNum);
                htmlText = htmlText.Replace("#AppNum#", formResult.AppNum);
                htmlText = htmlText.Replace("#EngScore#", formResult.EngScore.ToString());
                htmlText = htmlText.Replace("#Subject2#", formResult.Subject2);
                htmlText = htmlText.Replace("#Subject2Score#", formResult.Subject2Score.ToString());
                htmlText = htmlText.Replace("#Subject3#", formResult.Subject3);
                htmlText = htmlText.Replace("#Subject3Score#", formResult.Subject3Score.ToString());
                htmlText = htmlText.Replace("#Subject4#", formResult.Subject4);
                htmlText = htmlText.Replace("#Subject4Score#", formResult.Subject4Score.ToString());
                htmlText = htmlText.Replace("#TotalScore#", formResult.TotalScore.ToString());
                htmlText = htmlText.Replace("#FormName#", applicationForm.Name);
                htmlText = htmlText.Replace("#Url#", _configurationService.GetCurrentUrl());
                htmlText = htmlText.Replace("#Date#", localDate.ToString("dd-MMM-yyyy h:mm tt"));
                if (string.IsNullOrEmpty(formResult.EngScore.ToString()))
                {
                    htmlText = htmlText.Replace("English:", "");
                }
                var html = new StringReader(htmlText);

                //Paragraph paragraph = new Paragraph(resultBody);
                //paragraph.Alignment = Element.ALIGN_JUSTIFIED;
                document.Open();
                //document.Add(gif);
                //document.Add(new Paragraph(paragraph));
                XMLWorkerHelper.GetInstance().ParseXHtml(writer, document, html);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                document.Close();
                //ShowPdf(filename);
            }
            string fullPath = Path.Combine(Server.MapPath("~/App_Data/PdfFiles"), filename);
            return File(fullPath, "application/pdf", formResult.AppNum + "_Application_Result.pdf");

        }
        public ActionResult ContinueAppResultValidation()
        {
            var resultIsPresent = Convert.ToBoolean(Session["AppResultIsPresent"]);
            if (!resultIsPresent)
            {
                var appFormWorkFlowVal = Session["WorkFlowList"] as List<ApplicationFormWorkFlow>;
                var currentWorkFlowId = appFormWorkFlowVal.FirstOrDefault().WorkFlowId;
                var currentAppFromId = appFormWorkFlowVal.FirstOrDefault().ApplicationFormId;

                var applicationFormWorkFlow = _appForm.GetApplicationFormWorkFlow(currentAppFromId, currentWorkFlowId);
                if (applicationFormWorkFlow.IsCompulsory)
                {
                    return RedirectToAction("ValidateApplicationResult", new { canProceed = 0 });
                }

            }

            var applicationId = Convert.ToInt32(Session["AppId"]);
            var application = _registrationService.GetApplicationDetails(applicationId);

            var appFormWorkFlow = Session["WorkFlowList"] as List<ApplicationFormWorkFlow>;
            var applicationForm = _appForm.GetAppForms(appFormWorkFlow.FirstOrDefault().ApplicationFormId);

            //get the current work flow stage by getting the first work flow item in the list
            var workflowId = appFormWorkFlow[application.WorkFlowStage].WorkFlowId;
            var eventLog = new EventLog()
            {
                ApplicationFormId = applicationForm.Id,
                Username = User.Identity.Name,
                WorkFlowId = workflowId,
                Action = "validated his/her Application result for" + applicationForm.Name,
                Timestamp = DateTime.Now
            };
            _eventLogRepo.SaveEvent(eventLog);

            // application.AppNum = Session["AppNum"].ToString();
            application.WorkFlowStage++;
            _registrationService.SaveApplication(application);

            //appFormWorkFlow.RemoveAt(0);
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

            //if we get here then something happend
            ViewBag.message("Something went wrong");
            return View("ApplicationError");
        }
        [HttpGet]
        public ActionResult ValidateNonApplicationResult(int? canProceed)
        {
            if (canProceed == 0)
            {
                ModelState.AddModelError("", "Sorry, you cannot proceed with this application without a valid result");
            }
            var sessionResultModel = new SessionResultModel();
            var applicationId = Convert.ToInt32(Session["AppId"]);
            var application = _registrationService.GetApplicationDetails(applicationId);
            var appFormId = Convert.ToInt32(Session["AppFormId"]);
            var applicationForm = _appForm.GetAppForms(appFormId);

            var applicationFormWorkFlow = Session["WorkFlowList"] as List<ApplicationFormWorkFlow>;

            var isRightState = _stateManager.ConfirmWorkFlowStage(application, applicationFormWorkFlow, "Validate Non-Application Result");
            if (!isRightState)
            {
                return RedirectToAction("WorkFlowManager", "Application");
            }


            var regNum = application.RegNum;
            Session["RegNum"] = regNum;
            if (!string.IsNullOrEmpty(regNum))
            {
                var sessionResult = _configurationService.GetSessionResult(applicationForm.SessionId, regNum);
                if (sessionResult != null)
                {
                    sessionResultModel.RegNum = sessionResult.RegNum;
                    sessionResultModel.EngScore = sessionResult.EngScore;
                    sessionResultModel.Subject2 = sessionResult.Subject2;
                    sessionResultModel.Subject2Score = sessionResult.Subject2Score;
                    sessionResultModel.Subject3 = sessionResult.Subject3;
                    sessionResultModel.Subject3Score = sessionResult.Subject3Score;
                    sessionResultModel.Subject4 = sessionResult.Subject4;
                    sessionResultModel.Subject4Score = sessionResult.Subject4Score;
                    sessionResultModel.TotalScore = sessionResult.TotalScore;

                    Session["NonAppResultIsPresent"] = true;
                }
                //else
                //{
                //    ModelState.AddModelError("", "We were unable to automatically retrieve your result, Please enter you");

                //}
            }

            return View(sessionResultModel);
        }
        [HttpPost]
        public ActionResult ValidateNonApplicationResult(SessionResultModel result)
        {
            //var applicationId = Convert.ToInt32(Session["AppId"]);
            //var application = _registrationService.GetApplicationDetails(applicationId);
            //var regNum = application.RegNum;
            var loggedOnUser = User.Identity.Name;
            var regNum = result.RegNum;
            if (!string.IsNullOrEmpty(regNum))
            {
                var applicationWithRegNumEntered = _configurationService.GetApplicationsByRegNum(regNum).ToList();
                //if (!applicationWithRegNumEntered.Any())
                //{
                //    ModelState.AddModelError("", "Invalid Registration number or application number");
                //    return View(result);
                //}
                if (applicationWithRegNumEntered.Any(x => x.UserName != loggedOnUser && !string.IsNullOrEmpty(x.UserName)))
                {
                    ModelState.AddModelError("", "The registration number you entered does not belong to you");
                    return View(result);
                }

                var appFormId = Convert.ToInt32(Session["AppFormId"]);
                var applicationForm = _appForm.GetAppForms(appFormId);
                Session["RegNum"] = result.RegNum;
                var sessionResult = _configurationService.GetSessionResult(applicationForm.SessionId, result.RegNum);
                if (sessionResult != null)
                {
                    var sessionResultModel = new SessionResultModel()
                    {
                        RegNum = sessionResult.RegNum,
                        EngScore = sessionResult.EngScore,
                        Subject2 = sessionResult.Subject2,
                        Subject2Score = sessionResult.Subject2Score,
                        Subject3 = sessionResult.Subject3,
                        Subject3Score = sessionResult.Subject3Score,
                        Subject4 = sessionResult.Subject4,
                        Subject4Score = sessionResult.Subject4Score,
                        TotalScore = sessionResult.TotalScore
                    };
                    Session["NonAppResultIsPresent"] = true;
                    return View(sessionResultModel);
                }
                ModelState.AddModelError("", "We couldn't find any result with your registration number");
                return View(result);
            }
            ModelState.AddModelError("", "We encountered an error while trying to verify your result, please see Admin");
            return View(result);
        }
        public ActionResult PrintNonAppResult(string regNum)
        {
            var sessionResult = _configurationService.GetSessionResult(regNum);
            string path = Server.MapPath("~/App_Data/PdfFiles");
            string imgPath = Server.MapPath("~/images/SchoolLogo.jpg");
            var schoolName = _configurationService.GetSchoolName();
            var localDate = _configurationService.GetCurrentWestAfricanDateTime();
            var url = _configurationService.GetCurrentUrl();
            string filename = path + "/" + User.Identity.Name + "_Result.pdf";

            var document = new Document(PageSize.A4, 20f, 20f, 20f, 20f);
            try
            {
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(filename, FileMode.Create));
                //create object for image
                //iTextSharp.text.Image gif = iTextSharp.text.Image.GetInstance(imgPath + "/jamb-logo-150x150.jpg");

                //string fileName = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/PrintTemplates/SessionResultTemplate.txt");
                var htmlTemplate = Server.MapPath("~/PrintTemplates/NonApplicationResult.html");
                string htmlText = System.IO.File.ReadAllText(htmlTemplate);
                htmlText = htmlText.Replace("#ImageLogoUrl#", imgPath);
                htmlText = htmlText.Replace("#SchoolName#", schoolName);
                htmlText = htmlText.Replace("#RegNum#", sessionResult.RegNum);
                htmlText = htmlText.Replace("#EngScore#", sessionResult.EngScore.ToString());
                htmlText = htmlText.Replace("#Subject2#", sessionResult.Subject2);
                htmlText = htmlText.Replace("#Subject2Score#", sessionResult.Subject2Score.ToString());
                htmlText = htmlText.Replace("#Subject3#", sessionResult.Subject3);
                htmlText = htmlText.Replace("#Subject3Score#", sessionResult.Subject3Score.ToString());
                htmlText = htmlText.Replace("#Subject4#", sessionResult.Subject4);
                htmlText = htmlText.Replace("#Subject4Score#", sessionResult.Subject4Score.ToString());
                htmlText = htmlText.Replace("#TotalScore#", sessionResult.TotalScore.ToString());
                htmlText = htmlText.Replace("#Url#", url);
                htmlText = htmlText.Replace("#Date#", localDate.ToString("dd-MMM-yyyy h:mm tt"));
                if (string.IsNullOrEmpty(sessionResult.EngScore.ToString()))
                {
                    htmlText = htmlText.Replace("English:", "");
                }
                var html = new StringReader(htmlText);
                //Paragraph paragraph = new Paragraph(resultBody);
                //paragraph.Alignment = Element.ALIGN_JUSTIFIED;
                document.Open();
                //document.Add(gif);
                //document.Add(new Paragraph(paragraph));
                XMLWorkerHelper.GetInstance().ParseXHtml(writer, document, html);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                document.Close();
                //ShowPdf(filename);
            }
            string fullPath = Path.Combine(Server.MapPath("~/App_Data/PdfFiles"), filename);
            return File(fullPath, "application/pdf", sessionResult.RegNum + "_Result.pdf");

        }
        public ActionResult ContinueNonAppResultValidation()
        {
            var resultIsPresent = Convert.ToBoolean(Session["NonAppResultIsPresent"]);
            if (!resultIsPresent)
            {
                var appFormWorkFlowVal = Session["WorkFlowList"] as List<ApplicationFormWorkFlow>;
                var currentWorkFlowId = appFormWorkFlowVal.FirstOrDefault().WorkFlowId;
                var currentAppFromId = appFormWorkFlowVal.FirstOrDefault().ApplicationFormId;

                var applicationFormWorkFlow = _appForm.GetApplicationFormWorkFlow(currentAppFromId, currentWorkFlowId);
                if (applicationFormWorkFlow.IsCompulsory)
                {
                    return RedirectToAction("ValidateNonApplicationResult", new { canProceed = 0 });
                }

            }


            var appFormWorkFlow = Session["WorkFlowList"] as List<ApplicationFormWorkFlow>;
            var applicationForm = _appForm.GetAppForms(appFormWorkFlow.FirstOrDefault().ApplicationFormId);
            var applicationId = Convert.ToInt64(Session["AppId"]);
            var application = _registrationService.GetApplicationDetails(applicationId);
            //get the current work flow stage by getting the first work flow item in the list

            var workflowId = appFormWorkFlow[application.WorkFlowStage].WorkFlowId;
            var eventLog = new EventLog()
            {
                ApplicationFormId = applicationForm.Id,
                Username = User.Identity.Name,
                WorkFlowId = workflowId,
                Action = "validated his/her Session result for" + applicationForm.Name,
                Timestamp = DateTime.Now
            };
            _eventLogRepo.SaveEvent(eventLog);

            application.WorkFlowStage++;
            application.RegNum = Session["RegNum"].ToString();
            _registrationService.SaveApplication(application);
            // appFormWorkFlow.RemoveAt(0);
            if (application.WorkFlowStage < appFormWorkFlow.Count)
            {
                // Session["WorkFlowList"] = appFormWorkFlow;
                //get the next workflow which will be the first after removing the current one for this action
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
                    case "Validate Application Result":
                        return RedirectToAction("ValidateApplicationResult", "Validation");
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

            //if we get here then something happend
            ViewBag.message("Something went wrong");
            return View("ApplicationError");
        }
        public void ShowPdf(string filename)
        {
            //Clears all content output from Buffer Stream
            Response.ClearContent();
            //Clears all headers from Buffer Stream
            Response.ClearHeaders();
            //Adds an HTTP header to the output stream
            Response.AddHeader("Content-Disposition", "inline;filename=" + filename);
            //Gets or Sets the HTTP MIME type of the output stream
            Response.ContentType = "application/pdf";
            //Writes the content of the specified file directory to an HTTP response output stream as a file block
            Response.WriteFile(filename);
            //sends all currently buffered output to the client
            Response.Flush();
            //Clears all content output from Buffer Stream
            Response.Clear();
        }
        // GET: Validation
        [HttpGet]
        public ActionResult ValidateJamb(int? canProceed)
        {

            var applicationId = Convert.ToInt32(Session["AppId"]);
            var application = _registrationService.GetApplicationDetails(applicationId);


            var applicationFormWorkFlow = Session["WorkFlowList"] as List<ApplicationFormWorkFlow>;

            var isRightState = _stateManager.ConfirmWorkFlowStage(application, applicationFormWorkFlow, "Validate Jamb Score");
            if (!isRightState)
            {
                return RedirectToAction("WorkFlowManager", "Application");
            }


            var regNum = application.RegNum;
            var applicantsJambBreakDown = _configurationService.GetJambBreakDown(regNum);

            if (applicantsJambBreakDown != null)
            {
                var model = new JambScoreValidationModel()
                {
                    RegNum = applicantsJambBreakDown.RegNum,
                    LastName = applicantsJambBreakDown.LastName,
                    FirstName = applicantsJambBreakDown.FirstName,
                    MiddleName = applicantsJambBreakDown.MiddleName,
                    CourseOfStudy = applicantsJambBreakDown.CourseOfStudy,
                    EngScore = applicantsJambBreakDown.EngScore,
                    Subject2 = applicantsJambBreakDown.Subject2,
                    Subject2Score = applicantsJambBreakDown.Subject2Score,
                    Subject3 = applicantsJambBreakDown.Subject3,
                    Subject3Score = applicantsJambBreakDown.Subject3Score,
                    Subject4 = applicantsJambBreakDown.Subject4,
                    Subject4Score = applicantsJambBreakDown.Subject4Score,
                    TotalScore = applicantsJambBreakDown.TotalScore

                };
                Session["IsJambPresent"] = true;
                return View(model);
            }
            else
            {
                var applicantsManualJambBreakDown = regNum != null ? _registrationService.GetManualJambBreakDown(regNum) : null;
                if (applicantsManualJambBreakDown != null)
                {
                    var model = new JambScoreValidationModel()
                        {
                            RegNum = applicantsManualJambBreakDown.RegNum,
                            //LastName = applicantsManualJambBreakDown.LastName,
                            //FirstName = applicantsManualJambBreakDown.FirstName,
                            //MiddleName = applicantsManualJambBreakDown.MiddleName,
                            //CourseOfStudy = applicantsManualJambBreakDown.CourseOfStudy,
                            EngScore = Convert.ToInt32(applicantsManualJambBreakDown.EngScore),
                            Subject2 = applicantsManualJambBreakDown.Subject2,
                            Subject2Score = Convert.ToInt32(applicantsManualJambBreakDown.Subject2Score),
                            Subject3 = applicantsManualJambBreakDown.Subject3,
                            Subject3Score = Convert.ToInt32(applicantsManualJambBreakDown.Subject3Score),
                            Subject4 = applicantsManualJambBreakDown.Subject4,
                            Subject4Score = Convert.ToInt32(applicantsManualJambBreakDown.Subject4Score),
                            TotalScore = Convert.ToInt32(applicantsManualJambBreakDown.TotalScore)

                        };
                    Session["IsJambPresent"] = true;
                    return View(model);
                }
                else
                {
                    var errorModel = new JambScoreValidationModel()
                    {
                        RegNum = regNum
                    };
                    if (canProceed == 0)
                    {
                        ModelState.AddModelError("", "Sorry, you are not eligible to apply. Please contact Administrator");

                        return View(errorModel);
                    }
                    Session["IsJambPresent"] = false;
                    if (string.IsNullOrEmpty(application.RegNum))
                    {
                        ModelState.AddModelError("", "Enter registration number below");
                    }
                    else
                    {
                        ModelState.AddModelError("", "No Jamb record was found with the registration number specified");
                    }


                    return View(errorModel);
                }


            }

        }
        public ActionResult ValidateJamb(string regNum)
        {
            if (!string.IsNullOrEmpty(regNum))
            {
                var loggedOnUser = User.Identity.Name;
                var applicationsWithRegNumEntered = _configurationService.GetApplicationsByRegNum(regNum).ToList();
                if (applicationsWithRegNumEntered.Any(x => x.UserName != loggedOnUser))
                {
                    ModelState.AddModelError("", "The registration number entered does not belong to you");
                    var errorModel = new JambScoreValidationModel()
                    {
                        RegNum = regNum
                    };
                    return View(errorModel);
                }


                var applicationId = Convert.ToInt32(Session["AppId"]);
                var application = _registrationService.GetApplicationDetails(applicationId);
                application.RegNum = regNum;
                _registrationService.SaveApplication(application);
            }
            return RedirectToAction("ValidateJamb");
        }
        public ActionResult ContinueJambValidation()
        {
            var applicationId = Convert.ToInt32(Session["AppId"]);
            var application = _registrationService.GetApplicationDetails(applicationId);
            var appFormWorkFlow = Session["WorkFlowList"] as List<ApplicationFormWorkFlow>;
            var applicationForm = _appForm.GetAppForms(appFormWorkFlow.FirstOrDefault().ApplicationFormId);


            var isJambPresent = Convert.ToBoolean(Session["IsJambPresent"]);
            if (!isJambPresent)
            {
                var jambWorkFlow = appFormWorkFlow[application.WorkFlowStage];
                var isCompulsory = jambWorkFlow.IsCompulsory;
                if (isCompulsory)
                {
                    return RedirectToAction("ValidateJamb", new { canProceed = 0 });
                }
            }



            var workflowId = appFormWorkFlow[application.WorkFlowStage].WorkFlowId;
            var eventLog = new EventLog()
            {
                ApplicationFormId = applicationForm.Id,
                Username = User.Identity.Name,
                WorkFlowId = workflowId,
                Action = "validated his/her Jamb score for" + applicationForm.Name,
                Timestamp = DateTime.Now
            };
            _eventLogRepo.SaveEvent(eventLog);


            application.WorkFlowStage++;
            _registrationService.SaveApplication(application);
            //save the current workflow into a session before removing it from the list incase the user presses back from the next workflow
            //var passedWorkFlow = Session["PassedWorkFlow"] as List<ApplicationFormWorkFlow> ?? new List<ApplicationFormWorkFlow>();
            //passedWorkFlow.Insert(0, appFormWorkFlow.FirstOrDefault());
            //Session["PassedWorkFlow"] = passedWorkFlow;

            //appFormWorkFlow.RemoveAt(0);
            if (application.WorkFlowStage < appFormWorkFlow.Count)
            {
                var nextWorkFlow = appFormWorkFlow[application.WorkFlowStage];
                var workFlowItem = _appForm.GetWorkFlowItem(nextWorkFlow.WorkFlowId);
                //  Session["WorkFlowList"] = appFormWorkFlow;
                switch (workFlowItem.Name)
                {
                    case "Pay":
                        return RedirectToAction("ValidatePayment", "Payment");
                    case "Fill":
                        return RedirectToAction("Fill", "Fill");
                    case "Validate Application Result":
                        return RedirectToAction("ValidateApplicationResult");
                    case "Validate Non-Application Result":
                        return RedirectToAction("ValidateNonApplicationResult");
                    case "Validate Admission Status":
                        return RedirectToAction("CheckAdmissionStatus");
                    case "Venue Assignment":
                        return RedirectToAction("VenueAssignment", "Validation");

                }
            }
            else
            {
                return RedirectToAction("ApplicationPreview", "Application");

            }

            //if we get here then something happend
            ViewBag.message = "Something went wrong";
            return View("ApplicationError");
        }
        public ActionResult PrintResult(string regNum)
        {
            var applicantsJambBreakDown = _configurationService.GetJambBreakDown(regNum);
            var manualApplicantsJambBreakDown = _registrationService.GetManualJambBreakDown(regNum);
            string path = Server.MapPath("~/App_Data/PdfFiles/");
            string imgPath = Server.MapPath("~/images/jamb-logo-150x150.jpg");
            string filename = path + "/JambResult.pdf";
            var schoolName = _configurationService.GetSchoolName();
            var localTime = _configurationService.GetCurrentWestAfricanDateTime();
            var url = _configurationService.GetCurrentUrl();


            var document = new Document(PageSize.A4, 20f, 20f, 20f, 20f);
            try
            {
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(filename, FileMode.Create));
                //create object for image
                //iTextSharp.text.Image gif = iTextSharp.text.Image.GetInstance(imgPath + "/jamb-logo-150x150.jpg");

                var htmlTemplate = Server.MapPath("~/PrintTemplates/JambResult.html");
                string htmlText = System.IO.File.ReadAllText(htmlTemplate);
                htmlText = htmlText.Replace("#ImageLogoUrl#", imgPath);
                htmlText = htmlText.Replace("#SchoolName#", schoolName);
                htmlText = htmlText.Replace("#Url#", url);
                htmlText = htmlText.Replace("#RegNum#", applicantsJambBreakDown != null ? applicantsJambBreakDown.RegNum : manualApplicantsJambBreakDown.RegNum);
                //htmlText = htmlText.Replace("#LastName#", applicantsJambBreakDown.LastName);
                //htmlText = htmlText.Replace("#FirstName#", applicantsJambBreakDown.FirstName);
                htmlText = htmlText.Replace("#EngScore#", applicantsJambBreakDown != null ? applicantsJambBreakDown.EngScore.ToString() : manualApplicantsJambBreakDown.EngScore.ToString());
                htmlText = htmlText.Replace("#Subject2#", applicantsJambBreakDown != null ? applicantsJambBreakDown.Subject2 : manualApplicantsJambBreakDown.Subject2);
                htmlText = htmlText.Replace("#Subject2Score#", applicantsJambBreakDown != null ? applicantsJambBreakDown.Subject2Score.ToString() : manualApplicantsJambBreakDown.Subject2Score.ToString());
                htmlText = htmlText.Replace("#Subject3#", applicantsJambBreakDown != null ? applicantsJambBreakDown.Subject3 : manualApplicantsJambBreakDown.Subject3);
                htmlText = htmlText.Replace("#Subject3Score#", applicantsJambBreakDown != null ? applicantsJambBreakDown.Subject3Score.ToString() : manualApplicantsJambBreakDown.Subject3Score.ToString());
                htmlText = htmlText.Replace("#Subject4#", applicantsJambBreakDown != null ? applicantsJambBreakDown.Subject4 : manualApplicantsJambBreakDown.Subject4);
                htmlText = htmlText.Replace("#Subject4Score#", applicantsJambBreakDown != null ? applicantsJambBreakDown.Subject4Score.ToString() : manualApplicantsJambBreakDown.Subject4Score.ToString());
                htmlText = htmlText.Replace("#TotalScore#", applicantsJambBreakDown != null ? applicantsJambBreakDown.TotalScore.ToString() : manualApplicantsJambBreakDown.TotalScore.ToString());
                htmlText = htmlText.Replace("#Date#", localTime.ToString("dd-MMM-yyyy h:mm tt"));
                var html = new StringReader(htmlText);
                //Paragraph paragraph = new Paragraph(htmlText);
                //paragraph.Alignment = Element.ALIGN_JUSTIFIED;
                document.Open();
                //document.Add(gif);
                //document.Add(new Paragraph(paragraph));
                XMLWorkerHelper.GetInstance().ParseXHtml(writer, document, html);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                document.Close();
                //ShowPdf(filename);
            }
            string fullPath = Path.Combine(Server.MapPath("~/App_Data/PdfFiles"), filename);
            return File(fullPath, "application/pdf", regNum + " Jamb Result.pdf");

        }
        [HttpGet]
        public ActionResult CheckAdmissionStatus(int? canProceed)
        {
            var applicationId = Convert.ToInt32(Session["AppId"]);
            var application = _registrationService.GetApplicationDetails(applicationId);
            var applicationFormWorkFlow = Session["WorkFlowList"] as List<ApplicationFormWorkFlow>;

            var isRightState = _stateManager.ConfirmWorkFlowStage(application, applicationFormWorkFlow, "Validate Admission Status");
            if (!isRightState)
            {
                return RedirectToAction("WorkFlowManager", "Application");
            }
            if (canProceed == 0)
            {
                ModelState.AddModelError("", "Sorry, You cannot proceed with this application unless you have been admitted");
            }
            var admissionStatusModel = new ValidateAdmissionStatusModel();
            return View(admissionStatusModel);
        }
        [HttpPost]
        public ActionResult CheckAdmissionStatus(ValidateAdmissionStatusModel model)
        {
            var loggedOnUser = User.Identity.Name;
            var appNumOrRegNum = model.AppNumOrRegNum;

            if (string.IsNullOrEmpty(model.AppNumOrRegNum))
            {
                ModelState.AddModelError("", " You did not your registration number or applicatoin number");
                ViewData["IsAdmitted"] = null;
                return View(model);
            }
            var applicationFormId = Convert.ToInt32(Session["AppFormId"]);
            var mappedToFormIdz = _configurationService.GetMappedForms(applicationFormId).Select(x => x.MappedFormId).ToArray();
            if (mappedToFormIdz.Any())
            {

                //assume it is an application number
                var admissionEntry = _registrationService.GetAdmissionList(appNumOrRegNum);
                if (admissionEntry != null)
                {
                    //this meams a record was found and user entered his/her application number
                    //check if username for application is same as logged on users name
                    var app = _registrationService.GetApplicationDetailsByAppNum(model.AppNumOrRegNum);
                    if (app.UserName != loggedOnUser)
                    {
                        ModelState.AddModelError("", "The application number entered does not belong to you");
                        ViewData["IsAdmitted"] = null;
                        return View(model);
                    }
                    ViewData["IsAdmitted"] = true;
                    var programIdAdmittedInto = app.ProgramIdAdmittedInto;
                    var courseIdAdmittedInto = app.CourseIdAdmittedInto;
                    var programCode = _configurationService.GetProgram(programIdAdmittedInto).Code;
                    var courseName = _configurationService.GetCourse(courseIdAdmittedInto).Name;
                    model.ProgramCode = programCode;
                    model.CourseName = courseName;
                    return View(model);
                }

                //if we get here it means nothing was found when we assumed it was an application number
                //next we assume it is a registration number but an applicant
                foreach (var mappedFormId in mappedToFormIdz)
                {
                    admissionEntry = _registrationService.GetAdmissionList(appNumOrRegNum, mappedFormId);
                    if (admissionEntry != null)
                    {
                        var app = _registrationService.GetApplicationByRegNumAndFormId(model.AppNumOrRegNum, mappedFormId);
                        if (app != null)
                        {
                            if (app.UserName != loggedOnUser)
                            {
                                ModelState.AddModelError("", "The registration number entered does not belong to you");
                                ViewData["IsAdmitted"] = null;
                                return View(model);
                            }
                            ViewData["IsAdmitted"] = true;

                            var programIdAdmittedInto = app.ProgramIdAdmittedInto;
                            var courseIdAdmittedInto = app.CourseIdAdmittedInto;
                            var programCode = _configurationService.GetProgram(programIdAdmittedInto).Code;
                            var courseName = _configurationService.GetCourse(courseIdAdmittedInto).Name;
                            model.ProgramCode = programCode;
                            model.CourseName = courseName;
                            return View(model);
                        }

                        ModelState.AddModelError("", "we dint find any registration number attached to your application. please try using your application number or contact the admin");
                        return View(model);
                    }

                    //again if we get here it means no record was found while we assumed detail entered is for an applicant
                    //next we try for non-applicant
                    var nonApplicantAdmittedEntry = _registrationService.GetNonApplicantAdmittedList(appNumOrRegNum, mappedFormId);
                    if (nonApplicantAdmittedEntry != null)
                    {
                        ViewData["IsAdmitted"] = true;

                        var programIdAdmittedInto = nonApplicantAdmittedEntry.ProgramId;
                        var courseIdAdmittedInto = nonApplicantAdmittedEntry.CourseId;
                        var programCode = _configurationService.GetProgram(programIdAdmittedInto).Code;
                        var courseName = _configurationService.GetCourse(courseIdAdmittedInto).Name;
                        model.ProgramCode = programCode;
                        model.CourseName = courseName;
                        return View(model);
                    }
                }

                //if we get here then no record has been upploaded indicating admission for applicant
                //therefore applicant has not been admitted
                ViewData["IsAdmitted"] = false;
                return View(model);
            }
            ViewData["IsAdmitted"] = null;
            ModelState.AddModelError("", "An Error occured, please try again later");
            return View(model);
        }
        public ActionResult PrintAdmissionLetter(string appNumOrRegNum)
        {
            var localTime = _configurationService.GetCurrentWestAfricanDateTime();
            string url = _configurationService.GetCurrentUrl();
            var program = new Program();
            var courseOfStudy = new Course();
            var faculty = new Faculty();
            var session = new Session();
            var application = new Application();

            if (User.IsInRole("Student"))
            {
                var applicationFormId = Convert.ToInt32(Session["AppFormId"]);
                var mappedToFormIdz = _configurationService.GetMappedForms(applicationFormId).Select(x => x.MappedFormId).ToArray();
                //   var mappedFormId = currentAapplicationForm.MappedToFormId;
                if (mappedToFormIdz.Any())
                {
                    //first we check for applicant
                    //first we assume appNumOrRegNum is application number 
                    application = _configurationService.GetApplications(appNumOrRegNum);
                    if (application != null)
                    {
                        program = _configurationService.GetProgram(application.ProgramIdAdmittedInto);
                        courseOfStudy = _configurationService.GetCourse(application.CourseIdAdmittedInto);
                        faculty = courseOfStudy.Department.Faculty;
                        session = _configurationService.GetSession(application.SessionId);
                    }
                    else
                    {
                        foreach (var mappedFormId in mappedToFormIdz)
                        {
                            //next we assume appNumOrRegNum is a registration number
                            application = _registrationService.GetAdmittedApplicationByRegNumAndFormId(appNumOrRegNum, mappedFormId);
                            if (application != null)
                            {
                                program = _configurationService.GetProgram(application.ProgramIdAdmittedInto);
                                courseOfStudy = _configurationService.GetCourse(application.CourseIdAdmittedInto);
                                faculty = courseOfStudy.Department.Faculty;
                                session = _configurationService.GetSession(application.SessionId);
                                break;
                            }


                            //finally we check if application is null, if it is then no application record was found while we assumed it appNumOrRegNum was for ann applicant
                            //therefore we would now check non applicantAdmittedList
                            var nonApplicantAdmittedEntry = _registrationService.GetNonApplicantAdmittedList(appNumOrRegNum, mappedFormId);
                            if (nonApplicantAdmittedEntry != null)
                            {
                                application = new Application();
                                var programIdAdmittedInto = nonApplicantAdmittedEntry.ProgramId;
                                var courseIdAdmittedInto = nonApplicantAdmittedEntry.CourseId;
                                program = _configurationService.GetProgram(programIdAdmittedInto);
                                courseOfStudy = _configurationService.GetCourse(courseIdAdmittedInto);
                                faculty = courseOfStudy.Department.Faculty;
                                application.ModeOfEntry = nonApplicantAdmittedEntry.ModeOfEntry;
                                session = _configurationService.GetSession(nonApplicantAdmittedEntry.SessionId);
                                break;
                            }
                        }


                    }


                }
                else
                {
                    ViewBag.message = "An Error occured, please try again later";
                    return View("ApplicationError");
                }
            }
            else
            {
                application = _configurationService.GetApplications(appNumOrRegNum);
                if (application != null)
                {
                    if (!application.IsAdmitted)
                    {
                        return RedirectToAction("SearchErrorHandler", "Search",
                             new { errorMessage = "NOT ADMITTED" });

                    }
                    var programIdAdmittedInto = application.ProgramIdAdmittedInto;
                    var courseIdAdmittedInto = application.CourseIdAdmittedInto;
                    program = _configurationService.GetProgram(programIdAdmittedInto);
                    courseOfStudy = _configurationService.GetCourse(courseIdAdmittedInto);
                    faculty = courseOfStudy.Department.Faculty;
                    session = _configurationService.GetSession(application.SessionId);
                }

            }




            var studentPicture = _registrationService.GetPictureDetails(application.Id);
            string imgPath = Server.MapPath("~/images/jamb-logo-150x150.jpg");
            string passportPath = studentPicture != null ? Server.MapPath("~/images/StudentPassport/" + studentPicture.Name) : "";


            var htmlTemplate = Server.MapPath("~/PrintTemplates/AdmissionLetter.html");
            string htmlText = System.IO.File.ReadAllText(htmlTemplate);
            htmlText = htmlText.Replace("#ProgramName#", program.Code);
            htmlText = htmlText.Replace("#CourseName#", courseOfStudy.Name);
            htmlText = htmlText.Replace("#FacultyName#", faculty.Name);
            htmlText = htmlText.Replace("#SchoolName#", "University of Ibadan");
            htmlText = htmlText.Replace("#Session#", session.Name);
            htmlText = htmlText.Replace("#ModeOfEntry#", application.ModeOfEntry);
            htmlText = htmlText.Replace("#ImageLogoUrl#", imgPath);
            htmlText = htmlText.Replace("#ImageStudentUrl#", passportPath);
            htmlText = htmlText.Replace("#Date#", localTime.ToString("dd/MMM/yyyy h:mm tt"));
            htmlText = htmlText.Replace("#Url#", url);
            var html = new StringReader(htmlText);

            string path = Server.MapPath("~/App_Data/PdfFiles/");

            string filename = path + "AdmissionLetter.pdf";

            var document = new Document(PageSize.A4, 20f, 20f, 20f, 20f);
            try
            {
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(filename, FileMode.Create));
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
                //ShowPdf(filename);
            }
            string fullPath = Path.Combine(Server.MapPath("~/App_Data/PdfFiles"), filename);
            return File(fullPath, "application/pdf", application.AppNum + " Offer of Admission.pdf");

        }
        public ActionResult ContinueFromAdmStatus(bool admitted)
        {
            if (!admitted)
            {
                var appFormWorkFlowVal = Session["WorkFlowList"] as List<ApplicationFormWorkFlow>;
                var currentWorkFlowId = appFormWorkFlowVal.FirstOrDefault().WorkFlowId;
                var currentAppFromId = appFormWorkFlowVal.FirstOrDefault().ApplicationFormId;

                var applicationFormWorkFlow = _appForm.GetApplicationFormWorkFlow(currentAppFromId, currentWorkFlowId);
                if (applicationFormWorkFlow.IsCompulsory)
                {
                    return RedirectToAction("CheckAdmissionStatus", new { canProceed = 0 });
                }
            }
            var applicationId = Convert.ToInt32(Session["AppId"]);
            var application = _registrationService.GetApplicationDetails(applicationId);

            var appFormWorkFlow = Session["WorkFlowList"] as List<ApplicationFormWorkFlow>;
            var applicationForm = _appForm.GetAppForms(appFormWorkFlow.FirstOrDefault().ApplicationFormId);
            var workflowId = appFormWorkFlow[application.WorkFlowStage].WorkFlowId;
            var eventLog = new EventLog()
            {
                ApplicationFormId = applicationForm.Id,
                Username = User.Identity.Name,
                WorkFlowId = workflowId,
                Action = "checked his/her Admission Status for" + applicationForm.Name,
                Timestamp = DateTime.Now
            };
            _eventLogRepo.SaveEvent(eventLog);


            application.WorkFlowStage++;
            _registrationService.SaveApplication(application);


            // appFormWorkFlow.RemoveAt(0);
            if (application.WorkFlowStage < appFormWorkFlow.Count)
            {
                var nextWorkFlow = appFormWorkFlow[application.WorkFlowStage];
                var workFlowItem = _appForm.GetWorkFlowItem(nextWorkFlow.WorkFlowId);
                // Session["WorkFlowList"] = appFormWorkFlow;
                switch (workFlowItem.Name)
                {
                    case "Pay":
                        return RedirectToAction("ValidatePayment", "Payment");
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
                    case "Venue Assignment":
                        return RedirectToAction("VenueAssignment", "Validation");
                }
            }
            else
            {
                return RedirectToAction("ApplicationPreview", "Application");

            }

            //if we get here then something happend
            ViewBag.message("Something went wrong");
            return View("ApplicationError");
        }
        public ActionResult VenueAssignment()
        {
            var applicationId = Convert.ToInt64(Session["AppId"]);
            var application = _registrationService.GetApplicationDetails(applicationId);

            var applicationFormWorkFlow = Session["WorkFlowList"] as List<ApplicationFormWorkFlow>;

            var isRightState = _stateManager.ConfirmWorkFlowStage(application, applicationFormWorkFlow, "Venue Assignment");
            if (!isRightState)
            {
                return RedirectToAction("WorkFlowManager", "Application");
            }

            if (application.SeatNo == 0)//i.e if seat number has not been previously given
            {
                var formId = application.AppFormId;
                var programId = application.ProgramId;
                var courseId = application.CourseOfStudyId;

                var venueMappings = _configurationService.GetVenueMappings(formId, courseId, programId).ToList();
                if (!venueMappings.Any())
                {
                    //this means all venues that admin has not mapped any venue to applicants program course
                    ViewBag.message = "You could not be assigned to any venue, please notify the admin";
                    return View("ApplicationError");
                }
                var examVenueIdz = venueMappings.Select(x => x.ExamVenueId).ToArray();
                var examVenues = _configurationService.GetExamVenues().Where(x => examVenueIdz.Contains(x.Id) && !x.IsFilled).OrderByDescending(x => x.Venue.Capacity);
                if (examVenues.Any())
                {
                    //assign the first exam venue in the returned List which would be the venue with the largest capacity
                    application.ExamVenueId = examVenues.FirstOrDefault().Id;
                    // _registrationService.SaveApplication(application);


                    //increase venues No Of AllocatedSeat
                    if (application.ExamVenueId > 0)
                    {
                        var examVenue = examVenues.FirstOrDefault(x => x.Id == application.ExamVenueId);
                        examVenue.NoOfAllocatedSeats++;
                        if (examVenue.NoOfAllocatedSeats >= examVenue.Venue.Capacity)
                        {
                            examVenue.IsFilled = true;
                        }
                        application.SeatNo = examVenue.NoOfAllocatedSeats;
                        _configurationService.UpdateExamVenue(examVenue);
                    }
                }
                else
                {
                    ViewBag.message = "An error occured while trying to assign you a venue, please notify your admin";
                    return View("ApplicationError");
                }


                //log action in event log table
                applicationFormWorkFlow = Session["WorkFlowList"] as List<ApplicationFormWorkFlow>;
                var applicationForm = _appForm.GetAppForms(applicationFormWorkFlow.FirstOrDefault().ApplicationFormId);
                var workflowId = applicationFormWorkFlow[application.WorkFlowStage].WorkFlowId;
                var eventLog = new EventLog()
                {
                    ApplicationFormId = applicationForm.Id,
                    Username = User.Identity.Name,
                    WorkFlowId = workflowId,
                    Action = "was assigned an exam venue",
                    Timestamp = DateTime.Now
                };
                _eventLogRepo.SaveEvent(eventLog);
            }



            application.WorkFlowStage++;
            _registrationService.SaveApplication(application);
            ;
            if (application.WorkFlowStage < applicationFormWorkFlow.Count)
            {
                var nextWorkFlow = applicationFormWorkFlow[application.WorkFlowStage];
                var workFlowItem = _appForm.GetWorkFlowItem(nextWorkFlow.WorkFlowId);
                // Session["WorkFlowList"] = appFormWorkFlow;
                switch (workFlowItem.Name)
                {
                    case "Pay":
                        return RedirectToAction("ValidatePayment", "Payment");
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
                    case "Venue Assignment":
                        return RedirectToAction("VenueAssignment", "Validation");
                }
            }
            else
            {
                return RedirectToAction("ApplicationPreview", "Application");

            }
            //if we get here then something happend
            ViewBag.message("Something went wrong");
            return View("ApplicationError");
        }

    }
}