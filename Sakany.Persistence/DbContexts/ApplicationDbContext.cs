using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Sakany.Domain.Entities.Users;
using Sakany.Domain.IdentityEntities;
using System.Reflection;

namespace Sakany.Persistence.DbContexts
{
    public class SakanyDbContext : IdentityDbContext<ApplicationUser>
    {
        #region Constructors

        public SakanyDbContext(DbContextOptions<SakanyDbContext> options) : base(options)
        {
        }

        #endregion Constructors

        #region DbSets

        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<StudentProfile> StudentProfiles { get; set; }
        public DbSet<RealtorProfile> RealtorProfiles { get; set; }
        public DbSet<SuperAdminProfile> SuperAdminProfiles { get; set; }
        public DbSet<AdminProfile> AdminProfiles { get; set; }

        #endregion DbSets

        #region Methods

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            #region Identity Configuration

            // Config Identity Tables Names
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles", "Security");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims", "Security");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins", "Security");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims", "Security");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens", "Security");

            #endregion Identity Configuration

            #region Assemblies Configuration

            // Apply Entities Configurations
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            #endregion Assemblies Configuration
        }

        #endregion Methods
    }
}