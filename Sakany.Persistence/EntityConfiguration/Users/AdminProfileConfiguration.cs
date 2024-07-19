using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sakany.Domain.Entities.Users;

namespace Sakany.Persistence.EntityConfiguration.Users
{
    public class AdminProfileConfiguration : IEntityTypeConfiguration<AdminProfile>
    {
        public void Configure(EntityTypeBuilder<AdminProfile> builder)
        {
            #region Config Table Name

            builder.ToTable("AdminProfiles", "User");

            #endregion Config Table Name

            #region Config Properties

            builder.Property(profile => profile.CivilId)
                   .IsRequired(false);

            #endregion Config Properties
        }
    }
}