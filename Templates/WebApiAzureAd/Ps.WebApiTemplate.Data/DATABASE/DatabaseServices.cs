using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ps.EfCoreRepository.SqlServer.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace $safeprojectname$.Database
{
    public static class DatabaseServices
    {
        public static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddEfCoreRepository<AppDbContext>(configuration, "AppDbConnection");
        }
    }
}
