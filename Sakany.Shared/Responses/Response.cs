﻿using System.Net;

namespace Sakany.Shared.Responses
{
    public class Response<T>
    {
        #region Properties

        public HttpStatusCode? StatusCode { get; set; }
        public string? Message { get; set; }
        public bool? Succeeded { get; set; }
        public T? Data { get; set; }

        #endregion Properties

        #region Constructors

        public Response()
        {
        }

        public Response(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
        }

        public Response(HttpStatusCode statusCode, string message)
        {
            StatusCode = statusCode;
            Message = message;
        }

        public Response(HttpStatusCode statusCode, T data)
        {
            StatusCode = statusCode;
            Data = data;
        }

        public Response(HttpStatusCode statusCode, string message, T data)
        {
            StatusCode = statusCode;
            Message = message;
            Data = data;
        }

        #endregion Constructors
    }
}