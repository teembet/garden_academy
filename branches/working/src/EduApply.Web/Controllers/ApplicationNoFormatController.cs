using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using EduApply.Data.Entities;
using EduApply.Logic.Interfaces;
using EduApply.Web.Models;

namespace EduApply.Web.Controllers
{
    public class ApplicationNoFormatController : Controller
    {
        private IRepository _repo;
        private IApplicationFormRepository _appForm;
        private IRegistrationService _registrationService;
        private IConfigurationService _configurationService;
        private ILocationRepository _locationRepository;
        private IAuditTrailRepository _auditTrailRepository;
        //
        // GET: /ApplicationNoFormat/
        public ApplicationNoFormatController(IApplicationFormRepository appForm, IConfigurationService configurationService)
        {
            this._appForm = appForm;
            this._configurationService = configurationService;
        }

        public ActionResult Index()
        {
            var applicationNoFormarts = _configurationService.GetFormFormats();
            return View(applicationNoFormarts);
        }
        [HttpGet]
        public ActionResult Create()
        {
            var model = new ApplicationNoFormatModel()
            {
                ApplicationForms = _appForm.GetAppForms()
            };
            return View(model);
        }
        public ActionResult Create(ApplicationNoFormat format)
        {
            try
            {
                if (format.StartNumber.ToString().Length > format.Range)
                {
                    ModelState.AddModelError("", "The number of digits in your start number is greater than the range specified");
                    var formModel = new ApplicationNoFormatModel()
                    {
                        ApplicationForms = _appForm.GetAppForms()
                    };
                    return View(formModel);
                }

                //check if applicationForm chosen already has a format
                var applicationNoFormat = _configurationService.GetApplicationNoFormatByFormId(format.ApplicationFormId);
                if (applicationNoFormat != null)
                {
                    ModelState.AddModelError("", "An Application Number format has already been configured for the Form chosen");
                    var formModel = new ApplicationNoFormatModel()
                    {
                        ApplicationForms = _appForm.GetAppForms()
                    };
                    return View(formModel);
                }
                //if we get here then it means no wahala, oya save Format.
                _configurationService.SaveApplicationNoFormat(format);
                TempData["FormatSaved"] = "Success";
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                var model = Mapper.Map<ApplicationNoFormat, ApplicationNoFormatModel>(format);
                model.ApplicationForms = _appForm.GetAppForms();
                TempData["FormatSaved"] = "Failed";
                return View(model);
            }

        }

        [HttpGet]
        public ActionResult Edit(long formatId)
        {
            var applicationNumberFormat = _configurationService.GetApplicationNoFormat(formatId);
            return View(applicationNumberFormat);
        }
        [HttpPost]
        public ActionResult Edit(ApplicationNoFormat applicationNumberFormat)
        {
            try
            {
                if (applicationNumberFormat.StartNumber.ToString().Length > applicationNumberFormat.Range)
                {
                    ModelState.AddModelError("", "The number of digits in your start number is greater than the range specified");
                    var applicationNoFormat = _configurationService.GetApplicationNoFormat(applicationNumberFormat.Id);
                    return View(applicationNoFormat);
                }

                var applicationNumFormat = _configurationService.GetApplicationNoFormat(applicationNumberFormat.Id);
                applicationNumFormat.Prefix = applicationNumberFormat.Prefix;
                applicationNumFormat.Suffix = applicationNumberFormat.Suffix;
                applicationNumFormat.StartNumber = applicationNumberFormat.StartNumber;
                applicationNumFormat.Range = applicationNumberFormat.Range;
                _configurationService.SaveApplicationNoFormat(applicationNumberFormat);
                TempData["FormatEdited"] = "Success";
                return RedirectToAction("Index");
            }
            catch (Exception)
            {

                TempData["FormatSaved"] = "Failed";
                var applicationNoFormat = _configurationService.GetApplicationNoFormat(applicationNumberFormat.Id);
                return View(applicationNoFormat);
            }

        }
    }
}