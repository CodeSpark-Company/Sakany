using FluentValidation;
using Sakany.Application.Features.User.Profiles.Queries.GetRealtorProfile.Requests;

namespace Sakany.Application.Features.User.Profiles.Queries.GetRealtorProfile.Validators
{
    public class GetRealtorProfileQueryValidator : AbstractValidator<GetRealtorProfileQueryRequest>
    {
        #region Constructors

        public GetRealtorProfileQueryValidator()
        {
        }

        #endregion Constructors
    }
}