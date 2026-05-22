using Application.Exceptions;
using System.Net;
using System.Text.Json;

namespace Proyecto_Software.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger, RequestDelegate next)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex) { 
                _logger.LogError(ex, "An error occurred while processing the request.");
                
            }
        }

        public static Task handleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            var statusCode = HttpStatusCode.InternalServerError;
            var message = "An internal error occurred on the server.";

            if(ex is ConcurrencyException)
            {
                statusCode = HttpStatusCode.Conflict;
                message = ex.Message;
            }
            else if (ex is KeyNotFoundException) 
            {
                statusCode = HttpStatusCode.NotFound; 
                message = ex.Message;
            }

            context.Response.StatusCode = (int)statusCode;

            var response = new
            {
                status = context.Response.StatusCode,
                error = message,
                detailed = ex.InnerException?.Message 
            };
            var jsonResponse = JsonSerializer.Serialize(response, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            return context.Response.WriteAsync(jsonResponse);
        }
    }
}
