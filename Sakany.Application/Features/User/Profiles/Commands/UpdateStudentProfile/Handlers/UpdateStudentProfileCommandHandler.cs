using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Sakany.Application.Features.User.Profiles.Commands.UpdateStudentProfile.DTOs;
using Sakany.Application.Features.User.Profiles.Commands.UpdateStudentProfile.Requests;
using Sakany.Application.Interfaces.Services.Media;
using Sakany.Application.Interfaces.Specifications.Base;
using Sakany.Application.Interfaces.UnitOfWork;
using Sakany.Domain.Entities.Users;
using Sakany.Domain.IdentityEntities;
using Sakany.Shared.Responses;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Sakany.Application.Features.User.Profiles.Commands.UpdateStudentProfile.Handlers
{
    public class UpdateStudentProfileCommandHandler : ResponseHandler, IRequestHandler<UpdateStudentProfileCommandRequest, Response<UpdateStudentProfileCommandDTO>>
    {
        #region Properties

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly IMediaService _mediaService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBaseSpecification<StudentProfile> _realtorProfileSpecification;

        #endregion Properties

        #region Constructors

        public UpdateStudentProfileCommandHandler(IHttpContextAccessor httpContextAccessor, IMapper mapper, UserManager<ApplicationUser> userManager, IMediaService mediaService, IUnitOfWork unitOfWork, IBaseSpecification<StudentProfile> realtorProfileSpecification)
        {
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _userManager = userManager;
            _mediaService = mediaService;
            _unitOfWork = unitOfWork;
            _realtorProfileSpecification = realtorProfileSpecification;
        }

        #endregion Constructors

        #region Methods

        public async Task<Response<UpdateStudentProfileCommandDTO>> Handle(UpdateStudentProfileCommandRequest request, CancellationToken cancellationToken)
        {
            // Get Token From Request Authorization Header
            var authorizationHeader = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].FirstOrDefault();
            var token = authorizationHeader!.Substring($"{JwtBearerDefaults.AuthenticationScheme} ".Length).Trim();
            var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(token);

            // Get Email Claim From Token
            var email = jwtToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email)?.Value;
            if (email == null)
                return Unauthorized<UpdateStudentProfileCommandDTO>();

            // Get User
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return Forbidden<UpdateStudentProfileCommandDTO>();

            // Get StudentProfile
            _realtorProfileSpecification.Criteria = profile => profile.Id == user.UserProfile!.Id;
            var realtorProfile = await _unitOfWork.Repository<StudentProfile>().FindAsync(_realtorProfileSpecification);

            // Update User
            _mapper.Map(request, realtorProfile);

            if (request.Image != null)
                user.UserProfile!.ImageUrl = await _mediaService.UpdateAsync(user.UserProfile.ImageUrl, request.Image, "Images", "User", "Profiles", "Student", "Image");

            if (request.CivilId != null)
                user.UserProfile!.CivilId = await _mediaService.UpdateAsync(user.UserProfile.CivilId, request.CivilId, "Documents", "User", "Profiles", "Student", "CivilId");

            user.ModifiedAt = DateTime.UtcNow;
            await _userManager.UpdateAsync(user);

            var response = _mapper.Map<UpdateStudentProfileCommandDTO>(realtorProfile);
            return Success(response, "Updated Successfully");
        }

        #endregion Methods
    }
}