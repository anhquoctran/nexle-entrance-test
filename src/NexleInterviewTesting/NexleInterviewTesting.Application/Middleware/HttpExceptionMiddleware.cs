using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace NexleInterviewTesting.Application.Middleware
{
    /// <summary>
    /// This class using for global Exception handling
    /// </summary>
    public class HttpExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public HttpExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.Headers["Accept"] = "application/json";

            if (!httpContext.Response.HasStarted)
                await _next.Invoke(httpContext);

            if (httpContext.Response.StatusCode >= (int)HttpStatusCode.Unauthorized && 
                httpContext.Response.StatusCode <= (int)HttpStatusCode.NetworkAuthenticationRequired && 
                httpContext.Response.StatusCode != (int)HttpStatusCode.InternalServerError)
            {
                var httpStatus = (HttpStatusCode)httpContext.Response.StatusCode;
                var json = ResponseUtils.GetJsonResponseString(httpStatus);
                await httpContext.Response.WriteJson(json);
            }
        }
    }

    public static class HttpExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseHttpExceptionMiddleware(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<HttpExceptionMiddleware>();

            return builder;
        }
    }

}
