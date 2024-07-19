using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sakany.Domain.Entities.Users;

namespace Sakany.Persistence.EntityConfiguration.Users
{
    public class SuperAdminProfileConfiguration : IEntityTypeConfiguration<SuperAdminProfile>
    {
        public void Configure(EntityTypeBuilder<SuperAdminProfile> builder)
        {
            #region Config Table Name

            builder.ToTable("SuperAdminProfiles", "User");

            #endregion Config Table Name

            #region Config Properties

            builder.Property(profile => profile.CivilId)
                   .IsRequired(false);

            #endregion Config Properties
        }
    }
}