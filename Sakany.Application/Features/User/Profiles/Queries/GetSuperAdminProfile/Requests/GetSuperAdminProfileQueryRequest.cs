using MediatR;
using Sakany.Application.Features.User.Profiles.Queries.GetSuperAdminProfile.DTOs;
using Sakany.Shared.Responses;

namespace Sakany.Application.Features.User.Profiles.Queries.GetSuperAdminProfile.Requests
{
    public class GetSuperAdminProfileQueryRequest : IRequest<Response<GetSuperAdminProfileQueryDTO>>
    {
    }
}