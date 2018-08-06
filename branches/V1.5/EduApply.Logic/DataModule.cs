using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Autofac;
using Autofac.Builder;
using EduApply.Logic.Interfaces;
using EduApply.Logic.Repository;
using EduApply.Logic.Service;
using Autofac.Core;
using EduApply.Logic.Utility;
using Module = Autofac.Module;

namespace EduApply.Logic
{
    public class DataModule : Module
    {
        private string connString;

        //public DataModule(string _conString)
        //{
        //    this.connString = _conString;
        //}

        protected override void Load(ContainerBuilder builder)
        {
           //builder.Register(c => new EFContext(this.connString)).As<IDbContext>().InstancePerLifetimeScope();
            builder.RegisterType<EFContext>().As<IDbContext>().InstancePerLifetimeScope();

               //HttpContext.Current != null ?
               // (new HttpContextWrapper(HttpContext.Current) as HttpContextBase) :
               // (new FakeHttpContext("~/") as HttpContextBase))
               // .As<HttpContextBase>()
               // .InstancePerHttpRequest();

            if (HttpContext.Current != null)
            {
                       builder.Register(c => 
                           (new HttpContextWrapper(HttpContext.Current) as HttpContextBase)).As<HttpContextBase>()
                 .InstancePerLifetimeScope();
            }

            builder.Register(c => c.Resolve<HttpContextBase>().Request)
                .As<HttpRequestBase>().InstancePerLifetimeScope();
            builder.RegisterSource(new SettingsSource());
            builder.RegisterType<CurrentTenancyProvider>().As<Tenancy>();
            builder.RegisterType<CurrentThemeProvider>().As<Theme>();



            builder.RegisterType<SqlRepository>().As<IRepository>().InstancePerLifetimeScope();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
            builder.RegisterType<EmailSender>().As<IEmailSender>().InstancePerLifetimeScope();
            builder.RegisterType<EmailSettings>().As<IEmailSettings>().InstancePerLifetimeScope();
            builder.RegisterType<EncryptionService>().As<IEncryptionService>().InstancePerLifetimeScope();
            builder.RegisterType<LocationRepository>().As<ILocationRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ApplicationFormRepository>().As<IApplicationFormRepository>().InstancePerLifetimeScope();
            builder.RegisterType<RegistrationService>().As<IRegistrationService>().InstancePerLifetimeScope();
            builder.RegisterType<ConfigurationService>().As<IConfigurationService>().InstancePerLifetimeScope();
            builder.RegisterType<EventLogRepository>().As<IEventLogRepository>().InstancePerLifetimeScope();
            builder.RegisterType<AuditTrailRepository>().As<IAuditTrailRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ApiLogRepository>().As<IApiLogRepository>().InstancePerLifetimeScope();
            builder.RegisterType<SearchRepository>().As<ISearchRepository>().InstancePerLifetimeScope();
            builder.RegisterType<PrintService>().As<IPrintService>().InstancePerLifetimeScope();
            builder.RegisterType<StateManager>().As<IStateManager>().InstancePerLifetimeScope();

            base.Load(builder);
        }
    }

    public class SettingsSource : IRegistrationSource
    {
        static readonly MethodInfo BuildMethod = typeof(SettingsSource).GetMethod(
            "BuildRegistration",
            BindingFlags.Static | BindingFlags.NonPublic);

        public IEnumerable<IComponentRegistration> RegistrationsFor(
               Autofac.Core.Service service,
                Func<Autofac.Core.Service, IEnumerable<IComponentRegistration>> registrations)
        {
            var ts = service as TypedService;
            if (ts != null && typeof(IDbContext).IsAssignableFrom(ts.ServiceType))
            {
                var buildMethod = BuildMethod.MakeGenericMethod(ts.ServiceType);
                yield return (IComponentRegistration)buildMethod.Invoke(null, null);
            }
        }

        static IComponentRegistration BuildRegistration<TSettings>() where TSettings : IDbContext
        {
            //HTTP_HOST


            return RegistrationBuilder
                .ForDelegate((c, p) =>
                {


                    var _tenancy = EngineContext.Resolve<Tenancy>();

                    return new EFContext(_tenancy);
                    //nhibernate session must be static . . . we need test this line for multi tenancy
                  //  return c.ResolveNamed<IDictionary<string, ISessionFactory>>("SessionDictionary")[_currentUrl];
                })
                .SingleInstance()
                    // .InstancePerLifetimeScope()
                //.SingleInstance() //this should be appropriate, but the multi-tenancy effect nko??

                .CreateRegistration();


        }

        public bool IsAdapterForIndividualComponents { get { return false; } }
    }
}
