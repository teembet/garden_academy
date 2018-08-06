using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;

namespace EduApply.Logic.Utility
{
    public class CurrentThemeProvider : Theme
    {
        public CurrentThemeProvider()
        {
            GetThemeForCurrentContext();
        }


        private void GetThemeForCurrentContext()
        {
           
            var engine = HttpContext.Current;// EngineResolver.Resolve<HttpContextBase>();

            var _url = engine.Request.Url.Authority;
          
            var xmlFile = this.ConfigurationFile;

            if (xmlFile != null)
            {
                try
                {
                    var root = xmlFile.SelectNodes("Theme");

                    foreach (XmlNode node in root)
                    {
                        var url = node.Attributes["host"].Value;
                        if (url.ToLower() == _url.ToLower())
                        {
                            
                            var schoolName = node.Attributes["name"].Value;
                            this.SchoolName = schoolName;

                            var logo = node.Attributes["logo"].Value;
                            this.Logo = logo;
                            var headerImage = node.Attributes["headerImage"].Value;
                            this.HeaderImage = headerImage;

                            var _details = node.ChildNodes;
                            foreach (XmlNode n in _details)
                            {
                                var _name = n.Name;
                                if (_name.ToLower() == "backgroundcolor")
                                    this.BackgroundColor = n.InnerText;
                                else if (_name.ToLower() == "backgroundcolor2")
                                    this.BackgroundColor2 = n.InnerText;
                                else if (_name.ToLower() == "sidebarbackgroundcolor")
                                    this.SidebarBackgroundColor = n.InnerText;
                                else if (_name.ToLower() == "sidebarlink")
                                    this.SidebarLink = n.InnerText;
                                else if (_name.ToLower() == "outerlogo")
                                    this.OuterLogo = n.InnerText;


                            }


                        }
                        else
                            continue;



                    }
                }
                catch (Exception ex)
                {
                    throw new ApplicationException("The Theme Configuration File format is not valid " + ex.Message);

                }
            }

          

         
        }
    }
}
