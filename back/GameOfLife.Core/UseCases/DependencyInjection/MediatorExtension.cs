using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace GameOfLife.Core.UseCases.DependencyInjection
{
    public static class MediatorExtension
    {
        public static IServiceCollection AddMediatorToUseCases(this IServiceCollection services, string partOfAssemblyName = "Application")
        {
            services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies()
                .SingleOrDefault(assembly => assembly.GetName().Name.Contains(partOfAssemblyName)));

            return services;
        }
    }
}
