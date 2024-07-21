using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Sakany.Application.DTOs.Authentication.ConfirmEmail;
using Sakany.Application.DTOs.Authentication.ForgetPassword;
using Sakany.Application.DTOs.Authentication.ResetPassword;
using Sakany.Application.DTOs.Authentication.SignIn;
using Sakany.Application.DTOs.Authentication.SignUp;
using Sakany.Application.DTOs.Authentication.Token;
using Sakany.Application.Interfaces.Services.Authentication;
using Sakany.Application.Interfaces.Services.Mail;
using Sakany.Application.Interfaces.UnitOfWork;
using Sakany.Domain.Entities.Users;
using Sakany.Domain.IdentityEntities;
using Sakany.Shared.Helpers.MailConfiguration;
using System.Net;
using System.Net.Mail;
using System.Text.Encodings.Web;

namespace Sakany.Infrastructure.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        #region Properties

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMailService _mailService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        #endregion Properties

        #region Constructors

        public AuthenticationService(UserManager<ApplicationUser> userManager, ITokenService tokenService, IMapper mapper, IUnitOfWork unitOfWork, IMailService mailService, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _mailService = mailService;
            _httpContextAccessor = httpContextAccessor;
        }

        #endregion Constructors

        #region Methods

        private async Task SendConfirmationEmailAsync(ApplicationUser user)
        {
            // Build Confirmation Mail
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var encodedToken = WebUtility.UrlEncode(token);
            var confirmationUrl = $"{_httpContextAccessor.HttpContext!.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}/Api/V1/User/Authentication/ConfirmEmail?userId={user.Id}&token={encodedToken}";

            // Send Confirmation Mail
            var mailUserName = new MailAddress(user.Email!).User;
            var mailData = new MailData(
               mailUserName,
               user.Email!,
               "Confirm Your Email",
               $@"
                    <html>
                        <body>
                            <h2>Confirm Your Email</h2>
                            <p>Dear {mailUserName},</p>
                            <p>Please confirm your account by following this link: <a href='{HtmlEncoder.Default.Encode(confirmationUrl)}'>Confirm Email</a></p>
                            <p>Best regards,<br/>Sakany Inc.</p>
                        </body>
                    </html>"
                );

            await _mailService.SendAsync(mailData);
        }

        public async Task<SignUpDTOResponse> SignUpAsync(SignUpDTORequest request)
        {
            var user = _mapper.Map<ApplicationUser>(request);

            var mailUserName = new MailAddress(user.Email!).User;
            user.UserName = $"{mailUserName}-{user.Id}";

            // Create User
            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                return new SignUpDTOResponse
                {
                    IsAuthenticated = false,
                    Message = "User creation failed."
                };
            }

            await _unitOfWork.Repository<UserProfile>().AddAsync(new UserProfile() { UserId = user.Id, Bio = string.Empty });

            await _userManager.AddToRoleAsync(user, request.Role);

            await SendConfirmationEmailAsync(user);

            return new SignUpDTOResponse
            {
                IsAuthenticated = true,
                Message = "User created successfully. Please check your email to confirm your account."
            };
        }

        public async Task<SignInDTOResponse> SignInAsync(SignInDTORequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user is null || !await _userManager.CheckPasswordAsync(user, request.Password))
            {
                return await Task.FromResult(new SignInDTOResponse()
                {
                    IsAuthenticated = false,
                    Message = "Invalid Email or Password!"
                });
            }

            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                return await Task.FromResult(new SignInDTOResponse()
                {
                    IsAuthenticated = false,
                    Message = "Email not Confirmed!"
                });
            }

            // Build Response
            return new SignInDTOResponse()
            {
                Message = "SignIn Successfully.",
                IsAuthenticated = true,
                AccessToken = await _tokenService.GenerateAccessTokenAsync(user),
                RefreshToken = await _tokenService.GenerateRefreshTokenAsync(user),
            };
        }

        public async Task<AccessTokenDTO> GetAccessTokenAsync(string refreshToken)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(user => user.RefreshTokens.Any(r => r.Token == refreshToken));

            return await _tokenService.GenerateAccessTokenAsync(user!);
        }

        public async Task<RefreshTokenDTO> GetRefreshTokenAsync(string refreshToken)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(user => user.RefreshTokens.Any(r => r.Token == refreshToken));

            return await _tokenService.GenerateRefreshTokenAsync(user!, true);
        }

        public async Task<ConfirmEmailDTOResponse> ConfirmEmailAsync(ConfirmEmailDTORequest request)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);

            var token = WebUtility.UrlDecode(request.Token).Replace(" ", "+");
            var result = await _userManager.ConfirmEmailAsync(user!, token);

            if (!result.Succeeded)
            {
                return new ConfirmEmailDTOResponse()
                {
                    IsAuthenticated = false,
                    Message = "Email Confirmation Failed!"
                };
            }

            return new ConfirmEmailDTOResponse()
            {
                IsAuthenticated = true,
                Message = "Email Confirmation Successfully."
            };
        }

        private string GenerateOTP()
        {
            var random = new Random();
            return random.Next(100000, 999999).ToString();
        }

        public async Task<ForgetPasswordDTOResponse> ForgetPasswordAsync(ForgetPasswordDTORequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            // Build Confirmation Mail
            var otp = GenerateOTP();
            user!.ResetPasswordOTP = otp;
            user.ResetPasswordOTPExpiresAt = DateTime.UtcNow.AddHours(2);
            await _userManager.UpdateAsync(user);

            // Send Confirmation Mail
            var mailUserName = new MailAddress(user!.Email!).User;

            var mailData = new MailData(
                mailUserName,
                user.Email!,
                "Reset Your Password",
            $@"
                <html>
                    <body>
                        <h2>Reset Your Password</h2>
                        <p>Dear {mailUserName},</p>
                        <p>Your OTP for password reset is: <b>{otp}</b></p>
                        <p>This OTP is valid for 2 hours.</p>
                        <p>Best regards,<br/>Sakany Inc.</p>
                    </body>
                </html>"
            );
            await _mailService.SendAsync(mailData);

            return new ForgetPasswordDTOResponse
            {
                Message = "Please check your email for the OTP to reset your password."
            };
        }

        public async Task<ResetPasswordDTOResponse> ResetPasswordAsync(ResetPasswordDTORequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null || user.ResetPasswordOTP != request.OTP || user.ResetPasswordOTPExpiresAt < DateTime.UtcNow)
            {
                return new ResetPasswordDTOResponse
                {
                    IsAuthenticated = false,
                    Message = "OTP is Invalid or Expired!",
                };
            }

            var result = await _userManager.RemovePasswordAsync(user);
            if (!result.Succeeded)
            {
                return new ResetPasswordDTOResponse()
                {
                    IsAuthenticated = false,
                    Message = "Reset Password Failed!",
                };
            }

            result = await _userManager.AddPasswordAsync(user, request.NewPassword);
            if (!result.Succeeded)
            {
                return new ResetPasswordDTOResponse()
                {
                    IsAuthenticated = false,
                    Message = "Reset Password Failed!",
                };
            }

            user.ResetPasswordOTP = null;
            user.ResetPasswordOTPExpiresAt = null;
            await _userManager.UpdateAsync(user);

            // Build Response
            return new ResetPasswordDTOResponse()
            {
                IsAuthenticated = true,
                Message = "Password Reset Successfully.",
            };
        }

        #endregion Methods
    }
}