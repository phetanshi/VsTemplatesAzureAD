using Microsoft.AspNetCore.Authorization;
using $safeprojectname$.Auth;
using $safeprojectname$.AutoMapperProfiles;
using $safeprojectname$.Services;
using $safeprojectname$.Services.Definitions;

namespace $safeprojectname$.AppStart
{
    public static class ApplicationObjects
    {
        public static IServiceCollection AddApplicationObjects(this IServiceCollection services)
        {
            services.AddServiceDependencies();
            services.AddOthes();
            return services;
        }

        private static void AddServiceDependencies(this IServiceCollection services)
        {
            services.AddScoped<ISampleService, SampleService>();
        }
        private static void AddOthes(this IServiceCollection services)
        {
            services.AddScoped<IAuthorizationHandler, AppAuthorizationHandler>();
            services.AddAutoMapper(typeof(EmployeeAutoMapperProfile));
        }
    }
}
