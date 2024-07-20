﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sakany.API.Controllers.Base;

namespace Sakany.API.Controllers.User
{
    [Route("Api/V{version:apiVersion}/User/[controller]")]
    public class UserAPIBaseController : APIBaseController
    {
        #region Constructors

        public UserAPIBaseController(IMediator mediator) : base(mediator)
        {
        }

        #endregion Constructors
    }
}