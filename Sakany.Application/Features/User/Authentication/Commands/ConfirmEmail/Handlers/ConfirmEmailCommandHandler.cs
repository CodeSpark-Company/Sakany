using AutoMapper;
using MediatR;
using Sakany.Application.DTOs.Authentication.ConfirmEmail;
using Sakany.Application.Features.User.Authentication.Commands.ConfirmEmail.DTOs;
using Sakany.Application.Features.User.Authentication.Commands.ConfirmEmail.Requests;
using Sakany.Application.Interfaces.Services.Authentication;
using Sakany.Shared.Responses;

namespace Sakany.Application.Features.User.Authentication.Commands.ConfirmEmail.Handlers
{
    public class ConfirmEmailCommandHandler : ResponseHandler, IRequestHandler<ConfirmEmailCommandRequest, Response<ConfirmEmailCommandDTO>>
    {
        #region Properties

        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;

        #endregion Properties

        #region Constructors

        public ConfirmEmailCommandHandler(IAuthenticationService authenticationService, IMapper mapper)
        {
            _authenticationService = authenticationService;
            _mapper = mapper;
        }

        #endregion Constructors

        #region Methods

        public async Task<Response<ConfirmEmailCommandDTO>> Handle(ConfirmEmailCommandRequest request, CancellationToken cancellationToken)
        {
            var confirmEmailDTORequest = _mapper.Map<ConfirmEmailDTORequest>(request);
            var confirmEmailDTOResponse = await _authenticationService.ConfirmEmailAsync(confirmEmailDTORequest);

            var response = _mapper.Map<ConfirmEmailCommandDTO>(confirmEmailDTOResponse);
            return response.IsAuthenticated ? Success(response) : Unauthorized<ConfirmEmailCommandDTO>(response.Message);
        }

        #endregion Methods
    }
}