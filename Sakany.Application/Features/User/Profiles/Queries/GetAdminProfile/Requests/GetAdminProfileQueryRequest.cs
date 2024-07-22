using MediatR;
using Sakany.Application.Features.User.Profiles.Queries.GetAdminProfile.DTOs;
using Sakany.Shared.Responses;

namespace Sakany.Application.Features.User.Profiles.Queries.GetAdminProfile.Requests
{
    public class GetAdminProfileQueryRequest : IRequest<Response<GetAdminProfileQueryDTO>>
    {
    }
}