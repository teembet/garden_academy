using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduApply.Logic.Interfaces
{
    public interface IEmailSender
    {
        void SendEmail(string email, string emailName, string code, int emailType, string role);

        void SendEmailAdmission(string email, string emailName, string code, int emailType, string role, string schoolName, string session, string programCode, string courseName);

        // string NewAcountMail(string fullName, string verificationUrl, string accountLogo, string userName, string email, string password, string accountName, string accountUrl, string contactEmail);

        //void SendEmail(string subject, string body, string toAddress, string toName, IEnumerable<string> bcc = null, IEnumerable<string> cc = null);

    }
}
