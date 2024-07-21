namespace Sakany.Application.DTOs.Authentication.ResetPassword
{
    public class ResetPasswordDTORequest
    {
        #region Properties

        public string Email { get; set; } = default!;
        public string OTP { get; set; } = default!;
        public string NewPassword { get; set; } = default!;
        public string ConfirmNewPassword { get; set; } = default!;

        #endregion Properties
    }
}