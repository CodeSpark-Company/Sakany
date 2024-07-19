using Sakany.Application.DTOs.Authentication.Token;

namespace Sakany.Application.DTOs.Authentication.SignIn
{
    public class SignInDTOResponse
    {
        #region Properties

        public string? Message { get; set; }
        public bool IsAuthenticated { get; set; }
        public AccessTokenDTO? AccessToken { get; set; }
        public RefreshTokenDTO? RefreshToken { get; set; }

        #endregion Properties
    }
}