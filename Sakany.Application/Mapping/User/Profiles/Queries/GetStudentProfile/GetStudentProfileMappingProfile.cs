﻿using AutoMapper;
using Sakany.Application.Features.User.Profiles.Queries.GetStudentProfile.DTOs;
using Sakany.Domain.Entities.Users;

namespace Sakany.Application.Mapping.User.Profiles.Queries.GetStudentProfile
{
    public class GetStudentProfileMappingProfile : Profile
    {
        #region Constructors

        public GetStudentProfileMappingProfile()
        {
            CreateMap<StudentProfile, GetStudentProfileQueryDTO>()
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