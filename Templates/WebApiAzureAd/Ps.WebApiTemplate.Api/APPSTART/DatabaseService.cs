using Ps.EfCoreRepository.SqlServer.DependencyInjection;
using $ext_projectname$.Data.Database;

namespace $safeprojectname$.AppStart
{
    public static class DatabaseService
    {
        public static WebApplicationBuilder AddDatabase(this WebApplicationBuilder builder)
        {
            builder.Services.AddDatabase(builder.Configuration);
            return builder;
        }
    }
}
