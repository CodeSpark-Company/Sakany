using MediatR;
using Sakany.Application.Features.User.Authentication.Queries.GetAccessToken.DTOs;
using Sakany.Shared.Responses;

namespace Sakany.Application.Features.User.Authentication.Queries.GetAccessToken.Requests
{
    public class GetAccessTokenQueryRequest : IRequest<Response<GetAccessTokenQueryDTO>>
    {
        #region Properties

        public string RefreshToken { get; set; } = default!;

        #endregion Properties
    }
}