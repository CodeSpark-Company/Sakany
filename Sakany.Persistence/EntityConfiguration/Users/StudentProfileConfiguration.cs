using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sakany.Domain.Entities.Users;

namespace Sakany.Persistence.EntityConfiguration.Users
{
    public class StudentProfileConfiguration : IEntityTypeConfiguration<StudentProfile>
    {
        public void Configure(EntityTypeBuilder<StudentProfile> builder)
        {
            #region Config Table Name

            builder.ToTable("StudentProfiles", "User");

            #endregion Config Table Name

            #region Config Properties

            builder.Property(profile => profile.CivilId)
                   .IsRequired(false);

            builder.Property(profile => profile.UniversityId)
                   .IsRequired(false);

            #endregion Config Properties
        }
    }
}