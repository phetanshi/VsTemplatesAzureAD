﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Ps.WebApiTemplate.Logging.Database.Models;

namespace Ps.WebApiTemplate.Logging.Database
{
    public class AppLoggingDbContext : DbContext
    {
        public AppLoggingDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        public DbSet<ErrorType> ErrorTypes { get; set; }
        public DbSet<ActivityType> ActivityTypes { get; set; }
        public DbSet<ErrorLog> ErrorLogs { get; set; }
        public DbSet<ActivityLog> ActivityLogs { get; set; }
        public DbSet<AppLogLevel> AppLogLevels { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var conStr = Configuration.GetConnectionString("AppLogDbConnection");
            optionsBuilder.UseSqlServer(conStr);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
