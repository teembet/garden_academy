using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using EduApply.Data.Entities;
using EduApply.Logic.Interfaces;
using EduApply.Logic.Service;
using EduApply.Logic.Utility;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Newtonsoft.Json;
using NPOI.SS.Formula.Functions;
using Owin;
using EduApply.Web.Models;

namespace EduApply.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationUserManager _userManager;
        private IEmailSender _emailSender;
        private IRegistrationService _registrationService;
        private IEncryptionService _encryptionService;
        private IAuditTrailRepository _auditTrailRepository;
        private IUtilityService _utilityService;
        private IConfigurationService _configurationService;
        private Tenancy tenancy = EngineContext.Resolve<Tenancy>();

        public AccountController(IEmailSender emailSender, IConfigurationService configurationService, IUtilityService utilityService, IRegistrationService registrationService, IEncryptionService encryptionService, IAuditTrailRepository auditTrailRepository)
        {
            this._emailSender = emailSender;
            this._registrationService = registrationService;
            this._encryptionService = encryptionService;
            this._auditTrailRepository = auditTrailRepository;
            this._utilityService = utilityService;
            this._configurationService = configurationService;
        }

        public class CaptchaResponse
        {
            [JsonProperty("success")]
            public bool Success { get; set; }

            [JsonProperty("error-codes")]
            public List<string> ErrorCodes { get; set; }
        }

        public AccountController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ActionResult Index()
        {
            return RedirectToAction("Login");
        }
        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            //Commented out temporarily for test deployment, please review later

            if (HttpContext.User.Identity.IsAuthenticated)
            {
                if (System.Web.HttpContext.Current.User.IsInRole("Admin"))
                {
                    return RedirectToAction("Index", "Admin");
                }
                if (System.Web.HttpContext.Current.User.IsInRole("SchoolAdmin"))
                {
                    return RedirectToAction("Index", "ApplicationForm");
                }

                if (System.Web.HttpContext.Current.User.IsInRole("Student"))
                {
                    return RedirectToAction("Index", "Home");
                }
                //return View("Error", new string[] { "Access Denied" });
            }
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            var response = Request["g-recaptcha-response"];

            if (ModelState.IsValid)
            {

                var user = await UserManager.FindAsync(model.Email, model.Password);

                if (user != null)
                {
                    if (!await UserManager.IsEmailConfirmedAsync(user.Id))
                    {
                        return View("Error", new string[] { "You must have a confirmed email to log on." });
                    }
                    if (!user.IsActive)
                    {
                        return View("Error", new string[] { "Your Account is inactive. Please contact Administrator" });
                    }
                    if (user.AccessFailedCount > 3 && response == null)
                    {
                        //response == null simply means captcha is not showing cos user just probably called d url the first time
                        //this method redirects the user to the login screen and saves user into seesion where accessfailed can be checked
                        //captcha can be shown appropriately
                        Session["UnAuthenticatedUser"] = user;
                        var loginModel = new LoginViewModel()
                        {
                            Email = model.Email,
                            Password = model.Password
                        };
                        return View(loginModel);
                    }

                    if (user.AccessFailedCount > 3 && response != null)
                    {
                        //secret that was generated in key value pair
                        const string secret = "6LdekQITAAAAAI0UfYPY4Eafiq6mRzfymuUgLhtv";

                        var client = new WebClient();
                        var reply =
                            client.DownloadString(
                                string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secret, response));

                        var captchaResponse = JsonConvert.DeserializeObject<CaptchaResponse>(reply);

                        //when response is false check for the error message
                        if (!captchaResponse.Success)
                        {
                            if (captchaResponse.ErrorCodes.Count <= 0)
                            {
                                ModelState.AddModelError("", "Invalid Captcha Response, Please click the checkbox in the captcha below");
                                return View();
                            }

                            var error = captchaResponse.ErrorCodes[0].ToLower();
                            switch (error)
                            {
                                case ("missing-input-secret"):
                                    ModelState.AddModelError("", "The secret parameter is missing.");
                                    return View();
                                    //ViewBag.Message = "The secret parameter is missing.";
                                    break;
                                case ("invalid-input-secret"):
                                    //ViewBag.Message = "The secret parameter is invalid or malformed.";
                                    ModelState.AddModelError("", "The secret parameter is invalid or malformed.");
                                    return View();
                                    break;

                                case ("missing-input-response"):
                                    // ViewBag.Message = "The response parameter is missing.";
                                    ModelState.AddModelError("", "The response parameter is missing, Please click on the check box in the captcha below.");
                                    return View();
                                    break;
                                case ("invalid-input-response"):
                                    //ViewBag.Message = "The response parameter is invalid or malformed.";
                                    ModelState.AddModelError("", "The response parameter is invalid or malformed., Please click on the check box in the captcha below.");
                                    return View();
                                    break;

                                default:
                                    // ViewBag.Message = "Error occured. Please try again";
                                    ModelState.AddModelError("", "Error occured. Please try again.");
                                    return View();
                                    break;
                            }
                        }
                        else
                        {
                            Session["UnAuthenticatedUser"] = null;
                            await UserManager.ResetAccessFailedCountAsync(user.Id);
                            await SignInAsync(user, model.RememberMe);
                            var usernam = user.Email;
                            var userRol = (await UserManager.GetRolesAsync(user.Id)).First();
                            var localTime = _configurationService.GetCurrentWestAfricanDateTime();
                            if (!UserManager.IsInRole(user.Id, "Student"))
                            {
                                //try
                                //{

                                var auditTrail = new AuditTrail()
                                {
                                    UserId = user.Id,
                                    Username = usernam,
                                    AuditActionId = Convert.ToInt32(AuditTrailActions.Login),
                                    Details = "logged in",
                                    TimeStamp = localTime,
                                    UserRole = userRol,
                                    UserIp = _utilityService.GetIp()
                                };
                                _auditTrailRepository.SaveAuditTrail(auditTrail);
                                //}
                                //catch { }
                                //if (returnUrl != null)
                                //{
                                //    return RedirectToLocal(returnUrl);
                                //}
                                if (UserManager.IsInRole(user.Id, "Admin"))
                                {
                                    return RedirectToAction("Index", "Admin");
                                }
                                if (UserManager.IsInRole(user.Id, "SchoolAdmin"))
                                {
                                    return RedirectToAction("Index", "ApplicationForm");
                                }
                            }
                            return RedirectToAction("Index", "Home");
                        }
                    }

                    //check if user has been locked out
                    //if (await UserManager.IsLockedOutAsync(user.Id))
                    //{
                    //    return View("Error", new string[] { "Your Account has been locked out, please try Login in again by. " + Convert.ToDateTime(user.LockoutEndDateUtc).AddHours(1).AddMinutes(1).ToString("dd-MMM-yy hh:mm tt") });
                    //}
                    await UserManager.ResetAccessFailedCountAsync(user.Id);
                    await SignInAsync(user, model.RememberMe);
                    var username = user.Email;
                    var userRole = (await UserManager.GetRolesAsync(user.Id)).First();
                    if (!UserManager.IsInRole(user.Id, "Student"))
                    {
                        try
                        {
                            var localTime = _configurationService.GetCurrentWestAfricanDateTime();
                            var auditTrail = new AuditTrail()
                            {
                                UserId = user.Id,
                                Username = username,
                                AuditActionId = Convert.ToInt32(AuditTrailActions.Login),
                                Details = "logged in",
                                TimeStamp = localTime,
                                UserRole = userRole,
                                UserIp = _utilityService.GetIp()
                            };
                            _auditTrailRepository.SaveAuditTrail(auditTrail);
                        }
                        catch
                        {

                        }
                        //if (returnUrl != null)
                        //{
                        //    return RedirectToLocal(returnUrl);
                        //}
                        if (UserManager.IsInRole(user.Id, "Admin"))
                        {
                            return RedirectToAction("Index", "Admin");
                        }
                        if (UserManager.IsInRole(user.Id, "SchoolAdmin"))
                        {
                            return RedirectToAction("Index", "ApplicationForm");
                        }
                    }
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    //check if user exists
                    var applicationUser = await UserManager.FindByEmailAsync(model.Email);
                    if (applicationUser != null)
                    {
                        //this means password was the incorrect entry, therefore record failed attempt
                        await UserManager.AccessFailedAsync(applicationUser.Id);
                        ModelState.AddModelError("", "Invalid username or password.");
                        Session["UnAuthenticatedUser"] = applicationUser;

                    }
                    else
                    {
                        if (tenancy.Code.Equals("FUTO") || tenancy.Code.Equals("CRUTECH"))
                        {
                            var regNum = model.Email;
                            if (regNum.ToLower().Equals(model.Password.ToLower()))
                            {
                                var applications = _registrationService.GetApplicationsByRegNum(regNum).ToList();
                                if (applications.Any())
                                {
                                    var email = applications.FirstOrDefault().UserName;
                                    applicationUser = UserManager.FindByEmail(email);
                                    UserManager.ResetAccessFailedCount(applicationUser.Id);
                                    await SignInAsync(applicationUser, model.RememberMe);
                                    return RedirectToAction("Index", "Home");
                                }
                            }

                        }

                        ModelState.AddModelError("", "Invalid username or password.");
                    }

                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser()
                {
                    UserName = model.Email,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    IsActive = true
                };
                IdentityResult result = UserManager.Create(user, model.Password);
                if (result.Succeeded)
                {
                    //Add User to Student Role
                    UserManager.AddToRole(user.Id, "Student");
                    //Save basicInfo into PersonalInformationTable
                    var personalInformation = new PersonalInformation()
                    {
                        Id = user.Id,
                        LastName = model.LastName,
                        FirstName = model.FirstName,
                        MiddleName = model.MiddleName,
                        Email = model.Email,
                        PhoneNumber = model.PhoneNumber
                    };
                    _registrationService.SavePersonalInformation(personalInformation);
                    try
                    {

                        //prevent automatic sigining in and require Email confirmation by commenting out the line below
                        //await SignInAsync(user, isPersistent: false);
                        var localTime = _configurationService.GetCurrentWestAfricanDateTime();
                        var auditTrail = new AuditTrail()
                        {
                            UserId = user.Id,
                            Username = user.UserName,
                            AuditActionId = Convert.ToInt32(AuditTrailActions.ApplicantRegistration),
                            Details = "Registered on" + localTime,
                            TimeStamp = localTime,
                            UserRole = "Student",
                            UserIp = _utilityService.GetIp()
                        };
                        _auditTrailRepository.SaveAuditTrail(auditTrail);
                    }
                    catch
                    {

                    }

                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    string code = UserManager.GenerateEmailConfirmationToken(user.Id);
                    _emailSender.SendEmail(model.Email, personalInformation.FirstName, Server.UrlEncode(code), Convert.ToInt32(EmailType.EmailVerification), null);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    ViewBag.message = "An Email has been sent to " + model.Email + ", Please check and follow instructions to activate your account";
                    return View("Info");
                    //return RedirectToAction("Index", "Home");
                }
                else
                {
                    if (result.Errors.Count() > 1)
                    {
                        var errorList = result.Errors.ToList();
                        string errorMsg = "Email address already exists, Try another?";
                        ModelState.AddModelError("", errorMsg);
                    }
                    else
                    {
                        AddErrors(result);
                    }

                }
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult ConfirmEmail(string user, string code)
        {
            if (user == null || code == null)
            {
                return View("Error");
            }
            var userEmail = _encryptionService.DecryptUserName(user);
            var applicationUser = UserManager.FindByEmail(userEmail);

            if (applicationUser != null)
            {
                if (applicationUser.EmailConfirmed)
                {
                    ViewBag.message = "Your account has already been confirmed";
                    return View("Info");
                }
            }
            else
            {
                return View("Error", new string[] { "Your Sign up details were not found, Please ensure you followed the email sent to you properly" });
            }
            IdentityResult result = UserManager.ConfirmEmail(applicationUser.Id, code);
            if (result.Succeeded)
            {
                var userRole = UserManager.GetRoles(applicationUser.Id);
                var localTime = _configurationService.GetCurrentWestAfricanDateTime();
                var auditTrail = new AuditTrail()
                {
                    UserId = applicationUser.Id,
                    Username = applicationUser.UserName,
                    AuditActionId = Convert.ToInt32(AuditTrailActions.EmailVerification),
                    Details = "User Confirmed his/her email",
                    TimeStamp = localTime,
                    UserRole = userRole.First(),
                    UserIp = _utilityService.GetIp()
                };
                _auditTrailRepository.SaveAuditTrail(auditTrail);
                return View("ConfirmEmail");
            }
            else
            {
                AddErrors(result);
                return View();
            }
        }
        [AllowAnonymous]
        [HttpGet]
        public ActionResult ConfirmEmailNonApp(string user, string code)
        {
            //Na stands for non applicant
            if (user == null || code == null)
            {
                return View("Error", new string[] { "Could not find a record for you" });
            }
            var userEmail = _encryptionService.DecryptUserName(user);
            var applicationUser = UserManager.FindByEmail(userEmail);
            if (applicationUser != null)
            {
                if (applicationUser.EmailConfirmed)
                {
                    ViewBag.message = "Your account has already been confirmed";
                    return View("Info");
                }
            }
            else
            {
                return View("Error", new string[] { "Your Sign up details were not found, Please ensure you followed the email sent to you properly" });
            }
            var model = new CreateApplicantModel()
            {
                Email = userEmail,
                Code = code
            };
            return View(model);
        }
        [HttpPost]
        public ActionResult ConfirmEmailNonApp(CreateApplicantModel model)
        {
            var applicationUser = UserManager.FindByEmail(model.Email);
            IdentityResult validPass = null;
            if (!string.IsNullOrEmpty(model.Password))
            {
                //validPass = await UserManager.PasswordValidator.ValidateAsync(model.Password);
                //if (validPass.Succeeded)
                //{
                applicationUser.PasswordHash = UserManager.PasswordHasher.HashPassword(model.Password);
                applicationUser.IsActive = true;
                IdentityResult result = UserManager.Update(applicationUser);
                if (result.Succeeded)
                {
                    IdentityResult confirmResult = UserManager.ConfirmEmail(applicationUser.Id, model.Code);
                    if (confirmResult.Succeeded)
                    {
                        var userRole = UserManager.GetRoles(applicationUser.Id);
                        var localTime = _configurationService.GetCurrentWestAfricanDateTime();
                        var auditTrail = new AuditTrail()
                        {
                            UserId = applicationUser.Id,
                            Username = applicationUser.UserName,
                            AuditActionId = Convert.ToInt32(AuditTrailActions.EmailVerification),
                            Details = "User Confirmed his/her email",
                            TimeStamp = localTime,
                            UserRole = userRole.First(),
                            UserIp = _utilityService.GetIp()
                        };
                        _auditTrailRepository.SaveAuditTrail(auditTrail);
                        return View("ConfirmEmail");
                    }
                    else
                    {
                        AddErrors(confirmResult);
                        return View();
                    }
                }
                //}
                else
                {
                    AddErrors(validPass);
                }
            }
            var returnModel = new CreateApplicantModel()
            {
                Email = model.Email
            };
            return View(returnModel);
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = UserManager.FindByName(model.Email);
                if (user == null)
                {
                    ModelState.AddModelError("", "Sorry, We do not have a record for " + model.Email);
                    return View();
                }
                if (!(UserManager.IsEmailConfirmed(user.Id)))
                {
                    ModelState.AddModelError("", "User has not been confirmed yet");
                    return View();
                }
                string code = UserManager.GeneratePasswordResetToken(user.Id);
                _emailSender.SendEmail(model.Email, model.Email, Server.UrlEncode(code), Convert.ToInt32(EmailType.PasswordReset), "");
                ViewBag.message = "An email with the link to reset your password has been sent to " + model.Email;
                return View("Success");
                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);		
                // await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                // return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string user, string code, string resDt)
        {
            if (user == null)
            {
                return View("Error", new string[] { "Invalid Url, plese follow the link in your email appropriately" });
            }
            var userEmail = _encryptionService.DecryptUserName(user);
            var localTime = _configurationService.GetCurrentWestAfricanDateTime();
            var resetDate = DateTime.ParseExact(resDt, "dd/MMM/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);
            if (localTime > resetDate.AddDays(3))
            {
                return View("Error", new string[] { "The password link has expired" });
            }
            var resetPasswordModel = new ResetPasswordViewModel()
            {
                Email = userEmail,
                Code = code
            };
            return View(resetPasswordModel);
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = UserManager.FindByName(model.Email);
                if (user == null)
                {
                    ModelState.AddModelError("", "No user found.");
                    return View();
                }
                IdentityResult result = UserManager.ResetPassword(user.Id, model.Code, model.Password);
                if (result.Succeeded)
                {
                    var userRole = UserManager.GetRoles(user.Id);
                    var localTime = _configurationService.GetCurrentWestAfricanDateTime();
                    var auditTrail = new AuditTrail()
                    {
                        UserId = user.Id,
                        Username = user.UserName,
                        AuditActionId = Convert.ToInt32(AuditTrailActions.PasswordReset),
                        Details = "User reset his/her password",
                        TimeStamp = localTime,
                        UserRole = userRole.First(),
                        UserIp = _utilityService.GetIp()
                    };
                    _auditTrailRepository.SaveAuditTrail(auditTrail);
                    // return RedirectToAction("ResetPasswordConfirmation", "Account");
                    ViewBag.message = "Your password has been successfully reset, you can log in with your new password";
                    return View("Success");
                }
                else
                {
                    AddErrors(result);
                    return View();
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/Disassociate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Disassociate(string loginProvider, string providerKey)
        {
            ManageMessageId? message = null;
            IdentityResult result = UserManager.RemoveLogin(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
            if (result.Succeeded)
            {
                var user = UserManager.FindById(User.Identity.GetUserId());
                SignIn(user, isPersistent: false);
                message = ManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageMessageId.Error;
            }
            return RedirectToAction("Manage", new { Message = message });
        }

        public ActionResult ApplicantManage(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";
            ViewBag.HasLocalPassword = HasPassword();
            ViewBag.ReturnUrl = Url.Action("Manage");
            return View();
        }

        //
        // POST: /Account/Manage
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ApplicantManage(ManageUserViewModel model)
        {
            bool hasPassword = HasPassword();
            ViewBag.HasLocalPassword = hasPassword;
            ViewBag.ReturnUrl = Url.Action("Manage");
            if (hasPassword)
            {
                if (ModelState.IsValid)
                {
                    //Uncomment the code below if you want to prevent users from changing their password to the same current password
                    //var user = await UserManager.FindAsync(User.Identity.Name, model.OldPassword);
                    //if (user != null)
                    //{
                    //    if (model.OldPassword == model.NewPassword)
                    //    {
                    //        ModelState.AddModelError("","Old and New Password cannot be the same");
                    //        return View(model);
                    //    }
                    //}
                    IdentityResult result = UserManager.ChangePassword(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        var IUtilityService = EngineContext.Resolve<IUtilityService>();
                        var userRole = UserManager.GetRoles(User.Identity.GetUserId());
                        var localTime = _configurationService.GetCurrentWestAfricanDateTime();
                        var auditTrail = new AuditTrail()
                        {
                            UserId = User.Identity.GetUserId(),
                            Username = User.Identity.GetUserName(),
                            AuditActionId = Convert.ToInt32(AuditTrailActions.PasswordChange),
                            Details = "User changed his/her password",
                            TimeStamp = localTime,
                            UserRole = userRole.First(),
                            UserIp = IUtilityService.GetIp()
                        };
                        _auditTrailRepository.SaveAuditTrail(auditTrail);

                        ViewBag.message = "Your password has been successfully changed, You can Log in again with your new credentails";
                        AuthenticationManager.SignOut();
                        return View("Success");
                        //var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                        //await SignInAsync(user, isPersistent: false);
                        //return RedirectToAction("Manage", new { Message = ManageMessageId.ChangePasswordSuccess });
                    }
                    else
                    {
                        AddErrors(result);
                    }
                }
            }
            else
            {
                // User does not have a password so remove any validation errors caused by a missing OldPassword field
                ModelState state = ModelState["OldPassword"];
                if (state != null)
                {
                    state.Errors.Clear();
                }

                if (ModelState.IsValid)
                {
                    IdentityResult result = UserManager.AddPassword(User.Identity.GetUserId(), model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Manage", new { Message = ManageMessageId.SetPasswordSuccess });
                    }
                    else
                    {
                        AddErrors(result);
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/Manage
        public ActionResult Manage(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";
            ViewBag.HasLocalPassword = HasPassword();
            ViewBag.ReturnUrl = Url.Action("Manage");
            return View();
        }

        //
        // POST: /Account/Manage
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Manage(ManageUserViewModel model)
        {
            bool hasPassword = HasPassword();
            ViewBag.HasLocalPassword = hasPassword;
            ViewBag.ReturnUrl = Url.Action("Manage");
            if (hasPassword)
            {
                if (ModelState.IsValid)
                {
                    //Uncomment the code below if you want to prevent users from changing their password to the same current password
                    //var user = await UserManager.FindAsync(User.Identity.Name, model.OldPassword);
                    //if (user != null)
                    //{
                    //    if (model.OldPassword == model.NewPassword)
                    //    {
                    //        ModelState.AddModelError("","Old and New Password cannot be the same");
                    //        return View(model);
                    //    }
                    //}
                    IdentityResult result = UserManager.ChangePassword(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        var IUtilityService = EngineContext.Resolve<IUtilityService>();
                        var userRole = UserManager.GetRoles(User.Identity.GetUserId());
                        var localTime = _configurationService.GetCurrentWestAfricanDateTime();
                        var auditTrail = new AuditTrail()
                        {
                            UserId = User.Identity.GetUserId(),
                            Username = User.Identity.GetUserName(),
                            AuditActionId = Convert.ToInt32(AuditTrailActions.PasswordChange),
                            Details = "User changed his/her password",
                            TimeStamp = localTime,
                            UserRole = userRole.First(),
                            UserIp = IUtilityService.GetIp()
                        };
                        _auditTrailRepository.SaveAuditTrail(auditTrail);

                        ViewBag.message = "Your password has been successfully changed, You can Log in again with your new credentails";
                        AuthenticationManager.SignOut();
                        return View("Success");
                        //var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                        //await SignInAsync(user, isPersistent: false);
                        //return RedirectToAction("Manage", new { Message = ManageMessageId.ChangePasswordSuccess });
                    }
                    else
                    {
                        AddErrors(result);
                    }
                }
            }
            else
            {
                // User does not have a password so remove any validation errors caused by a missing OldPassword field
                ModelState state = ModelState["OldPassword"];
                if (state != null)
                {
                    state.Errors.Clear();
                }

                if (ModelState.IsValid)
                {
                    IdentityResult result = UserManager.AddPassword(User.Identity.GetUserId(), model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Manage", new { Message = ManageMessageId.SetPasswordSuccess });
                    }
                    else
                    {
                        AddErrors(result);
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public ActionResult ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = AuthenticationManager.GetExternalLoginInfo();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var user = UserManager.Find(loginInfo.Login);
            if (user != null)
            {
                SignIn(user, isPersistent: false);
                return RedirectToLocal(returnUrl);
            }
            else
            {
                // If the user does not have an account, then prompt the user to create an account
                ViewBag.ReturnUrl = returnUrl;
                ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        //
        // POST: /Account/LinkLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider)
        {
            // Request a redirect to the external login provider to link a login for the current user
            return new ChallengeResult(provider, Url.Action("LinkLoginCallback", "Account"), User.Identity.GetUserId());
        }

        //
        // GET: /Account/LinkLoginCallback
        public async Task<ActionResult> LinkLoginCallback()
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
            if (loginInfo == null)
            {
                return RedirectToAction("Manage", new { Message = ManageMessageId.Error });
            }
            IdentityResult result = await UserManager.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login);
            if (result.Succeeded)
            {
                return RedirectToAction("Manage");
            }
            return RedirectToAction("Manage", new { Message = ManageMessageId.Error });
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser() { UserName = model.Email, Email = model.Email };
                IdentityResult result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInAsync(user, isPersistent: false);

                        // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                        // Send an email with this link
                        // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                        // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                        // SendEmail(user.Email, callbackUrl, "Confirm your account", "Please confirm your account by clicking this link");

                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Account");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult RemoveAccountList()
        {
            var linkedAccounts = UserManager.GetLogins(User.Identity.GetUserId());
            ViewBag.ShowRemoveButton = HasPassword() || linkedAccounts.Count > 1;
            return (ActionResult)PartialView("_RemoveAccountPartial", linkedAccounts);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && UserManager != null)
            {
                UserManager.Dispose();
                UserManager = null;
            }
            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private async Task SignIn(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, await user.GenerateUserIdentityAsync(UserManager));
        }


        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, await user.GenerateUserIdentityAsync(UserManager));
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        private void SendEmail(string email, string callbackUrl, string subject, string message)
        {
            // For information on sending mail, please visit http://go.microsoft.com/fwlink/?LinkID=320771
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            Error
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        private class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties() { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}