using AutoMapper;
using Sakany.Application.Features.User.Profiles.Queries.GetSuperAdminProfile.DTOs;
using Sakany.Domain.Entities.Users;

namespace Sakany.Application.Mapping.User.Profiles.Queries.GetSuperAdminProfile
{
    public class GetAdminProfileMappingProfile : Profile
    {
        #region Constructors

        public GetAdminProfileMappingProfile()
        {
            CreateMap<SuperAdminProfile, GetSuperAdminProfileQueryDTO>()
                .ForMember(destination => destination.Email, options => options.MapFrom(source => source.User!.Email))
                .ForMember(destination => destination.Username, options => options.MapFrom(source => source.User!.UserName))
                .ForMember(destination => destination.FirstName, options => options.MapFrom(source => source.User!.FirstName))
                .ForMember(destination => destination.LastName, options => options.MapFrom(source => source.User!.LastName))
                .ForMember(destination => destination.PhoneNumber, options => options.MapFrom(source => source.User!.PhoneNumber))
                .ForMember(destination => destination.Age, options => options.MapFrom(source => source.User!.Age));
        }

        #endregion Constructors
    }
}