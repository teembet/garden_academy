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

    public class Theme
    {
        //private readonly HttpContextBase _http;
        //public Theme(HttpContextBase http)
        //{
        //    this._http = http;
        //}

        public string OuterLogo { get; set; }
        public string Logo { get; set; }
        public string SchoolName { get; set; }
        public string BackgroundColor { get; set; }
        public string BackgroundColor2 { get; set; }
        public string SidebarBackgroundColor { get; set; }
        public string SidebarLink { get; set; }
        public string HeaderImage { get; set; }
        public static Theme Current
        {
            get
            {
                Theme result = null;
                var engine = HttpContext.Current;// EngineResolver.Resolve<HttpContextBase>();

                var _url = engine.Request.Url.Authority;
                //var _cacheManager = EngineResolver.Resolve<ICacheManager>();
                //return _cacheManager.Get("Theme_For_Host_" + _url, () =>
                //{
                    var _theme = new Theme();

                    var xmlFile = _theme.ConfigurationFile;

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
                                    result = _theme;
                                    var schoolName = node.Attributes["name"].Value;
                                    result.SchoolName = schoolName;

                                    var logo = node.Attributes["logo"].Value;
                                    result.Logo = logo;
                                    var headerImage = node.Attributes["headerImage"].Value;
                                    result.HeaderImage = headerImage;
                                    

                                    var _details = node.ChildNodes;
                                    foreach (XmlNode n in _details)
                                    {
                                        var _name = n.Name;
                                        if (_name.ToLower() == "backgroundcolor")
                                            result.BackgroundColor = n.InnerText;
                                        else if (_name.ToLower() == "backgroundcolor2")
                                            result.BackgroundColor2 = n.InnerText;
                                        else if (_name.ToLower() == "sidebarbackgroundcolor")
                                            result.SidebarBackgroundColor = n.InnerText;
                                        else if (_name.ToLower() == "sidebarlink")
                                            result.SidebarLink = n.InnerText;
                                        else if (_name.ToLower() == "outerlogo")
                                            result.OuterLogo = n.InnerText;



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

                    return result;
               // });

            }

        }

        public XmlElement ConfigurationFile
        {

            get
            {
                XmlElement msg = null;
                XmlDocument doc = new XmlDocument();
                string filePath = "";
                //this is returning an error on web even log, you must find a solution for this
                if (HttpContext.Current != null)
                    filePath = HttpContext.Current.Server.MapPath("~/bin/Theme.Config.xml");
                else
                    filePath = HttpRuntime.BinDirectory + @"\Theme.Config.xml";
                FileInfo fInfo = new FileInfo(filePath);
                if (fInfo.Exists)
                {
                    doc.Load(filePath);
                    msg = doc.DocumentElement;
                }
                else
                    throw new ApplicationException("The Theme Configuration File " + filePath + " does not exist.");
                //msg = "The Tenancy Configuration File does not exist.";

                return msg;

            }

        }





    }

}
