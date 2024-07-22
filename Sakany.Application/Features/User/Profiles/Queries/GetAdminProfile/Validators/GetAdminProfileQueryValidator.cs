using FluentValidation;
using Sakany.Application.Features.User.Profiles.Queries.GetAdminProfile.Requests;

namespace Sakany.Application.Features.User.Profiles.Queries.GetAdminProfile.Validators
{
    public class GetAdminProfileQueryValidator : AbstractValidator<GetAdminProfileQueryRequest>
    {
        #region Constructors

        public GetAdminProfileQueryValidator()
        {
        }

        #endregion Constructors
    }
}