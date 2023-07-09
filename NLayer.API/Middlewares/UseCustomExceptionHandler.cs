using Microsoft.AspNetCore.Diagnostics;
using NLayer.Core.Dtos;
using NLayer.Service.Exceptions;
using System.Text.Json;

namespace NLayer.API.Middlewares
{
    public static class UseCustomExceptionHandler
    {
        public static void UseCustomException(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(opt =>
            {
                opt.Run(async context =>
                {
                    context.Response.ContentType = "application/json";
                    var exceptionFeatures = context.Features.Get<IExceptionHandlerFeature>();
                    var statusCode = exceptionFeatures.Error switch
                    {
                        ClientSideException => 400,
                        NotFoundException => 404,
                        _ => 500
                    };
                    context.Response.StatusCode = statusCode;
                    var response = CustomResponseDto<NoContentDto>.Fail(statusCode, exceptionFeatures.Error.Message);

                    await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                });
            });
        }
    }
}
