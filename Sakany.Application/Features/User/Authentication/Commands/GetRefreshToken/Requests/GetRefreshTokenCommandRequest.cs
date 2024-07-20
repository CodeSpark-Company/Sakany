using MediatR;
using Sakany.Application.Features.User.Authentication.Commands.GetRefreshToken.DTOs;
using Sakany.Shared.Responses;

namespace Sakany.Application.Features.User.Authentication.Commands.GetRefreshToken.Requests
{
    public class GetRefreshTokenCommandRequest : IRequest<Response<GetRefreshTokenCommandDTO>>
    {
        #region Properties

        public string RefreshToken { get; set; } = default!;

        #endregion Properties
    }
}