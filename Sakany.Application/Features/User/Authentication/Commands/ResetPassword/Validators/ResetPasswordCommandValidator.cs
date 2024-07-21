using Sakany.Application.Features.User.Authentication.Commands.ResetPassword.Requests;
using Sakany.Domain.IdentityEntities;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace Sakany.Application.Features.User.Authentication.Commands.ResetPassword.Validators
{
    public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommandRequest>
    {
        #region Properties

        private readonly UserManager<ApplicationUser> _userManager;

        #endregion Properties

        #region Constructors

        public ResetPasswordCommandValidator(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            InitializeRules();
        }

        #endregion Constructors

        #region Methods

        private void InitializeRules()
        {
            UserIdValidator();
            TokenValidator();
            PasswordValidator();
            ConfirmNewPasswordValidator();
        }

        private void UserIdValidator()
        {
            RuleFor(request => request.Email)
                .NotEmpty().WithMessage("Email is required field.")
                .NotNull().WithMessage("Email must be not null.")
                .Matches("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$").WithMessage("Email must be valid.")
                .MustAsync(async (email, cancellationToken) => (await _userManager.FindByEmailAsync(email)) != null).WithMessage("Email already exists.");
        }

        private void TokenValidator()
        {
            RuleFor(request => request.OTP)
                .NotEmpty().WithMessage("OTP is required field.")
                .NotNull().WithMessage("OTP must be not null.");
        }

        private void PasswordValidator()
        {
            RuleFor(request => request.NewPassword)
                   .NotEmpty().WithMessage("Password is required field.")
                   .NotNull().WithMessage("Password must be not null.")
                   .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
                   .Must(password => password.Any(char.IsUpper)).WithMessage("Password must contain an uppercase letter.")
                   .Must(password => password.Any(char.IsLower)).WithMessage("Password must contain a lowercase letter.")
                   .Must(password => password.Any(char.IsDigit)).WithMessage("Password must contain a digit.")
                   .Must(password => password.Any(c => !char.IsLetterOrDigit(c))).WithMessage("Password must contain a special character.");
        }

        private void ConfirmNewPasswordValidator()
        {
            RuleFor(request => request.ConfirmNewPassword)
                   .Equal(request => request.NewPassword).WithMessage("Passwords must match.");
        }

        #endregion Methods
    }
}