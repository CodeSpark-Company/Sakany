using Microsoft.AspNetCore.Identity;
using Sakany.Domain.Entities.Security;
using Sakany.Domain.Entities.Users;

namespace Sakany.Domain.IdentityEntities
{
    public class ApplicationUser : IdentityUser
    {
        #region Properties

        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public DateOnly? BirthDate { get; set; }
        public int? Age { get; set; }

        public string? Status { get; set; }

        public string? ResetPasswordOTP { get; set; }
        public DateTime? ResetPasswordOTPExpiresAt { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        #endregion Properties

        #region Relationships

        public virtual UserProfile? UserProfile { get; set; }
        public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();

        #endregion Relationships
    }
}