﻿using AutoMapper;
using MediatR;
using Sakany.Application.Features.User.Authentication.Queries.GetAccessToken.DTOs;
using Sakany.Application.Features.User.Authentication.Queries.GetAccessToken.Requests;
using Sakany.Application.Interfaces.Services.Authentication;
using Sakany.Shared.Responses;

namespace Sakany.Application.Features.User.Authentication.Queries.GetAccessToken.Handlers
{
    public class GetAccessTokenQueryHandler : ResponseHandler, IRequestHandler<GetAccessTokenQueryRequest, Response<GetAccessTokenQueryDTO>>
    {
        #region Properties

        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;

        #endregion Properties

        #region Constructors

        public GetAccessTokenQueryHandler(IAuthenticationService authenticationService, IMapper mapper)
        {
            _authenticationService = authenticationService;
            _mapper = mapper;
        }

        #endregion Constructors

        #region Methods

        public async Task<Response<GetAccessTokenQueryDTO>> Handle(GetAccessTokenQueryRequest request, CancellationToken cancellationToken)
        {
            var accessToken = await _authenticationService.GetAccessTokenAsync(request.RefreshToken);
            var response = _mapper.Map<GetAccessTokenQueryDTO>(accessToken);
            return Success(response);
        }

        #endregion Methods
    }
}