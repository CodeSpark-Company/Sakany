﻿using FluentValidation;
using Sakany.Application.Features.User.Authentication.Commands.GetRefreshToken.Requests;
using Sakany.Application.Interfaces.Specifications.Base;
using Sakany.Application.Interfaces.UnitOfWork;
using Sakany.Domain.Entities.Security;

namespace Sakany.Application.Features.User.Authentication.Commands.GetRefreshToken.Validators
{
    public class GetRefreshTokenCommandValidator : AbstractValidator<GetRefreshTokenCommandRequest>
    {
        #region Properties

        private readonly IUnitOfWork _unitOfWork;
        private readonly IBaseSpecification<RefreshToken> _refreshTokenSpecification;

        #endregion Properties

        #region Constructors

        public GetRefreshTokenCommandValidator(IUnitOfWork unitOfWork, IBaseSpecification<RefreshToken> refreshTokenSpecification)
        {
            _unitOfWork = unitOfWork;
            _refreshTokenSpecification = refreshTokenSpecification;
            InitializeRules();
        }

        #endregion Constructors

        #region Methods

        private void InitializeRules()
        {
            RefreshTokenValidator();
        }

        private void RefreshTokenValidator()
        {
            RuleFor(request => request.RefreshToken)
                .NotEmpty().WithMessage("RefreshToken is required field.")
                .NotNull().WithMessage("RefreshToken must be not null.")
                .MustAsync(async (token, cancellationToken) =>
                {
                    _refreshTokenSpecification.Criteria = r => r.Token == token && r.IsActive;
                    var refreshToken = await _unitOfWork.Repository<RefreshToken>().FindAsNoTrackingAsync(_refreshTokenSpecification);
                    return refreshToken is not null;
                }).WithMessage("RefreshToken is expired or invalid.");
        }

        #endregion Methods
    }
}