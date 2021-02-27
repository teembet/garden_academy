using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduApply.Logic.Interfaces;

namespace EduApply.Logic.Repository
{
    public class EmailSettings : IEmailSettings
    {
        public int Port
        {
            get
            {
                return 587;
            }
            set
            {
                throw new NotImplementedException();
            }
        }
        
        public bool EnableSSL
        {
            get
            {
                return true;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string HostName
        {
            get
            {
                return "smtp.gmail.com";
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string UserName
        {
            get
            {
                return "eduapply@schoolname.com";
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string ServerToken
        {
            get
            {
                return "5d148c39-9de9-4db2-92e3-6f1d7675d02a";
            }
            set
            {
                throw new NotImplementedException();
            }
        }


        public string EmailName
        {
            get
            {
                return "Edu Apply";
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
