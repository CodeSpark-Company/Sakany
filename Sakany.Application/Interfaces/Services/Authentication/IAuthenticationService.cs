using Sakany.Application.DTOs.Authentication.ConfirmEmail;
using Sakany.Application.DTOs.Authentication.ForgetPassword;
using Sakany.Application.DTOs.Authentication.ResetPassword;
using Sakany.Application.DTOs.Authentication.SignIn;
using Sakany.Application.DTOs.Authentication.SignUp;
using Sakany.Application.DTOs.Authentication.Token;

namespace Sakany.Application.Interfaces.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<SignUpDTOResponse> SignUpAsync(SignUpDTORequest request);

        Task<SignInDTOResponse> SignInAsync(SignInDTORequest request);

        Task<AccessTokenDTO> GetAccessTokenAsync(string refreshToken);

        Task<RefreshTokenDTO> GetRefreshTokenAsync(string refreshToken);

        Task<ConfirmEmailDTOResponse> ConfirmEmailAsync(ConfirmEmailDTORequest request);

        Task<ForgetPasswordDTOResponse> ForgetPasswordAsync(ForgetPasswordDTORequest request);

        Task<ResetPasswordDTOResponse> ResetPasswordAsync(ResetPasswordDTORequest request);
    }
}