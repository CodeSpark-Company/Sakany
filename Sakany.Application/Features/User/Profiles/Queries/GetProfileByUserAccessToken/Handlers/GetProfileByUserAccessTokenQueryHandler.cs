using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Sakany.Application.Features.User.Profiles.Queries.GetProfileByUserAccessToken.DTOs;
using Sakany.Application.Features.User.Profiles.Queries.GetProfileByUserAccessToken.Requests;
using Sakany.Application.Interfaces.Specifications.Base;
using Sakany.Application.Interfaces.UnitOfWork;
using Sakany.Domain.Entities.Users;
using Sakany.Domain.Enumerations.Users;
using Sakany.Domain.IdentityEntities;
using Sakany.Shared.Responses;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

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

        private async Task<GetProfileByUserAccessTokenQueryDTO> GetProfile(string role, int profileId)
        {
            if (role == nameof(UserRole.SuperAdmin))
            {
                _superAdminSpecification.Criteria = profile => profile.Id == profileId;
                var superAdminProfile = await _unitOfWork.Repository<SuperAdminProfile>().FindAsNoTrackingAsync(_superAdminSpecification);
                return _mapper.Map<GetProfileByUserAccessTokenQueryDTO>(superAdminProfile);
            }
            else if (role == nameof(UserRole.Admin))
            {
                _adminSpecification.Criteria = profile => profile.Id == profileId;
                var adminProfile = await _unitOfWork.Repository<AdminProfile>().FindAsNoTrackingAsync(_adminSpecification);
                return _mapper.Map<GetProfileByUserAccessTokenQueryDTO>(adminProfile);
            }
            else if (role == nameof(UserRole.Realtor))
            {
                _realtorSpecification.Criteria = profile => profile.Id == profileId;
                var realtorProfile = await _unitOfWork.Repository<RealtorProfile>().FindAsNoTrackingAsync(_realtorSpecification);
                var response = _mapper.Map<GetProfileByUserAccessTokenQueryDTO>(realtorProfile);
                response.AdditionalFields = new
                {
                    RealEstateContract = realtorProfile!.RealEstateContract
                };
                return response;
            }
            else if (role == nameof(UserRole.Student))
            {
                _studentSpecification.Criteria = profile => profile.Id == profileId;
                var studentProfile = await _unitOfWork.Repository<StudentProfile>().FindAsNoTrackingAsync(_studentSpecification);
                var response = _mapper.Map<GetProfileByUserAccessTokenQueryDTO>(studentProfile);
                response.AdditionalFields = new
                {
                    UniversityId = studentProfile!.UniversityId,
                    Unviersity = studentProfile.Unviersity,
                    College = studentProfile.College,
                    Level = studentProfile.Level
                };
                return response;
            }
            return new GetProfileByUserAccessTokenQueryDTO();
        }

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

            var profileId = user.UserProfile!.Id;
            var response = await GetProfile(role, profileId);

            // Map Response
            return Success(response);
        }

        #endregion Methods
    }
}