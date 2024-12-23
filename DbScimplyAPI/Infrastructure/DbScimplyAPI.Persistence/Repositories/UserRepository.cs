using DbScimplyAPI.Application.Repositories;
using DbScimplyAPI.Domain.Entities.User;
using DbScimplyAPI.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbScimplyAPI.Persistence.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(MyContext context) : base(context) { }
    }
}
