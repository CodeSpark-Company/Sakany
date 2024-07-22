using AutoMapper;
using Sakany.Application.Features.User.Profiles.Commands.UpdateRealtorProfile.DTOs;
using Sakany.Application.Features.User.Profiles.Commands.UpdateRealtorProfile.Requests;
using Sakany.Domain.Entities.Users;
using Sakany.Domain.IdentityEntities;

namespace Sakany.Application.Mapping.User.Profiles.Commands.UpdateRealtorProfile
{
    public class UpdateRealtorProfileMappingProfile : Profile
    {
        #region Properties

        public UpdateRealtorProfileMappingProfile()
        {
            CreateMap<UpdateRealtorProfileCommandRequest, RealtorProfile>()
                .ForMember(destination => destination.Bio, options => options.Condition(source => !string.IsNullOrEmpty(source.Bio)))
                .ForMember(destination => destination.ImageUrl, options => options.Condition(source => source.Image is not null))
                .ForMember(destination => destination.CivilId, options => options.Condition(source => source.CivilId is not null))
                .ForMember(destination => destination.RealEstateContract, options => options.Condition(source => source.RealEstateContract is not null))
                .ForMember(destination => destination.User, options => options.MapFrom<UserResolver>());

            CreateMap<RealtorProfile, UpdateRealtorProfileCommandDTO>()
                .ForMember(destination => destination.UserName, options => options.MapFrom(source => source.User.UserName))
                .ForMember(destination => destination.FirstName, options => options.MapFrom(source => source.User.FirstName))
                .ForMember(destination => destination.LastName, options => options.MapFrom(source => source.User.LastName))
                .ForMember(destination => destination.PhoneNumber, options => options.MapFrom(source => source.User.PhoneNumber))
                .ForMember(destination => destination.Age, options => options.MapFrom(source => source.User.Age));
        }

        #endregion Properties
    }

    public class UserResolver : IValueResolver<UpdateRealtorProfileCommandRequest, RealtorProfile, ApplicationUser>
    {
        public ApplicationUser Resolve(UpdateRealtorProfileCommandRequest source, RealtorProfile destination, ApplicationUser destMember, ResolutionContext context)
        {
            if (destination.User == null)
            {
                destination.User = new ApplicationUser();
            }

            if (!string.IsNullOrEmpty(source.UserName))
            {
                destination.User.UserName = source.UserName;
            }

            if (!string.IsNullOrEmpty(source.FirstName))
            {
                destination.User.FirstName = source.FirstName;
            }

            if (!string.IsNullOrEmpty(source.LastName))
            {
                destination.User.LastName = source.LastName;
            }

            if (!string.IsNullOrEmpty(source.PhoneNumber))
            {
                destination.User.PhoneNumber = source.PhoneNumber;
            }

            if (source.BirthDate.HasValue)
            {
                destination.User.BirthDate = source.BirthDate.Value;
            }

            return destination.User;
        }
    }
}