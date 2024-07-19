using Sakany.Application.DTOs.Authentication.Token;
using Sakany.Domain.IdentityEntities;

namespace Sakany.Application.Interfaces.Services.Authentication
{
    public interface ITokenService
    {
        Task<AccessTokenDTO> GenerateAccessTokenAsync(ApplicationUser user);

        Task<RefreshTokenDTO> GenerateRefreshTokenAsync(ApplicationUser user, bool revokeOld = false);
    }
}