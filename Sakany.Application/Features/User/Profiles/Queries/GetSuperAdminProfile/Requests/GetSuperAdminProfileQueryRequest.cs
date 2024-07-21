using Sakany.Application.Features.User.Profiles.Queries.GetSuperAdminProfile.DTOs;
using Sakany.Shared.Responses;
using MediatR;

namespace Sakany.Application.Features.User.Profiles.Queries.GetSuperAdminProfile.Requests
{
    public class GetSuperAdminProfileQueryRequest : IRequest<Response<GetSuperAdminProfileQueryDTO>>
    {
    }
}