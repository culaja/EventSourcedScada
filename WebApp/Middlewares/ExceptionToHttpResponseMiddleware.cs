using System;
using System.Net;
using System.Security.Authentication;
using System.Threading.Tasks;
using Common.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace WebApp.Middlewares
{
    public sealed class ExceptionToHttpResponseMiddleware
    {
        private readonly RequestDelegate _next;
// TODO        private readonly ILogger _logger = Log.Logger;

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
//                LogException(ex);
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
                case var ex when ex is AuthenticationException:
                    return new Tuple<HttpStatusCode, string>(HttpStatusCode.BadRequest, JsonConvert.SerializeObject(new { error = "Username or password is incorrect." }));
                case var ex when ex is BadRequestException:
                    return new Tuple<HttpStatusCode, string>(HttpStatusCode.BadRequest, JsonConvert.SerializeObject(new { error = exception.Message }));
                default:
                    return new Tuple<HttpStatusCode, string>(HttpStatusCode.InternalServerError, JsonConvert.SerializeObject(new { error = "Internal server error, please contact your administrator." }));
            }
        }

//        private void LogException(Exception ex)
//        {
//            _logger.Error(ex, "Unhandled exception");
//        }
    }
}