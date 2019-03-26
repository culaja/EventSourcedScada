using System;
using System.Net;
using System.Threading.Tasks;
using Common.Exceptions;
using Microsoft.AspNetCore.Http;
using static System.Net.HttpStatusCode;
using static Newtonsoft.Json.JsonConvert;

namespace WebApp.Middlewares
{
    public sealed class ExceptionToHttpResponseMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionToHttpResponseMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            var statusCodeAndMessage = ResolveStatusCodeAndMessageFrom(exception);
            context.Response.StatusCode = (int) statusCodeAndMessage.Item1;
            return context.Response.WriteAsync(statusCodeAndMessage.Item2);
        }

        private static Tuple<HttpStatusCode, string> ResolveStatusCodeAndMessageFrom(Exception exception)
        {
            switch (exception)
            {
                case var ex when ex is BadRequestException:
                    return new Tuple<HttpStatusCode, string>(BadRequest, SerializeObject(new {error = exception.Message}));
                default:
                    return new Tuple<HttpStatusCode, string>(InternalServerError, SerializeObject(new {error = "Internal server error, please contact your administrator."}));
            }
        }
    }
}