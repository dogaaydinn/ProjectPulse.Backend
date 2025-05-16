using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Shared.Constants;
using Shared.Exceptions;

namespace API.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
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
            _logger.LogError(ex, "Unhandled exception");
            var problem = ex switch
            {
                ValidationException ve => new ProblemDetails
                {
                    Title = "Validation Error",
                    Status = (int)HttpStatusCode.BadRequest,
                    Detail = ve.Message
                },
                AppException ae => new ProblemDetails
                {
                    Title = "Application Error",
                    Status = (int)HttpStatusCode.BadRequest,
                    Detail = ae.Message
                },
                UnauthorizedAccessException => new ProblemDetails
                {
                    Title = "Unauthorized",
                    Status = (int)HttpStatusCode.Unauthorized,
                    Detail = "Access is denied."
                },
                _ => new ProblemDetails
                {
                    Title = "Server Error",
                    Status = (int)HttpStatusCode.InternalServerError,
                    Detail = "An unexpected error occurred."
                }
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = problem.Status ?? (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsJsonAsync(problem);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, string message, string code, HttpStatusCode statusCode)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var response = new
        {
            success = false,
            error = new { code, message }
        };

        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}
