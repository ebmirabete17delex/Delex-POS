using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc; // ProblemDetails

namespace Delex_POS.Application.Common.Middlewares;

public class ValidationExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ValidationExceptionMiddleware> _logger;

    public ValidationExceptionMiddleware(RequestDelegate next, ILogger<ValidationExceptionMiddleware> logger)
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
        catch (Exceptions.ValidationException ex)
        {
            _logger.LogWarning(ex, "Validation failed");

            var problem = new ProblemDetails
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                Title = ex.Message ?? "One or more validation failures have occurred.",
                Status = StatusCodes.Status400BadRequest,
                Detail = "See the 'errors' property for details."
            };

            // Put the validation errors in an extension property named "errors"
            problem.Extensions["errors"] = ex.Errors;

            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Response.ContentType = "application/problem+json";

            await context.Response.WriteAsJsonAsync(problem);
        }
    }
}
