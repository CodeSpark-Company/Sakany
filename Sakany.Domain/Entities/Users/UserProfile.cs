using Sakany.Domain.Common.Abstracts;
using Sakany.Domain.IdentityEntities;

namespace Sakany.Domain.Entities.Users
{
    public class UserProfile : BaseEntity
    {
        #region Properties

        public string? Bio { get; set; }
        public string? ImageUrl { get; set; }
        public string? CivilId { get; set; }

        #endregion Properties

        #region Relationships

        public string? UserId { get; set; }
        public virtual ApplicationUser? User { get; set; }

        #endregion Relationships
    }
}