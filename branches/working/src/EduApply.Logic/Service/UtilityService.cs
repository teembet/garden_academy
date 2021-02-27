using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using EduApply.Logic.Interfaces;

namespace EduApply.Logic.Service
{
    public class UtilityService : IUtilityService
    {
        public readonly HttpContextBase _http;

        public UtilityService(HttpContextBase http)
        {
            this._http = http;
        }
        public  string GetIp()
        {
            try
            {
                string ip = "";
                ip = !string.IsNullOrWhiteSpace(_http.Request.ServerVariables["REMOTE_ADDR"])
                    ? _http.Request.ServerVariables["REMOTE_ADDR"]
                    : _http.Request.ServerVariables["REMOTE_HOST"];

                if (ip == "::1")
                {
                    ip = _http.Request.ServerVariables["LOCAL_ADDR"];
                }

                return ip;
            }
            catch
            {
                return "";
            }



        }
    }
}
