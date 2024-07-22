using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Sakany.Application.Features.User.Profiles.Commands.UpdateSuperAdminProfile.DTOs;
using Sakany.Application.Features.User.Profiles.Commands.UpdateSuperAdminProfile.Requests;
using Sakany.Application.Interfaces.Services.Media;
using Sakany.Application.Interfaces.Specifications.Base;
using Sakany.Application.Interfaces.UnitOfWork;
using Sakany.Domain.Entities.Users;
using Sakany.Domain.IdentityEntities;
using Sakany.Shared.Responses;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Sakany.Application.Features.User.Profiles.Commands.UpdateSuperAdminProfile.Handlers
{
    public class UpdateSuperAdminProfileCommandHandler : ResponseHandler, IRequestHandler<UpdateSuperAdminProfileCommandRequest, Response<UpdateSuperAdminProfileCommandDTO>>
    {
        #region Properties

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly IMediaService _mediaService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBaseSpecification<SuperAdminProfile> _superAdminProfileSpecification;

        #endregion Properties

        #region Constructors

        public UpdateSuperAdminProfileCommandHandler(IHttpContextAccessor httpContextAccessor, IMapper mapper, UserManager<ApplicationUser> userManager, IMediaService mediaService, IUnitOfWork unitOfWork, IBaseSpecification<SuperAdminProfile> superAdminProfileSpecification)
        {
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _userManager = userManager;
            _mediaService = mediaService;
            _unitOfWork = unitOfWork;
            _superAdminProfileSpecification = superAdminProfileSpecification;
        }

        #endregion Constructors

        #region Methods

        public async Task<Response<UpdateSuperAdminProfileCommandDTO>> Handle(UpdateSuperAdminProfileCommandRequest request, CancellationToken cancellationToken)
        {
            // Get Token From Request Authorization Header
            var authorizationHeader = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].FirstOrDefault();
            var token = authorizationHeader!.Substring($"{JwtBearerDefaults.AuthenticationScheme} ".Length).Trim();
            var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(token);

            // Get Email Claim From Token
            var email = jwtToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email)?.Value;
            if (email == null)
                return Unauthorized<UpdateSuperAdminProfileCommandDTO>();

            // Get User
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return Forbidden<UpdateSuperAdminProfileCommandDTO>();

            // Get SuperAdminProfile
            _superAdminProfileSpecification.Criteria = profile => profile.Id == user.UserProfile!.Id;
            var superAdminProfile = await _unitOfWork.Repository<SuperAdminProfile>().FindAsync(_superAdminProfileSpecification);

            // Update User
            _mapper.Map(request, superAdminProfile);

            if (request.Image != null)
                user.UserProfile!.ImageUrl = await _mediaService.UpdateAsync(user.UserProfile.ImageUrl, request.Image, "Images", "User", "Profiles", "SuperAdmin", "Image");

            if (request.CivilId != null)
                user.UserProfile!.CivilId = await _mediaService.UpdateAsync(user.UserProfile.CivilId, request.CivilId, "Documents", "User", "Profiles", "SuperAdmin", "CivilId");

            user.ModifiedAt = DateTime.UtcNow;
            await _userManager.UpdateAsync(user);

            var response = _mapper.Map<UpdateSuperAdminProfileCommandDTO>(superAdminProfile);
            return Success(response, "Updated Successfully");
        }

        #endregion Methods
    }
}