using FluentValidation;
using Sakany.Application.Features.User.Profiles.Queries.GetStudentProfile.Requests;

namespace Sakany.Application.Features.User.Profiles.Queries.GetSuperAdminProfile.Validators
{
    public class GetStudentProfileQueryValidator : AbstractValidator<GetStudentProfileQueryRequest>
    {
        #region Constructors

        public GetStudentProfileQueryValidator()
        {
        }

        #endregion Constructors
    }
}