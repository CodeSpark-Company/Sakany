using FluentValidation;
using Sakany.Application.Features.User.Profiles.Queries.GetSuperAdminProfile.Requests;

namespace Sakany.Application.Features.User.Profiles.Queries.GetSuperAdminProfile.Validators
{
    public class GetSuperAdminProfileQueryValidator : AbstractValidator<GetSuperAdminProfileQueryRequest>
    {
        #region Constructors

        public GetSuperAdminProfileQueryValidator()
        {
        }

        #endregion Constructors
    }
}