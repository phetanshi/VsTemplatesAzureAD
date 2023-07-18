using Microsoft.AspNetCore.Authorization;
using Ps.WebApiTemplate.Api.Auth;
using Ps.WebApiTemplate.Api.AutoMapperProfiles;
using Ps.WebApiTemplate.Api.Services;
using Ps.WebApiTemplate.Api.Services.Definitions;

namespace Ps.WebApiTemplate.Api.AppStart
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
