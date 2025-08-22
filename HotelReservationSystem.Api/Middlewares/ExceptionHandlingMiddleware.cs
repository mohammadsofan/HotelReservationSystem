using HotelReservationSystem.Api.Wrappers;
using HotelReservationSystem.Application.Exceptions;
using System.Net;
using System.Text.Json;

namespace HotelReservationSystem.Api.Middlewares
{
    public class ExceptionHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger)
        {
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception caught in middleware: {ExceptionType} - {Message}", ex.GetType().Name, ex.Message);
                await HandleExceptionsAsync(context, ex);
            }
        }

        private Task HandleExceptionsAsync(HttpContext context, Exception ex)
        {
            HttpStatusCode statusCode;
            ApiResponse response;
            switch (ex)
            {
                case NotFoundException nfException:
                    {
                        statusCode = HttpStatusCode.NotFound;
                        response = ApiResponse.Fail(nfException.Message);
                        break;
                    }
                case ValidationException validationException:
                    {
                        statusCode = HttpStatusCode.BadRequest;
                        response = ApiResponse.Fail(validationException.Message,
                            validationException.Errors.Select(e => $"{e.PropertyName}: {e.ErrorMessage}").ToList()
                        );
                        break;
                    }
                case AuthenticationException authException:
                    {
                        statusCode = HttpStatusCode.Unauthorized;
                        response = ApiResponse.Fail(authException.Message);
                        break;
                    }
                case UnauthorizedAccessException unauthorizedAccessException:
                    {
                        statusCode = HttpStatusCode.Forbidden;
                        response = ApiResponse.Fail(unauthorizedAccessException.Message);
                        break;
                    }
                case ConflictException conflictException:
                    {
                        statusCode = HttpStatusCode.Conflict;
                        response = ApiResponse.Fail(conflictException.Message);
                        break;
                    }
                default:
                    {
                        _logger.LogError(ex, "Handled exception: {ExceptionType} - {Message}", ex.GetType().Name, ex.Message);
                        statusCode = HttpStatusCode.InternalServerError;
                        response = ApiResponse.Fail($"Unexpected error occured. {ex.Message} {ex.InnerException?.Message}");
                        break;
                    }
            }
            _logger.LogWarning(ex, "Handled exception: {ExceptionType} - {Message}", ex.GetType().Name, ex.Message);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;
            var json = JsonSerializer.Serialize(response);
            return context.Response.WriteAsync(json);
        }
    }
}
