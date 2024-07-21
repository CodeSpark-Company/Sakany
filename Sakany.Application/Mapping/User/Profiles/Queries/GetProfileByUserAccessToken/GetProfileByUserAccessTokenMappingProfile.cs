using AutoMapper;
using Sakany.Application.Features.User.Profiles.Queries.GetProfileByUserAccessToken.DTOs;
using Sakany.Domain.Entities.Users;

namespace Sakany.Application.Mapping.User.Profiles.Queries.GetProfileByUserAccessToken
{
    public class GetProfileByUserAccessTokenMappingProfile : Profile
    {
        #region Constructors

        public GetProfileByUserAccessTokenMappingProfile()
        {
            CreateMap<UserProfile, GetProfileByUserAccessTokenQueryDTO>()
                .ForMember(destination => destination.Email, options => options.MapFrom(source => source.User!.Email))
                .ForMember(destination => destination.Username, options => options.MapFrom(source => source.User!.UserName))
                .ForMember(destination => destination.FirstName, options => options.MapFrom(source => source.User!.FirstName))
                .ForMember(destination => destination.LastName, options => options.MapFrom(source => source.User!.LastName))
                .ForMember(destination => destination.PhoneNumber, options => options.MapFrom(source => source.User!.PhoneNumber));
        }

        #endregion Constructors
    }
}