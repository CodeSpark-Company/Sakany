﻿using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Sakany.Application.DTOs.Authentication.Token;
using Sakany.Application.Interfaces.Services.Authentication;
using Sakany.Application.Interfaces.Specifications.Base;
using Sakany.Application.Interfaces.UnitOfWork;
using Sakany.Domain.Entities.Security;
using Sakany.Domain.IdentityEntities;
using Sakany.Shared.Helpers.JwtConfiguration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Sakany.Infrastructure.Services.Authentication
{
    public class TokenService : ITokenService
    {
        #region Properties

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBaseSpecification<RefreshToken> _refreshTokenSpecification;
        private readonly JwtConfiguration _jwtConfiguration;

        #endregion Properties

        #region Constructors

        public TokenService(UserManager<ApplicationUser> userManager, JwtConfiguration jwtConfiguration, IUnitOfWork unitOfWork, IBaseSpecification<RefreshToken> refreshTokenSpecification)
        {
            _userManager = userManager;
            _jwtConfiguration = jwtConfiguration;
            _unitOfWork = unitOfWork;
            _refreshTokenSpecification = refreshTokenSpecification;
        }

        #endregion Constructors

        #region Methods

        #region Generate Access Token

        public async Task<AccessTokenDTO> GenerateAccessTokenAsync(ApplicationUser user)
        {
            JwtSecurityToken token = new
            (
               issuer: _jwtConfiguration.Issuer,
               audience: _jwtConfiguration.Audience,
               expires: DateTime.UtcNow.AddDays(_jwtConfiguration.AccessTokenExpiryDays),
               claims: await GetTokenClaimsAsync(user),
               signingCredentials: GetSigningCredentials()
            );

            return new AccessTokenDTO()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ExpiresAt = token.ValidTo
            };
        }

        private SigningCredentials GetSigningCredentials()
        {
            SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(_jwtConfiguration.Key ?? "sz8eI7OdHBrjrIo8j9nTW/rQyO1OvY0pAQ2wDKQZw/0="));
            return new(key, SecurityAlgorithms.HmacSha256);
        }

        #endregion Generate Access Token

        #region Get Claims

        private async Task<IList<Claim>> GetTokenClaimsAsync(ApplicationUser user)
        {
            List<Claim> claims =
            [
                new Claim(ClaimTypes.NameIdentifier, user.Id!),
                new Claim(ClaimTypes.Email, user.Email!)
            ];

            claims.AddRange(await _userManager.GetClaimsAsync(user));
            claims.AddRange(await GetRolesClaimsAsync(user));

            return claims;
        }

        private async Task<IList<Claim>> GetRolesClaimsAsync(ApplicationUser user)
        {
            IList<string> roles = await _userManager.GetRolesAsync(user);
            IList<Claim> claims = new List<Claim>();

            foreach (string role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role));

            return claims;
        }

        #endregion Get Claims

        #region Generate Refresh Token

        public async Task<RefreshTokenDTO> GenerateRefreshTokenAsync(ApplicationUser user, bool revokeOld = false)
        {
            _refreshTokenSpecification.Criteria = r => r.UserId == user.Id && r.IsActive;
            var refreshToken = await _unitOfWork.Repository<RefreshToken>().FindAsync(_refreshTokenSpecification);

            if (refreshToken is null)
            {
                refreshToken = await GenerateRefreshTokenAsync();
                refreshToken.UserId = user.Id;
                await _unitOfWork.Repository<RefreshToken>().AddAsync(refreshToken);
            }
            else if (revokeOld)
            {
                refreshToken.RevokedAt = DateTime.UtcNow;
                refreshToken.ModifiedAt = DateTime.UtcNow;
                await _unitOfWork.SaveAsync();

                refreshToken = await GenerateRefreshTokenAsync();
                refreshToken.UserId = user.Id;
                await _unitOfWork.Repository<RefreshToken>().AddAsync(refreshToken);
            }

            return new RefreshTokenDTO()
            {
                Token = refreshToken.Token,
                ExpiresAt = refreshToken.ExpiresAt
            };
        }

        private async Task<RefreshToken> GenerateRefreshTokenAsync()
        {
            var randomNumber = new byte[32];

            using var generator = RandomNumberGenerator.Create();
            await Task.Run(() => generator.GetBytes(randomNumber));

            return new RefreshToken()
            {
                Token = Convert.ToBase64String(randomNumber),
                ExpiresAt = DateTime.UtcNow.AddDays(_jwtConfiguration.RefreshTokenExpiryDays),
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            };
        }

        #endregion Generate Refresh Token

        #endregion Methods
    }
}