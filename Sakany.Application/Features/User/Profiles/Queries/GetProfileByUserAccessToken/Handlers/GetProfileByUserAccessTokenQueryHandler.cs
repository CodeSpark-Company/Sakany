using AutoMapper;
using Sakany.Application.Features.User.Profiles.Queries.GetProfileByUserAccessToken.DTOs;
using Sakany.Application.Features.User.Profiles.Queries.GetProfileByUserAccessToken.Requests;
using Sakany.Domain.IdentityEntities;
using Sakany.Shared.Responses;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Sakany.Domain.Enumerations.Users;
using Microsoft.EntityFrameworkCore;
using Sakany.Domain.Entities.Users;
using Sakany.Application.Interfaces.UnitOfWork;
using Sakany.Application.Interfaces.Specifications.Base;

namespace Sakany.Application.Features.User.Profiles.Queries.GetProfileByUserAccessToken.Handlers
{
    public class GetProfileByUserAccessTokenQueryHandler : ResponseHandler, IRequestHandler<GetProfileByUserAccessTokenQueryRequest, Response<GetProfileByUserAccessTokenQueryDTO>>
    {
        #region Properties

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBaseSpecification<StudentProfile> _studentSpecification;
        private readonly IBaseSpecification<RealtorProfile> _realtorSpecification;
        private readonly IBaseSpecification<AdminProfile> _adminSpecification;
        private readonly IBaseSpecification<SuperAdminProfile> _superAdminSpecification;

        #endregion Properties

        #region Constructors

        public GetProfileByUserAccessTokenQueryHandler(IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager, IMapper mapper, IUnitOfWork unitOfWork, IBaseSpecification<StudentProfile> studentSpecification, IBaseSpecification<RealtorProfile> realtorSpecification, IBaseSpecification<AdminProfile> adminSpecification, IBaseSpecification<SuperAdminProfile> superAdminSpecification)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _studentSpecification = studentSpecification;
            _realtorSpecification = realtorSpecification;
            _adminSpecification = adminSpecification;
            _superAdminSpecification = superAdminSpecification;
        }

        #endregion Constructors

        #region Methods

        public async Task<Response<GetProfileByUserAccessTokenQueryDTO>> Handle(GetProfileByUserAccessTokenQueryRequest request, CancellationToken cancellationToken)
        {
            // Get Token From Request Authorization Header
            var authorizationHeader = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].FirstOrDefault();
            var token = authorizationHeader!.Substring($"{JwtBearerDefaults.AuthenticationScheme} ".Length).Trim();
            var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(token);

            // Get Email Claim From Token
            var email = jwtToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email)?.Value;
            if (email == null)
                return Unauthorized<GetProfileByUserAccessTokenQueryDTO>();

            // Get User
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return Forbidden<GetProfileByUserAccessTokenQueryDTO>();

            var role = jwtToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Role)?.Value;
            if (role == null)
                return Forbidden<GetProfileByUserAccessTokenQueryDTO>();

            var userProfileId = user.UserProfile!.Id;
            var response = new GetProfileByUserAccessTokenQueryDTO();

            switch (role)
            {
                case nameof(UserRole.SuperAdmin):
                    _superAdminSpecification.Criteria = profile => profile.Id == userProfileId;
                    var superAdminProfile = await _unitOfWork.Repository<SuperAdminProfile>().FindAsNoTrackingAsync(_superAdminSpecification);
                    response = _mapper.Map<GetProfileByUserAccessTokenQueryDTO>(superAdminProfile);
                    break;

                case nameof(UserRole.Admin):
                    _adminSpecification.Criteria = profile => profile.Id == userProfileId;
                    var adminProfile = await _unitOfWork.Repository<AdminProfile>().FindAsNoTrackingAsync(_adminSpecification);
                    response = _mapper.Map<GetProfileByUserAccessTokenQueryDTO>(adminProfile);
                    break;

                case nameof(UserRole.Realtor):
                    _realtorSpecification.Criteria = profile => profile.Id == userProfileId;
                    var realtorProfile = await _unitOfWork.Repository<RealtorProfile>().FindAsNoTrackingAsync(_realtorSpecification);
                    response = _mapper.Map<GetProfileByUserAccessTokenQueryDTO>(realtorProfile);
                    response.AdditionalFields = new
                    {
                        RealEstateContract = realtorProfile!.RealEstateContract
                    };
                    break;

                case nameof(UserRole.Student):
                    _studentSpecification.Criteria = profile => profile.Id == userProfileId;
                    var studentProfile = await _unitOfWork.Repository<StudentProfile>().FindAsNoTrackingAsync(_studentSpecification);
                    response = _mapper.Map<GetProfileByUserAccessTokenQueryDTO>(studentProfile);
                    response.AdditionalFields = new
                    {
                        UniversityId = studentProfile!.UniversityId,
                        Unviersity = studentProfile.Unviersity,
                        College = studentProfile.College,
                        Level = studentProfile.Level
                    };
                    break;

                default:
                    break;
            }

            // Map Response
            return Success(response);
        }

        #endregion Methods
    }
}