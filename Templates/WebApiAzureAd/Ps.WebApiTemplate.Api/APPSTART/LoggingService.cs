using $ext_projectname$.Logging.DependencyInjection;

namespace $safeprojectname$.AppStart
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
