using AutoMapper;
using MediatR;
using Sakany.Application.DTOs.Authentication.ResetPassword;
using Sakany.Application.Features.User.Authentication.Commands.ResetPassword.DTOs;
using Sakany.Application.Features.User.Authentication.Commands.ResetPassword.Requests;
using Sakany.Application.Interfaces.Services.Authentication;
using Sakany.Shared.Responses;

namespace Sakany.Application.Features.User.Authentication.Commands.ResetPassword.Handlers
{
    public class ResetPasswordCommandHandler : ResponseHandler, IRequestHandler<ResetPasswordCommandRequest, Response<ResetPasswordCommandDTO>>
    {
        #region Properties

        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;

        #endregion Properties

        #region Constructors

        public ResetPasswordCommandHandler(IAuthenticationService authenticationService, IMapper mapper)
        {
            _authenticationService = authenticationService;
            _mapper = mapper;
        }

        #endregion Constructors

        #region Methods

        public async Task<Response<ResetPasswordCommandDTO>> Handle(ResetPasswordCommandRequest request, CancellationToken cancellationToken)
        {
            var resetPasswordDTORequest = _mapper.Map<ResetPasswordDTORequest>(request);
            var resetPasswordDTOResponse = await _authenticationService.ResetPasswordAsync(resetPasswordDTORequest);

            var response = _mapper.Map<ResetPasswordCommandDTO>(resetPasswordDTOResponse);
            return response.IsAuthenticated ? Success(response) : Unauthorized<ResetPasswordCommandDTO>(response.Message);
        }

        #endregion Methods
    }
}