using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Sakany.Application.Features.User.Profiles.Queries.GetSuperAdminProfile.DTOs;
using Sakany.Application.Features.User.Profiles.Queries.GetSuperAdminProfile.Requests;
using Sakany.Application.Interfaces.Specifications.Base;
using Sakany.Application.Interfaces.UnitOfWork;
using Sakany.Domain.Entities.Users;
using Sakany.Domain.IdentityEntities;
using Sakany.Shared.Responses;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Sakany.Application.Features.User.Profiles.Queries.GetSuperAdminProfile.Handlers
{
    public class GetSuperAdminProfileQueryHandler : ResponseHandler, IRequestHandler<GetSuperAdminProfileQueryRequest, Response<GetSuperAdminProfileQueryDTO>>
    {
        #region Properties

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBaseSpecification<SuperAdminProfile> _superAdminSpecification;

        #endregion Properties

        #region Constructors

        public GetSuperAdminProfileQueryHandler(IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager, IMapper mapper, IUnitOfWork unitOfWork, IBaseSpecification<SuperAdminProfile> superAdminSpecification)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _superAdminSpecification = superAdminSpecification;
        }

        #endregion Constructors

        #region Methods

        public async Task<Response<GetSuperAdminProfileQueryDTO>> Handle(GetSuperAdminProfileQueryRequest request, CancellationToken cancellationToken)
        {
            // Get Token From Request Authorization Header
            var authorizationHeader = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].FirstOrDefault();
            var token = authorizationHeader!.Substring($"{JwtBearerDefaults.AuthenticationScheme} ".Length).Trim();
            var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(token);

            // Get Email Claim From Token
            var email = jwtToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email)?.Value;
            if (email == null)
                return Unauthorized<GetSuperAdminProfileQueryDTO>();

            // Get User
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return Forbidden<GetSuperAdminProfileQueryDTO>();

            _superAdminSpecification.Criteria = profile => profile.Id == user.UserProfile!.Id;
            var superAdminProfile = await _unitOfWork.Repository<SuperAdminProfile>().FindAsNoTrackingAsync(_superAdminSpecification);

            var response = _mapper.Map<GetSuperAdminProfileQueryDTO>(superAdminProfile);

            // Map Response
            return Success(response);
        }

        #endregion Methods
    }
}