using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Sakany.Application.Features.User.Profiles.Commands.UpdateAdminProfile.DTOs;
using Sakany.Application.Features.User.Profiles.Commands.UpdateAdminProfile.Requests;
using Sakany.Application.Interfaces.Services.Media;
using Sakany.Application.Interfaces.Specifications.Base;
using Sakany.Application.Interfaces.UnitOfWork;
using Sakany.Domain.Entities.Users;
using Sakany.Domain.IdentityEntities;
using Sakany.Shared.Responses;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Sakany.Application.Features.User.Profiles.Commands.UpdateAdminProfile.Handlers
{
    public class UpdateAdminProfileCommandHandler : ResponseHandler, IRequestHandler<UpdateAdminProfileCommandRequest, Response<UpdateAdminProfileCommandDTO>>
    {
        #region Properties

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly IMediaService _mediaService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBaseSpecification<AdminProfile> _adminProfileSpecification;

        #endregion Properties

        #region Constructors

        public UpdateAdminProfileCommandHandler(IHttpContextAccessor httpContextAccessor, IMapper mapper, UserManager<ApplicationUser> userManager, IMediaService mediaService, IUnitOfWork unitOfWork, IBaseSpecification<AdminProfile> adminProfileSpecification)
        {
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _userManager = userManager;
            _mediaService = mediaService;
            _unitOfWork = unitOfWork;
            _adminProfileSpecification = adminProfileSpecification;
        }

        #endregion Constructors

        #region Methods

        public async Task<Response<UpdateAdminProfileCommandDTO>> Handle(UpdateAdminProfileCommandRequest request, CancellationToken cancellationToken)
        {
            // Get Token From Request Authorization Header
            var authorizationHeader = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].FirstOrDefault();
            var token = authorizationHeader!.Substring($"{JwtBearerDefaults.AuthenticationScheme} ".Length).Trim();
            var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(token);

            // Get Email Claim From Token
            var email = jwtToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email)?.Value;
            if (email == null)
                return Unauthorized<UpdateAdminProfileCommandDTO>();

            // Get User
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return Forbidden<UpdateAdminProfileCommandDTO>();

            // Get AdminProfile
            _adminProfileSpecification.Criteria = profile => profile.Id == user.UserProfile!.Id;
            var adminProfile = await _unitOfWork.Repository<AdminProfile>().FindAsync(_adminProfileSpecification);

            // Update User
            _mapper.Map(request, adminProfile);

            if (request.Image != null)
                user.UserProfile!.ImageUrl = await _mediaService.UpdateAsync(user.UserProfile.ImageUrl, request.Image, "Images", "User", "Profiles", "Admin", "Image");

            if (request.CivilId != null)
                user.UserProfile!.CivilId = await _mediaService.UpdateAsync(user.UserProfile.CivilId, request.CivilId, "Documents", "User", "Profiles", "Admin", "CivilId");

            user.ModifiedAt = DateTime.UtcNow;
            await _userManager.UpdateAsync(user);

            var response = _mapper.Map<UpdateAdminProfileCommandDTO>(adminProfile);
            return Success(response, "Updated Successfully");
        }

        #endregion Methods
    }
}