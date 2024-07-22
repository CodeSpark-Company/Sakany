using MediatR;
using Microsoft.AspNetCore.Http;
using Sakany.Application.Features.User.Profiles.Commands.UpdateRealtorProfile.DTOs;
using Sakany.Shared.Responses;

namespace Sakany.Application.Features.User.Profiles.Commands.UpdateRealtorProfile.Requests
{
    public class UpdateRealtorProfileCommandRequest : IRequest<Response<UpdateRealtorProfileCommandDTO>>
    {
        #region Properties

        public string? UserName { get; set; }

        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public string? PhoneNumber { get; set; }
        public DateOnly? BirthDate { get; set; }

        public string? Bio { get; set; }
        public IFormFile? Image { get; set; }
        public IFormFile? CivilId { get; set; }
        public IFormFile? RealEstateContract { get; set; }

        #endregion Properties
    }
}