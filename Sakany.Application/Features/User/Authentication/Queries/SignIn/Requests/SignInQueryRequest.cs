using MediatR;
using Sakany.Application.Features.User.Authentication.Queries.SignIn.DTOs;
using Sakany.Shared.Responses;

namespace Sakany.Application.Features.User.Authentication.Queries.SignIn.Requests
{
    public class SignInQueryRequest : IRequest<Response<SignInQueryDTO>>
    {
        #region Properties

        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;

        #endregion Properties
    }
}