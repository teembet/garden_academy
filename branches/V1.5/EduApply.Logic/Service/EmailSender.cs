using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using EduApply.Logic.Interfaces;
using EduApply.Logic.Repository;
using EduApply.Logic.Utility;
using PostmarkDotNet;


namespace EduApply.Logic.Service
{
    public class EmailSender : IEmailSender
    {
        private IEncryptionService _encryptionService;
        private IEmailSettings _emailSettings;
        public EmailSender(IEncryptionService encryptionService, IEmailSettings emailSettings)
        {
            this._encryptionService = encryptionService;
            this._emailSettings = emailSettings;
        }
        public void SendEmail(string email, string emailName, string code, int emailType, string role)
        {
            try
            {
                string encryptedEmail = HttpContext.Current.Server.UrlEncode(_encryptionService.EncryptUserName(email));
                PostmarkMessage msg = new PostmarkMessage();
                msg.From = _emailSettings.UserName;
                msg.To = email;

                if (emailType == Convert.ToInt32(EmailType.PasswordReset))
                {
                    msg.Subject = "Password Reset";
                    var resetDate = DateTime.Now.ToString("dd/MMM/yyyy h:mm:ss tt");
                    string fileName = HttpContext.Current.Server.MapPath("~/EmailTemplates/ResetPassword.html");
                    string mailBody = System.IO.File.ReadAllText(fileName);
                    mailBody = mailBody.Replace("#Name#", emailName);
                    mailBody = mailBody.Replace("#EncryptedUserName#", encryptedEmail);
                    mailBody = mailBody.Replace("#Code#", code);
                    mailBody = mailBody.Replace("#resDt#", resetDate);
                    msg.HtmlBody = mailBody;
                    msg.TextBody = mailBody;

                }
                else if (emailType == Convert.ToInt32(EmailType.EmailVerification))
                {
                    msg.Subject = "Email Confirmation";
                    string fileName = HttpContext.Current.Server.MapPath("~/EmailTemplates/EmailVerification.html");
                    string mailBody = System.IO.File.ReadAllText(fileName);
                    mailBody = mailBody.Replace("#Name#", emailName);
                    mailBody = mailBody.Replace("#EncryptedUserName#", encryptedEmail);
                    mailBody = mailBody.Replace("#Code#", code);
                    msg.HtmlBody = mailBody;
                    msg.TextBody = mailBody;
                }
                else if (emailType == Convert.ToInt32(EmailType.AccountSetup))
                {
                    msg.Subject = "Account Set Up";
                    string fileName = HttpContext.Current.Server.MapPath("~/EmailTemplates/AccountSetUp.html");
                    string mailBody = System.IO.File.ReadAllText(fileName);
                    mailBody = mailBody.Replace("#Name#", emailName);
                    mailBody = mailBody.Replace("#EncryptedUserName#", encryptedEmail);
                    mailBody = mailBody.Replace("#Code#", code);
                    mailBody = mailBody.Replace("#Role#", role);
                    msg.HtmlBody = mailBody;
                    msg.TextBody = mailBody;
                }
                else if (emailType == Convert.ToInt32(EmailType.AccountSetUpForApplicant))
                {
                    msg.Subject = "Verify Email";
                    string fileName =
                        HttpContext.Current.Server.MapPath("~/EmailTemplates/AccountSetUpForApplicant.html");
                    string mailBody = System.IO.File.ReadAllText(fileName);
                    mailBody = mailBody.Replace("#Name#", emailName);
                    mailBody = mailBody.Replace("#EncryptedUserName#", encryptedEmail);
                    mailBody = mailBody.Replace("#Code#", code);
                    msg.HtmlBody = mailBody;
                    msg.TextBody = mailBody;
                }
                else if (emailType == Convert.ToInt32(EmailType.AdmissionOffered))
                {

                    msg.Subject = "Offer of Provisional Admission";
                    string fileName = HttpContext.Current.Server.MapPath("~/EmailTemplates/OfferOfAdmission.html");
                    string mailBody = System.IO.File.ReadAllText(fileName);
                    mailBody = mailBody.Replace("#Name#", emailName);
                    msg.HtmlBody = mailBody;
                    msg.TextBody = mailBody;
                }
                else
                {
                    msg.HtmlBody = "";
                    msg.TextBody = "";
                }


                PostmarkClient client = new PostmarkClient(_emailSettings.ServerToken);
                IAsyncResult result = client.BeginSendMessage(msg);
                if (result.AsyncWaitHandle.WaitOne())
                {
                    PostmarkResponse response = client.EndSendMessage(result);
                    //return true;
                }
            }
            catch (TypeInitializationException)
            {
                
            }
            catch (Exception ex)
            {
                //throw new System.ArgumentException(ex.Message);
            }
        }

        public void SendEmailAdmission(string email, string emailName, string code, int emailType, string role, string schoolName,string session, string programCode, string courseName)
        {
            try
            {
                string encryptedEmail = HttpContext.Current.Server.UrlEncode(_encryptionService.EncryptUserName(email));
                PostmarkMessage msg = new PostmarkMessage();
                msg.From = _emailSettings.UserName;
                msg.To = email;

              if (emailType == Convert.ToInt32(EmailType.AdmissionOffered))
                {

                    msg.Subject = "Offer of Provisional Admission";
                    string fileName = HttpContext.Current.Server.MapPath("~/EmailTemplates/OfferOfAdmission.html");
                    string mailBody = System.IO.File.ReadAllText(fileName);
                    mailBody = mailBody.Replace("#Name#", emailName);
                    mailBody = mailBody.Replace("#Session", session);
                    mailBody = mailBody.Replace("#Program", programCode);
                    mailBody = mailBody.Replace("#Course", courseName);
                    mailBody = mailBody.Replace("#SchoolName", schoolName);
                    msg.HtmlBody = mailBody;
                    msg.TextBody = mailBody;
                }
                else
                {
                    msg.HtmlBody = "";
                    msg.TextBody = "";
                }


                PostmarkClient client = new PostmarkClient(_emailSettings.ServerToken);
                IAsyncResult result = client.BeginSendMessage(msg);
                if (result.AsyncWaitHandle.WaitOne())
                {
                    PostmarkResponse response = client.EndSendMessage(result);
                    //return true;
                }
            }
            catch (TypeInitializationException)
            {

            }
            catch (Exception ex)
            {
                //throw new System.ArgumentException(ex.Message);
            }
        }

        //public string NewAcountMail(string fullName, string verificationUrl, string accountLogo, string userName, string email, string password, string accountName, string accountUrl, string contactEmail)
        //{
        //    throw new NotImplementedException();
        //}

        //public void SendEmail(string subject, string body, string toAddress, string toName, IEnumerable<string> bcc = null, IEnumerable<string> cc = null)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
