using Ps.EfCoreRepository.SqlServer.DependencyInjection;
using Ps.WebApiTemplate.Data.Database;

namespace Ps.WebApiTemplate.Api.AppStart
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
