using Microsoft.EntityFrameworkCore;
using Ps.Donet.EFCoreRepository.DependencyInjection;
using Ps.WebApiTemplate.Data.Database;
//using Ps.WebApiTemplate.Data.DependencyInjection;

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
