using System.Net;
using System.Text.Json;
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

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException ex) 
        
        {
            _logger.LogWarning(ex, "Validation failed.");
            await HandleExceptionAsync(context, ex.Message, ErrorCodes.Validation, HttpStatusCode.BadRequest);
        }
        catch (AppException ex)
        {
            _logger.LogWarning(ex, "Domain validation error occurred.");
            await HandleExceptionAsync(context, ex.Message, ErrorCodes.Validation, HttpStatusCode.BadRequest);
        }
        catch (UnauthorizedAccessException ex)
        {
            _logger.LogWarning(ex, "Unauthorized access.");
            await HandleExceptionAsync(context, ex.Message, "Error.Unauthorized", HttpStatusCode.Unauthorized);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception occurred.");
            await HandleExceptionAsync(context, "An unexpected error occurred.", ErrorCodes.Unexpected, HttpStatusCode.InternalServerError);
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
