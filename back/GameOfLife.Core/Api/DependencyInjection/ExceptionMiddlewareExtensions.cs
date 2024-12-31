using GameOfLife.Core.Api.Middleware;
using Microsoft.AspNetCore.Builder;

namespace GameOfLife.Core.Api.DependencyInjection
{
    public static class ExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();

            return app;
        }
    }
}
