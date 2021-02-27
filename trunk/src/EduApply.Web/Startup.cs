using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EduApply.Web.Startup))]
namespace EduApply.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
