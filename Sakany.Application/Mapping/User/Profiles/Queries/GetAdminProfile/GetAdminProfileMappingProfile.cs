using AutoMapper;
using Sakany.Application.Features.User.Profiles.Queries.GetAdminProfile.DTOs;
using Sakany.Domain.Entities.Users;

namespace Sakany.Application.Mapping.User.Profiles.Queries.GetAdminProfile
{
    public class GetAdminProfileMappingProfile : Profile
    {
        #region Constructors

        public GetAdminProfileMappingProfile()
        {
            CreateMap<AdminProfile, GetAdminProfileQueryDTO>()
                .ForMember(destination => destination.Email, options => options.MapFrom(source => source.User!.Email))
                .ForMember(destination => destination.UserName, options => options.MapFrom(source => source.User!.UserName))
                .ForMember(destination => destination.FirstName, options => options.MapFrom(source => source.User!.FirstName))
                .ForMember(destination => destination.LastName, options => options.MapFrom(source => source.User!.LastName))
                .ForMember(destination => destination.PhoneNumber, options => options.MapFrom(source => source.User!.PhoneNumber))
                .ForMember(destination => destination.Age, options => options.MapFrom(source => source.User!.Age));
        }

        #endregion Constructors
    }
}