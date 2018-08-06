using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduApply.Logic.Interfaces
{
  public  interface IEmailSettings
    {
        int Port { get; set; }
        bool EnableSSL { get; set; }
        string HostName { get; set; }
        string UserName { get; set; }
        string ServerToken { get; set; }
        string EmailName { get; set; }
    }
}
