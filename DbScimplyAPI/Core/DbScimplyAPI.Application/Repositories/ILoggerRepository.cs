using DbScimplyAPI.Domain.Entities.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbScimplyAPI.Application.Repositories
{
    public interface ILoggerRepository : IGenericRepository<Logs>
    {
    }
}
