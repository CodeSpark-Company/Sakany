using Sakany.Application.Features.User.Profiles.Queries.GetSuperAdminProfile.Requests;
using FluentValidation;

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