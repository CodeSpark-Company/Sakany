using Sakany.Application.Features.User.Profiles.Queries.GetStudentProfile.DTOs;
using Sakany.Shared.Responses;
using MediatR;

namespace Sakany.Application.Features.User.Profiles.Queries.GetStudentProfile.Requests
{
    public class GetStudentProfileQueryRequest : IRequest<Response<GetStudentProfileQueryDTO>>
    {
    }
}