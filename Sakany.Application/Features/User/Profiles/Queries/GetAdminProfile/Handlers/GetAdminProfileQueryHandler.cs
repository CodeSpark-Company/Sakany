using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Sakany.Application.Features.User.Profiles.Queries.GetAdminProfile.DTOs;
using Sakany.Application.Features.User.Profiles.Queries.GetAdminProfile.Requests;
using Sakany.Application.Interfaces.Specifications.Base;
using Sakany.Application.Interfaces.UnitOfWork;
using Sakany.Domain.Entities.Users;
using Sakany.Domain.IdentityEntities;
using Sakany.Shared.Responses;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Sakany.Application.Features.User.Profiles.Queries.GetAdminProfile.Handlers
{
    public class GetAdminProfileQueryHandler : ResponseHandler, IRequestHandler<GetAdminProfileQueryRequest, Response<GetAdminProfileQueryDTO>>
    {
        #region Properties

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBaseSpecification<AdminProfile> _adminSpecification;

        #endregion Properties

        #region Constructors

        public GetAdminProfileQueryHandler(IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager, IMapper mapper, IUnitOfWork unitOfWork, IBaseSpecification<AdminProfile> adminSpecification)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _adminSpecification = adminSpecification;
        }

        #endregion Constructors

        #region Methods

        public async Task<Response<GetAdminProfileQueryDTO>> Handle(GetAdminProfileQueryRequest request, CancellationToken cancellationToken)
        {
            // Get Token From Request Authorization Header
            var authorizationHeader = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].FirstOrDefault();
            var token = authorizationHeader!.Substring($"{JwtBearerDefaults.AuthenticationScheme} ".Length).Trim();
            var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(token);

            // Get Email Claim From Token
            var email = jwtToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email)?.Value;
            if (email == null)
                return Unauthorized<GetAdminProfileQueryDTO>();

            // Get User
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return Forbidden<GetAdminProfileQueryDTO>();

            _adminSpecification.Criteria = profile => profile.Id == user.UserProfile!.Id;
            var adminProfile = await _unitOfWork.Repository<AdminProfile>().FindAsNoTrackingAsync(_adminSpecification);

            var response = _mapper.Map<GetAdminProfileQueryDTO>(adminProfile);

            // Map Response
            return Success(response);
        }

        #endregion Methods
    }
}