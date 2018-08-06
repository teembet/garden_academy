using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using EduApply.Data.Entities;
using EduApply.Logic.Interfaces;
using EduApply.Logic.Service;
using EduApply.Logic.Utility;
using EduApply.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace EduApply.Web.Controllers
{
    [Authorize(Roles = "Admin, SchoolAdmin")]
    public class AdminController : Controller
    {
        private IEmailSender _emailSender;
        private IEncryptionService _encryptionService;
        private IRegistrationService _registrationService;
        private ILocationRepository _locationRepository;
        private IAuditTrailRepository _auditTrailRepository;
        private IConfigurationService _configurationService;
        public AdminController(IEmailSender emailSender, IEncryptionService encryptionService, IConfigurationService configurationService, IRegistrationService registrationService, ILocationRepository locationRepository, IAuditTrailRepository auditTrailRepository)
        {
            this._emailSender = emailSender;
            this._encryptionService = encryptionService;
            this._registrationService = registrationService;
            this._locationRepository = locationRepository;
            this._auditTrailRepository = auditTrailRepository;
            this._configurationService = configurationService;
        }
        public const string Success = "Success";
        // GET: Admin
        public ActionResult Index()
        {
            var tenancy = EngineContext.Resolve<Tenancy>();
            var connectionString = tenancy.ConnectionString;
            var adminUsers = new List<AdminUserModel>();

            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand("GetAdminUsers", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {

                    adminUsers = rdr.Cast<IDataRecord>().Select(r => new AdminUserModel
                       {
                           Id = r["id"] is DBNull ? null : r["id"].ToString(),
                           UserName = r["UserName"] is DBNull ? null : r["UserName"].ToString(),
                           Email = r["Email"] is DBNull ? null : r["Email"].ToString(),
                           PhoneNumber = r["PhoneNumber"] is DBNull ? null : r["PhoneNumber"].ToString(),
                           IsActive = Convert.ToBoolean(r["IsActive"])
                       }).ToList();
                }
            }
            //var allUsers = UserManager.Users.ToList();
            //var adminUsers = allUsers.Where(x => UserManager.IsInRole(x.Id, "SchoolAdmin") || UserManager.IsInRole(x.Id, "Admin"));

            // var model = Mapper.Map<IEnumerable<ApplicationUser>, IEnumerable<AdminUserModel>>(adminUsers);
            foreach (var user in adminUsers)
            {
                user.UserRole = UserManager.GetRoles(user.Id).First();

            }
            // var role = RoleManager.Roles.FirstOrDefault();

            return View(adminUsers);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {

            var model = new CreateAdminModel();
            model.AdminRoles = RoleManager.Roles.Where(r => r.Name != "Student");
            return View(model);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Create(CreateAdminModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ApplicationUser user = new ApplicationUser
                    {
                        UserName = model.Email,
                        Email = model.Email,
                        PhoneNumber = model.PhoneNumber
                    };
                    IdentityResult result = UserManager.Create(user);
                    if (result.Succeeded)
                    {
                        UserManager.AddToRole(user.Id, model.UserRole);

                        string code = UserManager.GenerateEmailConfirmationToken(user.Id);

                        _emailSender.SendEmail(model.Email, model.Email, Server.UrlEncode(code), Convert.ToInt32(EmailType.AccountSetup), model.UserRole);
                        var IUtilityService = EngineContext.Resolve<IUtilityService>();
                        TempData["Created"] = Success;
                        var loggedInUserRole = UserManager.GetRoles(User.Identity.GetUserId());
                        var localTime = _configurationService.GetCurrentWestAfricanDateTime();
                        var auditTrail = new AuditTrail()
                        {
                            UserId = User.Identity.GetUserId(),
                            Username = User.Identity.GetUserName(),
                            AuditActionId = Convert.ToInt32(AuditTrailActions.CreateUser),
                            Details = "created user " + user.UserName,
                            TimeStamp = localTime,
                            UserRole = loggedInUserRole.First(),
                            UserIp = IUtilityService.GetIp()
                        };
                        _auditTrailRepository.SaveAuditTrail(auditTrail);

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        if (result.Errors.Count() > 1)
                        {
                            var errorList = result.Errors.ToList();
                            string errorMsg = "Email address already exists. Try another?";
                            ModelState.AddModelError("", errorMsg);
                        }
                        else
                        {
                            AddErrorsFromResult(result);
                        }


                        model.AdminRoles = RoleManager.Roles.Where(r => r.Name != "Student");
                    }
                }
                catch (HttpException ex)
                {
                    return View("Error", new string[] { ex.ToString() });
                }
                catch (Exception e)
                {

                    return View("Error", new string[] { e.ToString() });
                }


            }
            else
            {
                ModelState.AddModelError("", "Please fill User Details correctly and select a Role");
                model.AdminRoles = RoleManager.Roles.Where(r => r.Name != "Student");
            }
            return View(model);
        }
        [AllowAnonymous]
        public ActionResult AdminRegistration(string user, string code)
        {
            //if (ViewData["ModelError"] != null)
            //{
            //    var errorResult = ViewData["ModelError"] as IdentityResult;
            //    foreach (string error in errorResult.Errors)
            //    {
            //        ModelState.AddModelError("", error);
            //    }
            //}
            var userEmail = _encryptionService.DecryptUserName(user);
            var applicationUser = UserManager.FindByEmail(userEmail);
            if (applicationUser != null)
            {
                if (applicationUser.EmailConfirmed)
                {
                    ViewBag.message = "Your account has already been confirmed, Click OK to Login";
                    return View("Info");
                }
                var residentStates = _locationRepository.GetStates();
                var countries = _locationRepository.GetCountries();
                var model = new AdminRegistrationModel()
                {
                    Id = applicationUser.Id,
                    Email = applicationUser.Email,
                    PhoneNumber = applicationUser.PhoneNumber,
                    ConfirmationCode = code,
                    ResidentStates = Mapper.Map<IEnumerable<State>, IEnumerable<StateModel>>(residentStates),
                    Countries = Mapper.Map<IEnumerable<Country>, IEnumerable<CountryModel>>(countries),
                    States = new List<StateModel>(),
                    Lgaz = new List<LocalGovernmentAreaModel>()
                };
                return View(model);
            }
            return View("Error",
                new string[] { "Your Account Details were not found, Please follow the Link in your Email appropriately" });
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AdminRegistration(AdminRegistrationModel model)
        {
            ApplicationUser user = await UserManager.FindByIdAsync(model.Id);
            if (user != null)
            {
                user.Email = model.Email;
                user.PhoneNumber = model.PhoneNumber;
                IdentityResult validEmail = await UserManager.UserValidator.ValidateAsync(user);
                if (!validEmail.Succeeded)
                {
                    AddErrorsFromResult(validEmail);

                }
                IdentityResult validPass = null;
                if (!string.IsNullOrEmpty(model.Password))
                {
                    validPass = await UserManager.PasswordValidator.ValidateAsync(model.Password);
                    if (validPass.Succeeded)
                    {
                        user.PasswordHash = UserManager.PasswordHasher.HashPassword(model.Password);
                    }
                    else
                    {
                        AddErrorsFromResult(validPass);
                    }
                }
                if (validEmail.Succeeded && validPass != null && validPass.Succeeded)
                {
                    user.IsActive = true;
                    IdentityResult result = await UserManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        IdentityResult confirmResult = await UserManager.ConfirmEmailAsync(model.Id, model.ConfirmationCode);
                        if (confirmResult.Succeeded)
                        {
                            try
                            {
                                AdminBiodata biodata = Mapper.Map<AdminRegistrationModel, AdminBiodata>(model);
                                _registrationService.SaveAdminBiodata(biodata);
                                ViewBag.message = "Congratulations, You have successfully completed your registration";
                                return View("Success");
                            }
                            catch (Exception ex)
                            {

                                return View("Error", new string[] { "An error occured while saving you Biodata, Please contact your Admin" });
                            }
                        }
                        else
                        {
                            AddErrorsFromResult(confirmResult);
                            return View("Error", new string[] { confirmResult.Errors.First() + " Please ensure the URL sent to your mail was properly followed" });
                        }


                    }
                    else
                    {
                        AddErrorsFromResult(result);
                    }
                }
            }
            return View("Error", new string[] { "An error occured, please ensure you followed all instructions properly" });
        }
        //
        // GET: /Account/ConfirmEmail
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            ApplicationUser user = await UserManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await UserManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    TempData["Deleted"] = Success;
                    return RedirectToAction("Index");

                }
                else
                {
                    return View("Error", result.Errors);
                }
            }
            else
            {
                return View("Error", new string[] { "User Not Found" });
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Deactivate(string id)
        {
            ApplicationUser user = UserManager.FindById(id);
            if (user != null)
            {
                var IUtilityService = EngineContext.Resolve<IUtilityService>();
                user.IsActive = false;
                IdentityResult result = UserManager.Update(user);
                if (result.Succeeded)
                {
                    TempData["Deactivated"] = Success;
                    var loggedInUserRole = UserManager.GetRoles(User.Identity.GetUserId());
                    var localTime = _configurationService.GetCurrentWestAfricanDateTime();
                    var auditTrail = new AuditTrail()
                {
                    UserId = User.Identity.GetUserId(),
                    Username = User.Identity.GetUserName(),
                    AuditActionId = Convert.ToInt32(AuditTrailActions.DeActivateUser),
                    Details = "Deactivated user " + user.UserName,
                    TimeStamp = localTime,
                    UserRole = loggedInUserRole.First(),
                    UserIp = IUtilityService.GetIp()
                };
                    _auditTrailRepository.SaveAuditTrail(auditTrail);

                    return RedirectToAction("Index");

                }
                else
                {
                    return View("Error", result.Errors);
                }
            }
            else
            {
                return View("Error", new string[] { "User Not Found" });
            }
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Activate(string id)
        {
            ApplicationUser user = UserManager.FindById(id);
            if (user != null)
            {
                var IUtilityService = EngineContext.Resolve<IUtilityService>();
                user.IsActive = true;
                IdentityResult result = UserManager.Update(user);
                if (result.Succeeded)
                {
                    TempData["Activated"] = Success;
                    var loggedInUserRole = UserManager.GetRoles(User.Identity.GetUserId());
                    var localTime = _configurationService.GetCurrentWestAfricanDateTime();
                    var auditTrail = new AuditTrail()
                    {
                        UserId = User.Identity.GetUserId(),
                        Username = User.Identity.GetUserName(),
                        AuditActionId = Convert.ToInt32(AuditTrailActions.ActivateUser),
                        Details = "Activated user " + user.UserName,
                        TimeStamp = localTime,
                        UserRole = loggedInUserRole.First(),
                        UserIp = IUtilityService.GetIp()
                    };
                    _auditTrailRepository.SaveAuditTrail(auditTrail);
                    return RedirectToAction("Index");

                }
                else
                {
                    return View("Error", result.Errors);
                }
            }
            else
            {
                return View("Error", new string[] { "User Not Found" });
            }
        }
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(string id)
        {
            ApplicationUser user = await UserManager.FindByIdAsync(id);
            if (user != null)
            {
                var userRole = UserManager.GetRoles(user.Id).First();
                var model = new CreateAdminModel()
                {
                    Id = user.Id,
                    PhoneNumber = user.PhoneNumber,
                    Email = user.Email,
                    UserRole = userRole,
                    AdminRoles = RoleManager.Roles.Where(r => r.Name != "Student")
                };
                return View(model);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Edit(CreateAdminModel adminModel)
        {
            ApplicationUser user = UserManager.FindById(adminModel.Id);
            if (user != null)
            {
                // user.Email = email;
                user.PhoneNumber = adminModel.PhoneNumber;
                //IdentityResult validEmail = await UserManager.UserValidator.ValidateAsync(user);
                //if (!validEmail.Succeeded)
                //{
                //    AddErrorsFromResult(validEmail);
                //}
                //IdentityResult validPass = null;
                //if (!string.IsNullOrEmpty(adminModel.Password))
                //{
                //    validPass = await UserManager.PasswordValidator.ValidateAsync(adminModel.Password);
                //    if (validPass.Succeeded)
                //    {
                //        user.PasswordHash = UserManager.PasswordHasher.HashPassword(adminModel.Password);
                //    }
                //    else
                //    {
                //        AddErrorsFromResult(validPass);
                //    }
                //}
                //if ((validEmail.Succeeded && validPass == null) || (validEmail.Succeeded && !string.IsNullOrEmpty(adminModel.Password) && validPass.Succeeded))
                //{
                var usersCurrentRole = UserManager.GetRoles(adminModel.Id).First();
                if (usersCurrentRole != adminModel.UserRole)
                {
                    UserManager.RemoveFromRole(adminModel.Id, usersCurrentRole);
                    UserManager.AddToRole(adminModel.Id, adminModel.UserRole);
                }
                var IUtilityService = EngineContext.Resolve<IUtilityService>();
                IdentityResult result = UserManager.Update(user);
                if (result.Succeeded)
                {
                    TempData["Edited"] = Success;
                    var loggedInUserRole = UserManager.GetRoles(User.Identity.GetUserId());
                    var localTime = _configurationService.GetCurrentWestAfricanDateTime();
                    var auditTrail = new AuditTrail()
                    {
                        UserId = User.Identity.GetUserId(),
                        Username = User.Identity.GetUserName(),
                        AuditActionId = Convert.ToInt32(AuditTrailActions.UpdateUserRecord),
                        Details = "updated record for " + user.UserName,
                        TimeStamp = localTime,
                        UserRole = loggedInUserRole.First(),
                        UserIp = IUtilityService.GetIp()
                    };
                    _auditTrailRepository.SaveAuditTrail(auditTrail);

                    return RedirectToAction("Index");
                }
                else
                {
                    AddErrorsFromResult(result);
                }
                //   }
            }
            else
            {
                ModelState.AddModelError("", "User Not Found");
            }
            return View(user);
        }

        private void AddErrorsFromResult(IdentityResult result)
        {
            ViewData["ModelError"] = result;
            foreach (string error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        public void UpdateRowMoved(int id, int fromPosition, int toPosition, string direction)
        {

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