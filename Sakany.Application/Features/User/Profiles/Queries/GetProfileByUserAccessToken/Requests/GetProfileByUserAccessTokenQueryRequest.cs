using Sakany.Application.Features.User.Profiles.Queries.GetProfileByUserAccessToken.DTOs;
using Sakany.Shared.Responses;
using MediatR;

namespace Sakany.Application.Features.User.Profiles.Queries.GetProfileByUserAccessToken.Requests
{
    public class GetProfileByUserAccessTokenQueryRequest : IRequest<Response<GetProfileByUserAccessTokenQueryDTO>>
    {
    }
}