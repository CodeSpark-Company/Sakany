using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sakany.Domain.Enumerations.Users;
using Sakany.Domain.IdentityEntities;

namespace Sakany.Persistence.EntityConfiguration.Users
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            #region Config Table Name

            builder.ToTable("Users", "User");

            #endregion Config Table Name

            #region Config Properties

            builder.Property(user => user.FirstName)
                   .HasMaxLength(100)
                   .IsRequired(false);

            builder.Property(user => user.LastName)
                   .HasMaxLength(100)
                   .IsRequired(false);

            builder.Property(user => user.BirthDate)
                   .IsRequired(false);

            builder.Property(user => user.Age)
                   .HasComputedColumnSql("CASE WHEN [BirthDate] IS NULL THEN NULL ELSE DATEDIFF(YEAR, [BirthDate], GETDATE()) - CASE WHEN DATEADD(YEAR, DATEDIFF(YEAR, [BirthDate], GETDATE()), [BirthDate]) > GETDATE() THEN 1 ELSE 0 END END");

            builder.Property(user => user.Status)
                   .HasDefaultValue(UserStatus.InActive.ToString())
                   .IsRequired();

            builder.Property(user => user.ResetPasswordOTP)
                   .IsRequired(false);

            builder.Property(user => user.ResetPasswordOTPExpiresAt)
                   .IsRequired(false);

            builder.Property(user => user.CreatedAt)
                   .HasDefaultValue(DateTime.UtcNow)
                   .IsRequired();

            builder.Property(user => user.ModifiedAt)
                   .HasDefaultValue(DateTime.UtcNow)
                   .IsRequired();

            builder.Property(user => user.DeletedAt)
                   .IsRequired(false);

            #endregion Config Properties
        }
    }
}