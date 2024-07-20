using AutoMapper;
using Sakany.Application.DTOs.Authentication.ResetPassword;
using Sakany.Application.Features.User.Authentication.Commands.ResetPassword.DTOs;
using Sakany.Application.Features.User.Authentication.Commands.ResetPassword.Requests;

namespace Sakany.Application.Mapping.User.Authentication.Commands.ResetPassword
{
    public class ResetPasswordMappingProfile : Profile
    {
        #region Constructor

        public ResetPasswordMappingProfile()
        {
            CreateMap<ResetPasswordCommandRequest, ResetPasswordDTORequest>();
            CreateMap<ResetPasswordDTOResponse, ResetPasswordCommandDTO>();
        }

        #endregion Constructor
    }
}