﻿using Ps.WebApiTemplate.Api.Services;
using Ps.WebApiTemplate.Api.Services.Definitions;
using Ps.WebApiTemplate.Data;
using Ps.WebApiTemplate.Data.Definitions;
using Ps.WebApiTemplate.Logging;

namespace Ps.WebApiTemplate.Api.AppStart
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
