using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sakany.Application.Features.User.Profiles.Queries.GetAdminProfile.DTOs;
using Sakany.Application.Features.User.Profiles.Queries.GetAdminProfile.Requests;
using Sakany.Application.Features.User.Profiles.Queries.GetRealtorProfile.DTOs;
using Sakany.Application.Features.User.Profiles.Queries.GetRealtorProfile.Requests;
using Sakany.Application.Features.User.Profiles.Queries.GetStudentProfile.DTOs;
using Sakany.Application.Features.User.Profiles.Queries.GetStudentProfile.Requests;
using Sakany.Application.Features.User.Profiles.Queries.GetSuperAdminProfile.DTOs;
using Sakany.Application.Features.User.Profiles.Queries.GetSuperAdminProfile.Requests;

namespace Sakany.API.Controllers.User.Profile
{
    [Authorize]
    [ApiVersion("1.0")]
    public class ProfileController : UserAPIBaseController
    {
        #region Constructors

        public ProfileController(IMediator mediator) : base(mediator)
        {
        }

        #endregion Constructors

        [HttpGet("SuperAdmin")]
        [Authorize(Roles = "SuperAdmin")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(GetSuperAdminProfileQueryDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GetSuperAdminProfileQueryDTO), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(GetSuperAdminProfileQueryDTO), StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetSuperAdminProfileAsync([FromQuery] GetSuperAdminProfileQueryRequest request)
        {
            var response = await Mediator.Send(request);
            return ResponseResult(response);
        }

        [HttpGet("Admin")]
        [Authorize(Roles = "Admin")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(GetAdminProfileQueryDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GetAdminProfileQueryDTO), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(GetAdminProfileQueryDTO), StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetAdminProfileAsync([FromQuery] GetAdminProfileQueryRequest request)
        {
            var response = await Mediator.Send(request);
            return ResponseResult(response);
        }

        [HttpGet("Realtor")]
        [Authorize(Roles = "Realtor")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(GetRealtorProfileQueryDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GetRealtorProfileQueryDTO), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(GetRealtorProfileQueryDTO), StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetRealtorProfileAsync([FromQuery] GetRealtorProfileQueryRequest request)
        {
            var response = await Mediator.Send(request);
            return ResponseResult(response);
        }

        [HttpGet("Student")]
        [Authorize(Roles = "Student")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(GetStudentProfileQueryDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GetStudentProfileQueryDTO), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(GetStudentProfileQueryDTO), StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetStudentProfileAsync([FromQuery] GetStudentProfileQueryRequest request)
        {
            var response = await Mediator.Send(request);
            return ResponseResult(response);
        }
    }
}