using System.Net;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using OnlinePoll.Application.Services.ExceptionHandling;

namespace OnlinePoll.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception exception)
        {
            httpContext.Response.Clear();

            var jsonOptions = new JsonSerializerOptions()
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                WriteIndented = true
            };

            httpContext.Response.ContentType = "application/json";

            switch (exception)
            {
                case NotFoundException notFoundException:
                    httpContext.Response.StatusCode = (int) HttpStatusCode.NotFound;
                    var errorMessage = new ErrorMessage
                    {
                        Message = notFoundException.Message
                    };
                    var jsonString = JsonSerializer.Serialize(errorMessage, jsonOptions);
                    await httpContext.Response.WriteAsync(jsonString);
                    break;
                default:
                    httpContext.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                    errorMessage = new ErrorMessage
                    {
                        Message = "Ошибка сервера"
                    };
                    jsonString = JsonSerializer.Serialize(errorMessage, jsonOptions);
                    await httpContext.Response.WriteAsync(jsonString);
                    break;
            }
        }
        await httpContext.Response.CompleteAsync();
    }

    public class ErrorMessage
    {
        public string Message { get; set; }
    }
}