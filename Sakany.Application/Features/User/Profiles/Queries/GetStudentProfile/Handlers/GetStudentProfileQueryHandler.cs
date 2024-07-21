using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Sakany.Application.Features.User.Profiles.Queries.GetStudentProfile.DTOs;
using Sakany.Application.Features.User.Profiles.Queries.GetStudentProfile.Requests;
using Sakany.Application.Interfaces.Specifications.Base;
using Sakany.Application.Interfaces.UnitOfWork;
using Sakany.Domain.Entities.Users;
using Sakany.Domain.IdentityEntities;
using Sakany.Shared.Responses;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Sakany.Application.Features.User.Profiles.Queries.GetStudentProfile.Handlers
{
    public class GetStudentProfileQueryHandler : ResponseHandler, IRequestHandler<GetStudentProfileQueryRequest, Response<GetStudentProfileQueryDTO>>
    {
        #region Properties

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBaseSpecification<StudentProfile> _studentSpecification;

        #endregion Properties

        #region Constructors

        public GetStudentProfileQueryHandler(IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager, IMapper mapper, IUnitOfWork unitOfWork, IBaseSpecification<StudentProfile> studentSpecification)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _studentSpecification = studentSpecification;
        }

        #endregion Constructors

        #region Methods

        public async Task<Response<GetStudentProfileQueryDTO>> Handle(GetStudentProfileQueryRequest request, CancellationToken cancellationToken)
        {
            // Get Token From Request Authorization Header
            var authorizationHeader = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].FirstOrDefault();
            var token = authorizationHeader!.Substring($"{JwtBearerDefaults.AuthenticationScheme} ".Length).Trim();
            var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(token);

            // Get Email Claim From Token
            var email = jwtToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email)?.Value;
            if (email == null)
                return Unauthorized<GetStudentProfileQueryDTO>();

            // Get User
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return Forbidden<GetStudentProfileQueryDTO>();

            _studentSpecification.Criteria = profile => profile.Id == user.UserProfile!.Id;
            var studentProfile = await _unitOfWork.Repository<StudentProfile>().FindAsNoTrackingAsync(_studentSpecification);

            var response = _mapper.Map<GetStudentProfileQueryDTO>(studentProfile);

            // Map Response
            return Success(response);
        }

        #endregion Methods
    }
}