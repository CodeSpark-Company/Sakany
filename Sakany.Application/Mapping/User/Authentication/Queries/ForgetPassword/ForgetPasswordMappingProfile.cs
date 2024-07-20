using AutoMapper;
using Sakany.Application.DTOs.Authentication.ForgetPassword;
using Sakany.Application.Features.User.Authentication.Queries.ForgetPassword.DTOs;
using Sakany.Application.Features.User.Authentication.Queries.ForgetPassword.Requests;

namespace Sakany.Application.Mapping.User.Authentication.Queries.ForgetPassword
{
    public class ForgetPasswordMappingProfile : Profile
    {
        #region Constructors

        public ForgetPasswordMappingProfile()
        {
            CreateMap<ForgetPasswordQueryRequest, ForgetPasswordDTORequest>();
            CreateMap<ForgetPasswordDTOResponse, ForgetPasswordQueryDTO>();
        }

        #endregion Constructors
    }
}