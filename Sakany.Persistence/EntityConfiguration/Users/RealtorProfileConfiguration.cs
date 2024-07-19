using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sakany.Domain.Entities.Users;

namespace Sakany.Persistence.EntityConfiguration.Users
{
    public class RealtorProfileConfiguration : IEntityTypeConfiguration<RealtorProfile>
    {
        public void Configure(EntityTypeBuilder<RealtorProfile> builder)
        {
            #region Config Table Name

            builder.ToTable("RealtorProfiles", "User");

            #endregion Config Table Name

            #region Config Properties

            builder.Property(profile => profile.CivilId)
                   .IsRequired(false);

            builder.Property(profile => profile.RealEstateContract)
                   .IsRequired(false);

            #endregion Config Properties
        }
    }
}