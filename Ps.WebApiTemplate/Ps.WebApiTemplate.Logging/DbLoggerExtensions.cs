using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;
using Ps.WebApiTemplate.Logging.Database.Models;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Ps.WebApiTemplate.Logging
{
    public static class DbLoggerExtensions
    {
        public static ILoggingBuilder AddDbLogger(this ILoggingBuilder builder)
        {
            builder.AddConfiguration();
            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, DbLoggerProvider>());
            LoggerProviderOptions.RegisterProviderOptions<DbLoggerConfiguration, DbLoggerProvider>(builder.Services);
            return builder;

        }

        public static ILoggingBuilder AddDbLogger(this ILoggingBuilder builder, Action<DbLoggerConfiguration> configure)
        {
            builder.AddDbLogger();
            builder.Services.Configure(configure);
            return builder;
        }

        public static void LogInformation(this ILogger logger, string loginUserId, string? message, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string callerName = "", params object?[] args)
        {

            MethodBase caller = new StackTrace().GetFrame(1).GetMethod();
            string callerMethodName = caller.DeclaringType.FullName;

            ActivityLog activityLog = new ActivityLog();
            activityLog.UserId = loginUserId;
            activityLog.UrlOrModule = callerMethodName ?? callerName;
            activityLog.Message = string.Format(message, args);
            activityLog.LogDateTime = DateTime.UtcNow;
            activityLog.LogLevelId = (int)LogLevel.Information;

            logger.Log<ActivityLog>(LogLevel.Information, 0, activityLog, null, LogObject);
        }
        public static void LogInformation(this ILogger logger, string loginUserId, string displayUrl, string? message, params object?[] args)
        {
            ActivityLog activityLog = new ActivityLog();
            activityLog.UserId = loginUserId;
            activityLog.UrlOrModule = displayUrl;
            activityLog.Message = string.Format(message, args);
            activityLog.LogDateTime = DateTime.UtcNow;
            activityLog.LogLevelId = (int)LogLevel.Information;
            logger.Log<ActivityLog>(LogLevel.Information, 0, activityLog, null, LogObject);
        }

        public static void LogError(this ILogger logger, string loginUserId, string displayUrl, Exception exception, string? message, params object?[] args)
        {
            ErrorLog errorLog = new ErrorLog();
            errorLog.Message = message;
            errorLog.LogLevelId = (int)LogLevel.Error;
            errorLog.ErrorDateTime = DateTime.Now;
            errorLog.UrlOrModule = displayUrl;
            errorLog.UserId = loginUserId;
            if (exception != null)
            {
                errorLog.ClassName = exception.TargetSite?.DeclaringType?.FullName;
                errorLog.MethodName = exception.TargetSite?.DeclaringType?.Name;
                errorLog.StackTrace = exception.StackTrace ?? "-";
                errorLog.ErrorType = exception.GetType()?.FullName ?? "-";
            }
            logger.Log<ErrorLog>(LogLevel.Error, 0, errorLog, exception, LogObject);
        }

        public static string LogObject(ActivityLog activityLog, Exception? ex)
        {
            return activityLog.Message;
        }
        public static string LogObject(ErrorLog error, Exception? ex)
        {
            return ex.Message;
        }
    }
}
