using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using CsvHelper;
using EduApply.Data.Entities;
using EduApply.Logic.Interfaces;
using EduApply.Logic.Service;
using EduApply.Logic.Utility;
using EduApply.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace EduApply.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class VenueController : Controller
    {
        public const string Success = "Success";
        private IConfigurationService _ConfigurationService;
        private IAuditTrailRepository _auditTrailRepository;
        private IVenueAssignmentService _venueService;
        public VenueController(IConfigurationService configurationService,
                     IVenueAssignmentService venueService, 
            IAuditTrailRepository auditTrailRepository)
        {
            this._ConfigurationService = configurationService;
            this._auditTrailRepository = auditTrailRepository;
            this._venueService = venueService;
        }
        public ActionResult Index(int? sucessfulUploads, int? updatedUploads, int? faileduploads)
        {
            var venues = _venueService.GetVenues().ToList();
            var venueModel = Mapper.Map<List<Venues>, List<VenueModel>>(venues);
            if (Session["UploadErrors"] != null)
            {
                var uploadError = Session["UploadErrors"] as List<string>;
                foreach (var error in uploadError)
                {
                    ModelState.AddModelError("", error);
                }
                Session["UploadErrors"] = null;
            }
            if (sucessfulUploads != null)
            {
                TempData["successfulUploads"] = sucessfulUploads;
            }
            if (faileduploads != null)
            {
                TempData["failedUploads"] = faileduploads;
            }
            if (updatedUploads != null)
            {
                TempData["updatedUploads"] = updatedUploads;
            }

            return View(venueModel);
        }
        [HttpGet]
        public ActionResult Create()
        {
            var model = new VenueModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult Create(Venues venue)
        {
            var duplicateVenue = _venueService.GetVenue(venue.Name);
            if (duplicateVenue.Any())
            {
                ModelState.AddModelError("", "A Venue with the name specified already exists, try using another name");
                return View();
            }
            _venueService.SaveVenue(venue);
            TempData["Created"] = Success;
            var IUtilityService = EngineContext.Resolve<IUtilityService>();
            var userRole = UserManager.GetRoles(User.Identity.GetUserId());
            var localTime = _ConfigurationService.GetCurrentWestAfricanDateTime();
            var auditTrail = new AuditTrail()
            {
                UserId = User.Identity.GetUserId(),
                Username = User.Identity.GetUserName(),
                AuditActionId = Convert.ToInt32(AuditTrailActions.AddVenue),
                Details = "created Venue \'" + venue.Name + "\'",
                TimeStamp = localTime,
                UserRole = userRole.First(),
                UserIp = IUtilityService.GetIp()
            };
            _auditTrailRepository.SaveAuditTrail(auditTrail);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int venueId)
        {
            var venue = _venueService.GetVenue(venueId);
            var venueModel = Mapper.Map<Venues, VenueModel>(venue);
            return View(venueModel);
        }
        [HttpPost]
        public ActionResult Edit(Venues venue)
        {
            var duplicateVenue = _venueService.GetVenues();
            if (duplicateVenue.Any(x => x.Name == venue.Name && x.Id != venue.Id))
            {
                ModelState.AddModelError("", "A Venue with the name specified already exists, try using another name");
                return View();
            }
            var venueToUpdate = _venueService.GetVenue(venue.Id);
            venueToUpdate.Name = venue.Name;
            venueToUpdate.Capacity = venue.Capacity;
            venueToUpdate.Active = venue.Active;
            _venueService.SaveVenue(venueToUpdate);

            //check if admin is increasing the capacity for a filled venue and change filled state of all mappings for that venue if new capacity is greater than current capacity
            var examVenues = _venueService.GetExamVenues(venue.Id);
            foreach (var examVenue in examVenues)
            {
                if (examVenue.IsFilled)
                {
                    if (venueToUpdate.Capacity > examVenue.NoOfAllocatedSeats)
                    {
                        examVenue.IsFilled = false;
                        _venueService.SaveExamVenue(examVenue);
                    }
                }
            } 
            
            var IUtilityService = EngineContext.Resolve<IUtilityService>();

            var userRole = UserManager.GetRoles(User.Identity.GetUserId());
            var localTime = _ConfigurationService.GetCurrentWestAfricanDateTime();
            var auditTrail = new AuditTrail()
            {
                UserId = User.Identity.GetUserId(),
                Username = User.Identity.GetUserName(),
                AuditActionId = Convert.ToInt32(AuditTrailActions.AddVenue),
                Details = "edited Venue \'" + venue.Name + "\'",
                TimeStamp = localTime,
                UserRole = userRole.First(),
                UserIp = IUtilityService.GetIp()
            };
            _auditTrailRepository.SaveAuditTrail(auditTrail);

            TempData["Edited"] = Success;
            return RedirectToAction("Index");
        }
        public ActionResult Activate(int id)
        {
            var venue = _venueService.GetVenue(id);
            venue.Active = true;
            _venueService.UpdateVenue(venue);
            TempData["Activate"] = Success;
            var IUtilityService = EngineContext.Resolve<IUtilityService>();
            var userRole = UserManager.GetRoles(User.Identity.GetUserId());
            var localTime = _ConfigurationService.GetCurrentWestAfricanDateTime();
            var auditTrail = new AuditTrail()
            {
                UserId = User.Identity.GetUserId(),
                Username = User.Identity.GetUserName(),
                AuditActionId = Convert.ToInt32(AuditTrailActions.AddVenue),
                Details = "activated Venue \'" + venue.Name + "\'",
                TimeStamp = localTime,
                UserRole = userRole.First(),
                UserIp = IUtilityService.GetIp()
            };
            _auditTrailRepository.SaveAuditTrail(auditTrail);

            return RedirectToAction("Index");
        }
        public ActionResult Deactivate(int id)
        {
            var venue = _venueService.GetVenue(id);
            venue.Active = false;
            _venueService.UpdateVenue(venue);
            TempData["Deactivate"] = Success;

            var IUtilityService = EngineContext.Resolve<IUtilityService>();
            var userRole = UserManager.GetRoles(User.Identity.GetUserId());
            var localTime = _ConfigurationService.GetCurrentWestAfricanDateTime();
            var auditTrail = new AuditTrail()
            {
                UserId = User.Identity.GetUserId(),
                Username = User.Identity.GetUserName(),
                AuditActionId = Convert.ToInt32(AuditTrailActions.AddVenue),
                Details = "deactivated Venue \'" + venue.Name + "\'",
                TimeStamp = localTime,
                UserRole = userRole.First(),
                UserIp = IUtilityService.GetIp()
            };
            _auditTrailRepository.SaveAuditTrail(auditTrail);
            return RedirectToAction("Index");
        }
        public ActionResult VenueUpload(HttpPostedFileBase venueUpload)
        {
            var uploadErrors = new List<string>();
            var uploadedVenues = new List<Venues>();
            var duplicateVenueName = new List<string>();
            int successfulUploads = 0;
            int failedUploads = 0;
            int updatedUploads = 0;
            bool uploaded = false;
            bool isUpdate = false;
            var lineReading = 2;
            if (venueUpload != null)
            {
                if (venueUpload.ContentLength > 0)
                {
                    string fileExtension = Path.GetExtension(venueUpload.FileName);
                    if (fileExtension != ".csv")
                    {
                        var venues = _venueService.GetVenues().ToList();
                        var venueModel = Mapper.Map<List<Venues>, List<VenueModel>>(venues);
                        ModelState.AddModelError("", "Incorrect File Format, only csv files are acceptable");
                        return View("Index", venueModel);
                    }
                    string filePath =
                        System.Web.HttpContext.Current.Server.MapPath("~/App_Data/AdminUploads/" + venueUpload.FileName);
                    venueUpload.SaveAs(filePath);
                    var filestream = new StreamReader(System.IO.File.OpenRead(filePath));
                    using (TextReader reader = filestream)
                    {
                        var csv = new CsvReader(reader);
                        while (csv.Read())
                        {
                            try
                             {
                                var name = csv.GetField<string>("Name").Trim();
                                var capacity = csv.GetField<int?>("Capacity");
                                var activeString = csv.GetField<string>("Active");

                                if (string.IsNullOrEmpty(name) && capacity == null && activeString == null)
                                {
                                    lineReading++;
                                    continue;
                                }


                                if (string.IsNullOrEmpty(name))
                                {
                                    uploadErrors.Add("Error Uploading data in line " + lineReading + " Name is a required field");
                                    lineReading++;
                                    failedUploads++;
                                    continue;
                                }
                                if (capacity == null)
                                {
                                    uploadErrors.Add("Error Uploading data in line " + lineReading + " Capacity is a required field");
                                    lineReading++;
                                    failedUploads++;
                                    continue;
                                }
                                if (!string.Equals(activeString, "true", StringComparison.InvariantCultureIgnoreCase) && !string.Equals(activeString, "false", StringComparison.InvariantCultureIgnoreCase))
                                {
                                    uploadErrors.Add("Invalid data in line " + lineReading + " for active field, enter true or false");
                                    lineReading++;
                                    failedUploads++;
                                    continue;
                                }

                                var active = Convert.ToBoolean(activeString);

                                var duplicateVenue = uploadedVenues.FirstOrDefault(x => x.Name == name);
                                if (duplicateVenue != null)
                                {
                                    uploadedVenues.Remove(duplicateVenue);
                                    failedUploads += 2;
                                    uploadErrors.Add("Duplicate name for venue in line " + lineReading);
                                    duplicateVenueName.Add(duplicateVenue.Name);
                                    lineReading++;
                                    continue;
                                }
                                if (duplicateVenueName.Contains(name))
                                {
                                    failedUploads++;
                                    uploadErrors.Add("Duplicate name for venue in line " + lineReading);
                                    lineReading++;
                                    continue;
                                }
                                //if compiler gets here t means all validations have been passed next we add it to list
                                uploadedVenues.Add(new Venues()
                                {
                                    Name = name,
                                    Capacity = Convert.ToInt32(capacity),
                                    Active = Convert.ToBoolean(active)
                                });
                                lineReading++;
                            }
                            catch (CsvMissingFieldException ex)
                            {
                                uploadErrors.Add(ex.Message);
                                uploaded = false;
                                break;
                            }
                            catch (Exception)
                            {

                                throw;
                            }

                        }
                    }
                    //at this point compiler has read all records in file and removed or skipped the ones with error
                    //now we save to db
                    if (uploadedVenues.Count > 0)
                    {
                        foreach (var venue in uploadedVenues)
                        {
                            var savedVenue = _venueService.GetVenue(venue.Name).FirstOrDefault() ?? new Venues();
                            isUpdate = savedVenue.Id > 0 ? true : false;
                            if (isUpdate)
                            {
                                updatedUploads++;
                                savedVenue.Name = venue.Name;
                                savedVenue.Capacity = venue.Capacity;
                                savedVenue.Active = venue.Active;
                                _venueService.UpdateVenue(savedVenue);
                                uploaded = true;
                            }
                            else
                            {
                                _venueService.SaveVenue(venue);
                                successfulUploads++;
                                uploaded = true;
                            }
                        }
                        if (uploaded)
                        {
                            var IUtilityService = EngineContext.Resolve<IUtilityService>();
                            var userRole = UserManager.GetRoles(User.Identity.GetUserId());
                            var localTime = _ConfigurationService.GetCurrentWestAfricanDateTime();
                            var auditTrail = new AuditTrail()
                            {
                                UserId = User.Identity.GetUserId(),
                                Username = User.Identity.GetUserName(),
                                AuditActionId = Convert.ToInt32(AuditTrailActions.AddVenue),
                                Details = "uploaded a list of venues " + "\'" + venueUpload.FileName + "\'",
                                TimeStamp = localTime,
                                UserRole = userRole.First(),
                                UserIp = IUtilityService.GetIp()
                            };
                            _auditTrailRepository.SaveAuditTrail(auditTrail);
                        }
                    }
                }
            }
            Session["UploadErrors"] = uploadErrors;
            return RedirectToAction("Index", new { sucessfulUploads = successfulUploads, UpdatedUploads = updatedUploads, faileduploads = failedUploads });
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