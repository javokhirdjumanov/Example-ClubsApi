using FluentValidation.TestHelper;
using Football.Domain.Exceptions;
using System.Net;
using System.Text.Json;

namespace Football.Api.Middlewares;
public class GlobalExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    public GlobalExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch(ValidationExceptions validationException)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            await HandleException(context, validationException.Message);
        }
        catch(NotFoundExcaptions notfoundException)
        {
            context.Response.StatusCode = (int)HttpStatusCode.NotFound;

            var serilizedOblect = JsonSerializer.Serialize(new
            {
                notfoundException.Message,
            });

            await HandleException(context, serilizedOblect);
        }
        catch (Exception exception)
        {
            await HandleException(context, exception.Message);
        }
    }
    private async Task HandleException(HttpContext context, string message)
    {
        context.Response.ContentType= "application/json";

        await context.Response.WriteAsync(message);
    }
}
