using AutoMapper;
using Sakany.Application.DTOs.Authentication.SignUp;
using Sakany.Application.Features.User.Authentication.Commands.SignUp.DTOs;
using Sakany.Application.Features.User.Authentication.Commands.SignUp.Requests;
using Sakany.Domain.IdentityEntities;

namespace Sakany.Application.Mapping.User.Authentication.Commands.SignUp
{
    public class SignUpMappingProfile : Profile
    {
        #region Constructors

        public SignUpMappingProfile()
        {
            CreateMap<SignUpDTORequest, ApplicationUser>();

            CreateMap<SignUpCommandRequest, SignUpDTORequest>();
            CreateMap<SignUpDTOResponse, SignUpCommandDTO>();
        }

        #endregion Constructors
    }
}