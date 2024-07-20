using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sakany.Shared.Responses;
using System.Net;

namespace Sakany.API.Controllers.Base
{
    [Route("Api/V{version:apiVersion}/[controller]")]
    [ApiController]
    public class APIBaseController : ControllerBase
    {
        #region Properties

        public IMediator Mediator { get; protected set; }

        #endregion Properties

        #region Constructors

        public APIBaseController(IMediator mediator)
        {
            Mediator = mediator;
        }

        #endregion Constructors

        #region Methods

        public IActionResult ResponseResult<T>(Response<T> response)
        {
            return response.StatusCode switch
            {
                HttpStatusCode.OK => Ok(response),
                HttpStatusCode.Created => Created("", response),
                HttpStatusCode.Unauthorized => Unauthorized(response),
                HttpStatusCode.BadRequest => BadRequest(response),
                HttpStatusCode.NotFound => NotFound(response),
                HttpStatusCode.NoContent => Ok(response),
                _ => Ok(response)
            };
        }

        #endregion Methods
    }
}