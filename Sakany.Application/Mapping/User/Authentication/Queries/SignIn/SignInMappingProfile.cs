using AutoMapper;
using Sakany.Application.DTOs.Authentication.SignIn;
using Sakany.Application.Features.User.Authentication.Queries.SignIn.DTOs;
using Sakany.Application.Features.User.Authentication.Queries.SignIn.Requests;

namespace Sakany.Application.Mapping.User.Authentication.Queries.SignIn
{
    public class SignInMappingProfile : Profile
    {
        #region Constructors

        public SignInMappingProfile()
        {
            CreateMap<SignInQueryRequest, SignInDTORequest>();
            CreateMap<SignInDTOResponse, SignInQueryDTO>()
                .ForMember(destination => destination.AccessToken, options => options.MapFrom(source => source.AccessToken!.Token))
                .ForMember(destination => destination.AccessTokenExpiresAt, options => options.MapFrom(source => source.AccessToken!.ExpiresAt))
                .ForMember(destination => destination.RefreshToken, options => options.MapFrom(source => source.RefreshToken!.Token))
                .ForMember(destination => destination.RefreshTokenExpiresAt, options => options.MapFrom(source => source.RefreshToken!.ExpiresAt));
        }

        #endregion Constructors
    }
}