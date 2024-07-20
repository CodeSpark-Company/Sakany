using AutoMapper;
using Sakany.Application.DTOs.Authentication.ConfirmEmail;
using Sakany.Application.Features.User.Authentication.Commands.ConfirmEmail.DTOs;
using Sakany.Application.Features.User.Authentication.Commands.ConfirmEmail.Requests;

namespace Sakany.Application.Mapping.User.Authentication.Commands.ConfirmEmail
{
    public class ConfirmEmailMappingProfile : Profile
    {
        #region Constructors

        public ConfirmEmailMappingProfile()
        {
            CreateMap<ConfirmEmailCommandRequest, ConfirmEmailDTORequest>();

            CreateMap<ConfirmEmailDTOResponse, ConfirmEmailCommandDTO>();
        }

        #endregion Constructors
    }
}