﻿using Ps.WebApiTemplate.Logging.DependencyInjection;

namespace Ps.WebApiTemplate.Api.AppStart
{
    public static class LoggingService
    {
        public static WebApplicationBuilder AddLogging(this WebApplicationBuilder builder)
        {
            builder.Logging.AddAppLogger(builder.Services, builder.Configuration);
            return builder;
        }
    }
}
