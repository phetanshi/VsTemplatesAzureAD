using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Ps.WebApiTemplate.Logging.Database;
using Ps.WebApiTemplate.Logging.Database.Models;

namespace Ps.WebApiTemplate.Logging
{
    public class DbLogger : ILogger
    {
        private readonly string name;
        private readonly DbLoggerConfiguration logConfig;
        private readonly AppLoggingDbContext loggerDbContext;

        public DbLogger(string name, IConfiguration configuration, DbLoggerConfiguration logConfig)
        {
            this.name = name;
            this.logConfig = logConfig;
            loggerDbContext = new AppLoggingDbContext(configuration);
        }
        public DbLogger(IConfiguration configuration, DbLoggerConfiguration logConfig)
        {
            this.logConfig = logConfig;
            loggerDbContext = new AppLoggingDbContext(configuration);
        }
        public IDisposable BeginScope<TState>(TState state)
        {
            return default;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logConfig.MinimumLogLevel <= logLevel ;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            try
            {
                if (!IsEnabled(logLevel))
                    return;

                string? message = formatter(state, exception);
                switch (logLevel)
                {
                    case LogLevel.Trace:
                    case LogLevel.Debug:
                    case LogLevel.Information:
                    case LogLevel.Warning:
                    case LogLevel.Critical:
                        ActivityLog logObj = ReadObj<TState, ActivityLog>(state);
                        if(logObj == null)
                        {
                            logObj = new ActivityLog();
                            logObj.UserId = "no-user";
                            logObj.Message = message;
                            logObj.UrlOrModule = "-";
                            logObj.LogDateTime = DateTime.UtcNow;
                        }
                        LogActivity(logObj);
                        break;
                    case LogLevel.Error:
                        ErrorLog error = ReadObj<TState, ErrorLog>(state);
                        if (error == null)
                            LogError(message, exception);
                        else
                            LogError(error);
                        break;
                }
            }
            catch(Exception ex)
            {

            }
        }

        private TResult ReadObj<TState, TResult>(TState state)
        {
            try
            {
                var result = (TResult)Convert.ChangeType(state, typeof(TResult));
                return result;
            }
            catch (Exception ex)
            {
            }
            return default(TResult);
        }
        private void LogActivity(ActivityLog activityLog)
        {
            try
            {
                loggerDbContext.ActivityLogs.Add(activityLog);
                loggerDbContext.SaveChanges();
            }
            catch(Exception ex)
            {

            }
        }

        private void LogError(ErrorLog errorLog)
        {
            try
            {
                loggerDbContext.ErrorLogs.Add(errorLog);
                loggerDbContext.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }
        private void LogError(string message, Exception? exception)
        {
            try
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.Message = message;
                errorLog.LogLevelId = (int)LogLevel.Error;
                errorLog.ErrorDateTime = DateTime.Now;
                errorLog.UrlOrModule = "-";
                errorLog.UserId = "not-login";
                if (exception != null)
                {
                    errorLog.ClassName = exception.TargetSite?.DeclaringType?.FullName;
                    errorLog.MethodName = exception.TargetSite?.DeclaringType?.Name;
                    errorLog.StackTrace = exception.StackTrace ?? "-";
                    errorLog.ErrorType = exception.GetType()?.FullName ?? "-";
                }

                loggerDbContext.ErrorLogs.Add(errorLog);
                loggerDbContext.SaveChanges();

                if (exception?.InnerException != null)
                    LogError(exception.InnerException.Message, exception.InnerException);
            }
            catch { }
        }
    }
}
