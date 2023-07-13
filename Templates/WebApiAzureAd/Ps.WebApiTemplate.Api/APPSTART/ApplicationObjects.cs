using $safeprojectname$.Services;
using $safeprojectname$.Services.Definitions;
using $ext_projectname$.Data;
using $ext_projectname$.Data.Definitions;
using $ext_projectname$.Logging;

namespace $safeprojectname$.AppStart
{
    public static class ApplicationObjects
    {
        public static IServiceCollection AddApplicationObjects(this IServiceCollection services)
        {
            services.AddServiceDependencies();
            services.AddRepository();
            services.AddOthes();
            return services;
        }

        private static void AddServiceDependencies(this IServiceCollection services)
        {
            services.AddScoped<ISampleService, SampleService>();
        }
        private static void AddRepository(this IServiceCollection services)
        {
            services.AddScoped<IRepository, AppRepository>();
        }
        private static void AddOthes(this IServiceCollection services)
        {
            services.AddScoped<DbLoggerProvider>();
        }
    }
}
