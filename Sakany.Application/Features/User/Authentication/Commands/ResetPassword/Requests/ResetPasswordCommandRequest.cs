using Sakany.Application.Features.User.Authentication.Commands.ResetPassword.DTOs;
using Sakany.Shared.Responses;
using MediatR;

namespace Sakany.Application.Features.User.Authentication.Commands.ResetPassword.Requests
{
    public class ResetPasswordCommandRequest : IRequest<Response<ResetPasswordCommandDTO>>
    {
        #region Properties

        public string Email { get; set; } = default!;
        public string OTP { get; set; } = default!;
        public string NewPassword { get; set; } = default!;
        public string ConfirmNewPassword { get; set; } = default!;

        #endregion Properties
    }
}