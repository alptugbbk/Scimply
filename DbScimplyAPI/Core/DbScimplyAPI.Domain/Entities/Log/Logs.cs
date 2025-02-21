using DbScimplyAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbScimplyAPI.Domain.Entities.Log
{
    public class Logs : BaseEntity
    {
        public string LogLevel { get; set; }
        public DateTime Timestamp { get; set; }
        public string? TableName { get; set; }
        public string Message { get; set; }
        public string? Data { get; set; }
        public string? Exception { get; set; }
        public string? UserId { get; set; }

    }
}
