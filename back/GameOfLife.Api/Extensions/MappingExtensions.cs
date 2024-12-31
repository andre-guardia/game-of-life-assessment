namespace GameOfLife.Api.Extensions
{
    public static class MappingExtensions
    {
        public static IServiceCollection AddMapping(this IServiceCollection services)
        {
            var asemblies = AppDomain.CurrentDomain
                                     .GetAssemblies()
                                     .Where(a => a.FullName.Contains("GameOfLife"))
                                     .Where(a => !a.FullName.Contains("Core"))
                                     .ToList();
            services.AddAutoMapper(asemblies);
            return services;
        }
    }
}
