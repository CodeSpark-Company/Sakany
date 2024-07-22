using MediatR;
using Sakany.Application.Features.User.Profiles.Queries.GetRealtorProfile.DTOs;
using Sakany.Shared.Responses;

namespace Sakany.Application.Features.User.Profiles.Queries.GetRealtorProfile.Requests
{
    public class GetRealtorProfileQueryRequest : IRequest<Response<GetRealtorProfileQueryDTO>>
    {
    }
}