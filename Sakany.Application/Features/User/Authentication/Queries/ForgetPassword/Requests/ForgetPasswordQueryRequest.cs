using Sakany.Application.Features.User.Authentication.Queries.ForgetPassword.DTOs;
using Sakany.Shared.Responses;
using MediatR;

namespace Sakany.Application.Features.User.Authentication.Queries.ForgetPassword.Requests
{
    public class ForgetPasswordQueryRequest : IRequest<Response<ForgetPasswordQueryDTO>>
    {
        #region Properties

        public string Email { get; set; } = default!;

        #endregion Properties
    }
}