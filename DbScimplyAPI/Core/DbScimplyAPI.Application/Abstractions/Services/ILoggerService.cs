using DbScimplyAPI.Domain.Entities.Log;
using DbScimplyAPI.Domain.Enums.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbScimplyAPI.Application.Abstractions.Services
{
    public interface ILoggerService
    {

        public Task TraceLog(string tableName, string message, string data);
        public Task DebugLog(string tableName, string message, string data, Exception exception);
        public Task InfoLog(string tableName, string message, string userId);
        public Task WarningLog(string tableName, string message, string data, string userId);
        public Task ErrorLog(string tableName, string message, string data, Exception exception, string userId);
        public Task CriticalLog(string tableName, string message, string data, Exception exception, string userId);
        public Task NoneLog(string message);
        

    }
}
