using DbScimplyAPI.Domain.Entities.Admin;
using DbScimplyAPI.Domain.Entities.Common;
using DbScimplyAPI.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbScimplyAPI.Domain.Entities.Role
{
    public class Role : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<User.User> Users { get; set; }
        public ICollection<Admin.Admin> Admins { get; set; }
        
    }
}
