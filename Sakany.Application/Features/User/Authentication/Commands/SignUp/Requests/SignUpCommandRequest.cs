using MediatR;
using Sakany.Application.Features.User.Authentication.Commands.SignUp.DTOs;
using Sakany.Shared.Responses;

namespace Sakany.Application.Features.User.Authentication.Commands.SignUp.Requests
{
    public class SignUpCommandRequest : IRequest<Response<SignUpCommandDTO>>
    {
        #region Properties

        public string Role { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string ConfirmPassword { get; set; } = default!;

        #endregion Properties
    }
}