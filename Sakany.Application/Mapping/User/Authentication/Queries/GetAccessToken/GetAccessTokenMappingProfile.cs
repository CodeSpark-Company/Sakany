using AutoMapper;
using Sakany.Application.DTOs.Authentication.Token;
using Sakany.Application.Features.User.Authentication.Queries.GetAccessToken.DTOs;

namespace Sakany.Application.Mapping.User.Authentication.Queries.GetAccessToken
{
    public class GetAccessTokenMappingProfile : Profile
    {
        #region Constructors

        public GetAccessTokenMappingProfile()
        {
            CreateMap<AccessTokenDTO, GetAccessTokenQueryDTO>()
                .ForMember(destination => destination.AccessToken, options => options.MapFrom(source => source.Token))
                .ForMember(destination => destination.AccessTokenExpiresAt, options => options.MapFrom(source => source.ExpiresAt));
        }

        #endregion Constructors
    }
}