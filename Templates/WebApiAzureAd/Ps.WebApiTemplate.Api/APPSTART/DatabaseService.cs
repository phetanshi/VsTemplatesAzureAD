using Microsoft.EntityFrameworkCore;
using $ext_projectname$.Data.Database;

namespace $safeprojectname$.AppStart
{
    public static class DatabaseService
    {
        public static WebApplicationBuilder AddDatabase(this WebApplicationBuilder builder)
        {
            builder.Services.AddSqlServerDatabase(builder.Configuration);
            return builder;
        }
        private static IServiceCollection AddSqlServerDatabase(this IServiceCollection services, IConfiguration config)
        {
            string connStr = config.GetConnectionString("AppDbConnection");
            services.AddDbContextPool<AppDbContext>(options => options.UseSqlServer(connStr));
            return services;
        }

        
    }
}
