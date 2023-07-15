using Ps.EfCoreRepository.SqlServer.DependencyInjection;
using Ps.WebApiTemplate.Data.Database;

namespace Ps.WebApiTemplate.Api.AppStart
{
    public static class DatabaseService
    {
        public static WebApplicationBuilder AddDatabase(this WebApplicationBuilder builder)
        {
            builder.Services.AddEfCoreRepository<AppDbContext>(builder.Configuration, "AppDbConnection");
            return builder;
        }
    }
}
