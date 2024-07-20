using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sakany.Application.Features.User.Authentication.Commands.SignUp.DTOs;
using Sakany.Application.Features.User.Authentication.Commands.SignUp.Requests;

namespace Sakany.API.Controllers.User.Authentication
{
    [ApiVersion("1.0")]
    public class AuthenticationController : UserAPIBaseController
    {
        #region Constructors

        public AuthenticationController(IMediator mediator) : base(mediator)
        {
        }

        #endregion Constructors

        #region Methods

        [HttpPost("SignUp")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(SignUpCommandDTO), StatusCodes.Status201Created)]
        public async Task<IActionResult> SignUpAsync([FromBody] SignUpCommandRequest request)
        {
            var response = await Mediator.Send(request);
            return ResponseResult(response);
        }

        #endregion Methods
    }
}