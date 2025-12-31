using Microsoft.AspNetCore.Diagnostics;

namespace Monetra.Api.Extensions;

public static class GlobalExceptionHandler 
{
    public static void UseGlobalExceptionHandler(this WebApplication app)
    {
        app.UseExceptionHandler(x
            => x.Run(async context =>
            {
                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";
                var ErrorType = context.Features.Get<IExceptionHandlerFeature>();

                if (ErrorType is not null)
                {
                    var logger = context.RequestServices
                        .GetRequiredService<ILoggerFactory>()
                        .CreateLogger("GlobalExceptionHandler");
                    var menssage = $"\nMenssage - {ErrorType.Error.Message}\n StackTrace - {ErrorType.Error.StackTrace}";
                    logger.Log(
                        LogLevel.Critical,
                        ErrorType.Error,
                        menssage ,
                        context.Request.Path
                        );
                    
                    await context.Response.WriteAsJsonAsync(new
                    {
                        Menssage = ErrorType.Error.Message,
                        StackTrace = ErrorType.Error.StackTrace
                    });
                }
            }));
    }
}