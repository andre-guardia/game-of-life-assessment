using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace GameOfLife.Core.UseCases.DependencyInjection
{
    public static class ValidatorsExtension
    {
        public static IServiceCollection AddValidators(this IServiceCollection services, string partOfAssemblyName = "Application")
        {
            AssemblyScanner.FindValidatorsInAssembly(AppDomain.CurrentDomain.GetAssemblies()
                    .SingleOrDefault(assembly => assembly.GetName().Name.Contains(partOfAssemblyName, StringComparison.OrdinalIgnoreCase)))
                .ForEach(result => services.AddScoped(result.InterfaceType, result.ValidatorType));

            return services;
        }
    }
}
