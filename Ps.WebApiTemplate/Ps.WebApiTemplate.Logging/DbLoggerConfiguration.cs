using Microsoft.Extensions.Logging;

namespace Ps.WebApiTemplate.Logging
{
    public class DbLoggerConfiguration
    {
        public int EventId { get; set; }
        public LogLevel MinimumLogLevel { get; set; } = LogLevel.Information;
        public string ConnectionString { get; set; }
        public string LoginUserId { get; set; }
    }
}