using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Sakany.Application.Features.User.Profiles.Queries.GetRealtorProfile.DTOs;
using Sakany.Application.Features.User.Profiles.Queries.GetRealtorProfile.Requests;
using Sakany.Application.Interfaces.Specifications.Base;
using Sakany.Application.Interfaces.UnitOfWork;
using Sakany.Domain.Entities.Users;
using Sakany.Domain.IdentityEntities;
using Sakany.Shared.Responses;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Sakany.Application.Features.User.Profiles.Queries.GetRealtorProfile.Handlers
{
    public class GetRealtorProfileQueryHandler : ResponseHandler, IRequestHandler<GetRealtorProfileQueryRequest, Response<GetRealtorProfileQueryDTO>>
    {
        #region Properties

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBaseSpecification<RealtorProfile> _realtorSpecification;

        #endregion Properties

        #region Constructors

        public GetRealtorProfileQueryHandler(IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager, IMapper mapper, IUnitOfWork unitOfWork, IBaseSpecification<RealtorProfile> realtorSpecification)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _realtorSpecification = realtorSpecification;
        }

        #endregion Constructors

        #region Methods

        public async Task<Response<GetRealtorProfileQueryDTO>> Handle(GetRealtorProfileQueryRequest request, CancellationToken cancellationToken)
        {
            // Get Token From Request Authorization Header
            var authorizationHeader = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].FirstOrDefault();
            var token = authorizationHeader!.Substring($"{JwtBearerDefaults.AuthenticationScheme} ".Length).Trim();
            var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(token);

            // Get Email Claim From Token
            var email = jwtToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email)?.Value;
            if (email == null)
                return Unauthorized<GetRealtorProfileQueryDTO>();

            // Get User
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return Forbidden<GetRealtorProfileQueryDTO>();

            _realtorSpecification.Criteria = profile => profile.Id == user.UserProfile!.Id;
            var realtorProfile = await _unitOfWork.Repository<RealtorProfile>().FindAsNoTrackingAsync(_realtorSpecification);

            var response = _mapper.Map<GetRealtorProfileQueryDTO>(realtorProfile);

            // Map Response
            return Success(response);
        }

        #endregion Methods
    }
}