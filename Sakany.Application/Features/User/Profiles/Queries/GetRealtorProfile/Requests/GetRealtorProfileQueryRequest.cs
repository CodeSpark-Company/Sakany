using Sakany.Application.Features.User.Profiles.Queries.GetRealtorProfile.DTOs;
using Sakany.Shared.Responses;
using MediatR;

namespace Sakany.Application.Features.User.Profiles.Queries.GetRealtorProfile.Requests
{
    public class GetRealtorProfileQueryRequest : IRequest<Response<GetRealtorProfileQueryDTO>>
    {
    }
}