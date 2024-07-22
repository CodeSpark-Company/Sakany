using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Sakany.Application.Features.User.Profiles.Commands.UpdateAdminProfile.Requests;
using Sakany.Domain.Constants.Media;
using Sakany.Domain.IdentityEntities;

namespace Sakany.Application.Features.User.Profiles.Commands.UpdateAdminProfile.Validators
{
    public class UpdateAdminProfileCommandValidator : AbstractValidator<UpdateAdminProfileCommandRequest>
    {
        #region Properties

        private readonly UserManager<ApplicationUser> _userManager;

        #endregion Properties

        #region Constructors

        public UpdateAdminProfileCommandValidator(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            InitializeRules();
        }

        #endregion Constructors

        #region Methods

        private void InitializeRules()
        {
            UsernameValidator();
            FirstNameValidator();
            LastNameValidator();
            PhoneNumberValidator();
            BirthDateValidator();
            BioValidator();
            ImageValidator();
            CivilIdValidator();
        }

        private void UsernameValidator()
        {
            RuleFor(request => request.UserName)
             .MustAsync(async (username, cancellationToken) => username == null || await _userManager.FindByNameAsync(username) == null).WithMessage("Username already exists.");
        }

        private void FirstNameValidator()
        {
            RuleFor(request => request.FirstName)
                .MaximumLength(100).WithMessage("FirstName maximum length must be 100.");
        }

        private void LastNameValidator()
        {
            RuleFor(request => request.LastName)
                .MaximumLength(100).WithMessage("LastName maximum length must be 100.");
        }

        private void PhoneNumberValidator()
        {
            RuleFor(request => request.PhoneNumber)
                .MaximumLength(30).WithMessage("PhoneNumber maximum length must be 30.");
        }

        private void BirthDateValidator()
        {
            //RuleFor(request => request.BirthDate)
            //    .Must(birthDate => BeLessThanEighteenYearsOld(birthDate))
            //    .WithMessage("User must be 18 years or older.");
        }

        private bool BeLessThanEighteenYearsOld(DateOnly? birthDate)
        {
            if (birthDate == null)
                return true; // Handle null birth date

            // Calculate the minimum allowed birth date using DateOnly
            DateOnly minimumBirthDate = DateOnly.FromDateTime(DateTime.UtcNow).AddYears(-18);

            // Ensure the birth date is less than the minimum allowed date
            return birthDate < minimumBirthDate;
        }

        private void BioValidator()
        {
            RuleFor(request => request.Bio)
                .MaximumLength(1000).WithMessage("Bio maximum length must be 100.");
        }

        private void ImageValidator()
        {
            // IFormFile Image
            RuleFor(request => request.Image)
                .Cascade(CascadeMode.Stop)
                .Must(IsImageValidSize).WithMessage("Image must be less than 5 MB.")
                .Must(IsImageValidExtension).WithMessage("Image must be a valid format (jpg, jpeg, png).")
                .Must(IsImageValidMimeType).WithMessage("Image MIME type must be valid (image/jpeg, image/png).");
        }

        private bool IsImageValidSize(IFormFile? file)
        {
            return file == null || file.Length <= Image.MaxSizeInBytes;
        }

        private bool IsImageValidExtension(IFormFile? file)
        {
            var allowedExtensions = Image.AllowedExtensions;
            var extension = Path.GetExtension(file?.FileName)?.ToLower();
            return file == null || allowedExtensions.Contains(extension);
        }

        private bool IsImageValidMimeType(IFormFile? file)
        {
            var allowedMimeTypes = Image.AllowedMimeTypes;
            return file == null || allowedMimeTypes.Contains(file.ContentType);
        }

        private void CivilIdValidator()
        {
            RuleFor(request => request.CivilId)
              .Cascade(CascadeMode.Stop)
              .Must(IsDocumentValidSize).WithMessage("Civil ID must be less than 5 MB.")
              .Must(IsDocumentValidExtension).WithMessage("Civil ID must be a PDF file (.pdf).")
              .Must(IsDocumentValidMimeType).WithMessage("Civil ID MIME type must be valid (application/pdf).");
        }

        private bool IsDocumentValidSize(IFormFile? file)
        {
            return file == null || file.Length <= Document.MaxSizeInBytes;
        }

        private bool IsDocumentValidExtension(IFormFile? file)
        {
            var allowedExtensions = Document.AllowedExtensions;
            var extension = Path.GetExtension(file?.FileName)?.ToLower();
            return file == null || allowedExtensions.Contains(extension);
        }

        private bool IsDocumentValidMimeType(IFormFile? file)
        {
            var allowedMimeTypes = Document.AllowedMimeTypes;
            return file == null || allowedMimeTypes.Contains(file.ContentType);
        }

        #endregion Methods
    }
}