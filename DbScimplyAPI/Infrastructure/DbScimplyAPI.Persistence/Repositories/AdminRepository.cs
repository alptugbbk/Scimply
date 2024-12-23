using DbScimplyAPI.Application.Repositories;
using DbScimplyAPI.Domain.Entities.Admin;
using DbScimplyAPI.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbScimplyAPI.Persistence.Repositories
{
    public class AdminRepository : GenericRepository<Admin>, IAdminRepository
    {
        public AdminRepository(MyContext context) : base(context)
        {
        }
    }
}
