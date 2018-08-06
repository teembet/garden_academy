using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace EduApply.Logic.Service
{
    public class UtilityService
    {
        public static string GetIp(HttpContext context)
        {
            string ip = "";
            ip = !string.IsNullOrWhiteSpace(context.Request.ServerVariables["REMOTE_ADDR"]) ? context.Request.ServerVariables["REMOTE_ADDR"] : context.Request.ServerVariables["REMOTE_HOST"];

            if (ip == "::1")
            {
                ip = context.Request.ServerVariables["LOCAL_ADDR"];
            }

            return ip;


        }
    }
}
