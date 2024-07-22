using MediatR;
using Microsoft.AspNetCore.Http;
using Sakany.Application.Features.User.Profiles.Commands.UpdateStudentProfile.DTOs;
using Sakany.Shared.Responses;

namespace Sakany.Application.Features.User.Profiles.Commands.UpdateStudentProfile.Requests
{
    public class UpdateStudentProfileCommandRequest : IRequest<Response<UpdateStudentProfileCommandDTO>>
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
        public IFormFile? UniversityId { get; set; }

        #endregion Properties
    }
}