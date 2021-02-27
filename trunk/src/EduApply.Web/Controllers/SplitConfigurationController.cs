using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EduApply.Data.Entities;
using EduApply.Logic.Interfaces;
using EduApply.Web.Models;

namespace EduApply.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SplitConfigurationController : Controller
    {
        private IConfigurationService _configurationService;
        private IApplicationFormRepository _applicationFormRepository;
        private IRegistrationService _registrationService;
        private IAuditTrailRepository _auditTrailRepository;

        public SplitConfigurationController(IConfigurationService configurationService, IVenueAssignmentService venueService, IApplicationFormRepository applicationFormRepository, IRegistrationService registrationService, IAuditTrailRepository auditTrailRepository)
        {
            this._configurationService = configurationService;
            this._registrationService = registrationService;
            this._applicationFormRepository = applicationFormRepository;
            this._auditTrailRepository = auditTrailRepository;
        }

        public ActionResult GetFormDetails(int? applicationFormId)
        {

            var applicationForm = new ApplicationForm();
            if (applicationFormId != null)
            {
                applicationForm = _applicationFormRepository.GetAppForms(Convert.ToInt32(applicationFormId));
            }
            var result = new { Fee = applicationForm.Fee != null ? applicationForm.Fee.ToString() : "", StartDate = applicationFormId != null ? applicationForm.StartDate.ToString("dd-MMM-yyyy h:mm tt") : "", EndDate = applicationFormId != null ? applicationForm.EndDate.ToString("dd-MMM-yyyy h:mm tt") : "" };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /SplitConfiguration/
        [HttpGet]
        public ActionResult Index()
        {
            var splitConfigModel = new SplitConfigModel()
            {
                ApplicationForms = _applicationFormRepository.GetAppForms(),
                Splits = new List<Split>(),
                Banks = _configurationService.GetBanks()
            };
            return View(splitConfigModel);
        }
        [HttpPost]
        public ActionResult Index(Split split)
        {
            var splitsForForm = _configurationService.GetSplits(split.ApplicationFormId).ToList();
            var splitsAmountSaved = splitsForForm.Sum(x => x.Amount);
            var applicationForm = _applicationFormRepository.GetAppForms(split.ApplicationFormId);
            if ((splitsAmountSaved + split.Amount) > applicationForm.Fee)
            {
                TempData["AmountExceeded"] = "exceeded";
            }
            else
            {
                if (splitsForForm.Any(x => x.Name.Equals(split.Name)))
                {
                    TempData["NameExist"] = "exists";
                }
                else
                {
                    _configurationService.SaveSplit(split);
                    TempData["SplitAdded"] = "Success";
                }

            }
            var splitConfigModel = new SplitConfigModel()
            {
                ApplicationFormId = split.ApplicationFormId,
                ApplicationForms = _applicationFormRepository.GetAppForms(),
                Splits = _configurationService.GetSplits(split.ApplicationFormId).ToList(),
                Banks = _configurationService.GetBanks()
            };
            return View(splitConfigModel);
        }

        public ActionResult Delete(long splitId)
        {
            var split = _configurationService.GetSplit(splitId);
            var appFormId = split.ApplicationFormId;
            _configurationService.DeleteSplit(split);
            TempData["Deleted"] = "Success";

            var splitConfigModel = new SplitConfigModel()
            {
                ApplicationFormId = appFormId,
                ApplicationForms = _applicationFormRepository.GetAppForms(),
                Splits = _configurationService.GetSplits(appFormId).ToList(),
                Banks = _configurationService.GetBanks()
            };
            return View("Index", splitConfigModel);
        }

        public ActionResult Update(Split split)
        {
            var splitsForForm = _configurationService.GetSplits(split.ApplicationFormId).Where(x => x.Id != split.Id).ToList();
            var splitToUpdate = _configurationService.GetSplit(split.Id);
            var splitsAmountSaved = splitsForForm.Sum(x => x.Amount);
            var applicationForm = _applicationFormRepository.GetAppForms(splitToUpdate.ApplicationFormId);
            if ((splitsAmountSaved + split.Amount) > applicationForm.Fee)
            {
                TempData["UpdateAmountExceeded"] = "exceeded";
            }
            else
            {
                if (splitsForForm.Any(x => x.Name.Equals(split.Name)))
                {
                    TempData["UpdateNameExist"] = "exists";
                }
                else
                {
                    splitToUpdate.AccountNumber = split.AccountNumber;
                    splitToUpdate.Amount = split.Amount;
                    splitToUpdate.BankId = split.BankId;
                    splitToUpdate.Name = split.Name;
                    splitToUpdate.Narration = split.Narration;
                    _configurationService.SaveSplit(splitToUpdate);
                    TempData["Updated"] = "Success";
                }

            }

            var splitConfigModel = new SplitConfigModel()
            {
                ApplicationFormId = splitToUpdate.ApplicationFormId,
                ApplicationForms = _applicationFormRepository.GetAppForms(),
                Splits = _configurationService.GetSplits(splitToUpdate.ApplicationFormId).ToList(),
                Banks = _configurationService.GetBanks()
            };
            return View("Index", splitConfigModel);

        }

        public ActionResult LoadSplit(int applicationFormId)
        {
            var splitConfigModel = new SplitConfigModel()
            {
                ApplicationFormId = applicationFormId,
                ApplicationForms = _applicationFormRepository.GetAppForms(),
                Splits = _configurationService.GetSplits(applicationFormId).ToList(),
                Banks = _configurationService.GetBanks()
            };
            return View("Index", splitConfigModel);
        }
    }
}