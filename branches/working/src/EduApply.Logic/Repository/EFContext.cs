using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduApply.Data.Entities;
using EduApply.Logic.Interfaces;
using EduApply.Logic.Service;
using EduApply.Logic.Utility;


namespace EduApply.Logic.Repository
{
    public class EFContextConfiguration : System.Data.Entity.Migrations.DbMigrationsConfiguration<EFContext>
    {
        public EFContextConfiguration()
        {
            this.AutomaticMigrationsEnabled = true;
        }
    }

    public class EFContextFactory : IDbContextFactory<EFContext>
    {
        public EFContext Create()
        {
            //needed to get migrations and migrate.exe to work....
            var _tenancy = EngineContext.Resolve<Tenancy>();



            return new EFContext(_tenancy);
        }
    }

    public class EFContext : DbContext, IDbContext
    {
        //public EFContext(string connectionString)
        //    : base(connectionString)
        //{
        //    Database.SetInitializer<EFContext>(null);
        //}
        public EFContext(Tenancy tenancy)
        : base(tenancy.ConnectionString)
    {
            
       // Database.SetInitializer(new MigrateDatabaseToLatestVersion<EFContext, EFContextConfiguration>());
    }
        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }
        //public new IDbSet<TEntity> Entry<TEntity>() where TEntity : class
        //{
        //    return base.Entry<TEntity>();
        //}

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<TemplatesInAppForms>();
            modelBuilder.Entity<Country>();
            modelBuilder.Entity<State>();
            modelBuilder.Entity<LocalGovernmentArea>();
            modelBuilder.Entity<ApplicationForm>();
            modelBuilder.Entity<FormTemplate>();
            modelBuilder.Entity<EncryptionKeys>();
            modelBuilder.Entity<PersonalInformation>();
            modelBuilder.Entity<AdminBiodata>();
            modelBuilder.Entity<OLevelGrade>();
            modelBuilder.Entity<OLevelSubject>();
            modelBuilder.Entity<Faculty>();
            modelBuilder.Entity<Department>();
            modelBuilder.Entity<Course>();
            modelBuilder.Entity<Session>();
            modelBuilder.Entity<Program>();
            modelBuilder.Entity<ProgramCourse>();
            modelBuilder.Entity<WorkFlow>();
            modelBuilder.Entity<ApplicationFormWorkFlow>();
            modelBuilder.Entity<EventLog>();
            modelBuilder.Entity<Application>();
            modelBuilder.Entity<FormTemplateSettings>();
            modelBuilder.Entity<ClassOfDegree>();
            modelBuilder.Entity<JambBreakDown>();
            modelBuilder.Entity<FormResult>();
            modelBuilder.Entity<SessionResult>();
            modelBuilder.Entity<AuditSection>();
            modelBuilder.Entity<AuditAction>();
            modelBuilder.Entity<AuditTrail>();
            modelBuilder.Entity<EducationalDetails>();
            modelBuilder.Entity<WorkExperience>();
            modelBuilder.Entity<Reference>();
            modelBuilder.Entity<Picture>();
            modelBuilder.Entity<Certificate>();
            modelBuilder.Entity<OLevelDetail>();
            modelBuilder.Entity<OLevelResult>();
            modelBuilder.Entity<CertificateType>();
            modelBuilder.Entity<ExamType>();
            modelBuilder.Entity<ManualJambBreakDown>();
            modelBuilder.Entity<AppFormProgramCourse>();
            modelBuilder.Entity<FeeRequestPayment>();
            modelBuilder.Entity<ApiLog>();
            modelBuilder.Entity<AttemptedPayment>();
            modelBuilder.Entity<Venues>();
            modelBuilder.Entity<VenueMappings>();
            modelBuilder.Entity<FormCategory>();
            modelBuilder.Entity<AdmissionList>();
            modelBuilder.Entity<NonApplicantAdmittedList>();
            modelBuilder.Entity<MappedForm>();
            modelBuilder.Entity<ExamVenue>();
            modelBuilder.Entity<ApplicantsProgramCourse>();
            modelBuilder.Entity<Gateway>();
            modelBuilder.Entity<AppFormGateway>();
            modelBuilder.Entity<Split>();
            modelBuilder.Entity<Bank>();
            modelBuilder.Entity<ApplicationNoFormat>();
            modelBuilder.Entity<AppFormClassOfDegree>();
            modelBuilder.Entity<AdmissionLetterFormat>();
            modelBuilder.Entity<JambBiodata>();
            base.OnModelCreating(modelBuilder);
        }

    }


}
