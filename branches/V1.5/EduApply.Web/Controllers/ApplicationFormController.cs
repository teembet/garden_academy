using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using EduApply.Data.Entities;
using EduApply.Logic.Interfaces;
using EduApply.Logic.Service;
using EduApply.Logic.Utility;
using EduApply.Web.Infrastructure;
using EduApply.Web.Models;
using iTextSharp.text;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using NPOI.SS.Formula.Functions;

namespace EduApply.Web.Controllers
{
    [Authorize(Roles = "Admin, SchoolAdmin")]
    public class ApplicationFormController : Controller
    {
        public const string Success = "Success";
        private IRepository _repo;
        private IApplicationFormRepository _appForm;
        private IRegistrationService _registrationService;
        private IConfigurationService _configurationService;
        private ILocationRepository _locationRepository;
        private IAuditTrailRepository _auditTrailRepository;
        // GET: Application
        public ApplicationFormController(IRepository repo, IApplicationFormRepository appForm, IRegistrationService registrationService, IConfigurationService configurationService, ILocationRepository locationRepository, IAuditTrailRepository auditTrailRepository)
        {
            this._repo = repo;
            this._appForm = appForm;
            this._registrationService = registrationService;
            this._configurationService = configurationService;
            this._locationRepository = locationRepository;
            this._auditTrailRepository = auditTrailRepository;
        }



        [AllowAnonymous]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult CheckForDuplicateName(string formNAme)
        {
            bool result = false;
            var appForms = _appForm.GetAppForms(formNAme);
            if (appForms.Any())
            {
                result = true;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [AllowAnonymous]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult VerifyDate(DateTime startDate, DateTime endDate)
        {
            bool result = startDate > endDate ? true : false;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [AllowAnonymous]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetStateByCountryId(string countryId)
        {
            var states = new List<State>();
            int outId = 0;
            if (string.IsNullOrEmpty(countryId))
            {
                countryId = "-1";
            }
            bool isValid = int.TryParse(countryId, out outId);

            states = _locationRepository.GetStates(int.Parse(countryId)).ToList();
            var result = (from s in states
                          select new
                          {
                              id = s.Id,
                              name = s.Name
                          }).ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [AllowAnonymous]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetLgaByStateId(string stateId)
        {
            var lgaz = new List<LocalGovernmentArea>();
            int outId = 0;
            if (string.IsNullOrEmpty(stateId))
            {
                stateId = "-1";
            }
            bool isValid = int.TryParse(stateId, out outId);

            lgaz = _locationRepository.GetLgas(int.Parse(stateId)).ToList();
            var result = (from r in lgaz
                          select new
                              {
                                  id = r.Id,
                                  name = r.Name
                              }).ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        [AllowAnonymous]
        public ActionResult GetCoursesByProgramId(int? programId)
        {
            if (programId == null)
                programId = -1;
            var courses = new List<Course>();
            if (User.IsInRole("Student"))
            {
                var programCourses = Session["ApplicantsProgramCourse"] as List<ProgramCourse>;
                var programCoursesForCurrentForm = programCourses.Where(x => x.ProgramId == programId);
                var courseIdz = programCoursesForCurrentForm.Select(x => x.CourseId).ToArray();
                courses = _configurationService.GetCourses().Where(x => courseIdz.Contains(x.Id) && x.IsActive).ToList();
            }
            else
            {
                courses = _configurationService.GetCourses(Convert.ToInt32(programId)).ToList();
            }

            var result = (from s in courses
                          select new
                          {
                              id = s.Id,
                              name = s.Name
                          }).ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Index()
        {
            var appForms = _appForm.GetAppForms().OrderByDescending(x => x.Id).ToList();
            var formTemplates = _appForm.GetFormTemplates();
            foreach (var appForm in appForms)
            {
                var tempInApp = _appForm.GeTemplatesInApp(appForm.Id);
                var templatesForThisApp = new List<FormTemplate>();
                foreach (var t in tempInApp)
                {
                    templatesForThisApp.Add(formTemplates.First(x => x.Id == t.FormTemplateId));
                }
                appForm.FormTemplates = templatesForThisApp;
            }
            return View(appForms);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            //The code below supplies value for grade and subjects dropdown for the OLevel Template
            //The code below supplies value for the subject drop down and grade dropdown for OLevel Template
            var grades = _registrationService.GetOLevelGrades();
            var subjects = _registrationService.GetOLevelSubjects();
            var examTypes = _registrationService.GeExamTypes();
            var yearList = new List<Years>();
            var currentYear = DateTime.Today.Year;
            for (int i = 1960; i <= currentYear; i++)
            {
                var newYear = new Years()
                {
                    Year = i
                };
                yearList.Add(newYear);
            }
            var oLevelResultDetailsViewModel = new OLevelResultDetailsViewModel();
            oLevelResultDetailsViewModel.MaxEntry = 0;

            oLevelResultDetailsViewModel.ResultDetails = new List<OLevelResultDetails>();
            var listOfResultDetails = new List<OLevelResultDetails>();
            var oLevelResultDetails = new OLevelResultDetails()
            {
                Subjects = subjects,
                Grades = grades,
                ExamTypes = examTypes,
                Years = yearList
            };
            listOfResultDetails.Add(oLevelResultDetails);
            oLevelResultDetailsViewModel.ResultDetails = listOfResultDetails;
            ViewData["OLevelTemplateModel"] = oLevelResultDetailsViewModel;

            //The code below supplies value for the class of degree drop down for Educational Details Template
            var educationalDetailsModel = new EducationalDetailsModel();
            educationalDetailsModel.Degrees = _configurationService.GetDegrees();
            ViewData["EducationalDetailsModel"] = educationalDetailsModel;


            //The code below supplies value for the subjects drop down for Jamb Result Details Template
            var manualJambResultModel = new ManualJambBreakDownModel();
            manualJambResultModel.Subjects = _registrationService.GetOLevelSubjects().ToList();
            ViewData["JambResultDetails"] = manualJambResultModel;

            //The code below supplies value for the country drop down and state of Residence dropdown for Biodata Template
            var personalInformation = new PersonalInformation();
            personalInformation.Countries = _locationRepository.GetCountries();
            personalInformation.States = new List<State>();
            personalInformation.Lgaz = new List<LocalGovernmentArea>();
            personalInformation.ResidentStates = _locationRepository.GetStates();
            var personalInformationModel = Mapper.Map<PersonalInformation, PersonalInformationModel>(personalInformation);
            ViewData["BiodataModel"] = personalInformationModel;

            //The code below supplies value for the program drop down for the program course template
            var applicantsProgramCourse = new ApplicantsProgramCourse();
            applicantsProgramCourse.Programs = _configurationService.GetPrograms();
            var applicantsProgramCourseModel = Mapper.Map<ApplicantsProgramCourse, ApplicantsProgramCourseModel>(applicantsProgramCourse);
            ViewData["AppProgramCourseModel"] = applicantsProgramCourseModel;



            var applicationForm = new ApplicationForm();
            var localTime = _configurationService.GetCurrentWestAfricanDateTime();
            applicationForm.FormTemplates = _appForm.GetFormTemplates();
            applicationForm.WorkFlowList = _appForm.GetDefaultWorkFlow();
            Session["WorkFlowList"] = applicationForm.WorkFlowList;

            var appFormModel = Mapper.Map<ApplicationForm, ApplicationFormModel>(applicationForm);
            appFormModel.FormCategories = _appForm.GetFormCategories();
            appFormModel.Sessions =
                _configurationService.GetSessions().Where(x => x.EndDate > localTime).OrderByDescending(x => x.Name);
            //  appFormModel.ApplicationForms = _appForm.GetAppForms();
            return View(appFormModel);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Create(ApplicationFormModificationModel model, string startDate, string endDate)
        {
            //try
            //{
            if (ModelState.IsValid)
            {
                var applicationForm = new ApplicationForm()
                {
                    Name = model.Name,
                    StartDate = Convert.ToDateTime(startDate),
                    EndDate = Convert.ToDateTime(endDate),
                    FormCategoryId = Convert.ToInt32(model.FormCategoryId),
                    SessionId = Convert.ToInt32(model.SessionId),
                    Fee = model.Fee,
                    AllowMultipleApplications = model.AllowMultipleApplications
                };
                //Server side check for incorrect date match
                if (applicationForm.StartDate > applicationForm.EndDate)
                {
                    TempData["DateError"] = true;
                    return RedirectToAction("Create");
                }
                //server side check for dupplicate Name for applicationForm
                var appForms = _appForm.GetAppForms(model.Name);
                if (appForms.Any())
                {
                    TempData["SameNameError"] = true;
                    return RedirectToAction("Create");
                }
                //validate that application form falls within the range of a session
                //var session = _configurationService.GetSessions();
                //if (!session.Any(x => x.StartDate <= applicationForm.StartDate && x.EndDate >= applicationForm.EndDate))
                //{
                //    TempData["NoSession"] = true;
                //    return RedirectToAction("Create");
                //}
                _appForm.Save(applicationForm);


                var userRole = UserManager.GetRoles(User.Identity.GetUserId());
                var auditTrail = new AuditTrail()
                {
                    UserId = User.Identity.GetUserId(),
                    Username = User.Identity.GetUserName(),
                    AuditActionId = Convert.ToInt32(AuditTrailActions.AddApplicationForm),
                    Details = "created Application Form \'" + applicationForm.Name + "\'",
                    TimeStamp = DateTime.Now,
                    UserRole = userRole.First(),
                    UserIp = UtilityService.GetIp(System.Web.HttpContext.Current)
                };
                _auditTrailRepository.SaveAuditTrail(auditTrail);
                foreach (int templateId in model.IdsToAdd ?? new int[] { })
                {
                    var templatesInApp = new TemplatesInAppForms()
                    {
                        FormTemplateId = templateId,
                        ApplicationFormId = applicationForm.Id
                    };
                    _appForm.SaveTemplatesInApp(templatesInApp);

                    //Save Template Settings
                    var formTemplate = _appForm.GetFormTemplate(templateId);
                    switch (formTemplate.Name)
                    {
                        case "Bio-Data":
                            //var bdformTemplateSettings = new FormTemplateSettings()
                            //{
                            //    ApplicationFormId = applicationForm.Id,
                            //    FormTemplateId = templateId,
                            //    MinEntry = model.BD_MinEntry,
                            //    MaxEntry = model.BD_MinEntry
                            //};
                            //_appForm.SaveFormTemplateSettings(bdformTemplateSettings);
                            break;
                        case "O-Level-Result":
                            var olrformTemplateSettings = new FormTemplateSettings()
                            {
                                ApplicationFormId = applicationForm.Id,
                                FormTemplateId = templateId,
                                MinEntry = model.OLR_MinEntry,
                                MaxEntry = model.OLR_MaxEntry
                            };
                            _appForm.SaveFormTemplateSettings(olrformTemplateSettings);
                            break;
                        case "Educational_Details":
                            var eduOlrformTemplateSettings = new FormTemplateSettings()
                            {
                                ApplicationFormId = applicationForm.Id,
                                FormTemplateId = templateId,
                                MinEntry = model.ED_MinEntry,
                                MaxEntry = model.ED_MaxEntry
                            };
                            _appForm.SaveFormTemplateSettings(eduOlrformTemplateSettings);
                            break;
                        case "Work_Experience":
                            var wEOlrformTemplateSettings = new FormTemplateSettings()
                            {
                                ApplicationFormId = applicationForm.Id,
                                FormTemplateId = templateId,
                                MinEntry = model.WE_MinEntry,
                                MaxEntry = model.WE_MaxEntry
                            };
                            _appForm.SaveFormTemplateSettings(wEOlrformTemplateSettings);
                            break;
                        case "Referee":
                            var refFormTemplateSettings = new FormTemplateSettings()
                            {
                                ApplicationFormId = applicationForm.Id,
                                FormTemplateId = templateId,
                                MinEntry = model.REF_MinEntry,
                                MaxEntry = model.REF_MaxEntry
                            };
                            _appForm.SaveFormTemplateSettings(refFormTemplateSettings);
                            break;
                        case "Passport_Upload":
                            //var puFormTemplateSettings = new FormTemplateSettings()
                            //{
                            //    ApplicationFormId = applicationForm.Id,
                            //    FormTemplateId = templateId,
                            //    MinEntry = model.BD_MinEntry,
                            //    MaxEntry = model.BD_MinEntry
                            //};
                            //_appForm.SaveFormTemplateSettings(puFormTemplateSettings);
                            break;
                        case "Certificate_Upload":
                            var cuFormTemplateSettings = new FormTemplateSettings()
                            {
                                ApplicationFormId = applicationForm.Id,
                                FormTemplateId = templateId,
                                MinEntry = model.CU_MinEntry,
                                MaxEntry = model.CU_MaxEntry
                            };
                            _appForm.SaveFormTemplateSettings(cuFormTemplateSettings);
                            break;
                        case "ProgramCourse":
                            //var pcOlrformTemplateSettings = new FormTemplateSettings()
                            //{
                            //    ApplicationFormId = applicationForm.Id,
                            //    FormTemplateId = templateId,
                            //    MinEntry = model.PC_MinEntry,
                            //    MaxEntry = model.PC_MinEntry
                            //};
                            //_appForm.SaveFormTemplateSettings(pcOlrformTemplateSettings);
                            break;
                    }


                }
                var workFlowItems = model.workFlowItems ?? new int[] {};
                var isCompulsoryItems = model.IsCompulsoryItems ?? new int[] {};
                var workFlowList = Session["WorkFlowList"] as List<WorkFlow>;
                if (workFlowList != null)
                {
                    workFlowList.RemoveAll(x => !workFlowItems.Contains(x.Id));
                    var workFlowOrder = string.Join(",", workFlowList.Select(x => x.Name));
                    foreach (var item in workFlowList)
                    {
                        var appWorkFlow = new ApplicationFormWorkFlow()
                           {
                               ApplicationFormId = applicationForm.Id,
                               WorkFlowOrder = workFlowOrder,
                               WorkFlowId = item.Id,
                               IsCompulsory = isCompulsoryItems.Contains(item.Id)
                           };
                        _appForm.SaveApplicationFormWorkFlow(appWorkFlow);
                    }

                }

                TempData["Created"] = Success;
            }

            //}
            //catch (Exception)
            //{

            //    TempData["Created"] = "Failed";
            //}
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            var appForm = _appForm.GetAppForms(id);
            _appForm.Delete(appForm);
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult AdvancedSettings(int formId)
        {
            var appForm = _appForm.GetAppForms(formId);
            var model = Mapper.Map<ApplicationForm, ApplicationFormModel>(appForm);
            model.ProgramCourses = _configurationService.GetProgramCourses().OrderBy(x => x.Program.Code);
            model.ApplicationForms = _appForm.GetAppForms().Where(x => x.Id != formId);
            model.ProgramCourseIdsForThisForm = _configurationService.GetAppFormProgramCourses(formId).Select(x => x.ProgramCourseId).ToArray();
            model.MappedFormIdsForThisForm = _configurationService.GetMappedForms(formId).Select(x => x.MappedFormId).ToArray();
            return View(model);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AdvancedSettings(AdvancedSettingsModel appForm)
        {

            var applicationForm = _appForm.GetAppForms(appForm.Id);
            try
            {

                applicationForm.AllowMultipleApplications = appForm.AllowMultipleApplications;
                applicationForm.AllowAppResultPrinting = appForm.AllowAppResultPrinting;
                applicationForm.AllowNonAppResultPrinting = appForm.AllowNonAppResultPrinting;
                applicationForm.AllowJambResultPrinting = appForm.AllowJambResultPrinting;
                applicationForm.AllowAdmissionLetterPriniting = appForm.AllowAdmissionLetterPriniting;
                applicationForm.AllowApplicationFormPrinting = appForm.AllowApplicationFormPrinting;
                applicationForm.AllowPhotoCardPrinting = appForm.AllowPhotoCardPrinting;
                applicationForm.AllowApplicationEditAfterSubmission = appForm.AllowApplicationEditAfterSubmission;

                _appForm.Update();

                var savedProgramCourseForThisForm = _configurationService.GetAppFormProgramCourses(appForm.Id).ToList();
                var idsOfsavedProgramCourseForThisForm = savedProgramCourseForThisForm.Select(x => x.ProgramCourseId).ToArray();
                var savedProgramCourseToDelete = savedProgramCourseForThisForm.Where(x => appForm.ProgramCourseIds != null && !appForm.ProgramCourseIds.Contains(x.ProgramCourseId)).ToList();
                if (appForm.ProgramCourseIds == null)
                {
                    appForm.ProgramCourseIds = new int[] { };
                    savedProgramCourseToDelete = savedProgramCourseForThisForm;
                }


                foreach (var item in savedProgramCourseToDelete)
                {
                    _configurationService.DeleteAppFormProgramCourse(item);
                }



                foreach (var id in appForm.ProgramCourseIds)
                {
                    if (idsOfsavedProgramCourseForThisForm.Contains(id))
                    {
                        continue;
                    }
                    var appFormProgramCourse = new AppFormProgramCourse()
                    {
                        AppFormId = appForm.Id,
                        ProgramCourseId = id
                    };
                    _configurationService.SaveAppFormProgramCourse(appFormProgramCourse);
                }

                //configure mappedForms
                var savedMappedFormForThisForm = _configurationService.GetMappedForms(appForm.Id).ToList();
                var idsOfSavedMappedFormForThisForm = savedMappedFormForThisForm.Select(x => x.MappedFormId).ToArray();
                var savedMappedFormToDelete = savedMappedFormForThisForm.Where(x => appForm.MappedFormIds != null && !appForm.MappedFormIds.Contains(x.MappedFormId)).ToList();
                if (appForm.MappedFormIds == null)
                {
                    appForm.MappedFormIds = new int[] { };
                    savedMappedFormToDelete = savedMappedFormForThisForm;
                }
                foreach (var item in savedMappedFormToDelete)
                {
                    _configurationService.DeleteMappedForm(item);
                }

                foreach (var id in appForm.MappedFormIds)
                {
                    if (idsOfSavedMappedFormForThisForm.Contains(id))
                    {
                        continue;
                    }
                    var mappedForm = new MappedForm()
                    {
                        FormId = appForm.Id,
                        MappedFormId = id
                    };
                    _configurationService.SaveMappedForm(mappedForm);
                }
                TempData["AdvancedSettingsSaved"] = true;
            }
            catch (Exception)
            {
                throw;
            }

            return RedirectToAction("Edit", new { appId = applicationForm.Id });
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int appId)
        {
            //This just helps supply value for the grade and subject dropdown field for OLevel Template so Admin can see how this works
            //when he clicks the preview button.

            //The code below supplies value for the subject drop down and grade dropdown for OLevel Template
            var grades = _registrationService.GetOLevelGrades();
            var subjects = _registrationService.GetOLevelSubjects();
            var examTypes = _registrationService.GeExamTypes();
            var yearList = new List<Years>();
            var currentYear = DateTime.Today.Year;
            for (int i = 1960; i <= currentYear; i++)
            {
                var newYear = new Years()
                {
                    Year = i
                };
                yearList.Add(newYear);
            }
            var oLevelResultDetailsViewModel = new OLevelResultDetailsViewModel();
            oLevelResultDetailsViewModel.MaxEntry = 0;

            oLevelResultDetailsViewModel.ResultDetails = new List<OLevelResultDetails>();
            var listOfResultDetails = new List<OLevelResultDetails>();
            var oLevelResultDetails = new OLevelResultDetails()
            {
                Subjects = subjects,
                Grades = grades,
                ExamTypes = examTypes,
                Years = yearList
            };
            listOfResultDetails.Add(oLevelResultDetails);
            oLevelResultDetailsViewModel.ResultDetails = listOfResultDetails;
            ViewData["OLevelTemplateModel"] = oLevelResultDetailsViewModel;


            //The code below supplies value for the class of degree drop down for Educational Details Template
            var educationalDetailsModel = new EducationalDetailsModel();
            educationalDetailsModel.Degrees = _configurationService.GetDegrees();
            ViewData["EducationalDetailsModel"] = educationalDetailsModel;

            //The code below supplies value for the subjects drop down for Jamb Result Details Template
            var manualJambResultModel = new ManualJambBreakDownModel();
            manualJambResultModel.Subjects = _registrationService.GetOLevelSubjects().ToList();
            ViewData["JambResultDetails"] = manualJambResultModel;

            //The code below supplies value for the country drop down and state of Residence dropdown for Biodata Template
            var personalInformation = new PersonalInformation();
            personalInformation.Countries = _locationRepository.GetCountries();
            personalInformation.States = new List<State>();
            personalInformation.Lgaz = new List<LocalGovernmentArea>();
            personalInformation.ResidentStates = _locationRepository.GetStates();
            var personalInformationModel = Mapper.Map<PersonalInformation, PersonalInformationModel>(personalInformation);
            ViewData["BiodataModel"] = personalInformationModel;

            //The code below supplies value for the program drop down for the program course template
            var applicantsProgramCourse = new ApplicantsProgramCourse();
            applicantsProgramCourse.Programs = _configurationService.GetPrograms();
            var applicantsProgramCourseModel = Mapper.Map<ApplicantsProgramCourse, ApplicantsProgramCourseModel>(applicantsProgramCourse);
            ViewData["AppProgramCourseModel"] = applicantsProgramCourseModel;



            var appForm = _appForm.GetAppForms(appId);
            appForm.FormTemplates = _appForm.GetFormTemplates();

            var tempInApp = _appForm.GeTemplatesInApp(appForm.Id);
            var formTemplateIdz = tempInApp != null ? tempInApp.Select(x => x.FormTemplateId).ToArray() : new int[0];

            //var formTemplatesForThisAppForm = formTemplates.Where(x => formTemplateIdz.Any(y => y == x.Id)).ToList();
            //var otherFormTemplates = formTemplates.Where(x => !formTemplateIdz.Any(y => y == x.Id)).ToList();

            //appForm.FormTemplates = formTemplatesForThisAppForm;
            foreach (var item in appForm.FormTemplates)
            {
                var formTemplateSettings = _registrationService.GetFormTemplateSettings(appForm.Id, item.Id);
                if (formTemplateSettings != null)
                {
                    item.MinEntry = formTemplateSettings.MinEntry;
                    item.MaxEntry = formTemplateSettings.MaxEntry;
                }
            }


            var defaultWorkflow = _appForm.GetDefaultWorkFlow().ToList();
            var workFlowForThisAppForm = _appForm.GetApplicationFormWorkFlow2(appForm.Id).ToList();

            var workFlowEdit = new List<WorkFlow>();

            foreach (var item in defaultWorkflow)
            {
                var t = workFlowForThisAppForm.FirstOrDefault(x => x.WorkFlowId == item.Id);
                if (t != null)
                {
                    item.Enabled = true;
                    item.IsCompulsory = t.IsCompulsory;
                }
                workFlowEdit.Add(item);
            }
            //order workFlow in the right way
            for (int i = 0; i < workFlowForThisAppForm.Count; i++)
            {
                var workFlowOrderItem = workFlowForThisAppForm[i];
                var workFlowItem = workFlowEdit.FirstOrDefault(x => x.Id == workFlowOrderItem.WorkFlowId);
                workFlowEdit.Remove(workFlowItem);
                workFlowEdit.Insert(i, workFlowItem);
            }
            appForm.WorkFlowList = workFlowEdit.Count > 0 ? workFlowEdit : defaultWorkflow;
            Session["WorkFlowList"] = appForm.WorkFlowList;
            var appFormModel = Mapper.Map<ApplicationForm, ApplicationFormModel>(appForm);
            appFormModel.FormCategories = _appForm.GetFormCategories();
            appFormModel.Sessions = _configurationService.GetSessions();
            appFormModel.FormTempletIdz = formTemplateIdz;
            //   appFormModel.ApplicationForms = _appForm.GetAppForms().Where(x => x.Id != appId);

            return View(appFormModel);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Edit(ApplicationFormModificationModel model)
        {
            if (ModelState.IsValid)
            {
                var appForm = _appForm.GetAppForms(model.Id);
                appForm.Name = model.Name;
                appForm.StartDate = model.StartDate;
                appForm.EndDate = model.EndDate;
                appForm.FormCategoryId = Convert.ToInt32(model.FormCategoryId);
                //  appForm.MappedToFormId = Convert.ToInt32(model.MappedToFormId);
                appForm.SessionId = Convert.ToInt32(model.SessionId);
                appForm.Fee = model.Fee;
                appForm.AllowMultipleApplications = model.AllowMultipleApplications;

                if (appForm.StartDate > appForm.EndDate)
                {
                    TempData["DateError"] = true;
                    return RedirectToAction("Edit", new { id = appForm.Id });
                }
                var appForms = _appForm.GetAppForms(model.Name).Where(x => x.Id != model.Id);
                if (appForms.Any())
                {
                    TempData["SameNameError"] = true;
                    return RedirectToAction("Edit", new { id = appForm.Id });
                }

                _appForm.Update();

                //Add New Template
                foreach (int templateId in model.IdsToAdd ?? new int[] { })
                {
                    var templatesInApp = _appForm.GetTemplatesInAppFormsByFormIdAndTempId(model.Id, templateId);
                    if (templatesInApp == null)
                    {
                        var templateInApp = new TemplatesInAppForms()
                        {
                            FormTemplateId = templateId,
                            ApplicationFormId = model.Id
                        };
                        _appForm.SaveTemplatesInApp(templateInApp);

                        //Save Template Settings
                        var formTemplate = _appForm.GetFormTemplate(templateId);
                        switch (formTemplate.Name)
                        {
                            case "Bio-Data":
                                break;
                            case "O-Level-Result":
                                var olrformTemplateSettings = new FormTemplateSettings()
                                {
                                    ApplicationFormId = appForm.Id,
                                    FormTemplateId = templateId,
                                    MinEntry = model.OLR_MinEntry,
                                    MaxEntry = model.OLR_MaxEntry
                                };
                                _appForm.SaveFormTemplateSettings(olrformTemplateSettings);
                                break;
                            case "Educational_Details":
                                var eduOlrformTemplateSettings = new FormTemplateSettings()
                                {
                                    ApplicationFormId = appForm.Id,
                                    FormTemplateId = templateId,
                                    MinEntry = model.ED_MinEntry,
                                    MaxEntry = model.ED_MaxEntry
                                };
                                _appForm.SaveFormTemplateSettings(eduOlrformTemplateSettings);
                                break;
                            case "Work_Experience":
                                var wEOlrformTemplateSettings = new FormTemplateSettings()
                                {
                                    ApplicationFormId = appForm.Id,
                                    FormTemplateId = templateId,
                                    MinEntry = model.WE_MinEntry,
                                    MaxEntry = model.WE_MaxEntry
                                };
                                _appForm.SaveFormTemplateSettings(wEOlrformTemplateSettings);
                                break;
                            case "Referee":
                                var refFormTemplateSettings = new FormTemplateSettings()
                                {
                                    ApplicationFormId = appForm.Id,
                                    FormTemplateId = templateId,
                                    MinEntry = model.REF_MinEntry,
                                    MaxEntry = model.REF_MaxEntry
                                };
                                _appForm.SaveFormTemplateSettings(refFormTemplateSettings);
                                break;
                            case "Passport_Upload":
                                break;
                            case "Certificate_Upload":
                                var cuFormTemplateSettings = new FormTemplateSettings()
                                {
                                    ApplicationFormId = appForm.Id,
                                    FormTemplateId = templateId,
                                    MinEntry = model.CU_MinEntry,
                                    MaxEntry = model.CU_MaxEntry
                                };
                                _appForm.SaveFormTemplateSettings(cuFormTemplateSettings);
                                break;
                            case "ProgramCourse":
                                break;
                        }
                    }

                }
                //select all the templates that are in the db and was not selected by the admin and tthen delete them
                var tempInAppNotSelected = _appForm.GetFormTemplatesByFormId(model.Id).Where(x => !model.IdsToAdd.Contains(x.Id));
                foreach (FormTemplate template in tempInAppNotSelected)
                {
                    var templatesInApp = _appForm.GetTemplatesInAppFormsByFormIdAndTempId(appForm.Id, template.Id);
                    _appForm.DeleteTempInApps(templatesInApp);
                    var formTemplateSettings = _registrationService.GetFormTemplateSettings(appForm.Id, template.Id);
                    if (formTemplateSettings != null)
                    {
                        _appForm.DeleteTemplateSettings(formTemplateSettings);
                    }
                }

                //Now having added and deleted templates to this form, get all the templates that are int the form and update its min and max entry
                var templatesInCurrentAppForm = _appForm.GeTemplatesInApp(model.Id).Select(x => x.FormTemplateId).ToArray();
                foreach (int templateId in templatesInCurrentAppForm ?? new int[] { })
                {
                    var formTemplateSettings = _appForm.GetFormTemplateSettings(model.Id, templateId);
                    if (formTemplateSettings != null)
                    {
                        var formTemplate = _appForm.GetFormTemplate(formTemplateSettings.FormTemplateId);
                        switch (formTemplate.Name)
                        {
                            case "Bio-Data":
                                break;
                            case "O-Level-Result":

                                formTemplateSettings.MinEntry = model.OLR_MinEntry;
                                formTemplateSettings.MaxEntry = model.OLR_MaxEntry;
                                _appForm.SaveFormTemplateSettings(formTemplateSettings);
                                break;
                            case "EducationalDetails":
                                formTemplateSettings.MinEntry = model.ED_MinEntry;
                                formTemplateSettings.MaxEntry = model.ED_MaxEntry;


                                _appForm.SaveFormTemplateSettings(formTemplateSettings);
                                break;
                            case "Work_Experience":

                                formTemplateSettings.MinEntry = model.WE_MinEntry;
                                formTemplateSettings.MaxEntry = model.WE_MaxEntry;
                                _appForm.SaveFormTemplateSettings(formTemplateSettings);
                                break;
                            case "Referee":
                                formTemplateSettings.MinEntry = model.REF_MinEntry;
                                formTemplateSettings.MaxEntry = model.REF_MaxEntry;
                                _appForm.SaveFormTemplateSettings(formTemplateSettings);
                                break;
                            case "Passport_Upload":
                                break;
                            case "Certificate_Upload":
                                formTemplateSettings.MinEntry = model.CU_MinEntry;
                                formTemplateSettings.MaxEntry = model.CU_MaxEntry;
                                _appForm.SaveFormTemplateSettings(formTemplateSettings);
                                break;
                            case "ProgramCourse":
                                break;
                        }
                    }
                    //Save Template Settings


                }

                //first of all we will delete any previously saved workflow, this is necessary because application will follow the order workflows
                //are saved and there is no way to insert into a certain row in sql
                var workFlowForThisAppForm = _appForm.GetApplicationFormWorkFlow2(appForm.Id).ToList();
                if (workFlowForThisAppForm.Any())
                {
                    _appForm.DeleteAppFormWorkFlow(workFlowForThisAppForm);
                }


                //after Deleteing now we save in the new order
                foreach (var item in model.workFlowItems)
                {
                    var appFormWorkFlow = new ApplicationFormWorkFlow();

                    appFormWorkFlow.ApplicationFormId = appForm.Id;
                    appFormWorkFlow.WorkFlowId = item;
                    if (model.IsCompulsoryItems != null)
                    {
                        appFormWorkFlow.IsCompulsory = model.IsCompulsoryItems.Contains(item);
                    }
                    else
                    {
                        appFormWorkFlow.IsCompulsory = false;
                    }
                    _appForm.SaveApplicationFormWorkFlow(appFormWorkFlow);
                }


                //var workFlowList = Session["WorkFlowList"] as List<WorkFlow>;
                //if (workFlowList != null)
                //{
                //    workFlowList.RemoveAll(x => !model.workFlowItems.Contains(x.Id));
                //    var workFlowOrder = string.Join(",", workFlowList.Select(x => x.Name));

                //    var workFlowForThisAppForm = _appForm.GetApplicationFormWorkFlow(appForm.Id);
                //    if (workFlowForThisAppForm != null)
                //    {
                //        workFlowForThisAppForm.WorkFlowOrder = workFlowOrder;
                //    }
                //    else //ideally code shouldnt reach here but this is necesarry for forms added b4 workflow module was added to application
                //    {
                //        workFlowForThisAppForm = new ApplicationFormWorkFlow()
                //        {
                //            ApplicationFormId = appForm.Id,
                //            WorkFlowOrder = workFlowOrder
                //        };
                //    }


                //    _appForm.SaveApplicationFormWorkFlow(workFlowForThisAppForm);
                //}
                TempData["Edited"] = Success;
                return RedirectToAction("Index");
            }
            return View();
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Clone(int appId)
        {
            //This just helps supply value for the grade and subject dropdown field for OLevel Template so Admin can see how this works
            //when he clicks the preview button.

            //The code below supplies value for the subject drop down and grade dropdown for OLevel Template
            var grades = _registrationService.GetOLevelGrades();
            var subjects = _registrationService.GetOLevelSubjects();
            var examTypes = _registrationService.GeExamTypes();
            var yearList = new List<Years>();
            var currentYear = DateTime.Today.Year;
            for (int i = 1960; i <= currentYear; i++)
            {
                var newYear = new Years()
                {
                    Year = i
                };
                yearList.Add(newYear);
            }
            var oLevelResultDetailsViewModel = new OLevelResultDetailsViewModel();
            oLevelResultDetailsViewModel.MaxEntry = 0;

            oLevelResultDetailsViewModel.ResultDetails = new List<OLevelResultDetails>();
            var listOfResultDetails = new List<OLevelResultDetails>();
            var oLevelResultDetails = new OLevelResultDetails()
            {
                Subjects = subjects,
                Grades = grades,
                ExamTypes = examTypes,
                Years = yearList

            };
            listOfResultDetails.Add(oLevelResultDetails);
            oLevelResultDetailsViewModel.ResultDetails = listOfResultDetails;
            ViewData["OLevelTemplateModel"] = oLevelResultDetailsViewModel;


            //The code below supplies value for the class of degree drop down for Educational Details Template
            var educationalDetailsModel = new EducationalDetailsModel();
            educationalDetailsModel.Degrees = _configurationService.GetDegrees();
            ViewData["EducationalDetailsModel"] = educationalDetailsModel;

            //The code below supplies value for the subjects drop down for Jamb Result Details Template
            var manualJambResultModel = new ManualJambBreakDownModel();
            manualJambResultModel.Subjects = _registrationService.GetOLevelSubjects().ToList();
            ViewData["JambResultDetails"] = manualJambResultModel;

            //The code below supplies value for the country drop down and state of Residence dropdown for Biodata Template
            var personalInformation = new PersonalInformation();
            personalInformation.Countries = _locationRepository.GetCountries();
            personalInformation.States = new List<State>();
            personalInformation.Lgaz = new List<LocalGovernmentArea>();
            personalInformation.ResidentStates = _locationRepository.GetStates();
            var personalInformationModel = Mapper.Map<PersonalInformation, PersonalInformationModel>(personalInformation);
            ViewData["BiodataModel"] = personalInformationModel;

            //The code below supplies value for the program drop down for the program course template
            var applicantsProgramCourse = new ApplicantsProgramCourse();
            applicantsProgramCourse.Programs = _configurationService.GetPrograms();
            var applicantsProgramCourseModel = Mapper.Map<ApplicantsProgramCourse, ApplicantsProgramCourseModel>(applicantsProgramCourse);
            ViewData["AppProgramCourseModel"] = applicantsProgramCourseModel;



            var appForm = _appForm.GetAppForms(appId);
            appForm.FormTemplates = _appForm.GetFormTemplates();

            var tempInApp = _appForm.GeTemplatesInApp(appForm.Id);
            var formTemplateIdz = tempInApp.Select(x => x.FormTemplateId).ToArray();
           
            foreach (var item in appForm.FormTemplates)
            {
                var formTemplateSettings = _registrationService.GetFormTemplateSettings(appForm.Id, item.Id);
                if (formTemplateSettings != null)
                {
                    item.MinEntry = formTemplateSettings.MinEntry;
                    item.MaxEntry = formTemplateSettings.MaxEntry;
                }

            }

            var defaultWorkflow = _appForm.GetDefaultWorkFlow().ToList();
            var workFlowForThisAppForm = _appForm.GetApplicationFormWorkFlow2(appForm.Id).ToList();

            var workFlowEdit = new List<WorkFlow>();
           
            foreach (var item in defaultWorkflow)
            {
                var t = workFlowForThisAppForm.FirstOrDefault(x => x.WorkFlowId == item.Id);
                if (t != null)
                {
                    item.Enabled = true;
                    item.IsCompulsory = t.IsCompulsory;
                }
                workFlowEdit.Add(item);
            }
            //order workFlow in the right way
            for (int i = 0; i < workFlowForThisAppForm.Count; i++)
            {
                var workFlowOrderItem = workFlowForThisAppForm[i];
                var workFlowItem = workFlowEdit.FirstOrDefault(x => x.Id == workFlowOrderItem.WorkFlowId);
                workFlowEdit.Remove(workFlowItem);
                workFlowEdit.Insert(i, workFlowItem);
            }

            appForm.WorkFlowList = workFlowEdit.Count > 0 ? workFlowEdit : defaultWorkflow;
            Session["WorkFlowList"] = appForm.WorkFlowList;
            var appFormModel = Mapper.Map<ApplicationForm, ApplicationFormModel>(appForm);
            appFormModel.Name = "";
            appFormModel.FormTempletIdz = formTemplateIdz;
            appFormModel.FormCategories = _appForm.GetFormCategories();
            appFormModel.Sessions = _configurationService.GetSessions();
            // appFormModel.ApplicationForms = _appForm.GetAppForms();

            return View("Clone", appFormModel);
        }
        public void UpdateRowMoved(int id, int fromPosition, int toPosition, string direction)
        {
            if (Session["WorkFlowList"] == null)
            {
                throw new Exception("Work Flow not found");
            }

            //var workFlow = new List<WorkFlow>();
            var workFlow = Session["WorkFlowList"] as List<WorkFlow>;
            if (workFlow != null)
            {
                var workFlowToRemove = workFlow[fromPosition - 1];
                workFlow.RemoveAt(fromPosition - 1);
                workFlow.Insert((toPosition - 1), workFlowToRemove);
                Session["WorkFlowList"] = workFlow;
            }

        }

        public ActionResult FormTemplates()
        {

            //The code below supplies value for the subject drop down and grade dropdown for OLevel Template
            var grades = _registrationService.GetOLevelGrades();
            var subjects = _registrationService.GetOLevelSubjects();
            var examTypes = _registrationService.GeExamTypes();
            var yearList = new List<Years>();
            var currentYear = DateTime.Today.Year;
            for (int i = 1960; i <= currentYear; i++)
            {
                var newYear = new Years()
                {
                    Year = i
                };
                yearList.Add(newYear);
            }
            var oLevelResultDetailsViewModel = new OLevelResultDetailsViewModel();
            oLevelResultDetailsViewModel.MaxEntry = 0;

            oLevelResultDetailsViewModel.ResultDetails = new List<OLevelResultDetails>();
            var listOfResultDetails = new List<OLevelResultDetails>();
            var oLevelResultDetails = new OLevelResultDetails()
            {
                Subjects = subjects,
                Grades = grades,
                ExamTypes = examTypes,
                Years = yearList
            };
            listOfResultDetails.Add(oLevelResultDetails);
            oLevelResultDetailsViewModel.ResultDetails = listOfResultDetails;
            ViewData["OLevelTemplateModel"] = oLevelResultDetailsViewModel;



            //var educationalDetailsModel = new EducationalDetailsModel();
            //educationalDetailsModel.Degrees = _configurationService.GetDegrees();
            //ViewData["EducationalDetailsModel"] = educationalDetailsModel;

            //The code below supplies value for the country drop down and state of Residence dropdown for Biodata Template
            var personalInformation = new PersonalInformation();
            personalInformation.Countries = _locationRepository.GetCountries();
            personalInformation.States = new List<State>();
            personalInformation.Lgaz = new List<LocalGovernmentArea>();
            personalInformation.ResidentStates = _locationRepository.GetStates();
            var personalInformationModel = Mapper.Map<PersonalInformation, PersonalInformationModel>(personalInformation);
            ViewData["BiodataModel"] = personalInformationModel;

            //The code below supplies value for the subjects drop down for Jamb Result Details Template
            var manualJambResultModel = new ManualJambBreakDownModel();
            manualJambResultModel.Subjects = _registrationService.GetOLevelSubjects().ToList();
            ViewData["JambResultDetails"] = manualJambResultModel;

            //The code below supplies value for the program drop down for the program course template
            var applicantsProgramCourse = new ApplicantsProgramCourse();
            applicantsProgramCourse.Programs = _configurationService.GetPrograms();
            var applicantsProgramCourseModel = Mapper.Map<ApplicantsProgramCourse, ApplicantsProgramCourseModel>(applicantsProgramCourse);
            ViewData["AppProgramCourseModel"] = applicantsProgramCourseModel;


            return View();
        }
        public void AddModelError(string error)
        {
            ModelState.AddModelError("", error);
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