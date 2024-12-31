using GameOfLife.Core.UseCases.Behaviors;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace GameOfLife.Core.UseCases.DependencyInjection
{
    public static class BehaviorsExtension
    {
        public static IServiceCollection AddFailFastValidationBehavior(this IServiceCollection services, ServiceLifetime lifetime = ServiceLifetime.Scoped)
        {
            var serviceDescriptor = new ServiceDescriptor(typeof(IPipelineBehavior<,>), typeof(FailFastValidationBehavior<,>), lifetime);
            services.Add(serviceDescriptor);
            return services;
        }
    }
}
