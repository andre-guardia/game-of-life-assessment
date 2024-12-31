using GameOfLife.Domain.Contexts;
using GameOfLife.Repository.Contexts;
using GameOfLife.Repository.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace GameOfLife.Repository.Extensions
{
    public static class RepositoryExtensions
    {
        public static IServiceCollection AddContext(this IServiceCollection services)
        {
            services.AddSingleton(new BoardContext());
            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IBoardRepository, BoardRepository>();
            return services;
        }
    }
}
