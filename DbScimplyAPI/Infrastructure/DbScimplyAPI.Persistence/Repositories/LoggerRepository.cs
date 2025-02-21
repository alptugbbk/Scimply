using DbScimplyAPI.Application.Repositories;
using DbScimplyAPI.Domain.Entities.Log;
using DbScimplyAPI.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbScimplyAPI.Persistence.Repositories
{
    public class LoggerRepository : GenericRepository<Logs>, ILoggerRepository
    {
        public LoggerRepository(MyContext context) : base(context)
        {
        }
    }
}
