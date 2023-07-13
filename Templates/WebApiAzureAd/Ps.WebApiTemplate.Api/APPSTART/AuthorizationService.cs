namespace $safeprojectname$.AppStart
{
    public static class AuthorizationService
    {
        public static IServiceCollection AddAppAuthorization(this IServiceCollection services, IConfiguration config)
        {
            services.AddAuthorization();
            return services;
        }
    }
}
