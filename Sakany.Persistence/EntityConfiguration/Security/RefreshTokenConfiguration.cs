﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sakany.Domain.Entities.Security;

namespace Sakany.Persistence.EntityConfiguration.Security
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            #region Config Table Name

            builder.ToTable("RefreshTokens", "Security");

            #endregion Config Table Name

            #region Config Properties

            builder.Property(token => token.Token)
                   .IsRequired();

            builder.Property(token => token.ExpiresAt)
                   .IsRequired();

            builder.Property(token => token.IsExpired)
                .HasComputedColumnSql("CASE WHEN [ExpiresAt] <= GETUTCDATE() THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END");

            builder.Property(token => token.RevokedAt)
                   .IsRequired(false);

            builder.Property(token => token.IsActive)
                   .HasComputedColumnSql("CASE WHEN [RevokedAt] IS NULL AND [ExpiresAt] > GETUTCDATE() THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END");

            builder.Property(token => token.CreatedAt)
                   .HasDefaultValue(DateTime.UtcNow)
                   .IsRequired();

            builder.Property(token => token.ModifiedAt)
                   .HasDefaultValue(DateTime.UtcNow)
                   .IsRequired();

            builder.Property(token => token.DeletedAt)
                   .IsRequired(false);

            #endregion Config Properties

            #region Config Relationships

            builder.HasOne(token => token.User)
                   .WithMany(user => user.RefreshTokens)
                   .HasForeignKey(token => token.UserId)
                   .IsRequired();

            #endregion Config Relationships
        }
    }
}