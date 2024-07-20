using AutoMapper;
using MediatR;
using Sakany.Application.DTOs.Authentication.SignUp;
using Sakany.Application.Features.User.Authentication.Commands.SignUp.DTOs;
using Sakany.Application.Features.User.Authentication.Commands.SignUp.Requests;
using Sakany.Application.Interfaces.Services.Authentication;
using Sakany.Shared.Responses;

namespace Sakany.Application.Features.User.Authentication.Commands.SignUp.Handlers
{
    public class SignUpCommandHandler : ResponseHandler, IRequestHandler<SignUpCommandRequest, Response<SignUpCommandDTO>>
    {
        #region Properties

        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;

        #endregion Properties

        #region Constructors

        public SignUpCommandHandler(IAuthenticationService authenticationService, IMapper mapper)
        {
            _authenticationService = authenticationService;
            _mapper = mapper;
        }

        #endregion Constructors

        #region Methods

        public async Task<Response<SignUpCommandDTO>> Handle(SignUpCommandRequest request, CancellationToken cancellationToken)
        {
            var signUpDTORequest = _mapper.Map<SignUpDTORequest>(request);
            var signUpDTOResponse = await _authenticationService.SignUpAsync(signUpDTORequest);
            var signUpCommandDTO = _mapper.Map<SignUpCommandDTO>(signUpDTOResponse);

            var response = Created(signUpCommandDTO);
            response.Message = "SignUp Successfully.";
            response.Data = null;

            return response;
        }

        #endregion Methods
    }
}