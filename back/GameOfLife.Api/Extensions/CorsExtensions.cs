namespace GameOfLife.Api.Extensions
{
    public static class CorsExtensions
    {
        private const string CORS_POLICY_NAME = "DefaultPolicy";

        public static IServiceCollection AddAppCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: CORS_POLICY_NAME,
                                  builder => builder.AllowAnyOrigin()
                                                    .AllowAnyMethod()
                                                    .AllowAnyHeader()
                                  );
            });

            return services;
        }

        public static IApplicationBuilder UseAppCors(this IApplicationBuilder app)
        {
            app.UseCors(CORS_POLICY_NAME);
            return app;
        }
    }

}
