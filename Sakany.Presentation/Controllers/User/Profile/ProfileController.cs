using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sakany.Application.Features.User.Profiles.Queries.GetProfileByUserAccessToken.DTOs;
using Sakany.Application.Features.User.Profiles.Queries.GetProfileByUserAccessToken.Requests;

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

        [HttpGet]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(GetProfileByUserAccessTokenQueryDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GetProfileByUserAccessTokenQueryDTO), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(GetProfileByUserAccessTokenQueryDTO), StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetProfileByUserAccessTokenAsync([FromQuery] GetProfileByUserAccessTokenQueryRequest request)
        {
            var response = await Mediator.Send(request);
            return ResponseResult(response);
        }
    }
}