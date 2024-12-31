using GameOfLife.Application.UseCases.CreateBoard;
using GameOfLife.Application.UseCases.GetBoard;
using GameOfLife.Application.UseCases.MoveBoardState;
using GameOfLife.Application.UseCases.RestartBoard;
using Microsoft.Extensions.DependencyInjection;

namespace GameOfLife.Application.Extensions
{
    public static class ApplicationExtenions
    {
        public static IServiceCollection AddUseCases(this IServiceCollection services)
        {
            services.AddScoped<IGetBoardUseCase, GetBoard>();
            services.AddScoped<ICreateBoardUseCase, CreateBoard>();
            services.AddScoped<IMoveBoardStateUseCase, MoveBoardState>();
            services.AddScoped<IRestartBoardUseCase, RestartBoard>();

            return services;
        }
    }
}
