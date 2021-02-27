using System;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using EduApply.Logic.Service;
using EduApply.Logic.Utility;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace EduApply.Web.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public bool IsActive { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole() : base() { }
        public ApplicationRole(string name) : base(name) { }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(Tenancy tenancy)
            : base(tenancy.ConnectionString, throwIfV1Schema: false)
        {
        }

        static ApplicationDbContext()
        {
            Database.SetInitializer<ApplicationDbContext>(new EduApplyDbInit());
        }

        public static ApplicationDbContext Create()
        {
            var _tenancy = EngineContext.Resolve<Tenancy>();
            return new ApplicationDbContext(_tenancy);
        }
    }

    public class EduApplyDbInit : NullDatabaseInitializer<ApplicationDbContext>
    {
      
    }
}