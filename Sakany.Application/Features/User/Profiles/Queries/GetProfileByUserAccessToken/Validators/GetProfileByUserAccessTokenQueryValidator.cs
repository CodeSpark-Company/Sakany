using Sakany.Application.Features.User.Profiles.Queries.GetProfileByUserAccessToken.Requests;
using FluentValidation;

namespace Sakany.Application.Features.User.Profiles.Queries.GetProfileByUserAccessToken.Validators
{
    public class GetProfileByUserAccessTokenQueryValidator : AbstractValidator<GetProfileByUserAccessTokenQueryRequest>
    {
        #region Constructors

        public GetProfileByUserAccessTokenQueryValidator()
        {
        }

        #endregion Constructors
    }
}