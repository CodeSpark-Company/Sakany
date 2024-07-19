﻿namespace Sakany.Application.DTOs.Authentication.ConfirmEmail
{
    public class ConfirmEmailDTOResponse
    {
        #region Properties

        public bool IsAuthenticated { get; set; }
        public string? Message { get; set; }

        #endregion Properties
    }
}