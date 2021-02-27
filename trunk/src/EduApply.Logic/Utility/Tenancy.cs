using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;

namespace EduApply.Logic.Utility
{
    public class Tenancy
    {
        public string Name { get; set; }
        public string Pmb { get; set; }
        public string HelpPhrase { get; set; }
        public string SchoolEmail { get; set; }
        public string EtranzactTerminalId { get; set; }
        public  string Host
        {
            get
            {
                string hostUrl = "";
                try
                {

                    HttpContext cntx = HttpContext.Current;
                    if (cntx != null)
                        hostUrl = cntx.Request.Headers["Host"];
                    else
                    {
                        hostUrl = HttpRuntime.Cache["_Firsthost" + HttpRuntime.AppDomainAppId].ToString();
                    }
                }
                catch
                {
                    //hostUrl = HttpRuntime.Cache["_Firsthost" + HttpRuntime.AppDomainAppId].ToString();
                }

              
                return hostUrl;
            }
            set
            {
                
            }
        }
        public string ConnectionString { get; set; }
        public string Code { get; set; }
  

        //public static Tenancy CurrentTenant
        //{
        //    get
        //    {
        //        //string host = HttpContext.Current.Request.Headers["Host"];
        //        Tenancy tenant = new Tenancy();
        //        XmlElement config = tenant.ConfigurationFile;


        //        //Get the nodes where the hostname matches
        //        XmlNodeList nodes = config.GetElementsByTagName("Tenant");

        //        foreach (XmlNode n in nodes)
        //        {
        //            tenant = null;


        //            string hostname = n["HostName"].InnerText;
        //            if (!string.IsNullOrEmpty(hostname))
        //            {
        //                if (hostname.Equals(Host, StringComparison.InvariantCultureIgnoreCase))
        //                {
        //                    tenant = new Tenancy();


        //                    //tenant.Host = Host;
        //                    tenant.ConnectionString = n["ConnectionString"].InnerText;
        //                    tenant.Name = n["Name"].InnerText;
        //                    tenant.Code = n["Code"].InnerText;

        //                    tenant.SchoolEmail = n["SchoolEmail"].InnerText;
        //                    tenant.EtranzactTerminalId = n["EtranzactTerminalId"].InnerText;

        //                    //tenant.Logo = n["Logo"].InnerText;
        //                    //tenant.PrimaryColor = n["PrimaryColor"].InnerText;
        //                    //tenant.SecondaryColor = n["SecondaryColor"].InnerText;
        //                    //tenant.SubName = n["SubName"].InnerText;

        //                    break;
        //                }
        //            }



        //        }
        //        //SchoolCodes = codes;
        //        //tenant = new Tenancy();
        //        if (tenant == null)
        //            throw new ApplicationException("Tenancy not Configured for host name: " + Host);
        //        return tenant;
        //    }
        //}

        public XmlElement ConfigurationFile
        {

            get
            {
                XmlElement msg = null;
                XmlDocument doc = new XmlDocument();
                string filePath = "";
                //this is returning an error on web even log, you must find a solution for this
                if (HttpContext.Current != null)
                    filePath = HttpContext.Current.Server.MapPath("~/bin/Tenancy.Config.xml");
                else
                    filePath = HttpRuntime.BinDirectory + @"\Tenancy.Config.xml";
                FileInfo fInfo = new FileInfo(filePath);
                if (fInfo.Exists)
                {
                    doc.Load(filePath);
                    msg = doc.DocumentElement;
                }
                else
                    throw new ApplicationException("The Tenancy Configuration File " + filePath + " does not exist.");
                //msg = "The Tenancy Configuration File does not exist.";

                return msg;

            }

        }


    }

}
