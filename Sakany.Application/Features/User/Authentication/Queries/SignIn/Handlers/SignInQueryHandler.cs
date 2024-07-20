using AutoMapper;
using MediatR;
using Sakany.Application.DTOs.Authentication.SignIn;
using Sakany.Application.Features.User.Authentication.Queries.SignIn.DTOs;
using Sakany.Application.Features.User.Authentication.Queries.SignIn.Requests;
using Sakany.Application.Interfaces.Services.Authentication;
using Sakany.Shared.Responses;

namespace Sakany.Application.Features.User.Authentication.Queries.SignIn.Handlers
{
    public class SignInQueryHandler : ResponseHandler, IRequestHandler<SignInQueryRequest, Response<SignInQueryDTO>>
    {
        #region Properties

        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;

        #endregion Properties

        #region Constructors

        public SignInQueryHandler(IAuthenticationService authenticationService, IMapper mapper)
        {
            _authenticationService = authenticationService;
            _mapper = mapper;
        }

        #endregion Constructors

        #region Methods

        public async Task<Response<SignInQueryDTO>> Handle(SignInQueryRequest request, CancellationToken cancellationToken)
        {
            var signInDTORequest = _mapper.Map<SignInDTORequest>(request);

            var signInDTOResponse = await _authenticationService.SignInAsync(signInDTORequest);

            var signInQueryDTO = _mapper.Map<SignInQueryDTO>(signInDTOResponse);

            return signInQueryDTO.IsAuthenticated ? Success(signInQueryDTO) : Unauthorized<SignInQueryDTO>(signInQueryDTO.Message);
        }

        #endregion Methods
    }
}