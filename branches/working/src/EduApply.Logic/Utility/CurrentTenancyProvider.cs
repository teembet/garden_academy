using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace EduApply.Logic.Utility
{
    public class CurrentTenancyProvider : Tenancy
    {
        public CurrentTenancyProvider()
        {
            GetTenancyForCurrentContext();
        }


        private void GetTenancyForCurrentContext()
        {
           
            XmlElement config = this.ConfigurationFile;


            //Get the nodes where the hostname matches
            XmlNodeList nodes = config.GetElementsByTagName("Tenant");

            foreach (XmlNode n in nodes)
            {
               


                string hostname = n["HostName"].InnerText;
                if (!string.IsNullOrEmpty(hostname))
                {
                    if (hostname.Equals(Host, StringComparison.InvariantCultureIgnoreCase))
                    {
                       


                        //this.Host = Host;
                        this.ConnectionString = n["ConnectionString"].InnerText;
                        this.Name = n["Name"].InnerText;
                        this.Code = n["Code"].InnerText;
                        this.Pmb = n["Pmb"].InnerText;
                        this.HelpPhrase = n["HelpPhrase"].InnerText;
                        this.SchoolEmail = n["SchoolEmail"].InnerText;
                        //this.DefaultColor = n["DefaultColor"].InnerText;
                        this.Host = hostname;
                        this.EtranzactTerminalId = n["EtranzactTerminalId"].InnerText;
                        this.SplashersAdminEamil = n["SplashersAdminEamil"].InnerText;
                        //this.PrimaryColor = n["PrimaryColor"].InnerText;
                        //this.SecondaryColor = n["SecondaryColor"].InnerText;
                        //this.SubName = n["SubName"].InnerText;

                        break;
                    }
                }



            }
         
        }
    }
}
