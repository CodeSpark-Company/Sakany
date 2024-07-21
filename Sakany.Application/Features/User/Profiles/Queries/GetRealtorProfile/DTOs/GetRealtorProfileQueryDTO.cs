﻿namespace Sakany.Application.Features.User.Profiles.Queries.GetRealtorProfile.DTOs
{
    public class GetRealtorProfileQueryDTO
    {
        #region Properties

        public string? Email { get; set; }
        public string? Username { get; set; }

        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public string? PhoneNumber { get; set; }
        public int? Age { get; set; }

        public string? Bio { get; set; }
        public string? ImageUrl { get; set; }
        public string? CivilId { get; set; }
        public string? RealEstateContract { get; set; }

        public DateTime CreatedAt { get; set; }

        #endregion Properties
    }
}