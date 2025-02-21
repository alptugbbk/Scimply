using DbScimplyAPI.Application.Abstractions.Services;
using DbScimplyAPI.Application.Repositories;
using DbScimplyAPI.Domain.Entities.Log;
using DbScimplyAPI.Domain.Entities.User;
using DbScimplyAPI.Domain.Enums.Log;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DbScimplyAPI.Persistence.Concretes.Services
{
    public class LoggerService : ILoggerService
    {

        private readonly ILoggerRepository _loggerRepository;
        private readonly string _logDirectory = @"C:\CdCenter\csProjects\Scimply\DbScimplyAPI\Presentation\DbScimplyAPI.API\DbScimplyAPI.API.csproj\Logs";
        private const long MaxFileSize = 5 * 1024 * 1024;

        public LoggerService(ILoggerRepository loggerRepository)
        {

            _loggerRepository = loggerRepository;
        }


        private async Task AddLog(LogLevel level, string message, string tableName = null, string data = null, Exception exception = null, string userId = null)
        {

            var log = new Logs
            {
                LogLevel = level.ToString(),
                Timestamp = DateTime.UtcNow,
                TableName = tableName,
                Message = message,
                Data = data,
                Exception = exception.ToString(),
                UserId = userId
            };

            //debug
            if(level == LogLevel.Debug)
            {
                Debug.WriteLine($"{{ \"timestamp\": \"{log.Timestamp}\", \"logLevel\": \"{log.LogLevel}\", \"tableName\": \"{log.TableName}\", \"message\": \"{log.Message}\", \"data\": \"{log.Data}\", \"userId\": \"{log.UserId}\" }}");
            }

            // file
            string logFilePath = GetLogFilePath(log.Timestamp);
            string logMessage = FormatLogMessage(log);
            await File.AppendAllTextAsync(logFilePath, logMessage + Environment.NewLine); // Dosyaya yaz
            
            // db
            await _loggerRepository.AddAsync(log);
            await _loggerRepository.SaveAsync();

        }


        private string GetLogFilePath(DateTime time)
        {

            DateTime startOfWeek = time.Date.AddDays(-(int)time.DayOfWeek + (int)DayOfWeek.Monday);
            DateTime endOfWeek = startOfWeek.AddDays(6);
            string weekFolder = Path.Combine(_logDirectory, $"{startOfWeek:yyyy-MM-dd}_to_{endOfWeek:yyyy-MM-dd}");
            Directory.CreateDirectory( weekFolder );

            int fileIndex = 1;
            string logFilePath;

            do
            {
                logFilePath = Path.Combine(weekFolder, $"logs_{fileIndex}.txt");
                fileIndex++;
            } while (File.Exists(logFilePath) && new FileInfo(logFilePath).Length > MaxFileSize);

            return logFilePath;

        }


        private string FormatLogMessage(Logs log)
        {
            var sb = new StringBuilder();
            sb.AppendLine("==============================================");
            sb.AppendLine($"Timestamp: {log.Timestamp}");
            sb.AppendLine($"LogLevel  : {log.LogLevel}");
            sb.AppendLine($"TableName : {log.TableName}");
            sb.AppendLine($"Message   : {log.Message}");
            sb.AppendLine($"Data      : {log.Data}");
            sb.AppendLine($"Exception : {log.Exception}");
            sb.AppendLine($"UserId    : {log.UserId}");
            sb.AppendLine("==============================================");

            return sb.ToString();
        }


        public Task TraceLog(string tableName, string message, string data)
        {
            return AddLog(LogLevel.Trace, tableName, message, data);
        }


        public Task DebugLog(string tableName, string message, string data, Exception exception)
        {
            return AddLog(LogLevel.Debug, tableName, message, data, exception, null);
        }


        public Task InfoLog(string tableName, string message, string userId)
        {
            return AddLog(LogLevel.Info, tableName, message, null, null, userId);
        }


        public Task WarningLog(string tableName, string message, string data, string userId)
        {
            return AddLog(LogLevel.Warning, tableName, message, data, null, userId);
        }


        public Task ErrorLog(string tableName, string message, string data, Exception exception, string userId)
        {
            return AddLog(LogLevel.Error, tableName, message, data, exception, userId);
        }


        public Task CriticalLog(string tableName, string message, string data, Exception exception, string userId)
        {
            return AddLog(LogLevel.Critical, tableName, message, data, exception, userId);
        }


        public Task NoneLog(string message)
        {
            return AddLog(LogLevel.None, null, message, null, null, null);
        }


    }
}
