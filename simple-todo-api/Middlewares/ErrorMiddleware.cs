using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Net;
using System.Text.Json;

namespace simple_todo_api.Middlewares
{
    public class ErrorMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorMiddleware> _logger;

        public ErrorMiddleware(RequestDelegate next, ILogger<ErrorMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An exception occurred.");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            HttpStatusCode statusCode;
            string errorMessage;

            if (IsEntityFrameworkException(exception))
            {
                (statusCode, errorMessage) = HandleEFCoreException(exception);
            }
            else
            {
                // General exception handling
                statusCode = HttpStatusCode.InternalServerError;
                errorMessage = "An unexpected error occurred. Please try again later.";
            }

            var errorResponse = new
            {
                Message = errorMessage
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var jsonResponse = JsonSerializer.Serialize(errorResponse);
            await context.Response.WriteAsync(jsonResponse);
        }

        private static bool IsEntityFrameworkException(Exception ex)
        {
            return ex is DbUpdateException || ex.InnerException is PostgresException;
        }

        private static (HttpStatusCode, string) HandleEFCoreException(Exception exception)
        {
            if (exception is DbUpdateException dbUpdateEx && dbUpdateEx.InnerException is PostgresException postgresEx)
            {
                // Handle PostgreSQL-specific errors
                return postgresEx.SqlState switch
                {
                    PostgresErrorCodes.UniqueViolation => (HttpStatusCode.Conflict, "A record with the same key already exists."),
                    PostgresErrorCodes.ForeignKeyViolation => (HttpStatusCode.BadRequest, "A foreign key constraint was violated."),
                    PostgresErrorCodes.CheckViolation => (HttpStatusCode.BadRequest, "A check constraint was violated."),
                    _ => (HttpStatusCode.InternalServerError, "A database error occurred.")
                };
            }

            // General EF Core exception
            return (HttpStatusCode.InternalServerError, "An unexpected Entity Framework error occurred.");
        }
    }
}