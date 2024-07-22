using MediatR;
using Microsoft.AspNetCore.Http;
using Sakany.Application.Features.User.Profiles.Commands.UpdateSuperAdminProfile.DTOs;
using Sakany.Shared.Responses;

namespace Sakany.Application.Features.User.Profiles.Commands.UpdateSuperAdminProfile.Requests
{
    public class UpdateSuperAdminProfileCommandRequest : IRequest<Response<UpdateSuperAdminProfileCommandDTO>>
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

        #endregion Properties
    }
}